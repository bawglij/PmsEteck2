using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "RateCardYears", Schema = "meter")]
    public class RateCardYear
    {
        [Key]
        public int iRateCardYearKey { get; set; }

        [Display(Name = "Opmerking")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Tariefkaart")]
        public int iRateCardKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Jaar")]
        public int iYear { get; set; }

        public RateCard RateCard { get; set; }
        
        [Display(Name = "Tariefkaartregels")]
        public List<RateCardRow> RateCardRows { get; set; }

        public ICollection<File> Files { get; set; }
    }
}