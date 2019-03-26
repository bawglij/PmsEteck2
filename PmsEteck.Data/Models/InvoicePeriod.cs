using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("InvoicePeriods", Schema = "invoice")]
    public class InvoicePeriod
    {
        public int InvoicePeriodID { get; set; }

        public string Description { get; set; }

        public ICollection<Debtor> Debtors { get; set; }
    }
}
