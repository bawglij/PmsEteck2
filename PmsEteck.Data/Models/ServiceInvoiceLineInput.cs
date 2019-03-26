using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("ServiceInvoiceLineInputs", Schema = "service")]
    public class ServiceInvoiceLineInput
    {
        #region Keys
        [Key]
        [Display(Name = "Servicefactuurregel")]
        public Guid ServiceInvoiceLineInputID { get; set; }

        [Display(Name = "Gebruiker")]
        [ForeignKey("User")]
        public string UserID { get; set; }
        #endregion

        #region Properties
        [Display(Name = "Materiaalkosten")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal? MaterialCost { get; set; }

        [Display(Name = "Werkuren")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal? WorkingHours { get; set; }

        [Display(Name = "Werkuren 125%")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal? WorkingHours25 { get; set; }

        [Display(Name = "Werkuren 150%")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal? WorkingHours50 { get; set; }

        [Display(Name = "Werkuren 200%")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal? WorkingHours100 { get; set; }

        [Display(Name = "Reisuren")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal? CallOutHours { get; set; }

        [Display(Name = "Reiskilometers")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal? CallOutKilometers { get; set; }

        [Display(Name = "Totale kosten")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal TotalCosts { get; set; }

        [Display(Name = "Aantekeningen")]
        [MaxLength(1000, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string Note { get; set; }

        [Display(Name = "Validatie moment")]
        public DateTime ValidatedDate { get; set; }
        #endregion

        #region Single References
        public virtual ApplicationUser User { get; set; }
        #endregion

        #region List References
        [Display(Name = "Werkbonnen")]
        public ICollection<WorkOrder> WorkOrders { get; set; }

        public ICollection<ServiceInvoiceLine> MaintenanceContactInputLines { get; set; }

        public ICollection<ServiceInvoiceLine> AssetmanagerInputLines { get; set; }

        public ICollection<ServiceInvoiceLine> CoordinatorInputLines { get; set; }

        public ICollection<ServiceInvoiceLine> OwnerInputLines { get; set; }
        #endregion
    }
}