using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "Units", Schema = "meter")]
    public class Unit
    {
        [Key]
        public int iUnitKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(150, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Eenheid")]
        public string sDescription { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(20, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Eenheid")]
        public string sUnit { get; set; }

        [Display(Name = "Eenheid actief?")]
        public bool bActive { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Column(Order = 1)]
        [Display(Name = "Sortering")]
        public int iOrder { get; set; }

        [Display(Name = "Gebruiken bij aanmaken telwerk?")]
        public bool bUsedForCounter { get; set; }
    }
}