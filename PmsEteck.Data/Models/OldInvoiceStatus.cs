using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("InvoiceStatuses", Schema = "invoice")]
    public class OldInvoiceStatus
    {
        [Key]
        public int iStatusID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Status")]
        public string sStatus { get; set; }

        public int Order { get; set; }

        public ICollection<Invoice> Invoices { get; set; }

        public ICollection<InvoiceBatch> InvoiceBatches { get; set; }
    }
}