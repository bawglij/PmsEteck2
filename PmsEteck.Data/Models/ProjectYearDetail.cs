using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("ProjectYearDetails")]
    public class ProjectYearDetail : BaseModel
    {

        [ForeignKey("ProjectInfo")]
        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        [Display(Name = "Jaar")]
        public int Year { get; set; }

        [Display(Name = "Project")]
        public virtual ProjectInfo ProjectInfo { get; set; }

        [Display(Name = "Seizoenspatronen telwerktype")]
        public ICollection<CounterTypeYearSeasonPattern> CounterTypeYearCurves { get; set; }

    }
}