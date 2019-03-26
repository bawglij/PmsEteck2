using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "RateCardScaleRows", Schema = "meter")]
    public class RateCardScaleRow
    {
        [Key]
        public int iRateCardScaleRowKey { get; set; }

        [Display(Name = "Staffel")]
        public int iRateCardScaleKey { get; set; }

        [MaxLength(150, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Omschrijving")]
        public string sDescription { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Van")]
        public int iRowStart { get; set; }

        [Display(Name = "Tot en met")]
        public int? iRowEnd { get; set; }

        [Display(Name = "Regels")]
        public RateCardScale RateCardScale { get; set; }
        
        public List<RateCardRow> RateCardRows { get; set; }
    }
}