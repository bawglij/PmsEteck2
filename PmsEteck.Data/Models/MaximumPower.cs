using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PmsEteck.Data.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PmsEteck.Data.Models
{
    [Table(name: "MaximumPowers", Schema = "meter")]
    public class MaximumPower
    {
        #region Constructor
            private PmsEteckContext db = new PmsEteckContext();
            private Fudura _fuduraService = new Fudura();
        #endregion

        #region Fields
        private string apiFuduraTokenEndPoint = "https://login.microsoftonline.com/9f84cace-b3ed-4c40-badc-838bf6e52b53/oauth2/token";
        private string apiFuduraEndPoint = "https://fdr-ws-prd.azurewebsites.net/api/v1";

        private string FuduraClientID = ConfigurationManager.AppSettings["FuduraClientID"];
        private string FuduraSecret = ConfigurationManager.AppSettings["FuduraSecret"];
        private string FuduraAccessToken;
        private DateTime FuduraExpiredDate;
        private string ChannelID;
        private string ProductType;
        #endregion

        #region Properties
        [Key]
        public int iMaximumPowerKey { get; set; }

        //[Required(ErrorMessage = "{0} is verplicht.")]
        //[Display(Name = "Verbruiksmeter")]
        //public int iConsumptionMeterKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Telwerk")]
        public int iCounterKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Adres")]
        public int iAddressKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Servicerun")]
        public int iServiceRunKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Datum beginstand")]
        public DateTime dtStartDateTime { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Datum eindstand")]
        public DateTime dtEndDateTime { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Maximaal vermogen")]
        public decimal dMaximumPower { get; set; }

        //public ConsumptionMeter ConsumptionMeter { get; set; }

        public virtual Counter Counter { get; set; }
        
        public virtual ServiceRun ServiceRun { get; set; }
        #endregion

        #region Methods
        private void GetFuduraAccessToken() {
            string accessToken = string.Empty;

            string resultString;

            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["grant_type"] = "client_credentials";
                values["resource"] = "https://fdrwsadprd.onmicrosoft.com/FuduraWebserviceProduction";
                values["client_id"] = FuduraClientID;
                values["client_secret"] = FuduraSecret;

                var response = client.UploadValues(apiFuduraTokenEndPoint, values);

                resultString = Encoding.Default.GetString(response);
            }


            var objectResult = JsonConvert.DeserializeObject(resultString) as Dictionary<string, object>;

            //var jsonObject = JsonConvert.DeserializeObject(resultString) as Dictionary<string, object>;

            // Set accesstoken
            FuduraAccessToken = objectResult["access_token"].ToString();

            int expiresIn = int.Parse(objectResult["expires_in"].ToString());

            // Set expired date
            FuduraExpiredDate = DateTime.Now.AddSeconds(expiresIn);
        }
        
        public async Task GetMaxPower()
        {
            while (true)
            {
                Service service = db.Services.Find(8);

                // Maak een nieuwe servicerun aan
                ServiceRun serviceRun = new ServiceRun {
                    iServiceKey = service.iServiceKey,
                    dtServiceRunStartDate = DateTime.UtcNow,
                    iServiceRunRowsUpdated = 0,
                    ServiceRunErrors = new List<ServiceRunError>()
                };

                try
                {
                    DateTime today = DateTime.Now;

                    // Haal nieuwe accessToken op als de oude verlopen is
                    if (today.AddMinutes(-10) >= FuduraExpiredDate)
                        GetFuduraAccessToken();

                    // Haal alle telwerken op van de meters van fudura voor het tellen van maximaal vermogen
                    List<Counter> counters = db.Counters
                                                    .Include(i => i.MaximumPowers)
                                                    .Where(w => new int[] { 11, 14 }.Contains(w.iCounterTypeKey) && w.ConsumptionMeter.iAddressKey != null && w.ConsumptionMeter.iConsumptionMeterSupplierKey == 2)
                                                    .ToList();

                    foreach (Counter counter in counters)
                    {
                        try
                        {
                            DateTime authorizedFromDate = _fuduraService.GetAuthorizedDateFrom(counter.ConsumptionMeter.sEANCode);
                            DateTime firstPickUpMonth = authorizedFromDate < new DateTime(2016, 1, 1) ? new DateTime(2016, 1, 1) : authorizedFromDate;
                            DateTime lastAvailableDate = GetFuduraLastAvailableData(counter.ConsumptionMeter.sEANCode);

                            MaximumPower lastRegisteredMaxPower = counter.MaximumPowers.OrderByDescending(o => o.dtEndDateTime).FirstOrDefault();

                            if (lastRegisteredMaxPower != null)
                                firstPickUpMonth = lastRegisteredMaxPower.dtEndDateTime;

                            if (firstPickUpMonth <= today.AddMonths(-1))
                            {
                                decimal dMaxPower = 0;

                                WebClient client = new WebClient { Encoding = Encoding.UTF8 };

                                client.Headers.Add(HttpRequestHeader.Authorization, "bearer " + FuduraAccessToken);
                                client.Headers.Add("Content-type", "application/json");
                                DateTime firstPickUpDayOfMonth = new DateTime(firstPickUpMonth.Year, firstPickUpMonth.Month, 1);

                                switch (counter.iCounterTypeKey)
                                {
                                    case 11:
                                        // Telwerk voor Elektra - Max vermogen
                                        string webResult = _fuduraService.GetDataPoints(counter.ConsumptionMeter.sEANCode, "E65_EMMLAx", firstPickUpMonth);
                                        JArray jsonArray = JArray.Parse(webResult);
                                        foreach (JToken item in jsonArray)
                                        {
                                            DateTime itemStartDateTime = DateTime.Parse(item["From"].ToString());
                                            DateTime itemEndDateTime = DateTime.Parse(item["To"].ToString());
                                            dMaxPower = decimal.Parse(item["ReadingN"].ToString());

                                            if (counter.MaximumPowers.Count() != 0)
                                            {
                                                if (itemStartDateTime > counter.MaximumPowers.OrderByDescending(o => o.dtEndDateTime).FirstOrDefault().dtStartDateTime)
                                                {
                                                    MaximumPower maxPower = new MaximumPower
                                                    {
                                                        iAddressKey = counter.ConsumptionMeter.iAddressKey.Value,
                                                        Counter = counter,
                                                        dtStartDateTime = itemStartDateTime,
                                                        dtEndDateTime = itemEndDateTime,
                                                        dMaximumPower = dMaxPower,
                                                        ServiceRun = serviceRun
                                                    };
                                                    counter.MaximumPowers.Add(maxPower);
                                                    serviceRun.iServiceRunRowsUpdated++;
                                                }
                                            }
                                            else
                                            {
                                                MaximumPower maxPower = new MaximumPower
                                                {
                                                    iAddressKey = counter.ConsumptionMeter.iAddressKey.Value,
                                                    Counter = counter,
                                                    dtStartDateTime = itemStartDateTime,
                                                    dtEndDateTime = itemEndDateTime,
                                                    dMaximumPower = dMaxPower,
                                                    ServiceRun = serviceRun
                                                };
                                                counter.MaximumPowers.Add(maxPower);
                                                serviceRun.iServiceRunRowsUpdated++;
                                            }
                                        }
                                        break;
                                    case 14:
                                        // Telwerk voor Gas - Max vermogen
                                        bool allDays = false;

                                        while (firstPickUpDayOfMonth < new DateTime(today.Year, today.Month, 1))
                                        {
                                            dMaxPower = 0;
                                            for (int i = 1; i <= DateTime.DaysInMonth(firstPickUpDayOfMonth.Year, firstPickUpDayOfMonth.Month); i++)
                                            {
                                                string result = _fuduraService.GetTimeSeries(counter.ConsumptionMeter.sEANCode, firstPickUpDayOfMonth);
                                                JArray resultArray = JArray.Parse(result);

                                                foreach (JToken item in resultArray)
                                                {
                                                    DateTime itemDateTime = DateTime.Parse(item["Timestamp"].ToString());
                                                    decimal maxPower = decimal.Parse(item["ReadingN"].ToString());
                                                    dMaxPower = Math.Max(dMaxPower, maxPower);

                                                    allDays = itemDateTime.Date == firstPickUpDayOfMonth.AddMonths(1).Date;
                                                }

                                            }

                                            if (allDays)
                                            {
                                                if (counter.MaximumPowers.Count() != 0)
                                                {
                                                    if (firstPickUpDayOfMonth > counter.MaximumPowers.OrderByDescending(o => o.dtEndDateTime).FirstOrDefault().dtStartDateTime)
                                                    {
                                                        MaximumPower maxPower = new MaximumPower
                                                        {
                                                            iAddressKey = counter.ConsumptionMeter.iAddressKey.Value,
                                                            Counter = counter,
                                                            dtStartDateTime = firstPickUpDayOfMonth,
                                                            dtEndDateTime = firstPickUpDayOfMonth.AddMonths(1),
                                                            dMaximumPower = dMaxPower,
                                                            ServiceRun = serviceRun
                                                        };
                                                        counter.MaximumPowers.Add(maxPower);
                                                        serviceRun.iServiceRunRowsUpdated++;
                                                    }
                                                }
                                                else
                                                {
                                                    MaximumPower maxPower = new MaximumPower
                                                    {
                                                        iAddressKey = counter.ConsumptionMeter.iAddressKey.Value,
                                                        Counter = counter,
                                                        dtStartDateTime = firstPickUpDayOfMonth,
                                                        dtEndDateTime = firstPickUpDayOfMonth.AddMonths(1),
                                                        dMaximumPower = dMaxPower,
                                                        ServiceRun = serviceRun
                                                    };
                                                    counter.MaximumPowers.Add(maxPower);
                                                    serviceRun.iServiceRunRowsUpdated++;
                                                }

                                            }

                                            firstPickUpDayOfMonth = firstPickUpDayOfMonth.AddMonths(1);
                                        }
                                        break;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            ServiceRunError error = new ServiceRunError
                            {
                                ServiceRun = serviceRun,
                                iStatusCode = 500,
                                dtEffectiveDateTime = DateTime.UtcNow,
                                sConsumptionMeterNumber = counter.ConsumptionMeter.sConsumptionMeterNumber,
                                sErrorMessage = e.Message
                            };
                            serviceRun.ServiceRunErrors.Add(error);
                            serviceRun.iServiceRunStatus = 500;
                        }

                    }

                    serviceRun.dtServiceRunEndDate = DateTime.UtcNow;
                    serviceRun.sServiceRunMessage = serviceRun.iServiceRunRowsUpdated + " row(s) added in maximum powers.";
                    serviceRun.iServiceRunStatus = serviceRun.ServiceRunErrors.Count > 0  || serviceRun.iServiceRunStatus == 500 ? 500 : 200;

                    if (serviceRun.iServiceRunRowsUpdated > 0)
                    {
                        db.ServiceRuns.Add(serviceRun);
                    }
                    db.SaveChanges();

                    DateTime now = DateTime.Now;
                    DateTime startDate = new DateTime(now.AddMonths(1).Year, now.AddMonths(1).Month, 1).Date.AddHours(5);
                    service.dtNextServiceRun = TimeZoneInfo.ConvertTimeToUtc(startDate, TimeZoneInfo.Local); 
                    db.Entry(service).State = EntityState.Modified;
                    db.SaveChanges();
                    TimeSpan interval = startDate - now;
                    await Task.Delay(interval);
                }
                catch (Exception e)
                {
                    serviceRun.dtServiceRunEndDate = DateTime.UtcNow;
                    serviceRun.sServiceRunMessage = e.Message;
                    serviceRun.iServiceRunStatus = 500;

                    db.ServiceRuns.Add(serviceRun);
                    db.SaveChanges();

                    DateTime now = DateTime.Now;
                    DateTime startDate = now.AddDays(1).Date.AddHours(5);
                    service.dtNextServiceRun = TimeZoneInfo.ConvertTimeToUtc(startDate, TimeZoneInfo.Local);
                    db.Entry(service).State = EntityState.Modified;
                    db.SaveChanges();
                    TimeSpan interval = startDate - now;
                    await Task.Delay(interval);
                }
            }
        }

        private DateTime GetFuduraLastAvailableData(string EANCode)
        {
            DateTime lastAvailableDate = new DateTime();

            string urlChannels = string.Format("{0}/aansluitingen/{1}/channels", apiFuduraEndPoint, EANCode);
            WebClient client = new WebClient { Encoding = Encoding.UTF8 };

            client.Headers.Add(HttpRequestHeader.Authorization, "bearer " + FuduraAccessToken);
            client.Headers.Add("Content-type", "application/json");

            var result = client.DownloadString(urlChannels);

            JArray objectResult = JArray.Parse(result);

            foreach (JToken channel in objectResult)
            {
                if (channel["ChannelType"].ToString().Contains("TimeSeries") && channel["Id"].ToString().Contains("Verbruik"))
                {
                    ChannelID = channel["Id"].ToString();
                    ProductType = channel["Metadata"]["ProductType"].ToString();
                    lastAvailableDate = DateTime.Parse(channel["LastReading"].ToString());
                }
            }

            return lastAvailableDate;

        }

        private DateTime GetFuduraConsumptionMeterAuthorizedFrom(string EANCode)
        {
            DateTime authorizedDate = new DateTime();

            string urlConnections = string.Format("{0}/aansluitingen", apiFuduraEndPoint);

            WebClient client = new WebClient { Encoding = Encoding.UTF8 };

            client.Headers.Add(HttpRequestHeader.Authorization, "bearer " + FuduraAccessToken);
            client.Headers.Add("Content-type", "application/json");

            var result = client.DownloadString(urlConnections);

            JArray objectResult = JArray.Parse(result);

            foreach (JToken connection in objectResult)
            {
                if (connection["Ean"].ToString() == EANCode)
                {
                    authorizedDate = DateTime.Parse(connection["AuthorizedFrom"].ToString());
                }
            }

            return authorizedDate;
        }

        public decimal GetMaxPowerByAddressInPeriod(int AddressKey, int CounterType, DateTime PeriodeStartDate, DateTime PeriodEndDate)
        {
            decimal dMaxPower = 0;

            IQueryable<MaximumPower> maxPower = db.MaximumPowers.Where(f => f.iAddressKey == AddressKey && f.dtStartDateTime >= PeriodeStartDate && f.dtStartDateTime <= PeriodEndDate && f.Counter.iCounterTypeKey == CounterType);

            if (maxPower.Count() > 0)
            {
                dMaxPower = maxPower.Sum(sm => sm.dMaximumPower);
            }

            return dMaxPower;
        }
        #endregion

    }
}