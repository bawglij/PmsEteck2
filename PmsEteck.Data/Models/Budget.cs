using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("FactBudget", Schema ="dbo")]
    public class Budget
    {
        [Key]
        public long iFactBudgetKey { get; set; }

        public int? iBudgetDimensionKey { get; set; }

        public int? iBudgetVersionKey { get; set; }

        public int? iPartnerID { get; set; }

        public int? iCustomerID { get; set; }

        public int? iLedgerKey { get; set; }

        public int? iProjectKey { get; set; }

        public int? iUserKey { get; set; }

        public int? iYear { get; set; }

        [MaxLength(10)]
        public string sPeriod { get; set; }

        public int? sReportingCode { get; set; }

        public int? sReportingCode_old { get; set; }

        public decimal? dcAmountBudget { get; set; }

        [MaxLength(255)]
        public string sComment { get; set; }

        public ProjectBase ProjectBase { get; set; }

        public BudgetDimension BudgetDimension { get; set; }

    }
}