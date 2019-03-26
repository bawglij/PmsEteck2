using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "PaymentHistory", Schema = "invoice")]
    public class PaymentHistory
    {
        [Key]
        public int iPaymentHistoryID { get; set; }

        [Display(Name = "Debiteur")]
        public int iDebtorID { get; set; }

        [Display(Name = "Aansluitadres")]
        [ForeignKey("Address")]
        public int? iAddressID { get; set; }

        [Display(Name = "Factuurnummer")]
        [StringLength(20, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sInvoiceNumber { get; set; }

        [Display(Name = "Omschrijving")]
        [StringLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sDescription { get; set; }
        
        [Display(Name = "Factuurdatum")]
        public DateTime dtInvoiceDate { get; set; }

        [Display(Name = "Vervaldatum")]
        public DateTime dtExpirationDate { get; set; }

        [Display(Name = "Betaaldatum")]
        public DateTime? dtPaymentDate { get; set; }

        [Display(Name = "Factuurbedrag (inclusief btw)")]
        public decimal dAmountinVAT { get; set; }

        [Display(Name = "Open saldo (inclusief btw)")]
        public decimal dOpenAmountinVAT { get; set; }

        [Display(Name = "Bekijk factuur")]
        public string sInvoiceLink { get; set; }

        [Display(Name = "Status")]
        public int? iInvoiceStatus { get; set; }

        [Display(Name = "Statusomschrijving")]
        [StringLength(150, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sInvoiceStatus { get; set; }

        [Display(Name = "Factuur periode")]
        [StringLength(10, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string InvoicePeriod { get; set; }

        [Display(Name = "Factuur")]
        [ForeignKey("Invoice")]
        public int? InvoiceID { get; set; }

        [Display(Name = "Aansluitadres")]
        public virtual Address Address { get; set; }

        [Display(Name = "Debiteur")]
        public virtual Debtor Debtor { get; set; }

        [Display(Name = "Factuur")]
        public virtual Invoice Invoice { get; set; }
    }
}
