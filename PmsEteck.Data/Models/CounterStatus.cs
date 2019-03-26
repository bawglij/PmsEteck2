using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "CounterStatus", Schema = "meter")]
    public class OldCounterStatus
    {

        [Key]
        public int iCounterStatusKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Telwerk")]
        public int iCounterKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Datum")]
        public DateTime dtDateTime { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Melding")]
        [MaxLength(250)]
        public string sMessage { get; set; }

        [Display(Name = "Heeft een fout")]
        public bool bHasError { get; set; }

        [Display(Name = "Heeft geen verbruik")]
        public bool bHasNoConsumption { get; set; }

        [Display(Name = "Heeft geen gekoppelde tariefkaarten")]
        public bool bHasNoRateCard { get; set; }

        [Display(Name = "Foutmelding in servicerun")]
        public bool bLastServiceRunHasError { get; set; }
        
        [Display(Name = "Telwerk")]
        public Counter Counter { get; set; }

    }

    public class CounterStatus : Status
    {
        public List<Counter> Counters { get; set; }

    }
}