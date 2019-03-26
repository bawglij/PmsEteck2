using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace PmsEteck.Data.Models
{
    [Table(name: "ReportValidations", Schema = "meter")]
    public class ReportValidation
    {

        #region Constructor
        private PmsEteckContext db = new PmsEteckContext();
        #endregion

        #region Properties

        [Key]
        public int iReportValidationKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Rapportage")]
        public int iReportKey { get; set; }

        [Display(Name = "HTTP statuscode")]
        public int? iStatusCode { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Bericht")]
        [MaxLength(500, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sValidationMessage { get; set; }

        public Report Report { get; set; }

        #endregion

        #region Methods
        /*
        public async Task<Report> CreateAsync()
        {
            var requestContext = HttpContext.Current.Request.RequestContext;
            if (Report.Project.iFinProjectKey.HasValue)
            {
                BudgetDimension dimension = await db.BudgetDimensions.FirstOrDefaultAsync(f => !f.bDraft && f.BudgetBase.iBudgetBaseTypeKey == 2 && f.BudgetBase.bActive && f.iProjectKey == Report.Project.iFinProjectKey && f.BudgetBase.iYear == Report.ReportPeriod.iYear && f.iBudgetDimensionTypeKey == 2);
                if (dimension == null)
                {
                    Report.ReportValidations.Add(new ReportValidation
                    {
                        iStatusCode = 400,
                        sValidationMessage = "Er is geen budget gevonden"
                    });
                }
            }
            else
            {
                Report.ReportValidations.Add(new ReportValidation
                {
                    iStatusCode = 400,
                    sValidationMessage = "Dit project is niet gekoppeld aan een financieel project. Hierdoor kan er geen budget getoond worden."
                });
            }

            DateTime periodStart = new DateTime(Report.ReportPeriod.iYear, Report.ReportPeriod.iPeriod, 1);
            DateTime periodEnd = periodStart.AddMonths(1);

            // Select all active counters for this reportProject
            List<Counter> counters = await db.Counters
                .Include(i => i.ConsumptionMeter.Address.AddressRateCards)
                .Include(i => i.ConsumptionMeter.Address.AddressRateCards.Select(s => s.RateCard))
                .Include(i => i.ConsumptionMeter.Address.AddressRateCards.Select(s => s.RateCard.RateCardYears))
                .Include(i => i.ConsumptionMeter.Address.AddressRateCards.Select(s => s.RateCard.RateCardYears.Select(se => se.RateCardRows)))
                .Where(w => !w.ConsumptionMeter.MeterType.sPurchaseOrSale.Contains("Distribution") && !w.ConsumptionMeter.Address.sConnectionTypeKey.StartsWith("8") && w.ConsumptionMeter.Address.iProjectKey == Report.iProjectKey).ToListAsync();

            foreach (Counter counter in counters)
            {
                // set firstConsumptionDate and lastConsumptionDate
                DateTime firstConsumptionDate = periodStart - TimeSpan.FromDays(counter.ConsumptionMeter.iDaysMargin);
                DateTime lastConsumptionDate = periodEnd - TimeSpan.FromDays(counter.ConsumptionMeter.iDaysMargin);
                bool hasConsumptionOnFirstDayOfPeriod = false;
                bool hasConsumptionOnLastDayOfPeriod = false;
                switch (counter.iCounterTypeKey)
                {
                    case 11:
                    case 14:
                        //Maximumpower
                        hasConsumptionOnFirstDayOfPeriod = await db.MaximumPowers.FirstOrDefaultAsync(w => w.iAddressKey == counter.ConsumptionMeter.iAddressKey && w.Counter.iCounterTypeKey == counter.iCounterTypeKey && w.dtStartDateTime == periodStart) != null;
                        hasConsumptionOnLastDayOfPeriod = hasConsumptionOnFirstDayOfPeriod;
                        break;
                    case 15:
                        //Blindconsumption
                        hasConsumptionOnFirstDayOfPeriod = await db.BlindConsumptions.FirstOrDefaultAsync(w => w.iAddressKey == counter.ConsumptionMeter.iAddressKey && w.Counter.iCounterTypeKey == counter.iCounterTypeKey && w.dtStartDateTime == periodStart) != null;
                        hasConsumptionOnLastDayOfPeriod = hasConsumptionOnFirstDayOfPeriod;
                        break;
                    default:
                        // Other consumption
                        hasConsumptionOnFirstDayOfPeriod = await db.Consumption.FirstOrDefaultAsync(w => w.iAddressKey == counter.ConsumptionMeter.iAddressKey && w.Counter.iCounterTypeKey == counter.iCounterTypeKey && w.dtStartDateTime <= firstConsumptionDate && w.dtEndDateTime >= firstConsumptionDate) != null;
                        hasConsumptionOnLastDayOfPeriod = await db.Consumption.FirstOrDefaultAsync(w => w.iAddressKey == counter.ConsumptionMeter.iAddressKey && w.Counter.iCounterTypeKey == counter.iCounterTypeKey && w.dtStartDateTime <= lastConsumptionDate && w.dtEndDateTime >= lastConsumptionDate) != null;
                        break;
                }

                if (!hasConsumptionOnFirstDayOfPeriod)
                {
                    Report.ReportValidations.Add(new ReportValidation
                    {
                        iStatusCode = 404,
                        sValidationMessage = string.Format("Er is geen verbruik gevonden op peildatum {0} voor telwerktype " +
                                                    "<a href=\"" + new UrlHelper(requestContext).Action("Details", "Counters", new { id = counter.iCounterKey }, requestContext.HttpContext.Request.Url.Scheme) + "\" >{1}</a>" +
                                                    "  van adres: <a href=\"" + new UrlHelper(requestContext).Action("Details", "Addresses", new { id = counter.ConsumptionMeter.iAddressKey }, requestContext.HttpContext.Request.Url.Scheme) + "\" >{2}</a>", firstConsumptionDate.ToShortDateString(), counter.CounterType.sCounterTypeDescription, counter.ConsumptionMeter.Address.GetAddressString())
                    });
                }

                if (!hasConsumptionOnLastDayOfPeriod)
                {
                    Report.ReportValidations.Add(new ReportValidation
                    {
                        iStatusCode = 404,
                        sValidationMessage = string.Format("Er is geen verbruik gevonden op peildatum {0} voor telwerktype " +
                                                    "<a href=\"" + new UrlHelper(requestContext).Action("Details", "Counters", new { id = counter.iCounterKey }, requestContext.HttpContext.Request.Url.Scheme) + "\" >{1}</a>" +
                                                    "  van adres: <a href=\"" + new UrlHelper(requestContext).Action("Details", "Addresses", new { id = counter.ConsumptionMeter.iAddressKey }, requestContext.HttpContext.Request.Url.Scheme) + "\" >{2}</a>", lastConsumptionDate.ToShortDateString(), counter.CounterType.sCounterTypeDescription, counter.ConsumptionMeter.Address.GetAddressString())
                    });
                }

                //bool counterHasTarif = counter.ConsumptionMeter.Address.RateCards.SelectMany(sm => sm.RateCardYears.Where(w => w.iYear == periodStart.Year)).SelectMany(sm => sm.RateCardRows).FirstOrDefault(f => f.iCounterTypeKey == counter.iCounterTypeKey && f.iUnitKey == counter.iUnitKey) != null;
                if (counter.ConsumptionMeter.Address.AddressRateCards.SelectMany(sm => sm.RateCard.RateCardYears.Where(w => w.iYear == periodStart.Year)).SelectMany(sm => sm.RateCardRows).FirstOrDefault(f => f.iCounterTypeKey == counter.iCounterTypeKey && f.iUnitKey == counter.iUnitKey) == null)
                {
                    Report.ReportValidations.Add(new ReportValidation
                    {
                        iStatusCode = 404,
                        sValidationMessage = string.Format("Er is geen tarief gevonden voor " +
                                                    "<a href=\"" + new UrlHelper(requestContext).Action("Details", "Counters", new { id = counter.iCounterKey }, requestContext.HttpContext.Request.Url.Scheme) + "\" >{0}-verbruik </a>" +
                                                    " in {1} op peildatum {2} van adres: <a href=\"" + new UrlHelper(requestContext).Action("Details", "Addresses", new { id = counter.ConsumptionMeter.iAddressKey }, requestContext.HttpContext.Request.Url.Scheme) + "\" >{3}</a>", counter.CounterType.sCounterTypeDescription, counter.Unit.sDescription, periodStart.ToShortDateString(), counter.ConsumptionMeter.Address.GetAddressString())
                    });
                }
            }
            return Report;
        }
        */
        public async Task<bool> ConsumptionOnDateAsync(Counter counter, DateTime date)
        {
            switch (counter.iCounterTypeKey)
            {
                case 11:
                case 14:
                    break;
                case 15:
                    break;
                default:
                    break;
            }
            bool returnValue = await db.Consumption.FirstOrDefaultAsync(w => w.iAddressKey == counter.ConsumptionMeter.iAddressKey && w.Counter.iCounterTypeKey == counter.iCounterTypeKey && w.dtStartDateTime <= date && w.dtEndDateTime >= date) == null;
            return await Task.FromResult(await db.Consumption.FirstOrDefaultAsync(w => w.iAddressKey == counter.ConsumptionMeter.iAddressKey && w.Counter.iCounterTypeKey == counter.iCounterTypeKey && w.dtStartDateTime <= date && w.dtEndDateTime >= date) == null);
        }
        #endregion
    }
}