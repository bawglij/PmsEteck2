using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("CalcCategories")]
    public class CalcCategory
    {
        [Key]
        public int iCalcCategoryKey { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name ="Calculatie categorie")]
        public string sCalcCategory { get; set; }

        public bool bActive { get; set; }

    }
}