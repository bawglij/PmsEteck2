using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("YearDegreeDayIndex", Schema ="budget")]
    public class YearDegreeDayIndex
    {
        [Key]
        public int iYearDegreeIndexKey { get; set; }

        public int iBudgetSettingKey { get; set; }

        [Display(Name ="Jaar")]
        public int iYear { get; set; }

        [Display(Name ="Graaddagen index")]
        public decimal dDegreeDayIndex { get; set; }

        public BudgetSetting BudgetSetting { get; set; }
    }
}