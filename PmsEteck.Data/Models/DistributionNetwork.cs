using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class DistributionNetwork
    {
        [Key]
        public int iDistributionNetWorkKey { get; set; }

        [MaxLength(25)]
        public string sDistributionNetWorkName { get; set; }

        public bool bActive { get; set; }

    }
}