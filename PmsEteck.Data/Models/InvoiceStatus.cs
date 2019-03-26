using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class InvoiceStatus : Status
    {
        [Display(Name = "Facturen")]
        public List<Invoice> Invoices { get; set; }
    }
}
