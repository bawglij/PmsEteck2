using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("ProjectStatuses")]
    public class ProjectStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int iProjectStatusID { get; set; }

        [StringLength(150, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Statusomschrijving")]
        public string sStatusDescription { get; set; }

        [Display(Name = "Projecten")]
        public List<ProjectInfo> Projects { get; set; }
    }
}