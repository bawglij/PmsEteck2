using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "MeasuringOfficers")]
    public class MeasuringOfficer
    {
        [Key]
        public int iMeasuringOfficerID { get; set; }

        [Display(Name = "Meetverantwoordelijke")]
        [MaxLength(100)]
        public string sName { get; set; }

        public bool bActive { get; set; } = true;
    }
}