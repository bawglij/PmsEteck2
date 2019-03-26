using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Periods")]
    public class Period
    {
        [Key]
        public int iPeriodKey { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name="Termijn")]
        public string sPeriod { get; set; }

        public bool bActive { get; set; }
    }
}