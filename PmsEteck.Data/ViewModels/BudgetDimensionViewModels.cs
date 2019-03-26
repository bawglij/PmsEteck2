using Microsoft.AspNetCore.Mvc.Rendering;
using PmsEteck.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.ViewModels
{
    public class BudgetDimensionIndexViewModel
    {
        [Key]
        public int iBudgetDimensionKey { get; set; }

        [Display(Name = "Begroting")]
        public int iBudgetBaseKey { get; set; }

        [Display(Name = "Dimensie type")]
        public string sBudgetDimensionTypeName { get; set; }

        [Display(Name = "Project")]
        public string sProjectName { get; set; }

        [Display(Name = "Omschrijving")]
        [MaxLength(250)]
        public string sDescription { get; set; }

        [Display(Name = "Laatste bewerkingsdatum")]
        [Required(ErrorMessage = "{0} is verplicht.")]
        public DateTime dtLastModified { get; set; }

        public bool bDraft { get; set; }
    }

    public class BudgetDimensionCreateViewModel
    {
        private PmsEteckContext db = new PmsEteckContext();

        [Display(Name = "Begroting")]
        public int iBudgetBaseKey { get; set; }

        [Display(Name = "Dimensie type")]
        public int iBudgetDimensionTypeKey { get; set; }
        public IEnumerable<SelectListItem> DimensionTypes { get; set; }

        [Display(Name = "Project")]
        public int? iProjectKey { get; set; }
        public IEnumerable<SelectListItem> Projects { get; set; }

        [Required]
        [Display(Name = "Omschrijving")]
        [MaxLength(250)]
        public string sDescription { get; set; }

        [Display(Name = "Baseer voorstel op boekjaar")]
        public int iYearPreview { get; set; }
        public IEnumerable<SelectListItem> YearsPreview { get; set; }

        [Display(Name = "Tot en met periode")]
        public int iEndPeriodPreview { get; set; }
        public IEnumerable<SelectListItem> EndPeriodsPreview { get; set; }
    }

    public class BudgetDimensionYearPreviewViewModel
    {
        [Display(Name = "Begroting")]
        public int iBudgetBaseKey { get; set; }

        [Display(Name = "Dimensie type")]
        public int iBudgetDimensionTypeKey { get; set; }

        [Display(Name = "Project")]
        public int? iProjectKey { get; set; }

        [Display(Name = "Omschrijving")]
        [MaxLength(250)]
        public string sDescription { get; set; }

        [Display(Name = "EBITDA")]
        public decimal dEbitda { get; set; }

        [Display(Name = "Bedrijfsresultaat")]
        public decimal dOperatingIncome { get; set; }

        [Display(Name = "Netto-resultaat")]
        public decimal dNettIncome { get; set; }

        [Display(Name = "Realisatiejaar")]
        public int iYearPreview { get; set; }

        [Display(Name = "Tot en met periode")]
        public int iEndPeriodPreview { get; set; }

        [Display(Name = "Begrotingsjaar")]
        public int iYearBudget { get; set; }

        public List<BudgetSectionYearPreviewViewModel> BudgetSections { get; set; }
    }

    public class BudgetDimensionMonthPreviewViewModel
    {
        [Key]
        public int iBudgetDimensionKey { get; set; }

        [Display(Name = "Begroting")]
        public int iBudgetBaseKey { get; set; }

        [Display(Name = "Dimensie type")]
        public int iBudgetDimensionTypeKey { get; set; }

        [Display(Name = "Project")]
        public int? iProjectKey { get; set; }

        [Display(Name = "Omschrijving")]
        [MaxLength(250)]
        public string sDescription { get; set; }

        [Display(Name = "Realisatiejaar")]
        public int iYearPreview { get; set; }

        [Display(Name = "Tot en met periode")]
        public int iEndPeriodPreview { get; set; }

        [Display(Name = "Begrotingsjaar")]
        public int iYearBudget { get; set; }

        public bool bDraft { get; set; }

        public IEnumerable<BudgetSectionMonthPreviewViewModel> BudgetSections { get; set; }
    }

    public class BudgetDimensionDetailsViewModel
    {
        [Key]
        public int iBudgetDimensionKey { get; set; }

        [Key]
        public int iBudgetSettingKey { get; set; }

        [Display(Name = "Begroting")]
        public int iBudgetBaseKey { get; set; }

        [Display(Name = "Project")]
        public string sProject { get; set; }

        [Display(Name = "Omschrijving")]
        [MaxLength(250)]
        public string sDescription { get; set; }

        [Display(Name = "Realisatiejaar")]
        public int iYearPreview { get; set; }

        [Display(Name = "Tot en met periode")]
        public int iEndPeriodPreview { get; set; }

        [Display(Name = "Begrotingsjaar")]
        public int iYearBudget { get; set; }

        public bool bDraft { get; set; }

        public IEnumerable<BudgetDimensionRuleDetailsViewModel> DimensionRules { get; set; }
    }
}