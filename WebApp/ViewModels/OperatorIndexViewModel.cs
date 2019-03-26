using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class OperatorIndexViewModel
    {
        [Display(Name = "Sleutel")]
        public int iOperatorID { get; set; }

        [Display(Name = "Netbeheerder")]
        public string sName { get; set; }

        [Display(Name = "Actief")]
        public bool bActive { get; set; }

        [Display(Name = "Aantal meters")]
        public int iMeterCount { get; set; }
    }

    public class OperatorEditViewModel
    {
        public int iOperatorID { get; set; }

        [Required]
        [Display(Name = "Naam")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "{0} moet tussen de {2} en {1} tekens bevatten")]
        public string sName { get; set; }

        [Display(Name = "Actief")]
        public bool bActive { get; set; }
    }
}
