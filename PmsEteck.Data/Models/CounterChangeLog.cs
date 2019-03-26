using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "CounterChangeLogs", Schema = "meter")]
    public class CounterChangeLog
    {
        [Key]
        public int iCounterChangeLogKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Telwerk")]
        public int iCounterKey { get; set; }

        [Display(Name = "Meterwijziging")]
        public int? iMeterChangeKey { get; set; }

        [ForeignKey("FromConsumptionMeter")]
        [Display(Name = "Verwijderd van")]
        public int? iConsumptionMeterKeyFrom { get; set; }

        [ForeignKey("ToConsumptionMeter")]
        [Display(Name = "Toegevoegd aan")]
        public int? iConsumptionMeterKeyTo { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Gebeurtenis")]
        public int iEventKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Eindstand")]
        public decimal dEndPosition { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Wisseldatum")]
        public DateTime dtEffectiveDateTime { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Event aangemaakt")]
        public DateTime dtEventCreated { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Gewijzigd door")]
        public string sUser { get; set; }

        [Display(Name = "Wisselformulier")]
        public int? iExchangeFormKey { get; set; }

        public virtual Counter Counter { get; set; }

        public virtual Event Event { get; set; }

        public ExchangeForm ExchangeForm { get; set; }

        public virtual ConsumptionMeter FromConsumptionMeter { get; set; }

        public virtual ConsumptionMeter ToConsumptionMeter { get; set; }

        public virtual MeterChangeLog MeterChange { get; set; }
        
    }
}