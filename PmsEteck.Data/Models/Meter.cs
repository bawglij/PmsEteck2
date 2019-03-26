using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Meters")]
    public class Meter
    {
        [Key]
        public int iMeterKey { get; set; }

        [Required]
        [MaxLength(100)]
        public string sMeter { get; set; }

        public bool bActive { get; set; }
    }
}