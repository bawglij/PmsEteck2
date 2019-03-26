using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "Operators")]
    public class Operator
    {
        [Key]
        public int iOperatorID { get; set; }

        [Display(Name = "Netbeheerder")]
        [MaxLength(100)]
        public string sName { get; set; }

        public bool bActive { get; set; } = true;
    }
}