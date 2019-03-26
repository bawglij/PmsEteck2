//using PmsEteck.Data.WSVendor;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace PmsEteck.Data.Models
{
    [Table("MaintenanceContacts")]
    public class MaintenanceContact
    {
        #region Constructor
        //WSVendor_Service _service;
        #endregion

        #region Static Fields
        private string WebserviceUser = ConfigurationManager.AppSettings["WebserviceUser"];
        private string WebserviceDomain = ConfigurationManager.AppSettings["WebserviceDomain"];
        private string WebservicePassword = ConfigurationManager.AppSettings["WebservicePassword"];
        private string ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        private string EETBV = ConfigurationManager.AppSettings["EETBV"];
        #endregion
        [Key]
        public int iMaintenanceContactKey { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Organisatie")]
        public string sOrganisation { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "Storingstelefoonnummer 1")]
        public string sErrorNumber1 { get; set; }

        [MaxLength(15)]
        [Display(Name = "Storingstelefoonnummer 2")]
        public string sErrorNumber2 { get; set; }

        [MaxLength(50)]
        [EmailAddress]
        [Display(Name = "Email tbv storingen")]
        public string sEmail { get; set; }

        [MaxLength(50)]
        [Display(Name = "1e Contactpersoon")]
        public string sContactName { get; set; }

        [MaxLength(15)]
        [Display(Name = "Telefoon")]
        public string sContactPhone { get; set; }

        [MaxLength(50)]
        [Display(Name = "Email")]
        public string sContactEmail { get; set; }

        [Display(Name = "Uurtarief arbeid")]
        public decimal? UnitPriceHourlyRate { get; set; }

        [Display(Name = "Uurtarief voorrijkosten (reisuur)")]
        public decimal? UnitPriceCallOutHours { get; set; }

        [Display(Name = "Kilometerprijs voorrijkosten (km's)")]
        public decimal? UnitPriceCallOutKilometers { get; set; }

        [Display(Name = "Maakt actief gebruik van PMS")]
        public bool ActiveService { get; set; }

        [Display(Name = "Crediteurnummer Navision")]
        [MaxLength(20, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string VendorCode { get; set; }

        [Display(Name = "Type uitgaand berichtenverkeer")]
        public MaintenanceContactCommunicationType MaintenanceContactCommunicationType { get; set; } = MaintenanceContactCommunicationType.Email;

        [Display(Name = "Global Location Number (GLN)")]
        [StringLength(128, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string GlobalLocationNumber { get; set; }

        public ICollection<ServiceTicket> ServiceTickets { get; set; }

        public ICollection<ServiceInvoice> ServiceInvoices { get; set; }

        public ICollection<ServiceInvoiceLine> ServiceInvoiceLines { get; set; }

        //vld
        public List<ProjectInfo> Projects { get; set; }
        //public ICollection<MaintenanceContractProjectInfo> Projects { get; set; }

        public ICollection<Opportunity> Opportunities { get; set; }

        public virtual ICollection<WebserviceConnection> WebserviceConnections { get; set; }
        /*
        #region Methods
        public List<SelectListItem> GetNavVendors()
        {
            List<SelectListItem> vendorList = new List<SelectListItem>();
            try
            {
                _service = new WSVendor_Service();
                _service.Credentials = new System.Net.NetworkCredential(WebserviceUser, WebservicePassword, WebserviceDomain);
                _service.Url = string.Format("{0}/WS/{1}/Page/WSVendor", ServiceUrl, Uri.EscapeDataString(EETBV));
                List<WSVendor_Filter> filters = new List<WSVendor_Filter>();
                var navVendors = _service.ReadMultiple(filters.ToArray(), null, int.MaxValue);
                vendorList.AddRange(navVendors.Select(s => new SelectListItem {
                    Value = s.No,
                    Text = s.No + " - " + s.Name + " (" + s.City + ")"
                }));
                return vendorList;
            }
            catch (System.Exception ex)
            {
                return vendorList;
            }
        }
        #endregion
        */
    }
}