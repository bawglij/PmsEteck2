using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("RateCardScaleHistories", Schema = "meter" )]
    public class RateCardScaleHistory
    {
        [Key]
        public int RateCardScaleHistoryID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Debiteur")]
        [ForeignKey("Debtor")]
        public int DebtorID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Aansluiting")]
        [ForeignKey("Address")]
        public int AddressID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Tariefkaartregel")]
        [ForeignKey("RateCardRow")]
        public int RateCardRowID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Verbruikt")]
        public decimal Consumed { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Periode")]
        public DateTime Period { get; set; }

        [Display(Name = "Debiteur")]
        public virtual Debtor Debtor { get; set; }

        [Display(Name = "Aansluiting")]
        public virtual Address Address { get; set; }

        [Display(Name = "Tariefkaartregel")]
        public virtual RateCardRow RateCardRow { get; set; }
    }
}