using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("WeqCategories")]
    public class WeqCategory
    {
        [Key]
        public int iWeqCategoryKey { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name ="Weq categorie")]
        public string sWeqCategory { get; set; }

        public bool bActive { get; set; }
    }
}