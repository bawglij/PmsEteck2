using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class OpportunityStatus
    {
        [Key]
        public int OpportunityStatusID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Description")]
        [StringLength(100, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string EnglishDescription { get; set; }

        [Display(Name = "Sortering")]
        public int Order { get; set; }

        [Display(Name = "In tijdlijn tonen")]
        public bool ShowInTimeline { get; set; }

        [Display(Name = "Tijdspan vanaf aanvraagdatum")]
        public int TimespanFromRequestDate { get; set; }

        [Display(Name = "Tijdspan vanaf vorige status")]
        public int TimespanFromPreviousStatus { get; set; }

        [Display(Name = "Opportunities")]
        public List<Opportunity> Opportunities { get; set; }
    }
}