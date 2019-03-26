using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("ServiceInvoiceLines", Schema = "service")]
    public class ServiceInvoiceLine
    {
        #region Keys
        [Key, ForeignKey("WorkOrder")]
        [Display(Name = "Servicefactuurregel")]
        public Guid ServiceInvoiceLineID { get; set; }

        [Display(Name = "Servicefactuur")]
        [ForeignKey("ServiceInvoice")]
        public Guid? ServiceInvoiceID { get; set; }

        [Display(Name = "Status")]
        [ForeignKey("Status")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public int StatusID { get; set; }

        [Display(Name = "Onderhoudspartij")]
        [ForeignKey("MaintenanceContact")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public int MaintenanceContactID { get; set; }

        [Display(Name = "Invoer onderhoudspartij")]
        [ForeignKey("MaintenanceContactInput")]
        public Guid? MaintenanceContactInputID { get; set; }

        [Display(Name = "Invoer asset manager")]
        [ForeignKey("AssetManagerInput")]
        public Guid? AssetManagerInputID { get; set; }

        [Display(Name = "Invoer S&O Coördinator")]
        [ForeignKey("CoordinatorInput")]
        public Guid? CoordinatorInputID { get; set; }

        [Display(Name = "Invoer bedrijfseigenaar")]
        [ForeignKey("OwnerInput")]
        public Guid? OwnerInputID { get; set; }

        [ForeignKey("User")]
        public string RejectByID { get; set; }
        #endregion

        #region Properties
        [Display(Name = "Aangemaakt")]
        [DisplayFormat(DataFormatString = "{0:d-M-yyyy}")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Afgerond")]
        [DisplayFormat(DataFormatString = "{0:d-M-yyyy}")]
        public DateTime? FinishedDateTime { get; set; }

        [StringLength(250)]
        public string ExternalReference { get; set; }

        public bool Rejected { get; set; }

        [MaxLength(1000)]
        public string RejectComment { get; set; }
        #endregion

        #region Single References
        public virtual WorkOrder WorkOrder { get; set; }

        public virtual ServiceInvoice ServiceInvoice { get; set; }

        public virtual ServiceInvoiceLineStatus Status { get; set; }

        public virtual MaintenanceContact MaintenanceContact { get; set; }

        [ForeignKey("MaintenanceContactInputID")]
        public virtual ServiceInvoiceLineInput MaintenanceContactInput { get; set; }

        [ForeignKey("AssetManagerInputID")]
        public virtual ServiceInvoiceLineInput AssetManagerInput { get; set; }

        [ForeignKey("CoordinatorInputID")]
        public virtual ServiceInvoiceLineInput CoordinatorInput { get; set; }

        [ForeignKey("OwnerInputID")]
        public virtual ServiceInvoiceLineInput OwnerInput { get; set; }

        public ApplicationUser User { get; set; }
        #endregion
    }
}