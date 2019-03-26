using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("BudgetDimensionTypes", Schema ="budget")]
    public class BudgetDimensionType
    {
        [Key]
        public int iBudgetDimensionTypeKey { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name ="Budget dimensie")]
        public string sBudgetDimensionTypeName { get; set; }
    }
}