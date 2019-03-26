using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("CO2Constants")]
    public class CO2Constant
    {
        [Key]
        public int iCO2ConstantKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Aangemaakt op")]
        public DateTime dtCreateDateTime { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Aangemaakt door")]
        public string sCreatedBy { get; set; }

        [Display(Name = "Regels")]
        public List<CO2ConstantRow> CO2ConstantRows { get; set; }
    }
}