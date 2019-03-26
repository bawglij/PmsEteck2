using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("CounterTypeYearSeasonPatterns")]
    public class CounterTypeYearSeasonPattern : BaseModel
    {
        [ForeignKey("CounterType")]
        [Display(Name = "Telwerktype")]
        public int CounterTypeId { get; set; }

        [ForeignKey("ProjectYearDetail")]
        [Display(Name = "Jaar")]
        public Guid ProjectYearDetailId { get; set; }

        [ForeignKey("SeasonalPattern")]
        [Display(Name = "Seizoenspatroon")]
        public Guid SeasonalPatternId { get; set; }
        
        [Display(Name = "Telwerktype")]
        public virtual CounterType CounterType { get; set; }

        [Display(Name = "Jaar")]
        public virtual ProjectYearDetail ProjectYearDetail { get; set; }

        [Display(Name = "Seizoenspatroon")]
        public virtual SeasonalPattern SeasonalPattern { get; set; }

    }
}