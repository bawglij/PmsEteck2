using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Demarcations")]
    public class Demarcation
    {
        [Key]
        public int iDemarcationKey { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name="Opwekking")]
        public string sDemarcation { get; set; }

        public bool bActive { get; set; }
    }
}