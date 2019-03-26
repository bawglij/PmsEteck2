using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class ValueOperator
    {
        [Key]
        public int ValueOperatorID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Operator")]
        [StringLength(100, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string Operator { get; set; }
    }
}