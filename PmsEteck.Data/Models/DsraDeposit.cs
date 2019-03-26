using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("DsraDeposits")]
    public class DsraDeposit
    {
        [Key]
        public int iDsraDepositKey { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name ="DSRA Storting")]
        public string sDsraDepositName { get; set; }

        public bool bActive { get; set; }
    }
}