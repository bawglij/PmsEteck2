using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    public class OpportunityNote
    {
        [Key]
        public int iOpportunityNoteID { get; set; }

        public int iOpportunityID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Note")]
        public string sNote { get; set; }

        [Display(Name = "Date")]
        public DateTime dtCreateDateTime { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [ForeignKey("ApplicationUser")]
        [Display(Name = "User")]
        public string sUserID { get; set; }

        [Display(Name = "Opportunity")]
        public Opportunity Opportunity { get; set; }

        [Display(Name = "User")]
        [NotMapped]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}