using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("MonthDegreeDayIndex", Schema ="budget")]
    public class MonthDegreeDayIndex
    {
        [Key]
        public int iMonthDegreeDayIndexKey { get; set; }

        public int iBudgetSettingKey { get; set; }

        public int iMonthKey { get; set; }

        [Display(Name ="Graaddagen index")]
        public decimal dDegreeDayIndex { get; set; }

        public BudgetSetting BudgetSetting { get; set; }

        public Month Month { get; set; }
    }
}