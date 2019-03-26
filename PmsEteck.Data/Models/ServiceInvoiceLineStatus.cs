using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class ServiceInvoiceLineStatus : Status
    {
        #region List References
        [Display(Name = "Servicefactuurregels")]
        public ICollection<ServiceInvoiceLine> ServiceInvoiceLines { get; set; }
        #endregion
    }
}