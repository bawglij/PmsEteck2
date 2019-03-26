using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "ConnectionTypes", Schema = "meter")]
    public class ConnectionType
    {
        [Key]
        [MaxLength(2, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} mag alleen getallen bevatten")]
        [Display(Name = "Aansluitnummer")]
        public string sConnectionTypeKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(100, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Aansluitomschrijving")]
        public string sConnectionTypeDescription { get; set; }

        [Display(Name = "Meter is verplicht")]
        public bool bConsumptionMeterRequired { get; set; }

        [Display(Name = "Aansluittype is actief")]
        public bool bActive { get; set; }

        [Display(Name = "Is vastrecht")]
        public bool IsStandingRight { get; set; }
    }
}