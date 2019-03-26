using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("PaymentTerms", Schema = "invoice")]
    public class PaymentTerm
    {
        [Key]
        public int iPaymentTermID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Code")]
        [MaxLength(20, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sCode { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Omschrijving")]
        [MaxLength(20, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sDescription { get; set; }

        [Display(Name = "Debiteuren")]
        public ICollection<Debtor> Debtors { get; set; }

        public ICollection<PaymentTermHistory> PaymentTermHistories { get; set; }
    }
}