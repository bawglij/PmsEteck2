using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("CO2ConstantRows")]
    public class CO2ConstantRow
    {
        [Key]
        public int iCO2ConstantRowKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Set")]
        public int iCO2ConstantKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(100, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Naam")]
        public string sCO2ConstantRowName { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Waarde")]
        public decimal dCO2ConstantRowValue { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(30, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Eenheid")]
        public string sCO2ConstantRowUnit { get; set; }

        [Display(Name = "Bron")]
        [MaxLength(150, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        public string sCO2ConstantRowSource { get; set; }
        
        [Display(Name = "Set")]
        public CO2Constant CO2Constant { get; set; }

    }
}