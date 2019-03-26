using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "CounterTypes", Schema = "meter")]
    public class CounterType
    {
        [Key]
        [Display(Name = "Telwerktype")]
        public int iCounterTypeKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(100, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Omschrijving")]
        public string sCounterTypeDescription { get; set; }

        [Display(Name = "Telwerktype actief?")]
        public bool bActive { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Column(Order = 1 )]
        [Display(Name = "Sortering")]
        public int iOrder { get; set; }

        [Display(Name = "Kan gewisseld worden")]
        public bool bCanExchange { get; set; }
        
        public IEnumerable<Counter> Counters { get; set; }
        
    }
}