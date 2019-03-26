using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class ServiceTicketStatus : Status
    {
        #region List References
        [Display(Name = "Servicemeldingen")]
        public ICollection<ServiceTicket> ServiceTickets { get; set; }
        #endregion
    }
}