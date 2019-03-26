using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.ViewModels
{
    public class BudgetDimensionRuleDetailsViewModel
    {
        [Key]
        public int iBudgetDimensionRuleKey { get; set; }

        public string sReportingStructureName { get; set; }

        public int iRecNo { get; set; }

        [Display(Name = "Totaal")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dTotal { get; set; }

        [Display(Name = "Januari")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dJanuary { get; set; }

        [Display(Name = "Februari")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dFebruary { get; set; }

        [Display(Name = "Maart")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dMarch { get; set; }

        [Display(Name = "April")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dApril { get; set; }

        [Display(Name = "Mei")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dMay { get; set; }

        [Display(Name = "Juni")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dJune { get; set; }

        [Display(Name = "Juli")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dJuly { get; set; }

        [Display(Name = "Augustus")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dAugust { get; set; }

        [Display(Name = "September")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dSeptember { get; set; }

        [Display(Name = "Oktober")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dOctober { get; set; }

        [Display(Name = "November")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dNovember { get; set; }

        [Display(Name = "December")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dDecember { get; set; }

        public bool bSpatie { get; set; }

        public bool bSubtotaal { get; set; }

        public bool bVariable { get; set; }
        public bool bFixedPart { get; set; }
        
        [MaxLength(255)]
        public string sComment { get; set; }
    }

    public class BudgetDimensionRuleCommentEditViewModel
    {
        [Key]
        public int BudgetDimensionRuleKey { get; set; }

        [MaxLength(255)]
        [Display(Name = "Opmerking")]
        public string sComment { get; set; }
    }
}