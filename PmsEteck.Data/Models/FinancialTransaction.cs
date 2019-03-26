using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("FactFinancialTransactions", Schema ="dbo")]
    public class FinancialTransaction
    {
        [Key]
        public int iFinancialTransactionKey { get; set; }

        public int iPartnerID { get; set; }

        public int iCustomerID { get; set; }

        [MaxLength(50)]
        public string sIncoiceNumber { get; set; }

        [MaxLength(20)]
        public string sEntryNumber { get; set; }

        public int? iRelationKey { get; set; }

        public int? iVATKey { get; set; }

        [MaxLength(15)]
        public string sVATCode { get; set; }

        public int? iJournalKey { get; set; }

        public int? iLedgerKey { get; set; }

        [MaxLength(255)]
        public string sDesc { get; set; }

        public int? iCostUnitKey { get; set; }

        public int? iProjectKey { get; set; }

        [MaxLength(10)]
        public string sPeriod { get; set; }

        public int? iTDDateKey { get; set; }

        public int? iEDDateKey { get; set; }

        public int? iCurrencyKey { get; set; }

        public int? iDiscount { get; set; }

        public decimal? dcAmountExVat { get; set; }

        public decimal? dcAmountInclVat { get; set; }

        public decimal? dcAmountVat { get; set; }

        public int? iCountRecord { get; set; }

        public int iSUFKey { get; set; }

        public int? iUserKey { get; set; }
    }
}