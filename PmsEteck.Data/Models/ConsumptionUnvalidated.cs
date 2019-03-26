using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("ConsumptionUnvalidated", Schema = "meter")]
    public class ConsumptionUnvalidated
    {
        [Key]
        public int iConsumptionUnvalidatedID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Adres")]
        public int iAddressKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Telwerk")]
        public int iCounterKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Servicerun")]
        public int iServiceRunKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Invoerdatum")]
        public DateTime dtImportDateTime { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Datum opname")]
        [DataType(DataType.Date)]
        public DateTime dtReadingDate { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Stand")]
        public decimal dPosition { get; set; }

        [Display(Name = "Gevalideerd")]
        public bool bValidated { get; set; }

        [ForeignKey("ValidatedByUser")]
        [Display(Name = "Gevalideerd door")]
        public string sValidatedBy { get; set; }

        public bool Deleted { get; set; }

        public virtual Counter Counter { get; set; }

        public virtual Address Address { get; set; }
        [NotMapped]
        public virtual ApplicationUser ValidatedByUser { get; set; }

        public virtual ServiceRun ServiceRun { get; set; }
    }
}
