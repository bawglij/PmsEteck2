using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("DimLedger", Schema="dbo")]
    public class Ledger
    {
        [Key]
        public int iLedgerKey { get; set; }

        public int? iPartnerID { get; set; }

        public int? iCustomerID { get; set; }

        [MaxLength(40)]
        public string sLedgerNumber { get; set; }

        [MaxLength(60)]
        public string sLedgerName { get; set; }

        [MaxLength(20)]
        public string sLedgerType { get; set; }

        [MaxLength(40)]
        public string sAccountCategoryCode { get; set; }

        [MaxLength(100)]
        public string sAccountCategory { get; set; }

        public int? sReportingCode { get; set; }

        public int? sReportingCode_old { get; set; }

        public int? iUserKey { get; set; }
    }
}