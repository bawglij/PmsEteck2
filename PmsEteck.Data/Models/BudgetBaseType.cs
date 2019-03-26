using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("BudgetBaseTypes", Schema ="budget")]
    public class BudgetBaseType
    {
        [Key]
        public int iBudgetBaseTypeKey { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name ="Budget type")]
        public string sBudgetBaseTypeName { get; set; }
    }
}