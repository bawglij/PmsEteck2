using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "MeterChangeLogs", Schema = "meter")]
    public class MeterChangeLog
    {
        [Key]
        public int iMeterChangeKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Meter")]
        public int iConsumptionMeterKey { get; set; }

        [ForeignKey("FromAddress")]
        [Display(Name = "Verwijderd van")]
        public int? iAddressKeyFrom { get; set; }

        [ForeignKey("ToAddress")]
        [Display(Name = "Toegevoegd aan")]
        public int? iAddressKeyTo { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Gebeurtenis")]
        public int iEventKey { get; set; }

        public int? iChangeReasonKey { get; set; }

        public int? iExchangeFormKey { get; set; }

        [Display(Name = "Overige opmerkingen")]
        [MaxLength(500)]
        public string sNotes { get; set; }
        
        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Wisseldatum")]
        public DateTime dtEffectiveDateTime { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Event aangemaakt")]
        public DateTime dtEventCreated { get; set; }

        //[ForeignKey("ApplicationUser")]
        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Gewijzigd door")]
        public string sUser { get; set; }

        public virtual Event Event { get; set; }

        public virtual ExchangeForm ExchangeForm { get; set; }

        public virtual ChangeReason ChangeReason { get; set; }

        public virtual ConsumptionMeter ConsumptionMeter { get; set; }

        public virtual Address FromAddress { get; set; }

        public virtual Address ToAddress { get; set; }

        //public virtual ApplicationUser ApplicationUser { get; set; }
    }
}