using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "RubricGroups", Schema = "meter")]
    public class RubricGroup
    {
        [Key]
        public int iRubricGroupKey { get; set; }

        [Display(Name = "Rubriekgroep")]
        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sRubricGroupDescription { get; set; }

    }
}