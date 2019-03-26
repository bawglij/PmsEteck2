using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace PmsEteck.Data.Models
{
    [Table("BudgetDimensions", Schema = "budget")]
    public class BudgetDimension
    {
        private PmsEteckContext db = new PmsEteckContext();

        [Key]
        public int iBudgetDimensionKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Dimensie type")]
        public int iBudgetDimensionTypeKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Begroting")]
        [ForeignKey("BudgetBase")]
        public int iBudgetBaseKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Budget instellingen")]
        public int iBudgetSettingKey { get; set; }

        [Display(Name = "Project")]
        public int? iProjectKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Realisatiejaar")]
        public int iYearPreview { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Tot en met periode")]
        public int iEndPeriodPreview { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Begrotingsjaar")]
        public int iYearBudget { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(250, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Omschrijving")]
        public string sBudgetDimensionDescription { get; set; }

        public Guid? BudgetReferenceId { get; set; }

        public bool BaseOnDefaultProfile { get; set; }

        public int? StartMonth { get; set; }

        [MaxLength(1000)]
        public string Note { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        public DateTime dtLastModified { get; set; }

        public bool bDraft { get; set; }

        public virtual BudgetBase BudgetBase { get; set; }

        public virtual BudgetReference BudgetReference { get; set; }

        public virtual BudgetDimensionType BudgetDimensionType { get; set; }

        public virtual ProjectBase ProjectBase { get; set; }

        public virtual BudgetSetting BudgetSetting { get; set; }

        public ICollection<BudgetDimensionRule> BudgetDimensionRules { get; set; }

        public ICollection<Budget> Budgets { get; set; }

        public void AddCalculatedRules()
        {
            using (PmsEteckContext _context = new PmsEteckContext())
            {
                _context.Database.SetCommandTimeout(300);
                _context.Database.OpenConnection();
                using (DbCommand _command = _context.Database.GetDbConnection().CreateCommand())
                {
                    _command.CommandTimeout = 300;
                    _command.CommandText = "budget.GetCalculatedDimensionRules";
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.Parameters.Add(new SqlParameter("@budgetDimensionId", iBudgetDimensionKey));

                    using (DbDataReader reader = _command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ReportingStructure reportingStructure = db.ReportingStructure.Find((int)reader["iReportingStructureKey"]);
                            BudgetDimensionRules.Add(
                                new BudgetDimensionRule()
                                {
                                    iReportingStructureKey = reportingStructure.iReportingStructureKey,
                                    iRecNo = reportingStructure.RecNo.Value,
                                    dTotal = reader["dTotal"] != DBNull.Value ? (decimal)reader["dTotal"] : 0,
                                    dJanuary = reader["dJanuary"] != DBNull.Value ? (decimal)reader["dJanuary"] : 0,
                                    dFebruary = reader["dFebruary"] != DBNull.Value ? (decimal)reader["dFebruary"] : 0,
                                    dMarch = reader["dMarch"] != DBNull.Value ? (decimal)reader["dMarch"] : 0,
                                    dApril = reader["dApril"] != DBNull.Value ? (decimal)reader["dApril"] : 0,
                                    dMay = reader["dMay"] != DBNull.Value ? (decimal)reader["dMay"] : 0,
                                    dJune = reader["dJune"] != DBNull.Value ? (decimal)reader["dJune"] : 0,
                                    dJuly = reader["dJuly"] != DBNull.Value ? (decimal)reader["dJuly"] : 0,
                                    dAugust = reader["dAugust"] != DBNull.Value ? (decimal)reader["dAugust"] : 0,
                                    dSeptember = reader["dSeptember"] != DBNull.Value ? (decimal)reader["dSeptember"] : 0,
                                    dOctober = reader["dOctober"] != DBNull.Value ? (decimal)reader["dOctober"] : 0,
                                    dNovember = reader["dNovember"] != DBNull.Value ? (decimal)reader["dNovember"] : 0,
                                    dDecember = reader["dDecember"] != DBNull.Value ? (decimal)reader["dDecember"] : 0,
                                    PercentJanuary = reader["PercentJan"] != DBNull.Value ? (decimal)reader["PercentJan"] : (decimal)1 / 12,
                                    PercentFebruary = reader["PercentFeb"] != DBNull.Value ? (decimal)reader["PercentFeb"] : (decimal)1 / 12,
                                    PercentMarch = reader["PercentMar"] != DBNull.Value ? (decimal)reader["PercentMar"] : (decimal)1 / 12,
                                    PercentApril = reader["PercentApr"] != DBNull.Value ? (decimal)reader["PercentApr"] : (decimal)1 / 12,
                                    PercentMay = reader["PercentMay"] != DBNull.Value ? (decimal)reader["PercentMay"] : (decimal)1 / 12,
                                    PercentJune = reader["PercentJun"] != DBNull.Value ? (decimal)reader["PercentJun"] : (decimal)1 / 12,
                                    PercentJuly = reader["PercentJul"] != DBNull.Value ? (decimal)reader["PercentJul"] : (decimal)1 / 12,
                                    PercentAugust = reader["PercentAug"] != DBNull.Value ? (decimal)reader["PercentAug"] : (decimal)1 / 12,
                                    PercentSeptember = reader["PercentSep"] != DBNull.Value ? (decimal)reader["PercentSep"] : (decimal)1 / 12,
                                    PercentOctober = reader["PercentOct"] != DBNull.Value ? (decimal)reader["PercentOct"] : (decimal)1 / 12,
                                    PercentNovember = reader["PercentNov"] != DBNull.Value ? (decimal)reader["PercentNov"] : (decimal)1 / 12,
                                    PercentDecember = reader["PercentDec"] != DBNull.Value ? (decimal)reader["PercentDec"] : (decimal)1 / 12,
                                    bSpatie = reader["bSpatie"] != DBNull.Value ? (bool)reader["bSpatie"] : false,
                                    bSubtotaal = reader["bSubtotaal"] != DBNull.Value ? (bool)reader["bSubtotaal"] : false,
                                });
                        };
                    }
                }
            }
        }

        public BudgetDimensionYearPreviewViewModel GenerateBudgetDimensionYearPreview(BudgetDimensionCreateViewModel model)
        {
            BudgetDimensionYearPreviewViewModel preview = new BudgetDimensionYearPreviewViewModel();

            bool projectHasFixedPart = false;

            try
            {
                BudgetBase budgetBase = db.BudgetBases
                                        .FirstOrDefault(f => f.iBudgetBaseKey == model.iBudgetBaseKey);

                BudgetSetting budgetSettings = db.BudgetSettings
                                        .Include(i => i.BudgetSectionIndex)
                                        .Include(i => i.BudgetSectionIndex.Select(s => s.ReportingStructure))
                                        .Include(i => i.YearDegreeDayIndex)
                                        .OrderByDescending(o => o.iBudgetSettingKey)
                                        .FirstOrDefault();


                IQueryable<Resultaatoverzicht> financialTransactions = db.Resultaatoverzicht
                                                 .Where(w => w.iCustomerID == budgetBase.iCustomerID && w.iYear == model.iYearPreview && w.iPeriod <= model.iEndPeriodPreview);

                IQueryable<Resultaatoverzicht> budgetTransactions = db.Resultaatoverzicht
                                                 .Where(w => w.iCustomerID == budgetBase.iCustomerID && w.iYear == model.iYearPreview && w.iPeriod > model.iEndPeriodPreview);

                if (model.iProjectKey.HasValue)
                {
                    financialTransactions = financialTransactions
                                                 .Where(w => w.iProjectKey == model.iProjectKey);

                    budgetTransactions = budgetTransactions
                                                 .Where(w => w.iProjectKey == model.iProjectKey);

                    ProjectInfo projectInfo = db.ProjectInfo.FirstOrDefault(f => f.iFinProjectKey == model.iProjectKey);

                    if (projectInfo != null)
                    {
                        int[] gasKeys = db.TechnicalPrincipals.Where(w => w.bIsGas).Select(s => s.iTechnicalPrincipalKey).ToArray();
                        projectHasFixedPart = projectInfo.bHotWater || (projectInfo.iTechnicalPrincipalMainKey.HasValue && gasKeys.Contains(projectInfo.iTechnicalPrincipalMainKey.GetValueOrDefault()));
                        projectHasFixedPart = !projectHasFixedPart ? projectInfo.iTechnicalPrincipalSub1Key.HasValue ? gasKeys.Contains(projectInfo.iTechnicalPrincipalSub1Key.Value) : false : projectHasFixedPart;
                        projectHasFixedPart = !projectHasFixedPart ? projectInfo.iTechnicalPrincipalSub2Key.HasValue ? gasKeys.Contains(projectInfo.iTechnicalPrincipalSub2Key.Value) : false : projectHasFixedPart;
                    }
                }

                preview.iBudgetDimensionTypeKey = model.iBudgetDimensionTypeKey;
                preview.iBudgetBaseKey = model.iBudgetBaseKey;
                preview.iProjectKey = model.iProjectKey;
                preview.sDescription = model.sDescription;
                preview.iYearPreview = model.iYearPreview;
                preview.iEndPeriodPreview = model.iEndPeriodPreview;
                preview.iYearBudget = budgetBase.iYear;

                preview.BudgetSections = new List<BudgetSectionYearPreviewViewModel>();

                foreach (BudgetSectionIndex item in budgetSettings.BudgetSectionIndex)
                {
                    decimal budgetSaldo = budgetTransactions.Where(w => w.RecNo == item.iRecNo).Count() > 0 ? budgetTransactions.Where(w => w.RecNo == item.iRecNo && w.SaldoBudget.HasValue).Sum(sm => sm.SaldoBudget.Value) : 0;
                    decimal realisedSaldo = financialTransactions.Where(w => w.RecNo == item.iRecNo).Count() > 0 ? financialTransactions.Where(w => w.RecNo == item.iRecNo && w.SaldoRealisatie.HasValue).Sum(sm => sm.SaldoRealisatie.Value) : 0;
                    decimal totalSaldo = budgetSaldo + realisedSaldo;

                    BudgetSectionYearPreviewViewModel previewSection = new BudgetSectionYearPreviewViewModel()
                    {
                        iRecNo = item.iRecNo,
                        iBudgetSectionIndexKey = item.iBudgetSectionIndexKey,
                        iReportingStructureKey = item.iReportingStructureKey,
                        sReportingStructureName = item.ReportingStructure.sDescription,
                        bSpatie = item.bSpatie,
                        bSubtotaal = item.bSubtotaal,
                        bVariable = item.bVariable,
                        bFixedPart = item.bFixedPart,
                        dRealised = realisedSaldo,
                        dBudget = budgetSaldo,
                        dTotal = realisedSaldo + budgetSaldo,
                        dSuggestion = CalculateYearSuggestion(realisedSaldo + budgetSaldo, item, projectHasFixedPart, model.iYearPreview, budgetBase.iYear).Value
                    };

                    preview.BudgetSections.Add(previewSection);
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return preview;
        }

        public int GenerateBudgetDimensionMonthPreview(BudgetDimensionYearPreviewViewModel model)
        {
            int budgetDimensionKey = 0;

            bool projectHasFixedPart = false;


            try
            {
                BudgetBase budgetBase = db.BudgetBases
                                        .FirstOrDefault(f => f.iBudgetBaseKey == model.iBudgetBaseKey);

                BudgetSetting budgetSettings = db.BudgetSettings
                                        .Include(i => i.BudgetSectionIndex)
                                        .Include(i => i.BudgetSectionIndex.Select(s => s.ReportingStructure))
                                        .Include(i => i.MonthDegreeDayIndex)
                                        .OrderByDescending(o => o.iBudgetSettingKey)
                                        .FirstOrDefault();

                // Create the BudgetDimension
                BudgetDimension budgetDimension = new BudgetDimension
                {
                    iBudgetBaseKey = model.iBudgetBaseKey,
                    bDraft = true,
                    BudgetSetting = budgetSettings,
                    iBudgetDimensionTypeKey = model.iBudgetDimensionTypeKey,
                    iProjectKey = model.iProjectKey,
                    iYearBudget = model.iYearBudget,
                    iYearPreview = model.iYearPreview,
                    iEndPeriodPreview = model.iEndPeriodPreview,
                    sBudgetDimensionDescription = model.sDescription,
                    dtLastModified = DateTime.UtcNow,
                    BudgetDimensionRules = new Collection<BudgetDimensionRule>()
                };

                if (model.iProjectKey.HasValue)
                {
                    ProjectInfo projectInfo = db.ProjectInfo.FirstOrDefault(f => f.iFinProjectKey == model.iProjectKey);

                    if (projectInfo != null)
                    {
                        int[] gasKeys = db.TechnicalPrincipals.Where(w => w.bIsGas).Select(s => s.iTechnicalPrincipalKey).ToArray();
                        projectHasFixedPart = projectInfo.bHotWater || (projectInfo.iTechnicalPrincipalMainKey.HasValue && gasKeys.Contains(projectInfo.iTechnicalPrincipalMainKey.GetValueOrDefault()));
                        projectHasFixedPart = !projectHasFixedPart ? projectInfo.iTechnicalPrincipalSub1Key.HasValue ? gasKeys.Contains(projectInfo.iTechnicalPrincipalSub1Key.Value) : false : projectHasFixedPart;
                        projectHasFixedPart = !projectHasFixedPart ? projectInfo.iTechnicalPrincipalSub2Key.HasValue ? gasKeys.Contains(projectInfo.iTechnicalPrincipalSub2Key.Value) : false : projectHasFixedPart;
                    }
                }

                //Check of er gekozen is voor een DCF
                if (budgetBase.iBudgetBaseTypeKey == 1)
                {
                    //Hierbij is gekozen voor een DCF Hierbij hoeft geen verdeling over maanden plaats te vinden
                    foreach (BudgetSectionYearPreviewViewModel item in model.BudgetSections)
                    {
                        BudgetDimensionRule budgetDimensionRule = new BudgetDimensionRule
                        {
                            bSpatie = item.bSpatie,
                            bSubtotaal = item.bSubtotaal,
                            dJanuary = item.dSuggestion.HasValue ? item.dSuggestion.Value : 0,
                            dFebruary = 0,
                            dMarch = 0,
                            dApril = 0,
                            dMay = 0,
                            dJune = 0,
                            dJuly = 0,
                            dAugust = 0,
                            dSeptember = 0,
                            dOctober = 0,
                            dNovember = 0,
                            dDecember = 0,
                            dTotal = !item.bSpatie && !item.bSubtotaal ? item.dSuggestion.HasValue ? item.dSuggestion.Value : 0 : 0,
                            iRecNo = item.iRecNo,
                            iReportingStructureKey = item.iReportingStructureKey,
                            sComment = item.sComment
                        };

                        budgetDimension.BudgetDimensionRules.Add(budgetDimensionRule);
                    }
                }
                else
                {
                    // Hier wordt het bedrag over de maanden uitgesmeerd! Niet nodig voor begroting van DCF.
                    foreach (BudgetSectionYearPreviewViewModel item in model.BudgetSections)
                    {
                        BudgetDimensionRule budgetDimensionRule = new BudgetDimensionRule
                        {
                            bSpatie = item.bSpatie,
                            bSubtotaal = item.bSubtotaal,
                            dJanuary = CalculateMonthSuggestion(item.dSuggestion.HasValue ? item.dSuggestion.Value : 0, budgetSettings.BudgetSectionIndex.FirstOrDefault(f => f.iBudgetSectionIndexKey == item.iBudgetSectionIndexKey), projectHasFixedPart, 1, budgetSettings.MonthDegreeDayIndex),
                            dFebruary = CalculateMonthSuggestion(item.dSuggestion.HasValue ? item.dSuggestion.Value : 0, budgetSettings.BudgetSectionIndex.FirstOrDefault(f => f.iBudgetSectionIndexKey == item.iBudgetSectionIndexKey), projectHasFixedPart, 2, budgetSettings.MonthDegreeDayIndex),
                            dMarch = CalculateMonthSuggestion(item.dSuggestion.HasValue ? item.dSuggestion.Value : 0, budgetSettings.BudgetSectionIndex.FirstOrDefault(f => f.iBudgetSectionIndexKey == item.iBudgetSectionIndexKey), projectHasFixedPart, 3, budgetSettings.MonthDegreeDayIndex),
                            dApril = CalculateMonthSuggestion(item.dSuggestion.HasValue ? item.dSuggestion.Value : 0, budgetSettings.BudgetSectionIndex.FirstOrDefault(f => f.iBudgetSectionIndexKey == item.iBudgetSectionIndexKey), projectHasFixedPart, 4, budgetSettings.MonthDegreeDayIndex),
                            dMay = CalculateMonthSuggestion(item.dSuggestion.HasValue ? item.dSuggestion.Value : 0, budgetSettings.BudgetSectionIndex.FirstOrDefault(f => f.iBudgetSectionIndexKey == item.iBudgetSectionIndexKey), projectHasFixedPart, 5, budgetSettings.MonthDegreeDayIndex),
                            dJune = CalculateMonthSuggestion(item.dSuggestion.HasValue ? item.dSuggestion.Value : 0, budgetSettings.BudgetSectionIndex.FirstOrDefault(f => f.iBudgetSectionIndexKey == item.iBudgetSectionIndexKey), projectHasFixedPart, 6, budgetSettings.MonthDegreeDayIndex),
                            dJuly = CalculateMonthSuggestion(item.dSuggestion.HasValue ? item.dSuggestion.Value : 0, budgetSettings.BudgetSectionIndex.FirstOrDefault(f => f.iBudgetSectionIndexKey == item.iBudgetSectionIndexKey), projectHasFixedPart, 7, budgetSettings.MonthDegreeDayIndex),
                            dAugust = CalculateMonthSuggestion(item.dSuggestion.HasValue ? item.dSuggestion.Value : 0, budgetSettings.BudgetSectionIndex.FirstOrDefault(f => f.iBudgetSectionIndexKey == item.iBudgetSectionIndexKey), projectHasFixedPart, 8, budgetSettings.MonthDegreeDayIndex),
                            dSeptember = CalculateMonthSuggestion(item.dSuggestion.HasValue ? item.dSuggestion.Value : 0, budgetSettings.BudgetSectionIndex.FirstOrDefault(f => f.iBudgetSectionIndexKey == item.iBudgetSectionIndexKey), projectHasFixedPart, 9, budgetSettings.MonthDegreeDayIndex),
                            dOctober = CalculateMonthSuggestion(item.dSuggestion.HasValue ? item.dSuggestion.Value : 0, budgetSettings.BudgetSectionIndex.FirstOrDefault(f => f.iBudgetSectionIndexKey == item.iBudgetSectionIndexKey), projectHasFixedPart, 10, budgetSettings.MonthDegreeDayIndex),
                            dNovember = CalculateMonthSuggestion(item.dSuggestion.HasValue ? item.dSuggestion.Value : 0, budgetSettings.BudgetSectionIndex.FirstOrDefault(f => f.iBudgetSectionIndexKey == item.iBudgetSectionIndexKey), projectHasFixedPart, 11, budgetSettings.MonthDegreeDayIndex),
                            dDecember = CalculateMonthSuggestion(item.dSuggestion.HasValue ? item.dSuggestion.Value : 0, budgetSettings.BudgetSectionIndex.FirstOrDefault(f => f.iBudgetSectionIndexKey == item.iBudgetSectionIndexKey), projectHasFixedPart, 12, budgetSettings.MonthDegreeDayIndex),
                            dTotal = !item.bSpatie && !item.bSubtotaal ? item.dSuggestion.HasValue ? item.dSuggestion.Value : 0 : 0,
                            iRecNo = item.iRecNo,
                            iReportingStructureKey = item.iReportingStructureKey,
                            sComment = item.sComment
                        };

                        budgetDimensionRule.dDecember = item.dSuggestion.GetValueOrDefault() -
                            budgetDimensionRule.dJanuary -
                            budgetDimensionRule.dFebruary -
                            budgetDimensionRule.dMarch -
                            budgetDimensionRule.dApril -
                            budgetDimensionRule.dMay -
                            budgetDimensionRule.dJune -
                            budgetDimensionRule.dJuly -
                            budgetDimensionRule.dAugust -
                            budgetDimensionRule.dSeptember -
                            budgetDimensionRule.dOctober -
                            budgetDimensionRule.dNovember;

                        budgetDimension.BudgetDimensionRules.Add(budgetDimensionRule);
                    }
                }

                db.BudgetDimensions.Add(budgetDimension);
                db.SaveChanges();

                budgetDimensionKey = budgetDimension.iBudgetDimensionKey;

            }
            catch (Exception e)
            {
                throw e;
            }

            return budgetDimensionKey;
        }

        public bool CreateBudget(int iBudgetDimensionKey, int partnerid)
        {
            try
            {
                BudgetDimension budgetDimension = db.BudgetDimensions
                                            .Include(i => i.BudgetBase)
                                            .Include(i => i.Budgets)
                                            .Include(i => i.BudgetDimensionRules)
                                            .Include(i => i.BudgetDimensionRules.Select(s => s.ReportingStructure))
                                            .Include(i => i.BudgetSetting)
                                            .FirstOrDefault(f => f.iBudgetDimensionKey == iBudgetDimensionKey);

                if (budgetDimension.Budgets != null)
                {
                    if (!RemoveBudget(budgetDimension.iBudgetDimensionKey))
                    {
                        return false;
                    }
                }

                foreach (BudgetDimensionRule item in budgetDimension.BudgetDimensionRules.Where(w => !w.bSpatie && !w.bSubtotaal))
                {
                    if (item.dJanuary != 0)
                    {
                        Budget budget = new Budget()
                        {
                            dcAmountBudget = item.dJanuary,
                            iCustomerID = budgetDimension.BudgetBase.iCustomerID,
                            iBudgetDimensionKey = budgetDimension.iBudgetDimensionKey,
                            sReportingCode = item.ReportingStructure.RcStart,
                            iPartnerID = partnerid,
                            iProjectKey = budgetDimension.iProjectKey,
                            iYear = budgetDimension.BudgetBase.iYear,
                            sPeriod = "1",
                            sComment = item.sComment
                        };
                        db.Budgets.Add(budget);
                    }
                    if (item.dFebruary != 0)
                    {
                        Budget budget = new Budget()
                        {
                            dcAmountBudget = item.dFebruary,
                            iCustomerID = budgetDimension.BudgetBase.iCustomerID,
                            iBudgetDimensionKey = budgetDimension.iBudgetDimensionKey,
                            sReportingCode = item.ReportingStructure.RcStart,
                            iPartnerID = partnerid,
                            iProjectKey = budgetDimension.iProjectKey,
                            iYear = budgetDimension.BudgetBase.iYear,
                            sPeriod = "2",
                            sComment = item.sComment
                        };
                        db.Budgets.Add(budget);
                    }
                    if (item.dMarch != 0)
                    {
                        Budget budget = new Budget()
                        {
                            dcAmountBudget = item.dMarch,
                            iCustomerID = budgetDimension.BudgetBase.iCustomerID,
                            iBudgetDimensionKey = budgetDimension.iBudgetDimensionKey,
                            sReportingCode = item.ReportingStructure.RcStart,
                            iPartnerID = partnerid,
                            iProjectKey = budgetDimension.iProjectKey,
                            iYear = budgetDimension.BudgetBase.iYear,
                            sPeriod = "3",
                            sComment = item.sComment
                        };
                        db.Budgets.Add(budget);
                    }
                    if (item.dApril != 0)
                    {
                        Budget budget = new Budget()
                        {
                            dcAmountBudget = item.dApril,
                            iCustomerID = budgetDimension.BudgetBase.iCustomerID,
                            iBudgetDimensionKey = budgetDimension.iBudgetDimensionKey,
                            sReportingCode = item.ReportingStructure.RcStart,
                            iPartnerID = partnerid,
                            iProjectKey = budgetDimension.iProjectKey,
                            iYear = budgetDimension.BudgetBase.iYear,
                            sPeriod = "4",
                            sComment = item.sComment
                        };
                        db.Budgets.Add(budget);
                    }
                    if (item.dMay != 0)
                    {
                        Budget budget = new Budget()
                        {
                            dcAmountBudget = item.dMay,
                            iCustomerID = budgetDimension.BudgetBase.iCustomerID,
                            iBudgetDimensionKey = budgetDimension.iBudgetDimensionKey,
                            sReportingCode = item.ReportingStructure.RcStart,
                            iPartnerID = partnerid,
                            iProjectKey = budgetDimension.iProjectKey,
                            iYear = budgetDimension.BudgetBase.iYear,
                            sPeriod = "5",
                            sComment = item.sComment
                        };
                        db.Budgets.Add(budget);
                    }
                    if (item.dJune != 0)
                    {
                        Budget budget = new Budget()
                        {
                            dcAmountBudget = item.dJune,
                            iCustomerID = budgetDimension.BudgetBase.iCustomerID,
                            iBudgetDimensionKey = budgetDimension.iBudgetDimensionKey,
                            sReportingCode = item.ReportingStructure.RcStart,
                            iPartnerID = partnerid,
                            iProjectKey = budgetDimension.iProjectKey,
                            iYear = budgetDimension.BudgetBase.iYear,
                            sPeriod = "6",
                            sComment = item.sComment
                        };
                        db.Budgets.Add(budget);
                    }
                    if (item.dJuly != 0)
                    {
                        Budget budget = new Budget()
                        {
                            dcAmountBudget = item.dJuly,
                            iCustomerID = budgetDimension.BudgetBase.iCustomerID,
                            iBudgetDimensionKey = budgetDimension.iBudgetDimensionKey,
                            sReportingCode = item.ReportingStructure.RcStart,
                            iPartnerID = partnerid,
                            iProjectKey = budgetDimension.iProjectKey,
                            iYear = budgetDimension.BudgetBase.iYear,
                            sPeriod = "7",
                            sComment = item.sComment
                        };
                        db.Budgets.Add(budget);
                    }
                    if (item.dAugust != 0)
                    {
                        Budget budget = new Budget()
                        {
                            dcAmountBudget = item.dAugust,
                            iCustomerID = budgetDimension.BudgetBase.iCustomerID,
                            iBudgetDimensionKey = budgetDimension.iBudgetDimensionKey,
                            sReportingCode = item.ReportingStructure.RcStart,
                            iPartnerID = partnerid,
                            iProjectKey = budgetDimension.iProjectKey,
                            iYear = budgetDimension.BudgetBase.iYear,
                            sPeriod = "8",
                            sComment = item.sComment
                        };
                        db.Budgets.Add(budget);
                    }
                    if (item.dSeptember != 0)
                    {
                        Budget budget = new Budget()
                        {
                            dcAmountBudget = item.dSeptember,
                            iCustomerID = budgetDimension.BudgetBase.iCustomerID,
                            iBudgetDimensionKey = budgetDimension.iBudgetDimensionKey,
                            sReportingCode = item.ReportingStructure.RcStart,
                            iPartnerID = partnerid,
                            iProjectKey = budgetDimension.iProjectKey,
                            iYear = budgetDimension.BudgetBase.iYear,
                            sPeriod = "9",
                            sComment = item.sComment
                        };
                        db.Budgets.Add(budget);
                    }
                    if (item.dOctober != 0)
                    {
                        Budget budget = new Budget()
                        {
                            dcAmountBudget = item.dOctober,
                            iCustomerID = budgetDimension.BudgetBase.iCustomerID,
                            iBudgetDimensionKey = budgetDimension.iBudgetDimensionKey,
                            sReportingCode = item.ReportingStructure.RcStart,
                            iPartnerID = partnerid,
                            iProjectKey = budgetDimension.iProjectKey,
                            iYear = budgetDimension.BudgetBase.iYear,
                            sPeriod = "10",
                            sComment = item.sComment
                        };
                        db.Budgets.Add(budget);
                    }
                    if (item.dNovember != 0)
                    {
                        Budget budget = new Budget()
                        {
                            dcAmountBudget = item.dNovember,
                            iCustomerID = budgetDimension.BudgetBase.iCustomerID,
                            iBudgetDimensionKey = budgetDimension.iBudgetDimensionKey,
                            sReportingCode = item.ReportingStructure.RcStart,
                            iPartnerID = partnerid,
                            iProjectKey = budgetDimension.iProjectKey,
                            iYear = budgetDimension.BudgetBase.iYear,
                            sPeriod = "11",
                            sComment = item.sComment
                        };
                        db.Budgets.Add(budget);
                    }
                    if (item.dDecember != 0)
                    {
                        Budget budget = new Budget()
                        {
                            dcAmountBudget = item.dDecember,
                            iCustomerID = budgetDimension.BudgetBase.iCustomerID,
                            iBudgetDimensionKey = budgetDimension.iBudgetDimensionKey,
                            sReportingCode = item.ReportingStructure.RcStart,
                            iPartnerID = partnerid,
                            iProjectKey = budgetDimension.iProjectKey,
                            iYear = budgetDimension.BudgetBase.iYear,
                            sPeriod = "12",
                            sComment = item.sComment
                        };
                        db.Budgets.Add(budget);
                    }

                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public bool RemoveBudget(int iBudgetDimensionKey)
        {
            try
            {
                BudgetDimension budgetDimension = db.BudgetDimensions
                                            .Include(i => i.BudgetDimensionRules)
                                            .Include(i => i.Budgets)
                                            .FirstOrDefault(f => f.iBudgetDimensionKey == iBudgetDimensionKey);

                db.Budgets.RemoveRange(budgetDimension.Budgets);
                db.SaveChanges();

                return true;

            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        private decimal? CalculateYearSuggestion(decimal? dTotal, BudgetSectionIndex section, bool projectHasFixedPart, int previewYear, int realisedYear)
        {
            decimal? result = 0;

            //if (!section.bSpatie && !section.bSubtotaal)
            if (!section.bSpatie)
            {
                if (section.bVariable)
                {
                    decimal? previewYearIndex = section.BudgetSetting.YearDegreeDayIndex.FirstOrDefault(f => f.iYear == previewYear).dDegreeDayIndex;

                    decimal? realisedYearIndex = section.BudgetSetting.YearDegreeDayIndex.FirstOrDefault(f => f.iYear == realisedYear).dDegreeDayIndex;

                    if (section.bFixedPart && projectHasFixedPart)
                    {
                        dTotal = ((1 - section.dFixedPart) * dTotal / previewYearIndex) * realisedYearIndex + section.dFixedPart * dTotal;
                    }
                    else
                    {

                        dTotal = (dTotal / previewYearIndex) * realisedYearIndex;
                    }

                }

                result = dTotal * section.dSectionIndex;
            }

            return result;
        }

        private decimal CalculateMonthSuggestion(decimal dSuggestion, BudgetSectionIndex section, bool projectHasFixedPart, int iMonthKey, IEnumerable<MonthDegreeDayIndex> monthDegreeDayIndex)
        {
            decimal result = 0;

            if (!section.bSpatie && !section.bSubtotaal)
            {
                if (section.bVariable)
                {
                    if (section.bFixedPart && projectHasFixedPart)
                    {
                        result =
                            section.dFixedPart *
                            dSuggestion /
                            12 +
                            (1 - section.dFixedPart) *
                            dSuggestion /
                            monthDegreeDayIndex.Sum(sm => sm.dDegreeDayIndex) *
                            monthDegreeDayIndex.Where(w => w.iMonthKey == iMonthKey).Sum(sm => sm.dDegreeDayIndex);

                    }
                    else
                    {
                        result =
                            (dSuggestion / monthDegreeDayIndex.Sum(sm => sm.dDegreeDayIndex)) *
                            monthDegreeDayIndex.Where(w => w.iMonthKey == iMonthKey).Sum(sm => sm.dDegreeDayIndex);
                    }
                }
                else
                {
                    result = dSuggestion / 12;
                }
            }

            return result;
        }

    }
}