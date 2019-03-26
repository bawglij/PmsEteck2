using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("SubsidyCategories")]
    public class SubsidyCategory
    {
        [Key]
        public int iSubsidyCategoryKey { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name ="Subsidiecategorie")]
        public string sSubsidyCategory { get; set; }

        public bool bActive { get; set; }
    }
}