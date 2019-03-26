using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class TemperatureRange
    {
        [Key]
        public int iTemperatureRangeKey { get; set; }

        [Display(Name = "Temperatuurtraject")]
        [MaxLength(100)]
        public string sTemperatureRange { get; set; }

        public bool bActive { get; set; }
        
        [MaxLength(1)]
        public string sHeatOrCold { get; set; }
    }
}