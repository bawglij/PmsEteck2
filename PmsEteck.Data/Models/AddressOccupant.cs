using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("AddressOccupants", Schema = "invoice")]
    public class AddressOccupant
    {
        [Key]
        public int iAdressOccupantID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Adres")]
        [Index("IX_AddressOccupantUnique", 1, IsUnique = true)]
        [ForeignKey("Address")]
        public int iAddressID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Bewoner")]
        [Index("IX_AddressOccupantUnique", 2, IsUnique = true)]
        public int iOccupantID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Startdatum")]
        [Index("IX_AddressOccupantUnique", 3, IsUnique = true)]
        [DataType(DataType.Date)]
        public DateTime dtStartDate { get; set; }

        [Display(Name = "Einddatum")]
        [DataType(DataType.Date)]
        public DateTime? dtEndDate { get; set; }

        [Display(Name = "Actief")]
        public bool bIsActive { get; set; }

        [Display(Name = "Adres")]
        public virtual Address Address { get; set; }

        [Display(Name = "Bewoner")]
        public virtual Occupant Occupant { get; set; }
    }
}