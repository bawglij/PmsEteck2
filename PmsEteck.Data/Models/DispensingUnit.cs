using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class DispensingUnit
    {
        [Key]
        public int iDispensingUnitKey { get; set; }

        [Display(Name = "Unit")]
        [MaxLength(100)]
        public string sDispensingUnitName { get; set; }

        public bool bActive { get; set; }

    }
}