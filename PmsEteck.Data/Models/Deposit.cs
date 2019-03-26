using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Deposits", Schema = "invoice")]
    public class Deposit
    {
        [Key]
        public int iDepositID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Adresdebiteur")]
        public int iAddressDebtorID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Voorschotbedrag")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dAmount { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Voorschotbedrag (excl BTW)")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal dAmountexVAT { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Startdatum")]
        [DataType(DataType.Date)]
        public DateTime dtStartDate { get; set; }

        [Display(Name = "Einddatum")]
        [DataType(DataType.Date)]
        public DateTime? dtEndDate { get; set; }

        [Display(Name = "Actief")]
        public bool bIsActive { get; set; }

        public bool bDecreasedByDebtor { get; set; }

        [Display(Name = "Adresdebiteur")]
        public AddressDebtor AddressDebtor { get; set; }
    }
}
