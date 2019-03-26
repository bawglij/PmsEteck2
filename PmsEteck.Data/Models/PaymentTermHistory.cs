using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("PaymentTermHistory", Schema = "invoice")]
    public class PaymentTermHistory
    {
        [Key]
        public int iPaymentTermHistoryID { get; set; }

        public int iPaymentTermID { get; set; }

        public int iDebtorID { get; set; }

        [ForeignKey("User")]
        public string sUserID { get; set; }

        public DateTime dtStartDate { get; set; }

        public DateTime? dtEndDate { get; set; }

        public virtual PaymentTerm PaymentTerm { get; set; }

        public virtual Debtor Debtor { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}