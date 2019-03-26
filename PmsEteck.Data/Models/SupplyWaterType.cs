using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class SupplyWaterType
    {
        [Key]
        public int iSupplyWaterTypeKey { get; set; }

        [Display(Name = "Soort levering")]
        [MaxLength(50)]
        public string sSupplyWaterType { get; set; }

        public bool bActive { get; set; } = true;

    }
}