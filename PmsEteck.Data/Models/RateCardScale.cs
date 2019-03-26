using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "RateCardScales", Schema = "meter")]
    public class RateCardScale
    {
        [Key]
        public int iRateCardScaleKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(100, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Staffelgroep")]
        public string sRateCardScaleDescription { get; set; }

        [Display(Name = "Regels")]
        public List<RateCardScaleRow> RateCardScaleRows { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Eenheid")]
        public int iUnitKey { get; set; }

        [Display(Name = "Eenheid")]
        public Unit Unit { get; set; }
        
    }
}