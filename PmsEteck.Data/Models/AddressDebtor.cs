using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("AddressDebtors", Schema = "invoice")]
    public class AddressDebtor
    {
        [Key]
        public int iAddressDebtorID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Adres")]
        [Index("IX_AddressDebtorUnique", 1, IsUnique = true)]
        [ForeignKey("Address")]
        public int iAddressID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Debiteur")]
        [Index("IX_AddressDebtorUnique", 2, IsUnique = true)]
        public int iDebtorID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Startdatum")]
        [Index("IX_AddressDebtorUnique", 3, IsUnique = true)]
        [DataType(DataType.Date)]
        public DateTime dtStartDate { get; set; }

        [Display(Name = "Einddatum")]
        [DataType(DataType.Date)]
        public DateTime? dtEndDate { get; set; }

        [Display(Name = "Actieve regel")]
        public bool bIsActive { get; set; }

        [Display(Name = "Volledig afgerond")]
        public bool bFinished { get; set; }

        [Display(Name = "Type facturatie")]
        public int? BillingTypeID { get; set; }

        [Display(Name = "Adres")]
        public virtual Address Address { get; set; }

        [Display(Name = "Debiteur")]
        public virtual Debtor Debtor { get; set; }

        [Display(Name = "Type facturatie")]
        public virtual BillingType BillingType { get; set; }

        [Display(Name = "Voorschot")]
        public ICollection<Deposit> Deposits { get; set; }

    }
}