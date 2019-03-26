using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PmsEteck.Data.Models
{
    [Table("BudgetSectionIndex", Schema= "budget")]
    public class BudgetSectionIndex
    {
        [Key]
        public int iBudgetSectionIndexKey { get; set; }

        public int iBudgetSettingKey { get; set; }

        public int iReportingStructureKey { get; set; }

        public int iRecNo { get; set; }

        [Display(Name ="Variabel")]
        public bool bVariable { get; set; }

        [Display(Name ="Indexeringsvoet")]
        public decimal dSectionIndex { get; set; }

        public bool bSpatie { get; set; }

        public bool bSubtotaal { get; set; }

        [Display(Name = "Vaste voet?")]
        public bool bFixedPart { get; set; }

        [Display(Name = "Percentage vaste voet")]
        public decimal dFixedPart { get; set; }


        public BudgetSetting BudgetSetting { get; set; }

        public ReportingStructure ReportingStructure { get; set; }


    }
}