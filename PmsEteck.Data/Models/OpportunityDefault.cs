using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class OpportunityDefault
    {
        [Key]
        public int iOpportunityDefaultID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sName { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Value")]
        public decimal dValue { get; set; }


    }
}
