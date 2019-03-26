using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "RubricTypes", Schema = "meter")]
    public class RubricType
    {
        [Key]
        public int iRubricTypeKey { get; set; }

        [Display(Name = "Rubriektype")]
        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sRubricTypeDescription { get; set; }
    }
}