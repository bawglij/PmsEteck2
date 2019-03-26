using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("BudgetSettings", Schema ="budget")]
    public class BudgetSetting
    {
        [Key]
        public int iBudgetSettingKey { get; set; }

        public DateTime dtCreatedDate { get; set; }

        public ICollection<BudgetSectionIndex>  BudgetSectionIndex { get; set; }

        public ICollection<MonthDegreeDayIndex> MonthDegreeDayIndex { get; set; }

        public ICollection<YearDegreeDayIndex> YearDegreeDayIndex { get; set; }

    }
}