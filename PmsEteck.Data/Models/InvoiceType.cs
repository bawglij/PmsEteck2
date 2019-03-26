using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("InvoiceTypes", Schema = "invoice")]
    public class InvoiceType
    {
        [Key]
        public int iInvoiceTypeID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Factuurtype")]
        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sInvoiceTypeName { get; set; }
    }
}