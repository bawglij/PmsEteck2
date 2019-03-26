using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;

namespace PmsEteck.Data.Models
{
    public enum AddressTypes
    {
        Huur = 1,
        Koop = 2
    }

   

    [Table(name: "Addresses", Schema = "pms")]
    public class Address
    {
        /*
        readonly IHttpContextAccessor _httpContextAccessor;

        public Address(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        */
        #region Constructor
        private PmsEteckContext db = new PmsEteckContext();
        #endregion
        #region Fields
        private string WebServiceEndPoint = "https://postcode-api.apiwise.nl/v2/addresses/";
        private string WebServiceApiKey = "9aDKUG8ZLo6cM0ytCsNBM7xbTNKk4xEW8kp5jlyI";
        #endregion

        [Key]
        public int iAddressKey { get; set; }

        [ForeignKey("CollectiveAddress")]
        [Display(Name = "Collectieve aansluiting")]
        public int? CollectiveAddressID { get; set; }

        [Display(Name = "Is collectieve aansluiting")]
        public bool IsCollectiveAddress { get; set; }

        [Display(Name = "Kostendelermethodiek actief?")]
        public bool CostlierServiceActive { get; set; }

        [Display(Name = "FINE-Code")]
        [MaxLength(10, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} mag alleen getallen bevatten")]
        public string sFineCode { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(150, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Straatnaam")]
        public string sStreetName { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Huisnummer")]
        public int iNumber { get; set; }

        [Display(Name = "Toevoeging")]
        [MaxLength(10, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        public string sNumberAddition { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Postcode")]
        [MaxLength(7, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        public string sPostalCode { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Plaats")]
        [MaxLength(100, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        public string sCity { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Project")]
        public int iProjectKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(2, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} mag alleen getallen bevatten")]
        [Display(Name = "Aansluittype")]
        public string sConnectionTypeKey { get; set; }

        [Display(Name = "Latitude")]
        public decimal? dLatitude { get; set; }

        [Display(Name = "Longitude")]
        public decimal? dLongitude { get; set; }

        [Display(Name = "Extra toelichting")]
        [MaxLength(150, ErrorMessage = "{0} mag maximaal {1} karakters lang zijn.")]
        public string sAddressComment { get; set; }

        [Display(Name = "Servicerun")]
        public int? iServiceRunKey { get; set; }

        [Display(Name = "Servicerun")]
        public virtual ServiceRun ServiceRun { get; set; }

        public virtual ProjectInfo Project { get; set; }

        [Display(Name = "Collectieve aansluiting")]
        public virtual Address CollectiveAddress { get; set; }

        [Display(Name = "Tariefkaarten")]
        public List<AddressRateCard> AddressRateCards { get; set; }

        [Display(Name = "Meters")]
        public List<ConsumptionMeter> ConsumptionMeters { get; set; }

        [Display(Name = "Verbruik")]
        public List<Consumption> Consumption { get; set; }

        [Display(Name = "Energie (koude)")]
        [DisplayFormat(DataFormatString = "{0:0.## kW}", ApplyFormatInEditMode = true)]
        public decimal? dColdElectricPower { get; set; }

        [Display(Name = "Energie (warmte)")]
        [DisplayFormat(DataFormatString = "{0:0.## kW}", ApplyFormatInEditMode = true)]
        public decimal? dHeatElectricPower { get; set; }

        [Display(Name = "Energie (tapwater)")]
        [DisplayFormat(DataFormatString = "{0:0.## kW}", ApplyFormatInEditMode = true)]
        public decimal? dTapWaterPower { get; set; }

        [Display(Name = "Bedrijfsvloeroppervlak")]
        [DisplayFormat(DataFormatString = "{0:0.## m²}", ApplyFormatInEditMode = true)]
        public decimal? dBVO { get; set; }

        [Display(Name = "Gecontracteerd vermogen elektra")]
        [DisplayFormat(DataFormatString = "{0:0.## kW/maand}", ApplyFormatInEditMode = true)]
        public decimal? dContractedCapacityEnergy { get; set; }

        [Display(Name = "Gecontracteerd vermogen gas")]
        [DisplayFormat(DataFormatString = "{0:0.##  m³/uur}", ApplyFormatInEditMode = true)]
        public decimal? dContractedCapacityGas { get; set; }

        [Display(Name = "Categorie")]
        public int? iCategory { get; set; }

        [Display(Name = "Woning type")]
        public AddressTypes? iAddressType { get; set; }

        [Display(Name = "Heeft binneninstallatie")]
        public bool IsIndoorInstallation { get; set; }

        [Display(Name = "Aansluittype")]
        public virtual ConnectionType ConnectionType { get; set; }

        [Display(Name = "Debiteuren")]
        public ICollection<AddressDebtor> AddressDebtors { get; set; }

        [Display(Name = "Bewoners")]
        public ICollection<AddressOccupant> AddressOccupants { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public ICollection<ServiceTicket> ServiceTickets { get; set; }

        [Display(Name = "Betalingsgegevens")]
        public List<PaymentHistory> PaymentHistory { get; set; }

        [Display(Name = "Betaalstaffel")]
        public ICollection<RateCardScaleHistory> RateCardScaleHistories { get; set; }

        [Display(Name = "Kostendelerwaarden")]
        public ICollection<CostlierValue> CostlierValues { get; set; }

        [Display(Name = "ID van het object waaronder de meters binnen WMS vallen.")]
        public int? ObjectID { get; set; }

        [Display(Name = "Aansluiting afgesloten?")]
        public bool Closed { get; set; }

        #region Properties for PurchaseAddresses

        [Display(Name = "Percentage distributieverlies")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal PercentLossOfDistribution { get; set; }

        [Display(Name = "Maximaal distributieverlies (dag)")]
        [DisplayFormat(DataFormatString = "{0:N2} GJ")]
        public decimal MaxLossOfDistribution { get; set; }

        [Display(Name = "Percentage hoofdelijke omslag")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal PercentPollTax { get; set; }

        [Display(Name = "Maximale hoofdelijke omslag (dag)")]
        [DisplayFormat(DataFormatString = "{0:N2} GJ")]
        public decimal MaxPollTax { get; set; }

        #endregion

        #region Methods
        public string GetAddressString()
        {
            return string.Format("{0} {1}, {2}", sStreetName, string.IsNullOrEmpty(sNumberAddition) ? iNumber.ToString() : iNumber + "-" + sNumberAddition, sCity);
        }

        public bool HasError
        {
            get
            {

                return Project.ProjectIsActive &&
                    ConnectionType.bConsumptionMeterRequired ?
                        ConsumptionMeters.Count() != 0 ?
                            ConsumptionMeters.Any(u => u.Counters.Count() == 0) ?
                                true
                            : ConsumptionMeters.Any(c => c.Counters.Any(co => co.OldCounterStatus.Count > 0 && co.OldCounterStatus.FirstOrDefault().bHasError))
                        : true
                    : AddressRateCards.Count() == 0;
            }
        }

        public void Delete(PmsEteckContext context, bool save)
        {
            if (ConsumptionMeters != null)
                ConsumptionMeters.ToList().ForEach(f => f.Delete(context, false));

            if (Tickets != null)
                foreach (Ticket ticket in Tickets)
                {
                    ticket.iAddressID = null;
                    context.Entry(ticket).State = EntityState.Modified;
                }

            context.Addresses.Remove(this);

       

            //if (save)
               // context.SaveChanges(_httpContextAccessor.HttpContext.User.Identity.GetUserID());
        }
        #endregion
        public JObject GetAddressDetails(string postcode, int number)
        {
            string url = string.Format("{0}?postcode={1}&number={2}", WebServiceEndPoint, postcode, number);

            WebClient client = new WebClient { Encoding = Encoding.UTF8 };
            client.Headers.Add("X-Api-Key", WebServiceApiKey);

            string resultString = client.DownloadString(url);

            JObject jObject = JObject.Parse(resultString);

            return jObject;
        }
    }
}