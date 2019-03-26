using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PmsEteck.Data.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace PmsEteck.Data.Models
{
    [Table(name: "Consumption", Schema = "meter")]
    public class Consumption
    {
        #region Constructor
            private PmsEteckContext db = new PmsEteckContext();
        private ConsumptionMeter _consumptionMeter = new ConsumptionMeter();
        #endregion

        #region Fields
        #region SDS Fields
        private string apiSDSEndPoint = "https://data.smartdatasolutions.nl/";
        private string SDSApiKey = ConfigurationManager.AppSettings["SDSApiKey"];

        private string AssetType;
        #endregion

        #endregion

        #region Properties
        [Key]
        public int iConsumptionKey { get; set; }

        [Display(Name = "Adres")]
        public int? iAddressKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Telwerk")]
        public int iCounterKey { get; set; }

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
        [Display(Name = "Verbruik")]
        public decimal dConsumption { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Eindstand")]
        public decimal dEndPosition { get; set; }

        [Display(Name = "Uitgezonderd voor rapportage")]
        public bool bExcludeForReport { get; set; }

        [Display(Name = "Gevalideerd")]
        public bool bValidated { get; set; }

        [Display(Name = "Ongevalideerde regel")]
        public int? iConsumptionUnvalidatedID { get; set; }

        public virtual Counter Counter { get; set; }

        public virtual ConsumptionUnvalidated ConsumptionUnvalidated { get; set; }
        
        public virtual Address Address { get; set; }

        public virtual ServiceRun ServiceRun { get; set; }

        #endregion

        #region Methods
        public Task AddConsumption(DateTime dateTime, Counter counter, decimal position)
        {

            ServiceRun serviceRun = new ServiceRun {
                iServiceKey = 3,
                dtServiceRunStartDate = DateTime.Now
            };
            // Get all Consumption
            Consumption lastConsumption = db.Consumption.Where(w => w.iCounterKey == counter.iCounterKey).OrderByDescending(o => o.dtEndDateTime).FirstOrDefault();

            // Check of datetime is higher than last consumption
            bool isHigherDateTime = dateTime > lastConsumption.dtEndDateTime;

            if (isHigherDateTime)
            {
                if (position >= lastConsumption.dEndPosition)
                {
                    var newConsumption = new Consumption
                    {
                        Counter = counter,
                        dtStartDateTime = lastConsumption.dtEndDateTime,
                        dtEndDateTime = dateTime,
                        dEndPosition = position,
                        dConsumption = position - lastConsumption.dEndPosition,
                        iAddressKey = lastConsumption.iAddressKey,
                        bExcludeForReport = counter.ConsumptionMeter.MeterType.sPurchaseOrSale.Contains("Distribution"),
                        bValidated = true,
                        ServiceRun = serviceRun
                    };

                    db.Consumption.Add(newConsumption);

                    serviceRun.dtServiceRunEndDate = DateTime.Now;
                    serviceRun.iServiceRunStatus = 200;
                    serviceRun.iServiceRunRowsUpdated = 1;
                    serviceRun.sServiceRunMessage = "1 row added.";

                    db.SaveChanges();

                    return Task.FromResult(1);
                }
            }

            return Task.FromResult(0);
        }
        
        public decimal GetConsumption(int ProjectKey, int RateCardKey, DateTime? PeriodStartDate, DateTime? PeriodEndDate, int? CounterTypeKey, int? UnitKey)
        {
            decimal consumption = 0;

            ProjectInfo project = db.ProjectInfo
                                        .Include(i => i.Addresses)
                                        .Include(i => i.Addresses.Select(s => s.Consumption))
                                        .Include(i => i.Addresses.Select(s => s.Consumption.Select(c => c.Counter)))
                                        .Include(i => i.Addresses.Select(s => s.AddressRateCards))
                                        .Include(i => i.Addresses.Select(s => s.AddressRateCards.Select(sm => sm.RateCard.RateCardYears)))
                                        .Include(i => i.Addresses.Select(s => s.AddressRateCards.Select(sm => sm.RateCard.RateCardYears.Select(r => r.RateCardRows))))
                                        .First(f => f.iProjectKey == ProjectKey);

            IEnumerable<Consumption> consumptionList = project.Addresses.Where(w => w.AddressRateCards.Any(u => u.iRateCardKey == RateCardKey)).SelectMany(s => s.Consumption.OrderBy(o => o.dtEndDateTime).Where(w => !w.bExcludeForReport));

            if (PeriodStartDate.HasValue && PeriodEndDate.HasValue)
            { 
                consumptionList = consumptionList.Where(w => w.dtStartDateTime <= PeriodEndDate && w.dtEndDateTime > PeriodStartDate);
                                
            }
            else if (PeriodStartDate.HasValue)
                consumptionList = consumptionList.Where(w => w.dtStartDateTime >= PeriodStartDate);
            else if (PeriodEndDate.HasValue)
                consumptionList = consumptionList.Where(w => w.dtEndDateTime <= PeriodEndDate);

            if (CounterTypeKey.HasValue)
                consumptionList = consumptionList.Where(w => w.Counter.iCounterTypeKey == CounterTypeKey);

            if (UnitKey.HasValue)
                consumptionList = consumptionList.Where(w => w.Counter.iUnitKey == UnitKey);

            int daysInPeriod = new TimeSpan(PeriodEndDate.Value.Ticks - PeriodStartDate.Value.Ticks).Days + 1;

            foreach (Consumption item in consumptionList)
            {
                TimeSpan itemTimeSpan = item.dtEndDateTime - item.dtStartDateTime;
                int itemDays = itemTimeSpan.Days;

                if (item.dtStartDateTime < PeriodStartDate && item.dtEndDateTime > PeriodEndDate)
                {
                    item.dConsumption = item.dConsumption / itemDays * daysInPeriod;
                }
                else if (item.dtStartDateTime < PeriodStartDate)
                {
                    TimeSpan activeTimeSpan = item.dtEndDateTime - PeriodStartDate.Value;
                    item.dConsumption = item.dConsumption / itemDays * activeTimeSpan.Days;
                }
                else if (item.dtEndDateTime > PeriodEndDate)
                {
                    TimeSpan activeTimeSpan = PeriodEndDate.Value.AddDays(1) - item.dtStartDateTime;
                    item.dConsumption = item.dConsumption / itemDays * activeTimeSpan.Days;
                }
            }

            consumption = consumptionList.Sum(sm => sm.dConsumption);
            
            return consumption;
        }

        public decimal GetAddressConsumption(int AddressKey, int RateCardKey, DateTime? PeriodStartDate, DateTime? PeriodEndDate, int? CounterTypeKey, int? UnitKey)
        {
            decimal consumption = 0;

            Address address = db.Addresses
                                        .Include(i => i.Consumption)
                                        .Include(i => i.Consumption.Select(c => c.Counter))
                                        .Include(i => i.Consumption.Select(c => c.Counter.ChangeLogs))
                                        .Include(i => i.Consumption.Select(c => c.Counter.ChangeLogs.Select(cl => cl.FromConsumptionMeter)))
                                        .Include(i => i.Consumption.Select(c => c.Counter.ChangeLogs.Select(cl => cl.FromConsumptionMeter.MeterType)))
                                        .Include(i => i.Consumption.Select(c => c.Counter.ChangeLogs.Select(cl => cl.ToConsumptionMeter)))
                                        .Include(i => i.Consumption.Select(c => c.Counter.ChangeLogs.Select(cl => cl.ToConsumptionMeter.MeterType)))
                                        .Include(i => i.Consumption.Select(c => c.Counter.ConsumptionMeter))
                                        .Include(i => i.Consumption.Select(c => c.Counter.ConsumptionMeter.MeterType))
                                        .Include(i => i.AddressRateCards)
                                        .Include(i => i.AddressRateCards.Select(sm => sm.RateCard.RateCardYears))
                                        .Include(i => i.AddressRateCards.Select(sm => sm.RateCard.RateCardYears.Select(r => r.RateCardRows)))
                                        .First(f => f.iAddressKey == AddressKey);

            IEnumerable<Consumption> consumptionList = address.Consumption
                                                                                                                                        .OrderBy(o => o.dtEndDateTime)
                                                                                                                                        .Where(w => !w.bExcludeForReport);
            
            if (PeriodStartDate.HasValue && PeriodEndDate.HasValue)
                consumptionList = consumptionList.Where(w => (w.dtStartDateTime < PeriodEndDate.Value.AddDays(1) && w.dtEndDateTime > PeriodStartDate) || (w.dtStartDateTime == PeriodStartDate && w.dtEndDateTime == PeriodStartDate));
            else if (PeriodStartDate.HasValue)
                consumptionList = consumptionList.Where(w => w.dtStartDateTime >= PeriodStartDate);
            else if (PeriodEndDate.HasValue)
                consumptionList = consumptionList.Where(w => w.dtEndDateTime <= PeriodEndDate);

            if (CounterTypeKey.HasValue)
                consumptionList = consumptionList.Where(w => w.Counter.iCounterTypeKey == CounterTypeKey);

            if (UnitKey.HasValue)
                consumptionList = consumptionList.Where(w => w.Counter.iUnitKey == UnitKey);

            int daysInPeriod = new TimeSpan(PeriodEndDate.Value.Ticks - PeriodStartDate.Value.Ticks).Days + 1;

            List<Consumption> list = consumptionList.ToList();
            foreach (Consumption item in list)
            {
                item.dtEndDateTime = item.dtEndDateTime.Date;
                item.dtStartDateTime = item.dtStartDateTime.Date;
                TimeSpan itemTimeSpan = item.dtEndDateTime - item.dtStartDateTime;
                int itemDays = itemTimeSpan.Days;

                // Voer berekening uit als de verbruiksregel over meer dan 1 dag gaat, anders moet het hele verbruik meegenomen worcden.
                if (itemDays > 0)
                {
                    if (item.dtStartDateTime <= PeriodStartDate && item.dtEndDateTime > PeriodEndDate.Value.AddDays(1))
                    {
                        item.dConsumption = item.dConsumption / itemDays * daysInPeriod;
                    }
                    else if (item.dtStartDateTime < PeriodStartDate)
                    {
                        //TimeSpan activeTimeSpan = item.dtEndDateTime.AddDays(1) - PeriodStartDate.Value;
                        TimeSpan activeTimeSpan = item.dtEndDateTime - PeriodStartDate.Value;
                        item.dConsumption = item.dConsumption / itemDays * activeTimeSpan.Days;
                    }
                    else if (item.dtEndDateTime > PeriodEndDate.Value.AddDays(1))
                    {
                        //TimeSpan activeTimeSpan = PeriodEndDate.Value.AddDays(1) - item.dtStartDateTime;
                        TimeSpan activeTimeSpan = PeriodEndDate.Value - item.dtStartDateTime;
                        item.dConsumption = item.dConsumption / itemDays * activeTimeSpan.Days;
                    }
                }
            }

            consumption = list.Sum(sm => sm.dConsumption);

            return consumption;

            //db.Database.Connection.Open();
            //var cmd = db.Database.Connection.CreateCommand();
            //cmd.CommandText = "meter.GetCounterConsumption";
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.Parameters.Add(new SqlParameter("@parCounterKey", 4449));
            //cmd.Parameters.Add(new SqlParameter("@parDateStart", new DateTime(2017, 1, 1)));
            //cmd.Parameters.Add(new SqlParameter("@parDateEnd", new DateTime(2017, 9, 5)));

            //decimal consumption = (decimal)(cmd.ExecuteScalar());
        }
        public async Task<Consumption> GetAddressConsumptionAsync(int addressKey, int counterTypeKey, int unitKey, DateTime startDate, DateTime endDate)
        {
            // default input startdate is first day of month and enddate is last day of month
            Consumption consumption = new Consumption();

            // Get all addressconsumptionRow without the distributionconsumption
            IQueryable<Consumption> consumptionList = db.Consumption.Where(w => w.iAddressKey == addressKey && !w.bExcludeForReport);
            // Get only periodconsumption
            consumptionList = consumptionList.Where(w => w.dtStartDateTime < endDate && w.dtEndDateTime > startDate);

            // Get only consumption from counterType
            consumptionList = consumptionList.Where(w => w.Counter.iCounterTypeKey == counterTypeKey);

            // Get only consumption with the same unit
            consumptionList = consumptionList.Where(w => w.Counter.iUnitKey == unitKey);

            // Get only consumption for visible meters
            consumptionList = consumptionList.Where(w => w.Counter.ConsumptionMeter.bVisibleForCustomers);

            var list = consumptionList.ToList();
            // Get number of days in the input timespan
            int daysInPeriod = (endDate - startDate).Days + 1;

            foreach (Consumption item in list)
            {
                item.dtEndDateTime = item.dtEndDateTime.Date;
                item.dtStartDateTime = item.dtStartDateTime.Date;
                int itemDays = (item.dtEndDateTime - item.dtStartDateTime).Days;

                // Voer berekening uit als de verbruiksregel over meer dan 1 dag gaat, anders moet het hele verbruik meegenomen worcden.
                if (itemDays > 0)
                {
                    if (item.dtStartDateTime <= startDate && item.dtEndDateTime >= endDate)
                    {
                        item.dConsumption = item.dConsumption / itemDays * daysInPeriod;
                    }
                    else if (item.dtStartDateTime <= startDate)
                    {
                        //TimeSpan activeTimeSpan = item.dtEndDateTime.AddDays(1) - PeriodStartDate.Value;
                        TimeSpan activeTimeSpan = item.dtEndDateTime.AddDays(1) - startDate;
                        item.dConsumption = item.dConsumption / itemDays * activeTimeSpan.Days;
                    }
                    else if (item.dtEndDateTime >= endDate)
                    {
                        //TimeSpan activeTimeSpan = PeriodEndDate.Value.AddDays(1) - item.dtStartDateTime;
                        TimeSpan activeTimeSpan = endDate.AddDays(1) - item.dtStartDateTime;
                        item.dConsumption = item.dConsumption / itemDays * activeTimeSpan.Days;
                    }
                }
            }

            consumption.dtStartDateTime = startDate;
            consumption.dtEndDateTime = endDate;
            consumption.dConsumption = consumptionList.Count() == 0 ? 0 : list.Sum(sm => sm.dConsumption);
            consumption.dEndPosition = consumptionList.Count() == 0 ? 0 : list.Max(m => m.dEndPosition);

            return await Task.FromResult(consumption);
        }
        #endregion

        #region Methods SDS

        // Get all Consumption From SDS Api en save it in the database

        public async Task GetSDSConsumption()
        {
            while (true)
            {
                Service service = db.Services.Find(1);
                // Maak een nieuwe serviceRun aan
                ServiceRun serviceRun = new ServiceRun {
                    iServiceKey = service.iServiceKey,
                    dtServiceRunStartDate = DateTime.UtcNow,
                    ServiceRunErrors = new List<ServiceRunError>()
                };

                // Maak een lege lijst aan waarin vervolgens het verbruik in gestopt kan worden
                List<Consumption> consumptionList = new List<Consumption>();

                // Deze Hashset lijst wordt gebruikt om het al aanwezige verbruik toe te voege
                HashSet<Consumption> existedConsumptionList = new HashSet<Consumption>();

                try
                {
                    // Pak de huidige datum
                    DateTime today = DateTime.Now;

                    // Haal alle meters en telwerken op van de meters van SDS die aan een adres gekoppeld zijn.
                    var currentConsumptionMeters = db.ConsumptionMeters
                                                        .Include(i => i.Counters)
                                                        .Where(w => w.iConsumptionMeterSupplierKey == 1 && w.iAddressKey != null).ToList();

                    //Draai een loop af voor elke meter
                    foreach (ConsumptionMeter meter in currentConsumptionMeters)
                    //foreach (ConsumptionMeter meter in currentConsumptionMeters)
                    {
                        // Check of consumptionMeter exist in API
                        SmartDataSolution _sds = new SmartDataSolution();
                        if (await _sds.ConsumptionMeterExists(meter.sEANCode))
                        {
                            // Check of de meter ook telwerken heeft
                            if (meter.Counters.Count == 0)
                            {
                                throw new Exception(string.Format("Er zijn nog geen telwerken gevonden voor de meter met EANCode {0}", meter.sEANCode));
                            }

                            // Haal de eerste beschikbare datum op van de meter
                            DateTime dataAvailableFromDate = GetSDSConsumptionMeterAvailableFrom(meter.sEANCode);

                            if (dataAvailableFromDate != new DateTime())
                            {
                                // Loop elk telwerk af
                                foreach (Counter counter in meter.Counters.Where(w => new int[] { 2, 3, 4 }.Contains(w.iCounterTypeKey)))
                                {
                                    Consumption lastConsumption = db.Consumption.Where(w => w.iCounterKey == counter.iCounterKey).OrderByDescending(o => o.dtEndDateTime).ThenByDescending(t => t.dEndPosition).FirstOrDefault();

                                    if (lastConsumption == null)
                                    {
                                        throw new Exception(string.Format("Voor telwerk {0} is geen verbruiksregel geregistreerd.", string.Join(", ", counter.sCounterCode)));
                                    }

                                    // Voeg de regel toe aan de HashSet
                                    existedConsumptionList.Add(lastConsumption);

                                    // Wat is de laatste datum van het verbruik
                                    DateTime firstPickUpDay = lastConsumption.dtEndDateTime;

                                    if (firstPickUpDay < dataAvailableFromDate)
                                    {
                                        throw new Exception(string.Format("Meter {0} is geauthorizeerd vanaf {1} en er wordt geprobeerd data op te halen vanaf {2}.", meter.sEANCode, dataAvailableFromDate.Date, firstPickUpDay.Date));
                                    }

                                    // Haal laatst beschikbare datum op uit de webservice
                                    DateTime lastAvailableData = GetSDSLastAvailableData(meter.sEANCode);

                                    switch (counter.iCounterTypeKey)
                                    {
                                        case 1:
                                            //Check of ws ook elektra meet
                                            if (AssetType != "P4Electricity")
                                            {
                                                throw new Exception(string.Format("Vanuit Database verwachten we bij teller {1} voor elektra en we krijgen nu een meter die {0} meet.", AssetType, counter.iCounterKey));
                                            }
                                            break;

                                        case 2:
                                            //Check of ws ook gas meet
                                            if (AssetType != "P4Gas")
                                            {
                                                throw new Exception(string.Format("Vanuit Database verwachten we bij teller {1} voor elektra en we krijgen nu een meter die {0} meet.", AssetType, counter.iCounterKey));
                                            }
                                            break;

                                        case 3:
                                            //Check of ws ook elektra meet
                                            if (AssetType != "P4Electricity")
                                            {
                                                throw new Exception(string.Format("Vanuit Database verwachten we bij teller {1} voor elektra en we krijgen nu een meter die {0} meet.", AssetType, counter.iCounterKey));
                                            }
                                            break;

                                        case 4:
                                            //Check of ws ook elektra meet
                                            if (AssetType != "P4Electricity")
                                            {
                                                throw new Exception(string.Format("Vanuit Database verwachten we bij teller {1} voor elektra en we krijgen nu een meter die {0} meet.", AssetType, counter.iCounterKey));
                                            }
                                            break;

                                    }

                                    // Check tot welke datum opgehaald moet worden (vandaag of laatste registratiedatum)
                                    DateTime lastPickUpDateTime = lastAvailableData < today ? lastAvailableData : today.Date;

                                    TimeSpan timeSpan = lastPickUpDateTime - firstPickUpDay;

                                    string urlConsumption = string.Format("{0}readings/Identifier/{1}/Day/{2}?apikey={3}&dateto={4}&datetimeformat=nl&format=json&source=day", apiSDSEndPoint, meter.sEANCode, firstPickUpDay.ToString("yyyy-MM-dd"), SDSApiKey, lastPickUpDateTime.ToString("yyyy-MM-dd"));

                                    try
                                    {
                                        WebClient client = new WebClient { Encoding = Encoding.UTF8 };
                                        string resultString = client.DownloadString(urlConsumption);
                                        JObject resultObject = JObject.Parse(resultString);
                                        JArray jsonArray = JArray.Parse(resultObject["Payload"].ToString());
                                        foreach (JToken consumption in jsonArray)
                                        {
                                            Consumption previousConsumption = consumptionList.Where(w => w.Counter == counter).OrderByDescending(o => o.dtEndDateTime).FirstOrDefault() != null ? consumptionList.Where(w => w.Counter == counter).OrderByDescending(o => o.dtEndDateTime).FirstOrDefault() : existedConsumptionList.Where(w => w.Counter == counter).OrderByDescending(o => o.dtEndDateTime).FirstOrDefault();
                                            decimal ApiEndPosition = 0;
                                            switch (counter.iCounterTypeKey)
                                            {
                                                case 2:
                                                    ApiEndPosition = decimal.Parse(consumption["R180"].ToString());
                                                    break;
                                                case 3:
                                                    ApiEndPosition = decimal.Parse(consumption["R181"].ToString());
                                                    break;
                                                case 4:
                                                    ApiEndPosition = decimal.Parse(consumption["R182"].ToString());
                                                    break;
                                                default:
                                                    break;
                                            }

                                            DateTime consumptionDateTime = DateTime.Parse(consumption["DateTime"].ToString());

                                            // Set the correctionfactor
                                            if (consumption == jsonArray.First)
                                            {
                                                counter.dCorrectionMutation = counter.dCorrectionMutation != 0 ? counter.dCorrectionMutation : previousConsumption.dEndPosition - ApiEndPosition;
                                            }
                                            else if (consumptionDateTime > previousConsumption.dtEndDateTime)
                                            {
                                                // Set correctionEndPosition
                                                ApiEndPosition = ApiEndPosition + counter.dCorrectionMutation;
                                                Consumption newConsumption = new Consumption
                                                {
                                                    iAddressKey = counter.ConsumptionMeter.iAddressKey.Value,
                                                    Counter = counter,
                                                    ServiceRun = serviceRun,
                                                    dtStartDateTime = previousConsumption.dtEndDateTime,
                                                    dtEndDateTime = consumptionDateTime,
                                                    dConsumption = ApiEndPosition - previousConsumption.dEndPosition,
                                                    dEndPosition = ApiEndPosition,
                                                    bExcludeForReport = counter.ConsumptionMeter.MeterType.sPurchaseOrSale.Contains("Distribution"),
                                                    bValidated = true
                                                };
                                                consumptionList.Add(newConsumption);

                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        ServiceRunError error = new ServiceRunError
                                        {
                                            dtEffectiveDateTime = DateTime.Now,
                                            iStatusCode = 500,
                                            sConsumptionMeterNumber = counter.ConsumptionMeter.sConsumptionMeterNumber,
                                            sErrorMessage = e.Message,
                                            ServiceRun = serviceRun
                                        };
                                        serviceRun.ServiceRunErrors.Add(error);
                                        serviceRun.iServiceRunStatus = 500;
                                    }

                                } // End of foreach counter
                            }
                            else
                            {
                                ServiceRunError error = new ServiceRunError {
                                    ServiceRun = serviceRun,
                                    dtEffectiveDateTime = DateTime.Now,
                                    iStatusCode = 500,
                                    sConsumptionMeterNumber = meter.sConsumptionMeterNumber,
                                    sErrorMessage = "Er kan geen data uit de API gelezen worden voor de meter"
                                };
                                serviceRun.ServiceRunErrors.Add(error);
                                serviceRun.iServiceRunStatus = 500;
                            }
                        }
                        else
                        {
                            ServiceRunError serviceRunError = new ServiceRunError
                            {
                                iStatusCode = 500,
                                sErrorMessage = string.Format("Meter met EAN-Code {0} kan niet gevonden worden in de API", meter.sEANCode),
                                sConsumptionMeterNumber = meter.sConsumptionMeterNumber,
                                ServiceRun = serviceRun,
                            };
                            serviceRun.ServiceRunErrors.Add(serviceRunError);
                            serviceRun.iServiceRunStatus = 500;
                        }
                    } // End of foreach meter

                    //Voeg de hele lijst toe aan de database
                    db.Consumption.AddRange(consumptionList);

                    int rowsAdded = db.ChangeTracker.Entries().Count(c => c.State == EntityState.Added);

                    serviceRun.dtServiceRunEndDate = DateTime.UtcNow;
                    serviceRun.sServiceRunMessage = rowsAdded + " row(s) updated.";
                    serviceRun.iServiceRunRowsUpdated = rowsAdded;
                    serviceRun.iServiceRunStatus = serviceRun.iServiceRunStatus == 500 ? 500 : 200;

                    if (rowsAdded > 0)
                    {
                        db.ServiceRuns.Add(serviceRun);
                    }

                    db.SaveChanges();
                    currentConsumptionMeters.Clear();

                    //Check invoeren over saldo van afgelopen dag/maand zelfde als ws
                    if (today.Day == 1)
                    {
                        //Checks invoeren
                    }

                    DateTime now = DateTime.Now;
                    DateTime startDate = now.AddDays(1).Date.AddHours(5);
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

                    // Verwijder de lijst als aanpassingen hebben plaatsgevonden
                    if (db.ChangeTracker.HasChanges())
                    {
                        db.Consumption.RemoveRange(consumptionList);
                    }


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

        private DateTime GetSDSLastAvailableData(string EANCode)
        {
            DateTime availableLast = new DateTime();

            string urlAssetDetails = string.Format("{0}assets/{1}?apikey={2}&format=json", apiSDSEndPoint, EANCode, SDSApiKey);

            WebClient client = new WebClient { Encoding = Encoding.UTF8 };

            string resultString = client.DownloadString(urlAssetDetails);

            JObject returnObject = JObject.Parse(resultString);

            JToken payLoad = returnObject["Payload"];

            if (payLoad["Identifier"].ToString() == EANCode)
            {
                availableLast = DateTime.Parse(payLoad["DataAvailableLast"].ToString());
            }

            return availableLast;
        }

        private DateTime GetSDSConsumptionMeterAvailableFrom(string EANCode)
        {
            DateTime availableFirst = new DateTime();

            string urlAssetDetails = string.Format("{0}assets/{1}?apikey={2}&format=json", apiSDSEndPoint, EANCode, SDSApiKey);

            WebClient client = new WebClient { Encoding = Encoding.UTF8 };

            string resultString = client.DownloadString(urlAssetDetails);

            JObject returnObject = JObject.Parse(resultString);

            JToken payLoad = returnObject["Payload"];

            if (payLoad["Identifier"].ToString() == EANCode)
            {
                var parsed = DateTime.TryParse(payLoad["DataAvailableFirst"].ToString(), out availableFirst);
                //availableFirst = DateTime.Parse(payLoad["DataAvailableFirst"].ToString());
                AssetType = payLoad["AssetType"].ToString();
            }

            return availableFirst;
        }

        public decimal? GetSDSDayConsumption(string EANCode, DateTime day)
        {
            decimal? dayConsumption = 0;

            string endPoint = apiSDSEndPoint + "usages/Identifier/" + EANCode + "/Day/" + day.ToString("yyyy-MM-dd") + "?apikey=" + SDSApiKey + "&datetimeformat=nl&format=json";

            string webResult = new WebClient() { Encoding = Encoding.UTF8 }.DownloadString(endPoint);

            var jsonObject = JsonConvert.DeserializeObject(webResult) as Dictionary<string, object>;

            var results = (Array)jsonObject["Payload"];

            foreach (Dictionary<string, object> item in results)
            {
                dayConsumption = decimal.Parse(item["Usage"].ToString());
            }


            return dayConsumption;
        }

        public decimal? GetSDSMonthConsumption(string EANCode, DateTime day)
        {
            decimal? monthConsumption = 0;

            string endPoint = apiSDSEndPoint + "usages/Identifier/" + EANCode + "/Month/" + day.ToString("yyyy-MM-dd") + "?apikey=" + SDSApiKey + "&datetimeformat=nl&format=json";

            string webResult = new WebClient() { Encoding = Encoding.UTF8 }.DownloadString(endPoint);

            var jsonObject = JsonConvert.DeserializeObject(webResult) as Dictionary<string, object>;

            var results = (Array)jsonObject["Payload"];

            foreach (Dictionary<string, object> item in results)
            {
                monthConsumption = decimal.Parse(item["Usage"].ToString());
            }


            return monthConsumption;
        }
        #endregion

        #region Methods Fudura

        public async Task GetFuduraConsumption()
        {
            while (true)
            {
                // Maak een nieuwe serviceRun aan
                Service service = db.Services.Find(2);

                ServiceRun serviceRun = new ServiceRun {
                    iServiceKey = service.iServiceKey,
                    dtServiceRunStartDate = DateTime.UtcNow,
                    ServiceRunErrors = new List<ServiceRunError>()
                };

                try
                {
                    // Haal alle meters en telwerken op van de meters van Fudura die aan een adres gekoppeld zijn.
                    var currentConsumptionMeters = db.ConsumptionMeters
                                                                    .Include(i => i.Counters)
                                                                    .Where(w => w.iConsumptionMeterSupplierKey == 2 && w.iAddressKey != null).ToList();

                    // Draai loop af voor elke meter
                    foreach (var meter in currentConsumptionMeters)
                    {
                        // Get foreach meter the consumption and save it to the database
                        serviceRun = await _consumptionMeter.GetMeterConsumption(meter.iConsumptionMeterKey, serviceRun);
                    } // End of foreach meter
                   
                    serviceRun.dtServiceRunEndDate = DateTime.UtcNow;
                    serviceRun.sServiceRunMessage = serviceRun.iServiceRunRowsUpdated + " row(s) updated.";
                    serviceRun.iServiceRunStatus = serviceRun.ServiceRunErrors.Count > 0 || serviceRun.iServiceRunStatus == 500 ? 500 : 200;

                    db.Entry(serviceRun).State = EntityState.Modified;
                    db.SaveChanges();
                    
                    currentConsumptionMeters.Clear();
                    
                    DateTime now = DateTime.Now;
                    DateTime startDate = now.AddDays(1).Date.AddHours(5);
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

                    db.Entry(serviceRun).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }

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
        #endregion
    }
}