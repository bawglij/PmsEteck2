using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PmsEteck.ViewModels
{
    //TM
    public class EnergySupplierIndexViewModel
    {
        [Display(Name = "Sleutel")]
        public int iEnergySupplierID { get; set; }

        [Display(Name = "Energieleverancier")]
        public string sName { get; set; }

        [Display(Name = "Actief")]
        public bool bActive { get; set; }

        public int iMeterCount { get; set; }
    }

    public class EnergySupplierCreateViewModel
    {
        [Required]
        [Display(Name = "Naam")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "{0} moet tussen de {2} en {1} tekens bevatten")]
        public string sName { get; set; }
    }

    public class EnergySupplierEditViewModel
    {
        public int iEnergySupplierID { get; set; }

        [Required]
        [Display(Name = "Naam")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "{0} moet tussen de {2} en {1} tekens bevatten")]
        public string sName { get; set; }

        [Display(Name = "Actief")]
        public bool bActive { get; set; }
    }
}
