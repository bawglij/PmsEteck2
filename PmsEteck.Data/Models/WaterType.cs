using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class WaterType
    {
        [Key]
        public int iWaterTypeKey { get; set; }

        [MaxLength(100)]
        public string sWaterType { get; set; }

        public bool bActive { get; set; }
    }
}