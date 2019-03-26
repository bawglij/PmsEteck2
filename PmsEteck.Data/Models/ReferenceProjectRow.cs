using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("ReferenceProjectRows")]
    public class ReferenceProjectRow
    {
        [Key]
        public int iReferenceProjectRowKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Referentieproject")]
        public int iReferenceProjectKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Telwerktype")]
        public int iCounterTypeKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(2, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} mag alleen getallen bevatten")]
        [Display(Name = "Aansluittype")]
        public string sConnectionTypeKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Distriubtie rendement")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:P2}")]
        public decimal dDistributionEfficiency { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Opwekkings rendement")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:P2}")]
        public decimal dGenerationEfficiency { get; set; }
        
        [Display(Name = "Aansluittype")]
        public ConnectionType ConnectionType { get; set; }

        [Display(Name = "Telwerktype")]
        public CounterType CounterType { get; set; }

        [Display(Name = "Referentieproject")]
        public ReferenceProject ReferenceProject { get; set; }

    }
}