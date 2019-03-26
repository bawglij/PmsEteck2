using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using PmsEteck.Data.Models.Results;

namespace PmsEteck.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using PmsEteck.Data.Extensions;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    //using WSGenJournal;

    [Table(name: "Reports", Schema = "meter")]
    public class Report
    {
        #region Constructor
        private PmsEteckContext db = new PmsEteckContext();
        //WSGenJournal_Service _service;
        #endregion

        #region Static Fields
        private string WebserviceUser = ConfigurationManager.AppSettings["WebserviceUser"];
        private string WebserviceDomain = ConfigurationManager.AppSettings["WebserviceDomain"];
        private string WebservicePassword = ConfigurationManager.AppSettings["WebservicePassword"];
        private string ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        private string IncassoBV = ConfigurationManager.AppSettings["IncassoBV"];
        #endregion

        #region Properties
        [Key]
        public int iReportKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Project")]
        public int iProjectKey { get; set; }

        [Display(Name = "Is Concept")]
        public bool bConcept { get; set; }

        [Display(Name = "Geboekt")]
        public bool bBooked { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Rapportageperiode")]
        public int iReportPeriodKey { get; set; }

        [Display(Name = "Opmerking bij validatie")]
        [MaxLength(500, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sValidationNote { get; set; }

        [Display(Name = "Notitie")]
        [MaxLength(500, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sReportNote { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Aangemaakt door")]
        [MaxLength(128, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sCreatedBy { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Laatst gewijzigd door")]
        [MaxLength(128, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sLastEditedBy { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Datum gemaakt")]
        public DateTime dtDateTimeCreated { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Datum laatste wijziging")]
        public DateTime dtDateTimeLastEdited { get; set; }

        [Display(Name = "Validatieprofiel")]
        public int? ReportValidationSetID { get; set; }

        [Display(Name = "Rapportage op basis van budget")]
        public bool ReportOnBudget { get; set; }

        [Display(Name = "Automatisch gerapporteerd")]
        public bool AutomaticReport { get; set; }

        [Display(Name = "Project")]
        public virtual ProjectInfo Project { get; set; }

        [Display(Name = "Rapportageperiode")]
        public virtual ReportPeriod ReportPeriod { get; set; }

        [Display(Name = "Validatieprofiel")]
        public virtual ReportValidationSet ReportValidationSet { get; set; }

        [Display(Name = "Rapportageregels")]
        public List<ReportRow> ReportRows { get; set; }

        [Display(Name = "Validatieregels")]
        public List<ReportValidation> ReportValidations { get; set; }
        #endregion

        #region Methods
        public async Task<ReportResult> CreateReportAsync()
        {
            Project = await db.ProjectInfo.FindAsync(iProjectKey);
            ReportPeriod = await db.ReportPeriods.FindAsync(iReportPeriodKey);

            Task<ReportResult> reportRowsCreateTask = GetReportRowValues();
            Task<ReportResult> reportValidationTask = GetReportValidations();

            await Task.WhenAll(reportRowsCreateTask, reportValidationTask);
            if (reportRowsCreateTask.Result.Succeeded && reportValidationTask.Result.Succeeded) { 
                db.Reports.Add(this);
                await db.SaveChangesAsync();
                await db.Entry(this).GetDatabaseValuesAsync();
                return await Task.FromResult(ReportResult.Success);
            }
            List<string> errorList = reportRowsCreateTask.Result.Errors.ToList();
            errorList.AddRange(reportValidationTask.Result.Errors);
            return await Task.FromResult(ReportResult.Failed(errorList.ToArray()));
        }
        
        private async Task<ReportResult> AddReportRowsAsync(List<ReportRow> reportRows)
        {
            // Get all Rubrics
            try
            {
                List<Rubric> rubricList = await db.Rubrics.OrderBy(o => o.iRowNo).ToListAsync();
                foreach (Rubric rubric in rubricList)
                {
                    ReportRow reportRow = new ReportRow();
                    if (reportRows.Any(u => u.iRubricKey == rubric.iRubricKey))
                    {
                        reportRow = reportRows.FirstOrDefault(f => f.iRubricKey == rubric.iRubricKey);
                        reportRow.dCumReportAmount = db.Reports.Where(w => w.iProjectKey == iProjectKey && w.ReportPeriod.iYear == ReportPeriod.iYear && w.ReportPeriod.iPeriod < ReportPeriod.iPeriod).Count() > 0 ? db.Reports.Where(w => w.iProjectKey == iProjectKey && w.ReportPeriod.iYear == ReportPeriod.iYear && w.ReportPeriod.iPeriod < ReportPeriod.iPeriod).Sum(sm => sm.ReportRows.Where(w => w.iRubricKey == rubric.iRubricKey).Sum(s => s.dOriginalAmount + s.dMutationAmount)) : 0;
                        reportRow.dCumReportAmount += reportRow.dOriginalAmount;
                    }
                    else
                    {
                        if (rubric.bTotal)
                        {
                            reportRow = new ReportRow
                            {
                                dBudgetAmount = rubric.iRubricGroupKey == 3 ? ReportRows.Where(w => !w.Rubric.bTotal && w.Rubric.iRubricGroupKey == 2 && w.Rubric.iRowNoStart >= rubric.iRowNoStart && w.Rubric.iRowNoEnd <= rubric.iRowNoEnd).Sum(s => s.dBudgetAmount) - ReportRows.Where(w => !w.Rubric.bTotal && w.Rubric.iRubricGroupKey == 1 && w.Rubric.iRowNoStart >= rubric.iRowNoStart && w.Rubric.iRowNoEnd <= rubric.iRowNoEnd).Sum(s => s.dBudgetAmount)
                                    : ReportRows.Where(w => !w.Rubric.bTotal && w.Rubric.iRowNoStart >= rubric.iRowNoStart && w.Rubric.iRowNoEnd <= rubric.iRowNoEnd).Sum(s => s.dBudgetAmount),
                                dCumBudgetAmount = rubric.iRubricGroupKey == 3 ? ReportRows.Where(w => !w.Rubric.bTotal && w.Rubric.iRubricGroupKey == 2 && w.Rubric.iRowNoStart >= rubric.iRowNoStart && w.Rubric.iRowNoEnd <= rubric.iRowNoEnd).Sum(s => s.dCumBudgetAmount) - ReportRows.Where(w => !w.Rubric.bTotal && w.Rubric.iRubricGroupKey == 1 && w.Rubric.iRowNoStart >= rubric.iRowNoStart && w.Rubric.iRowNoEnd <= rubric.iRowNoEnd).Sum(s => s.dCumBudgetAmount)
                                    : ReportRows.Where(w => !w.Rubric.bTotal && w.Rubric.iRowNoStart >= rubric.iRowNoStart && w.Rubric.iRowNoEnd <= rubric.iRowNoEnd).Sum(s => s.dCumBudgetAmount),
                                dCumCalculateAmount = rubric.iRubricGroupKey == 3 ?
                                        ReportRows.Where(w => !w.Rubric.bTotal && w.Rubric.iRubricGroupKey == 2 && w.Rubric.iRowNoStart >= rubric.iRowNoStart && w.Rubric.iRowNoEnd <= rubric.iRowNoEnd).Sum(s => s.dCumCalculateAmount) - ReportRows.Where(w => !w.Rubric.bTotal && w.Rubric.iRubricGroupKey == 1 && w.Rubric.iRowNoStart >= rubric.iRowNoStart && w.Rubric.iRowNoEnd <= rubric.iRowNoEnd).Sum(s => s.dCumCalculateAmount)
                                    : ReportRows.Where(w => !w.Rubric.bTotal && w.Rubric.iRowNoStart >= rubric.iRowNoStart && w.Rubric.iRowNoEnd <= rubric.iRowNoEnd).Sum(s => s.dCumCalculateAmount),
                                dCumReportAmount = rubric.iRubricGroupKey == 3 ?
                                        ReportRows.Where(w => !w.Rubric.bTotal && w.Rubric.iRubricGroupKey == 2 && w.Rubric.iRowNoStart >= rubric.iRowNoStart && w.Rubric.iRowNoEnd <= rubric.iRowNoEnd).Sum(s => s.dCumReportAmount) - ReportRows.Where(w => !w.Rubric.bTotal && w.Rubric.iRubricGroupKey == 1 && w.Rubric.iRowNoStart >= rubric.iRowNoStart && w.Rubric.iRowNoEnd <= rubric.iRowNoEnd).Sum(s => s.dCumReportAmount)
                                    : ReportRows.Where(w => !w.Rubric.bTotal && w.Rubric.iRowNoStart >= rubric.iRowNoStart && w.Rubric.iRowNoEnd <= rubric.iRowNoEnd).Sum(s => s.dCumReportAmount),
                                dMutationAmount = 0,
                                dOriginalAmount = rubric.iRubricGroupKey == 3 ?
                                        ReportRows.Where(w => !w.Rubric.bTotal && w.Rubric.iRubricGroupKey == 2 && w.Rubric.iRowNoStart >= rubric.iRowNoStart && w.Rubric.iRowNoEnd <= rubric.iRowNoEnd).Sum(s => s.dOriginalAmount) - ReportRows.Where(w => !w.Rubric.bTotal && w.Rubric.iRubricGroupKey == 1 && w.Rubric.iRowNoStart >= rubric.iRowNoStart && w.Rubric.iRowNoEnd <= rubric.iRowNoEnd).Sum(s => s.dOriginalAmount)
                                    : ReportRows.Where(w => !w.Rubric.bTotal && w.Rubric.iRowNoStart >= rubric.iRowNoStart && w.Rubric.iRowNoEnd <= rubric.iRowNoEnd).Sum(s => s.dOriginalAmount),
                                iRubricKey = rubric.iRubricKey,
                                Rubric = rubric
                            };
                        }
                        else
                        {
                            reportRow = new ReportRow
                            {
                                dBudgetAmount = 0,
                                dCumBudgetAmount = 0,
                                dCumCalculateAmount = 0,
                                dCumReportAmount = db.Reports.Where(w => w.iProjectKey == iProjectKey && w.ReportPeriod.iYear == ReportPeriod.iYear && w.ReportPeriod.iPeriod < ReportPeriod.iPeriod).Count() > 0 ? db.Reports.Where(w => w.iProjectKey == iProjectKey && w.ReportPeriod.iYear == ReportPeriod.iYear && w.ReportPeriod.iPeriod < ReportPeriod.iPeriod).Sum(sm => sm.ReportRows.Where(w => w.iRubricKey == rubric.iRubricKey).Sum(s => s.dOriginalAmount + s.dMutationAmount)) : 0,
                                dMutationAmount = 0,
                                dOriginalAmount = 0,
                                iRubricKey = rubric.iRubricKey,
                                Rubric = rubric
                            };
                        }
                    }
                    ReportRows.Add(reportRow);
                }
                return await Task.FromResult(ReportResult.Success);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ReportResult.Failed(ex.Message));
            }
        }

        private async Task<ReportResult> GetReportRowValues()
        {
            List<ReportRow> EMReportRows = new List<ReportRow>();
            try
            {
                using (PmsEteckContext _context = new PmsEteckContext())
                {
                    _context.Database.SetCommandTimeout(300);
                    _context.Database.OpenConnection();
                    using (DbCommand _command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        _command.CommandTimeout = 300;
                        _command.CommandText = "meter.GetEMReportByProjectPeriod";
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.Add(new SqlParameter("@parProject", iProjectKey));
                        _command.Parameters.Add(new SqlParameter("@parReportEndDate", ReportPeriod.dtPeriodDate.LastDayOfMonth()));

                        using (DbDataReader reader = _command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EMReportRows.Add(new ReportRow {
                                    iRubricKey = int.Parse(reader["TariefkaartRegelRubriekKey"].ToString()),
                                    dBudgetAmount = string.IsNullOrEmpty(reader["dBudgetAmount"].ToString()) ? 0 : decimal.Parse(reader["dBudgetAmount"].ToString()),
                                    dCumBudgetAmount = string.IsNullOrEmpty(reader["dCumBudgetAmount"].ToString()) ? 0 : decimal.Parse(reader["dCumBudgetAmount"].ToString()),
                                    dCumCalculateAmount = string.IsNullOrEmpty(reader["dCumCalculateAmount"].ToString()) ? 0 : decimal.Parse(reader["dCumCalculateAmount"].ToString()),
                                    dCumReportAmount = string.IsNullOrEmpty(reader["dCumReportAmount"].ToString()) ? 0 : decimal.Parse(reader["dCumReportAmount"].ToString()),
                                    dOriginalAmount = string.IsNullOrEmpty(reader["dOriginalAmount"].ToString()) ? 0 : decimal.Parse(reader["dOriginalAmount"].ToString()),
                                    dMutationAmount = string.IsNullOrEmpty(reader["dMutationAmount"].ToString()) ? 0 : decimal.Parse(reader["dMutationAmount"].ToString()),
                                    Rubric = db.Rubrics.Find(int.Parse(reader["TariefkaartRegelRubriekKey"].ToString()))
                                });
                            }
                        }
                    }
                }

                await AddReportRowsAsync(EMReportRows);
                return await Task.FromResult(ReportResult.Success);
            }
            catch (SqlException ex)
            {
                return await Task.FromResult(ReportResult.Failed("SQL Error: " + ex.Message + " DB: " + db.Database.GetDbConnection().ToString()));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ReportResult.Failed("Error: " + ex.Message));
            }
        }

        private async Task<ReportResult> GetReportValidations()
        {
            List<ReportValidation> reportValidationList = new List<ReportValidation>();
            try
            {
                DataTable EMDataTable = new DataTable();

                using (PmsEteckContext _context = new PmsEteckContext())
                {
                    _context.Database.SetCommandTimeout(300);
                    _context.Database.OpenConnection();
                    using (DbCommand _command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        _command.CommandTimeout = 300;
                        _command.CommandText = "meter.GetEMReportValidationsByProjectPeriod";
                        _command.CommandType = CommandType.StoredProcedure;
                        _command.Parameters.Add(new SqlParameter("@parProject", iProjectKey));
                        _command.Parameters.Add(new SqlParameter("@parReportEndDate", ReportPeriod.dtPeriodDate.LastDayOfMonth()));

                        using (DbDataReader reader = _command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!string.IsNullOrEmpty(reader["sValidationMessage"].ToString()))
                                {
                                    ReportValidations.Add(new ReportValidation
                                    {
                                        iStatusCode = int.Parse(reader["iStatusCode"].ToString()),
                                        sValidationMessage = reader["sValidationMessage"].ToString()
                                    });
                                }
                            }
                        }
                    }
                }
                return await Task.FromResult(ReportResult.Success);
            }
            catch (SqlException ex)
            {
                return await Task.FromResult(ReportResult.Failed("SQL Error: " + ex.Message + " DB: " + db.Database.GetDbConnection().ToString()));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ReportResult.Failed("Error: " + ex.Message));
            }
        }
        /*
        public ReportResult AddToNav(int reportID)
        {
            Report report = db.Reports
                .Include(i => i.Project)
                .Include(i => i.ReportPeriod)
                .Include(i => i.ReportRows)
                .FirstOrDefault(f => f.iReportKey == reportID);
            if (report != null)
            {
                ProjectBase finProject = db.ProjectBases
                    .Include(i => i.Customer)
                    .FirstOrDefault(f => f.iProjectKey == report.Project.iFinProjectKey);
                if (finProject != null)
                {
                    DateTime rapportageDatum = new DateTime(report.ReportPeriod.iYear, report.ReportPeriod.iPeriod, DateTime.DaysInMonth(report.ReportPeriod.iYear, report.ReportPeriod.iPeriod));
                    try
                    {
                        _service = new WSGenJournal_Service
                        {
                            Credentials = new NetworkCredential(WebserviceUser, WebservicePassword, WebserviceDomain),
                            Url = string.Format("{0}/WS/{1}/Page/WSGenJournal", ServiceUrl, Uri.EscapeDataString(finProject.Customer.NavisionPrefix))
                        };
                        List<WSGenJournal> wsGenJournalList = new List<WSGenJournal>();
                        foreach (ReportRow reportRow in report.ReportRows.Where(w => !w.Rubric.bTotal && (w.dOriginalAmount + w.dMutationAmount) != 0))
                        {
                            wsGenJournalList.Add(new WSGenJournal
                            {
                                Account_No = reportRow.Rubric.iAccountNumber.ToString(),
                                Amount = reportRow.Rubric.iRubricGroupKey == 1 ? (reportRow.dOriginalAmount + reportRow.dMutationAmount) * -1 : reportRow.dOriginalAmount + reportRow.dMutationAmount,
                                AmountSpecified = true,
                                Bal_Account_No = reportRow.Rubric.iCounterAccountNumber.ToString(),
                                Description = string.Format("{0}-EM Rapportage {1}", finProject.sProjectCode, rapportageDatum.ToString("MMMM yyyy")),
                                Document_No = string.Format("MEMO-{0}-EM{1}", finProject.Customer.sClientcode, report.ReportPeriod.iPeriod.ToString("000")),
                                Shortcut_Dimension_1_Code = finProject.sProjectCode,
                                Immediate_Posting = true,
                                Immediate_PostingSpecified = true,
                                Journal_Batch_Name = "MEMORIAAL",
                                Journal_Template_Name = "MEMORIAAL",
                                Line_No = reportRow.iReportRowKey,
                                Line_NoSpecified = true,
                                Posting_Date = rapportageDatum,
                                Posting_DateSpecified = true
                            });
                        }
                        var wsGenJournalArray = wsGenJournalList.ToArray();
                        _service.CreateMultiple(ref wsGenJournalArray);

                        return ReportResult.Success;
                    }
                    catch (Exception e)
                    {
                        return ReportResult.Failed(e.Message);
                    }
                }
                else
                {
                    return ReportResult.Failed("Financiële project kan niet gevonden worden.");
                }
            }
            else
            {
                return ReportResult.Failed("Rapportage kan niet gevonden worden.");
            }

        }
        */
        #endregion
    }
}