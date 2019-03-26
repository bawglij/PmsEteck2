using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "RateCardTypes", Schema = "meter")]
    public class RateCardType
    {
        [Key]
        public int iRateCardTypeKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(150, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Omschrijving")]
        public string sDescription { get; set; }
    }
}