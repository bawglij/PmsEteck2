using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    [Table(name: "BlindConsumptions", Schema = "meter")]
    public class BlindConsumption
    {
        #region Constructor
        private PmsEteckContext db = new PmsEteckContext();
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
        public int iBlindConsumptionKey { get; set; }

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
        [Display(Name = "Blindverbruik")]
        [DisplayFormat(DataFormatString = "{0:#,###.## kVArh}")]
        public decimal dBlindConsumption { get; set; }

        public virtual Counter Counter { get; set; }

        public virtual ServiceRun ServiceRun { get; set; }

        public virtual Address Address { get; set; }

        #endregion

        #region Methods
        public decimal GetBlindConsumptionByAddress(int AddressKey, int CounterTypeKey, DateTime PeriodeStartDate, DateTime PeriodEndDate)
        {
            decimal blindConsumption = 0;
            IQueryable<BlindConsumption> blindConsumptionRows = db.BlindConsumptions.Where(f => f.iAddressKey == AddressKey && f.Counter.iCounterTypeKey == CounterTypeKey && f.dtStartDateTime >= PeriodeStartDate && f.dtStartDateTime <= PeriodEndDate);

            if (blindConsumptionRows.Count() > 0)
                blindConsumption = blindConsumptionRows.Sum(sm => sm.dBlindConsumption);
            
            return blindConsumption;
        }

        private void GetFuduraAccessToken()
        {
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

            // Set accesstoken
            FuduraAccessToken = objectResult["access_token"].ToString();
            int expiresIn = int.Parse(objectResult["expires_in"].ToString());

            // Set expired date
            FuduraExpiredDate = DateTime.Now.AddSeconds(expiresIn);
        }

        public async Task GetBlindConsumption()
        {
            while (true)
            {
                Service service = db.Services.Find(7);

                // Maak een nieuwe servicerun aan
                ServiceRun serviceRun = new ServiceRun {
                    iServiceKey = 7,
                    dtServiceRunStartDate = DateTime.UtcNow,
                    ServiceRunErrors  = new List<ServiceRunError>()
                };

                try
                {
                    DateTime today = DateTime.Now;


                    // Haal nieuwe accessToken op als de oude verlopen is
                    if (today.AddMinutes(-10) >= FuduraExpiredDate)
                        GetFuduraAccessToken();

                    // Haal alle meters van fudura op die gekoppeld zijn aan een adres en metertype inkoop/distributie/verkoop elektra of gas hebben hebben
                    List<ConsumptionMeter> consumptionMeters = db.ConsumptionMeters
                                                                        .Include(i => i.Counters)
                                                                        .Include(i => i.Counters.Select(s => s.BlindConsumption))
                                                                        .Where(w => w.iConsumptionMeterSupplierKey == 2 && w.iAddressKey != null && w.Counters.Any(u => u.iCounterTypeKey == 15))
                                                                        .ToList();

                    foreach (ConsumptionMeter consumptionMeter in consumptionMeters)
                    {
                        try
                        {
                            DateTime authorizedFromDate = GetFuduraConsumptionMeterAuthorizedFrom(consumptionMeter.sEANCode);
                            DateTime firstPickUpMonth = authorizedFromDate < new DateTime(2016, 1, 1) ? new DateTime(2016, 1, 1) : authorizedFromDate;
                            DateTime lastAvailableDate = GetFuduraLastAvailableData(consumptionMeter.sEANCode);

                            // Haal de blindverbruiksmeter op bij de meter
                            Counter blindConsumptionCounter = consumptionMeter.Counters.FirstOrDefault(f => f.iCounterTypeKey == 15);

                            if (blindConsumptionCounter == null)
                                throw new Exception(string.Format("Er is geen telwerk gevonden voor het meten van blindverbruik bij meter met EANCode {0}", consumptionMeter.sEANCode));

                            // Haal het laatst geregistreerd blindverbruik van het telwerk op
                            BlindConsumption lastRegisteredBlindConsumption = blindConsumptionCounter.BlindConsumption.OrderByDescending(o => o.dtEndDateTime).FirstOrDefault();

                            if (lastRegisteredBlindConsumption != null)
                                firstPickUpMonth = lastRegisteredBlindConsumption.dtEndDateTime;

                            if (firstPickUpMonth <= today.AddMonths(-1))
                            {
                                decimal dBlindConsumption = 0;
                                WebClient client = new WebClient { Encoding = Encoding.UTF8 };

                                client.Headers.Add(HttpRequestHeader.Authorization, "bearer " + FuduraAccessToken);
                                client.Headers.Add("Content-type", "application/json");
                                DateTime firstPickUpDayOfMonth = new DateTime(firstPickUpMonth.Year, firstPickUpMonth.Month, 1);

                                string urlChannels = string.Format("{0}/aansluitingen/{1}/channels/E65_EMVLRx/datapoints/{2}/{3}/{4}", apiFuduraEndPoint, consumptionMeter.sEANCode, firstPickUpMonth.Year, firstPickUpMonth.Month, firstPickUpMonth.Day);

                                string result = client.DownloadString(urlChannels);

                                JArray jsonArray = JArray.Parse(result);

                                foreach (JToken item in jsonArray)
                                {
                                    DateTime itemStartDateTime = DateTime.Parse(item["From"].ToString());
                                    DateTime itemEndDateTime = DateTime.Parse(item["To"].ToString());
                                    dBlindConsumption = decimal.Parse(item["ReadingN"].ToString());

                                    if (blindConsumptionCounter.BlindConsumption.Count() != 0)
                                    {
                                        if (itemStartDateTime > blindConsumptionCounter.BlindConsumption.OrderByDescending(o => o.dtEndDateTime).FirstOrDefault().dtStartDateTime)
                                        {
                                            BlindConsumption blindConsumption = new BlindConsumption
                                            {
                                                iAddressKey = consumptionMeter.iAddressKey.Value,
                                                Counter = blindConsumptionCounter,
                                                dtStartDateTime = itemStartDateTime,
                                                dtEndDateTime = itemEndDateTime,
                                                dBlindConsumption = dBlindConsumption,
                                                ServiceRun = serviceRun
                                            };
                                            blindConsumptionCounter.BlindConsumption.Add(blindConsumption);
                                        }
                                    }
                                    else
                                    {
                                        BlindConsumption blindConsumption = new BlindConsumption
                                        {
                                            iAddressKey = consumptionMeter.iAddressKey.Value,
                                            Counter = blindConsumptionCounter,
                                            dtStartDateTime = itemStartDateTime,
                                            dtEndDateTime = itemEndDateTime,
                                            dBlindConsumption = dBlindConsumption,
                                            ServiceRun = serviceRun
                                        };
                                        blindConsumptionCounter.BlindConsumption.Add(blindConsumption);
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            ServiceRunError error = new ServiceRunError
                            {
                                sConsumptionMeterNumber = consumptionMeter.sConsumptionMeterNumber,
                                sErrorMessage = e.Message,
                                iStatusCode = 500,
                                ServiceRun = serviceRun,
                                dtEffectiveDateTime = DateTime.UtcNow
                            };
                            serviceRun.ServiceRunErrors.Add(error);
                            ServiceRun.iServiceRunStatus = 500;
                        }
                        
                    }

                    int rowsAdded = db.ChangeTracker.Entries().Count(c => c.State == EntityState.Added);
                    serviceRun.dtServiceRunEndDate = DateTime.UtcNow;
                    serviceRun.sServiceRunMessage = rowsAdded + " row(s) added at BlindConsumption.";
                    serviceRun.iServiceRunRowsUpdated = rowsAdded;
                    serviceRun.iServiceRunStatus = serviceRun.ServiceRunErrors.Count > 0 || serviceRun.iServiceRunStatus == 500 ? 500 : 200;

                    if (rowsAdded > 0)
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
                    serviceRun.iServiceRunRowsUpdated = 0;

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
        #endregion
    }
}