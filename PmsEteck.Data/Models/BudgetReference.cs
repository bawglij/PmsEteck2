using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("BudgetReferences", Schema = "budget")]
    public class BudgetReference : BaseModel
    {
        [MaxLength(100)]
        public string Description { get; set; }

        public int Code { get; set; }

        public int Order { get; set; }
    }
}