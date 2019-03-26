using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class OpportunityType
    {
        [Key]
        public int OpportunityTypeID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Code")]
        [StringLength(10, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string Code { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Description")]
        [StringLength(100, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string EnglishDescription { get; set; }

        [Display(Name = "Opportunities")]
        public List<Opportunity> Opportunities { get; set; }
    }
}