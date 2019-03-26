using PmsEteck.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data
{
    [Table("InvoiceChecks", Schema = "invoice")]
    public class InvoiceCheck
    {
        [Key]
        public int InvoiceCheckID { get; set; }

        [ForeignKey("Invoice")]
        public int InvoiceID { get; set; }

        [ForeignKey("InvoiceCheckOption")]
        public int InvoiceCheckOptionID { get; set; }

        public bool Valid { get; set; }

        [MaxLength(250)]
        public string Message { get; set; }

        public DateTime CheckDateTime { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual InvoiceCheckOption InvoiceCheckOption { get; set; }
    }
}