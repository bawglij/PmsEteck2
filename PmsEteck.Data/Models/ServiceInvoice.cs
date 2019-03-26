using PmsEteck.Data.Models.Results;
//using PmsEteck.Data.WSPurchase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace PmsEteck.Data.Models
{
    [Table("ServiceInvoices", Schema = "service")]
    public class ServiceInvoice
    {
        #region Constructor
            private readonly PmsEteckContext context = new PmsEteckContext();
            //WSPurchase_Service _service;
        #endregion

        #region Static Fields
        private string WebserviceUser = ConfigurationManager.AppSettings["WebserviceUser"];
        private string WebserviceDomain = ConfigurationManager.AppSettings["WebserviceDomain"];
        private string WebservicePassword = ConfigurationManager.AppSettings["WebservicePassword"];
        private string ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        private string EETBV = ConfigurationManager.AppSettings["EETBV"];
        #endregion

        #region Keys
        [Key]
        [Display(Name = "Servicefactuur")]
        public Guid ServiceInvoiceID { get; set; }

        [Display(Name = "Status")]
        [ForeignKey("Status")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public int StatusID { get; set; }

        [Display(Name = "Onderhoudspartij")]
        [ForeignKey("MaintenanceContact")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public int MaintenanceContactID { get; set; }

        [Display(Name = "Project")]
        [ForeignKey("Project")]
        public int? ProjectID { get; set; }

        [Display(Name = "Factuurnummer")]
        public int ServiceInvoiceCode { get; set; }
        public string ServiceInvoiceCodeString => "I" + ServiceInvoiceCode.ToString("00000000");

        #endregion

        #region Properties
        [Display(Name = "Aangemaakt")]
        [DisplayFormat(DataFormatString = "{0:d-M-yyyy}")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Geplaatst")]
        [DisplayFormat(DataFormatString = "{0:d-M-yyyy}")]
        public DateTime? PostedDateTime { get; set; }

        [StringLength(250)]
        public string ExternalReference { get; set; }
        #endregion

        #region Single References
        public virtual ServiceInvoiceStatus Status { get; set; }

        public virtual MaintenanceContact MaintenanceContact { get; set; }

        public virtual ProjectInfo Project { get; set; }
        #endregion

        #region List References
        [Display(Name = "Servicefactuurregels")]
        public ICollection<ServiceInvoiceLine> ServiceInvoiceLines { get; set; }
        #endregion

        #region Methods
        public Task<Result> CreateServiceInvoice(IEnumerable<ServiceInvoiceLine> serviceInvoiceLines)
        {
            if (serviceInvoiceLines.Count() > 0)
            {
                CreatedDateTime = DateTime.UtcNow;
                MaintenanceContactID = serviceInvoiceLines.First().MaintenanceContactID;
                ProjectID = serviceInvoiceLines.First().WorkOrder.ServiceTicket.ProjectID;
                StatusID = context.ServiceInvoiceStatuses.FirstOrDefault(f => f.StatusCode == 100).StatusID;
                ServiceInvoiceID = Guid.NewGuid();
                ServiceInvoiceLines = new Collection<ServiceInvoiceLine>();
                ServiceInvoiceLines = serviceInvoiceLines.ToList();
                return Task.FromResult(Result.Success);

            }
            return Task.FromResult(Result.Failed("Er konden geen factuurregels gekoppeld worden."));
        }
        /*
        public Task<Result> AddToNav(List<ServiceInvoiceStatus> invoiceStatuses)
        {
            try
            {
                ProjectBase finProject = context.ProjectBases.FirstOrDefault(f => f.iProjectKey == Project.iFinProjectKey);
                if (finProject == null)
                    throw new Exception("Financieel project kan niet gevonden worden.");
                _service = new WSPurchase_Service();
                _service.Credentials = new System.Net.NetworkCredential(WebserviceUser, WebservicePassword, WebserviceDomain);
                _service.Url = string.Format("{0}/WS/{1}/Page/WSPurchase", ServiceUrl, Uri.EscapeDataString(EETBV));

                WSPurchase.WSPurchase purchase = new WSPurchase.WSPurchase() {
                    Buy_from_Vendor_No = MaintenanceContact.VendorCode,
                    Document_Date = CreatedDateTime,
                    Document_DateSpecified = true,
                    Document_Type = Document_Type.Order,
                    Document_TypeSpecified = true,
                    No = ServiceInvoiceCodeString,
                    Payment_Terms_Code = "INCASSO",
                    Pay_to_Vendor_No = MaintenanceContact.VendorCode,
                    Posting_Date = DateTime.Now,
                    Posting_DateSpecified = true,
                    Posting_Description = "",
                    Purchase_Line_Import = ServiceInvoiceLines.Select(s => new WS_Purch_Doc_Line_Import
                    {
                        Description = string.Format("Werkbon: {0}", s.WorkOrder.WorkOrderCodeString),
                        Description_2 = string.Format("Storingsnummer: {0}", s.WorkOrder.ServiceTicket.TicketCodeString),
                        Direct_Unit_Cost = s.OwnerInput.TotalCosts,
                        Direct_Unit_CostSpecified = true,
                        //Customer_No = Klant aan wie kosten doorbelast moeten worden,
                        Customer_Order_Type = "05",
                        Line_No = 1 + ServiceInvoiceLines.ToList().IndexOf(s),
                        Line_NoSpecified = true,
                        No = "A.04", // Artikelnummer
                        Project_No = Project.ProjectBase.sProjectCode,
                        Project_Task_No = "0520", // 110/120
                        Quantity = 1,
                        QuantitySpecified =true,
                        Type = WSPurchase.Type.Item,
                        TypeSpecified = true,
                        Unit_of_Measure = "Stuks"
                    }).ToArray(),
                    Transaction_Mode_Code = "001", // Crediteuren binnenland
                    //Your_Reference
                };

                _service.Create(ref purchase);
                Status = invoiceStatuses.FirstOrDefault(f => f.StatusCode == 400);
                PostedDateTime = DateTime.UtcNow;
                return Task.FromResult(Result.Success);
            }
            catch (Exception ex)
            {

                return Task.FromResult(Result.Failed(ex.Message));
            }
        }
        */
        #endregion
    }
}