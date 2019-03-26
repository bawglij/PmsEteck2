using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class ServiceInvoiceStatus : Status
    {
        #region List References
        [Display(Name = "Servicefacturen")]
        public ICollection<ServiceInvoice> ServiceInvoices { get; set; }
        #endregion
    }
}