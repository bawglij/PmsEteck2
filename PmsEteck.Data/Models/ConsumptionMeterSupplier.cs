using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "ConsumptionMeterSuppliers", Schema = "meter")]
    public class ConsumptionMeterSupplier
    {
        [Key]
        public int iConsumptionMeterSupplierKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(100, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Leverancier")]
        public string sConsumptionMeterSupplier { get; set; }

        public IEnumerable<ConsumptionMeter> ConsumptionMeters { get; set; }

    }
}