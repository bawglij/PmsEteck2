using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using System.Linq;
using PmsEteck.Data.Models.Results;
using Microsoft.EntityFrameworkCore;

namespace PmsEteck.Data.Models
{
    [Table(name: "Counters", Schema = "meter")]
    public class Counter
    {
        #region Constructor
        private PmsEteckContext db = new PmsEteckContext();
        #endregion

        #region Properties
        [Key]
        public int iCounterKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(100, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Code")]
        public string sCounterCode { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Eenheid")]
        public int iUnitKey { get; set; }

        [Display(Name = "Meter")]
        public int? iConsumptionMeterKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Telwerktype")]
        public int iCounterTypeKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Omzetverhouding van toepassing")]
        public bool bHasTurnOverRatio { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Omzetverhouding van")]
        public int iTurnOverRatioFrom { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Omzetverhouding naar")]
        public int iTurnOverRatioTo { get; set; }

        [Display(Name = "Omzetverhouding naar")]
        public decimal dCorrectionMutation { get; set; }

        [Display(Name = "Servicerun")]
        public int? iServiceRunKey { get; set; }

        [Display(Name = "# volledige rondes")]
        public int? iCompletedRounds { get; set; }

        [Display(Name = "Maximale tellerhoogte")]
        public int? iMaxCounterValue { get; set; }

        [Display(Name = "Actief")]
        public bool bActive { get; set; } = true;

        [Display(Name = "Gebruik standaard voor afwijking van gemiddeld verbruik")]
        public bool DefaultPercentageDeviationFromAverage { get; set; }

        [Display(Name = "Procentuele afwijking van gemiddelde verbruik")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal PercentageDeviationFromAverage { get; set; }

        [Display(Name = "Status")]
        [ForeignKey("CounterStatus")]
        public int? StatusID { get; set; }

        [Display(Name = "Status")]
        [StringLength(500, ErrorMessage = "{0} mag maximaal {1} karakters bevatten")]
        public string StatusDescription { get; set; }

        [Display(Name = "Toelichting")]
        [StringLength(500, ErrorMessage = "{0} mag maximaal {1} karakters bevatten")]
        public string StatusChangeDescription { get; set; }
        
        [Display(Name = "Servicerun")]
        public virtual ServiceRun ServiceRun { get; set; }

        public virtual CounterType CounterType { get; set; }

        public virtual ConsumptionMeter ConsumptionMeter { get; set; }

        public virtual Unit Unit { get; set; }

        public virtual CounterStatus CounterStatus { get; set; }
        
        public List<Consumption> Consumption { get; set; }

        public List<BlindConsumption> BlindConsumption { get; set; }

        public List<OldCounterStatus> OldCounterStatus { get; set; }

        public List<CounterChangeLog> ChangeLogs { get; set; }

        public List<MaximumPower> MaximumPowers { get; set; }

        public List<CounterYearConsumption> CounterYearConsumptions { get; set; }

        #endregion

        #region Methods
        public async Task CheckCounterStatus(/*TimeSpan interval*/)
        {
            //while (true)
            //{
            // Maak een nieuwe serviceRun aan
            ServiceRun serviceRun = new ServiceRun
            {
                iServiceKey = 6,
                dtServiceRunStartDate = DateTime.UtcNow
            };

            try
            {
                // Haal alle telwerken op die aan een meter gekoppeld zijn waarvan de meter gekoppeld is aan een adres (dus verbruik moet geregistreerd worden)
                //List<Counter> counters = db.Counters
                //                                .Include(i => i.ConsumptionMeter)
                //                                .Include(i => i.ConsumptionMeter.Frequency)
                //                                .Include(i => i.ConsumptionMeter.MeterType)
                //                                .Include(i => i.CounterStatus)
                //                                .Include(i => i.Unit)
                //                                .Where(w => w.ConsumptionMeter != null && w.ConsumptionMeter.iAddressKey.HasValue)
                //                                .OrderByDescending(o => o.iCounterKey)
                //                                .ToList();

                //foreach (Counter counter in counters)
                //{

                //Check of counter al statusregel heeft, zo ja, wijzig deze en zo niet maak een nieuwe aan
                Counter counter = db.Counters
                    .Include(i => i.ConsumptionMeter)
                    .Include(i => i.ConsumptionMeter.Frequency)
                    .Include(i => i.ConsumptionMeter.MeterType)
                    .Include(i => i.OldCounterStatus)
                    .Include(i => i.Unit)
                    .Single(s => s.ConsumptionMeter != null && s.ConsumptionMeter.iAddressKey.HasValue && s.iCounterKey == this.iCounterKey);

                OldCounterStatus counterStatus = counter.OldCounterStatus.FirstOrDefault();

                if (counterStatus == null)
                {
                    counterStatus = new OldCounterStatus
                    {
                        Counter = counter,
                        sMessage = "No message",
                    };

                    counter.OldCounterStatus.Add(counterStatus);
                    db.Entry(counterStatus).State = EntityState.Added;
                }

                // check wat van dit telwerk het laatste verbruik is
                DateTime lastConsumptionDateTime = DateTime.Now;
                DateTime firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

                if (new int[] { 11, 14 }.Contains(counter.iCounterTypeKey))
                {
                    bool counterHasMaximumPower = db.MaximumPowers.Where(w => w.iCounterKey == counter.iCounterKey).FirstOrDefault() != null;
                    if (counterHasMaximumPower)
                    {
                        lastConsumptionDateTime = db.MaximumPowers.Where(w => w.iCounterKey == counter.iCounterKey).OrderByDescending(o => o.dtEndDateTime).FirstOrDefault().dtEndDateTime;
                        counterStatus.bHasNoConsumption = lastConsumptionDateTime < firstDayOfMonth;
                    }
                    else
                    {
                        counterStatus.bHasNoConsumption = true;
                        counterStatus.sMessage = "Van dit telwerk is nog geen maximaal vermogen gevonden.";
                    }
                }
                else if (counter.iCounterTypeKey == 15)
                {
                    bool counterHasBlindConsumption = db.BlindConsumptions.FirstOrDefault(f => f.iCounterKey == counter.iCounterKey) != null;
                    if (counterHasBlindConsumption)
                    {
                        lastConsumptionDateTime = db.BlindConsumptions.Where(w => w.iCounterKey == counter.iCounterKey).OrderByDescending(o => o.dtEndDateTime).FirstOrDefault().dtEndDateTime;
                        counterStatus.bHasNoConsumption = lastConsumptionDateTime < firstDayOfMonth;
                    }
                    else
                    {
                        counterStatus.bHasNoConsumption = true;
                        counterStatus.sMessage = "Van dit telwerk is nog geen blindverbruik gevonden.";
                    }
                }
                else
                {
                    bool counterHasConsumption = db.Consumption.FirstOrDefault(f => f.iCounterKey == counter.iCounterKey) != null;
                    if (counterHasConsumption)
                    {
                        lastConsumptionDateTime = db.Consumption.Where(w => w.iCounterKey == counter.iCounterKey).OrderByDescending(o => o.dtEndDateTime).FirstOrDefault().dtEndDateTime;

                    }
                    else
                    {
                        counterStatus.sMessage = "Van dit telwerk is geen verbruik gevonden.";
                        throw new Exception(string.Format("Van telwerk {0} is geen verbruik gevonden.", counter.iCounterKey));
                    }
                }
                // Haal de instellingen van de meter op, zodat bekend is met welke frequentie de data opgehaald en wordt en welke marge aanwezig moet zijn
                ConsumptionMeter meter = counter.ConsumptionMeter;

                // Haal frequentie op waarmee de data ingeladen wordt
                Frequency frequency = meter.Frequency;

                DateTime today = DateTime.Today;
                DateTime lastRequiredDateTime = today;

                switch (frequency.sFrequencyDescription)
                {
                    case "Dagelijks":
                        lastRequiredDateTime = DateTime.Today.AddDays(-1);
                        break;

                    case "Wekelijks":
                        lastRequiredDateTime = DateTime.Today.AddDays(-7);
                        break;

                    case "Maandelijks":
                        lastRequiredDateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        break;

                    case "Driemaandelijks":
                        if (DateTime.Today.Month < 4)
                        {
                            lastRequiredDateTime = new DateTime(DateTime.Today.Year, 1, 1);
                        }
                        else if (DateTime.Today.Month < 7)
                        {
                            lastRequiredDateTime = new DateTime(DateTime.Today.Year, 4, 1);
                        }
                        else if (DateTime.Today.Month < 10)
                        {
                            lastRequiredDateTime = new DateTime(DateTime.Today.Year, 7, 1);
                        }
                        else
                        {
                            lastRequiredDateTime = new DateTime(DateTime.Today.Year, 10, 1);
                        }
                        break;

                    case "Jaarlijks":
                        lastRequiredDateTime = new DateTime(DateTime.Today.Year, 1, 1);
                        break;
                }

                lastRequiredDateTime = lastRequiredDateTime - TimeSpan.FromDays(meter.iDaysMargin);

                // Check 1: Of er verbruik tot de verplichte datum aanwezig is
                if (!new int[] { 11, 14, 15 }.Contains(counter.iCounterTypeKey))
                    counterStatus.bHasNoConsumption = lastConsumptionDateTime.Date < lastRequiredDateTime;

                //Start for second Check (RateCards)
                counterStatus.bHasNoRateCard = counter.ConsumptionMeter.MeterType.sPurchaseOrSale != "Distribution" ? db.RateCardRows.FirstOrDefault(f => f.iCounterTypeKey == counter.iCounterTypeKey
                                                                                && f.iUnitKey == counter.iUnitKey
                                                                                && f.RateCardYear.iYear == today.Year
                                                                                && f.RateCardYear.RateCard.AddressRateCards.Any(u => u.iAddressKey == counter.ConsumptionMeter.iAddressKey)
                                                                            ) == null : false;
                counterStatus.dtDateTime = DateTime.Now;

                // Check of een van de checks op true staat en zet dan de counter has error op true
                if (counterStatus.bHasNoConsumption || counterStatus.bHasNoRateCard || counterStatus.bLastServiceRunHasError)
                {
                    counterStatus.bHasError = true;
                }
                else
                {
                    counterStatus.bHasError = false;
                }

                // SET number of checked counters
                serviceRun.iServiceRunRowsUpdated += 1;
                serviceRun.sServiceRunMessage = serviceRun.iServiceRunRowsUpdated + " row(s) updated.";
                serviceRun.iServiceRunStatus = 200;

                // Save changes to database
                await db.SaveChangesAsync();
                //}

                serviceRun.dtServiceRunEndDate = DateTime.UtcNow;
                db.ServiceRuns.Add(serviceRun);
                // Save all changes to database
                await db.SaveChangesAsync();

                //await Task.Delay(interval);
            }
            catch (Exception e)
            {
                serviceRun.dtServiceRunEndDate = DateTime.UtcNow;
                serviceRun.sServiceRunMessage = e.Message;
                serviceRun.iServiceRunStatus = 500;
                serviceRun.iServiceRunRowsUpdated = 0;
                db.ServiceRuns.Add(serviceRun);
                await db.SaveChangesAsync();

                //await Task.Delay(TimeSpan.FromMinutes(1));
            }
            //}
        }

        public Task<Tuple<Result, Consumption>> AddStanding(Consumption consumption)
        {
            if (consumption.dtEndDateTime == null)
                return Task.FromResult(new Tuple<Result, Consumption>(Result.Failed(consumption.dtEndDateTime == null ? "De einddatum is niet gevuld" : null), consumption));

            Consumption = Consumption.OrderByDescending(o => o.dtEndDateTime).ThenByDescending(t => t.dEndPosition).ToList();

            //Check if endDate is After last consumption enddate
            if (consumption.dtEndDateTime >= Consumption.FirstOrDefault().dtEndDateTime)
            {
                if (consumption.dEndPosition < Consumption.FirstOrDefault().dEndPosition)
                    return Task.FromResult(new Tuple<Result, Consumption>(Result.Failed("Er wordt geprobeerd een lagere stand in te voeren na de laatste bekende stand."), consumption));
                //Standing is after last standing
                consumption.dConsumption = consumption.dEndPosition - Consumption.FirstOrDefault().dEndPosition;
                consumption.dtStartDateTime = Consumption.FirstOrDefault().dtEndDateTime;
                consumption.iAddressKey = ConsumptionMeter.iAddressKey;

                // AddStanding at list of consumption
                Consumption.Add(consumption);
                return Task.FromResult(new Tuple<Result, Consumption>(Result.Success, consumption));
            }

            if (consumption.dtEndDateTime < Consumption.LastOrDefault().dtEndDateTime)
                return Task.FromResult(new Tuple<Result, Consumption>(Result.Failed("Er wordt geprobeerd een stand voor de eerste stand ingevoerd."), consumption));

            //Get ConsumptionRow where the new standing will be placedd inn
            Consumption changedConsumption = Consumption.FirstOrDefault(f => f.dtStartDateTime <= consumption.dtEndDateTime && f.dtEndDateTime >= consumption.dtEndDateTime);
            if (changedConsumption.dEndPosition < consumption.dEndPosition || (changedConsumption.dEndPosition - changedConsumption.dConsumption) > consumption.dEndPosition)
                return Task.FromResult(new Tuple<Result, Consumption>(Result.Failed(string.Format("Deze stand {0} kan niet toegevoegd worden omdat de beginstand {1} hoger of de eindstand {2} lager is.", consumption.dEndPosition, (changedConsumption.dEndPosition - changedConsumption.dConsumption), changedConsumption.dEndPosition)), consumption));

            if (consumption.dEndPosition - (changedConsumption.dEndPosition - changedConsumption.dConsumption) < 0)
                return Task.FromResult(new Tuple<Result, Consumption>(Result.Failed(string.Format("Er kan geen verbruik minder dan 0 ingevoerd worden. Het verbruik nu is: {0}", consumption.dEndPosition - (changedConsumption.dEndPosition - changedConsumption.dConsumption))), consumption));

            consumption.iAddressKey = changedConsumption.iAddressKey;
            consumption.dtStartDateTime = changedConsumption.dtStartDateTime;
            consumption.dConsumption = consumption.dEndPosition - (changedConsumption.dEndPosition - changedConsumption.dConsumption);
            Consumption.Add(consumption);
            // Change consumption and endposition
            changedConsumption.dtStartDateTime = consumption.dtEndDateTime;
            changedConsumption.dConsumption = changedConsumption.dEndPosition - consumption.dEndPosition;

            return Task.FromResult(new Tuple<Result, Consumption>(Result.Success, consumption));
        }

        public Consumption GetLastConsumptionRow()
        {
            if (Consumption.Count > 0)
            {
                return Consumption.OrderByDescending(o => o.dtEndDateTime).ThenByDescending(o => o.dEndPosition).FirstOrDefault();
            }
            return null;
        }

        public Task<Tuple<Result, Consumption>> AddConsumption(Consumption consumption)
        {
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            if (consumption.dtEndDateTime == null || consumption.dConsumption == null)
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                return Task.FromResult(new Tuple<Result, Consumption>(Result.Failed("Verbruik en einddatum zijn verplicht."), consumption));
            if (consumption.dtEndDateTime < GetLastConsumptionRow().dtEndDateTime)
                return Task.FromResult(new Tuple<Result, Consumption>(Result.Failed("Verbruik moet na laatst bekende stand ingevoerd worden."), consumption));

            Consumption lastConsumptionRow = GetLastConsumptionRow();
            consumption.dtStartDateTime = lastConsumptionRow.dtEndDateTime;
            consumption.dEndPosition = lastConsumptionRow.dEndPosition += consumption.dConsumption;
            consumption.iCounterKey = iCounterKey;
            return Task.FromResult(new Tuple<Result, Consumption>(Result.Success, consumption)); ;
        }
        #endregion

    }
}