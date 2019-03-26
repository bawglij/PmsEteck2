using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("NoInvoicePeriods", Schema = "invoice")]
    public class NoInvoicePeriods : BaseModel
    {
        public NoInvoicePeriods(int debtorId, DateTime blockedPeriod)
        {
            if (blockedPeriod == DateTime.MinValue) throw new ArgumentException(nameof(blockedPeriod));

            Id = Guid.NewGuid();
            DebtorId = debtorId;
            BlockedPeriod = blockedPeriod;
            DateCreated = DateTime.UtcNow;
        }

        private NoInvoicePeriods() { }

        [Required(ErrorMessage = "{0} is verplicht")]
        [ForeignKey("Debtor")]
        [Display(Name = "Debiteur")]
        public int DebtorId { get; set; }

        [Display(Name = "Geblokkeerde periode")]
        public DateTime BlockedPeriod { get; set; }

        [Display(Name = "Debiteur")]
        public virtual Debtor Debtor { get; set; }
    }
}