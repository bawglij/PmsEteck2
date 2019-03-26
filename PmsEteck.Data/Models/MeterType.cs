using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "MeterTypes", Schema = "meter")]
    public class MeterType
    {
        [Key]
        public int iMeterTypeKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(50, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Omschrijving")]
        public string sDescription { get; set; }

        [MaxLength(50, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "In- of verkoop")]
        public string sPurchaseOrSale { get; set; }

        public IEnumerable<ConsumptionMeter> ConsumptionMeters { get; set; }
    }
}