using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Financers")]
    public class Financer
    {
        [Key]
        public int iFinancerKey { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name="Financier")]
        public string sFinancer { get; set; }

        [MaxLength(50)]
        [Display(Name = "Rekeningnummer rente")]
        public string sLedgerNumberInterest { get; set; }

        [MaxLength(50)]
        [Display(Name = "Rekeningnummer aflossing")]
        public string sLedgerNumberAmortization { get; set; }

        public bool bActive { get; set; }
    }
}