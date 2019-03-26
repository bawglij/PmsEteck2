using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class InvoiceBatchStatus : Status
    {
        [Display(Name = "Factuurbatches")]
        public List<InvoiceBatch> InvoiceBatches { get; set; }

    }
}
