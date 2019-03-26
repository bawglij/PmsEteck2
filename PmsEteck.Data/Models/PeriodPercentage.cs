using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("PeriodPercentages")]
    public class PeriodPercentage : BaseModel
    {
        [ForeignKey("SeasonalPattern")]
        public Guid SeasonalPatternId { get; set; }
        
        [Display(Name = "Periodenummer")]
        public int PeriodNumber { get; set; }

        [Display(Name = "Pecentage")]
        public decimal Percentage { get; set; }

        public virtual SeasonalPattern SeasonalPattern { get; set; }

    }
}