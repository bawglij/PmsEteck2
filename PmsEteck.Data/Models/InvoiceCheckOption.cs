using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("InvoiceCheckOptions", Schema = "invoice")]
    public class InvoiceCheckOption
    {
        [Key]
        public int InvoiceCheckOptionID { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public ICollection<InvoiceCheck> InvoiceChecks { get; set; }
    }
}