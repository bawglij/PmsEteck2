using PmsEteck.Data.Models;
using System.ComponentModel.DataAnnotations;


namespace PmsEteck.Data.ViewModels
{
    public class BudgetSectionIndexCreateViewModel
    {
        [Key]
        public int? iBudgetSectionIndexKey { get; set; }

        public int iRecNo { get; set; }

        public int iReportingStructureKey { get; set; }

        [Display(Name = "Variabel")]
        public bool bVariable { get; set; }

        [Display(Name = "Indexeringsvoet")]
        public decimal? dSectionIndex { get; set; }

        [Display(Name = "Vaste voet?")]
        public bool bFixedPart { get; set; }

        [Display(Name = "Percentage vaste voet")]
        public decimal dFixedPart { get; set; }

        public bool bSpatie { get; set; }

        public bool bSubtotaal { get; set; }

        public ReportingStructure ReportingStructure { get; set; }

    }

    public class BudgetSectionIndexDetailsViewModel
    {
        [Key]
        public int iBudgetSectionIndexKey { get; set; }

        [Display(Name = "Rubriek")]
        public string sReportingStructureName { get; set; }

        public int iRecNo { get; set; }

        [Display(Name = "Variabel")]
        public bool bVariable { get; set; }

        [Display(Name = "Indexeringsvoet")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#0.00%}")]
        public decimal dSectionIndex { get; set; }

        public bool bSpatie { get; set; }

        public bool bSubtotaal { get; set; }

        [Display(Name = "Vaste voet?")]
        public bool bFixedPart { get; set; }

        [Display(Name = "Percentage vaste voet")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:#0.0%}")]
        public decimal dFixedPart { get; set; }
    }

    public class BudgetSectionYearPreviewViewModel
    {
        [Key]
        public int iBudgetSectionIndexKey { get; set; }

        public int iReportingStructureKey { get; set; }

        public int iRecNo { get; set; }

        [Display(Name ="Hoofd/subrubriek")]
        public string sReportingStructureName { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dRealised { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal? dBudget { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal? dTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestion { get; set; }

        [MaxLength(255)]
        public string sComment { get; set; }

        public bool bSpatie { get; set; }

        public bool bSubtotaal { get; set; }

        public bool bVariable { get; set; }

        public bool bFixedPart { get; set; }
    }

    public class BudgetSectionMonthPreviewViewModel
    {
        [Key]
        public int iBudgetSectionIndexKey { get; set; }

        public int iReportingStructureKey { get; set; }

        public int iRecNo { get; set; }

        [Display(Name = "Hoofd/subrubriek")]
        public string sReportingStructureName { get; set; }

        public bool bVariable { get; set; }

        public bool bFixedPart { get; set; }

        public string sComment { get; set; }
        
        [Display(Name ="Totaal")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal? dTotal { get; set; }

        [Display(Name ="Januari")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionJan { get; set; }

        [Display(Name = "Februari")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionFeb { get; set; }

        [Display(Name = "Maart")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionMar { get; set; }

        [Display(Name = "April")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionApr { get; set; }

        [Display(Name = "Mei")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionMay { get; set; }

        [Display(Name = "Juni")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionJun { get; set; }

        [Display(Name = "Juli")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionJul { get; set; }

        [Display(Name = "Augustus")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionAug { get; set; }

        [Display(Name ="September")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionSep { get; set; }

        [Display(Name ="Oktober")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionOct { get; set; }

        [Display(Name ="November")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionNov { get; set; }

        [Display(Name ="December")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionDec { get; set; }

        public bool bSpatie { get; set; }

        public bool bSubtotaal { get; set; }

        public int? iRcStart { get; set; }

        public int? iRcEnd { get; set; }
    }
    
    public class BudgetSectionDetailsViewModel
    {
        [Key]
        public int iBudgetSectionIndexKey { get; set; }

        public int iReportingStructureKey { get; set; }

        public int iRecNo { get; set; }

        [Display(Name = "Hoofd/subrubriek")]
        public string sReportingStructureName { get; set; }

        [Display(Name = "Totaal")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal? dTotal { get; set; }

        [Display(Name = "Januari")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionJan { get; set; }

        [Display(Name = "Februari")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionFeb { get; set; }

        [Display(Name = "Maart")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionMar { get; set; }

        [Display(Name = "April")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionApr { get; set; }

        [Display(Name = "Mei")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionMay { get; set; }

        [Display(Name = "Juni")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionJun { get; set; }

        [Display(Name = "Juli")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionJul { get; set; }

        [Display(Name = "Augustus")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionAug { get; set; }

        [Display(Name = "September")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionSep { get; set; }

        [Display(Name = "Oktober")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionOct { get; set; }

        [Display(Name = "November")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionNov { get; set; }

        [Display(Name = "December")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? dSuggestionDec { get; set; }

        public bool bSpatie { get; set; }

        public bool bSubtotaal { get; set; }

        public string sComment { get; set; }
    }
} 