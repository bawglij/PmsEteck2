using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class WorkOrderStatus : Status
    {
        #region List References
        [Display(Name = "Werkbonnen")]
        public ICollection<WorkOrder> WorkOrders { get; set; }
        #endregion
    }
}