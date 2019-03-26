using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("WeqMutations")]
    public class WeqMutation
    {
        [Key]
        public int iWeqMutationKey { get; set; }

        public int iWeqCategoryKey { get; set; }

        public int iProjectKey { get; set; }

        [Display(Name = "Afgifte unit")]
        public int? iDispensingUnitKey { get; set; }
        
        [Display(Name = "Datum")]
        public DateTime dDate { get; set; }

        [Display(Name = "WEQ's gereed")]
        public int iDone { get; set; }

        [Display(Name = "WEQ’s nog te realiseren")]
        public int iToBeRealised { get; set; }

        [Display(Name = "Aantal aansluitingen")]
        public int? iConnections { get; set; }

        [Display(Name = "Aantal nog te realiseren aansluitingen")]
        public int? iConnectionsToBeRealised { get; set; }
        
        [Display(Name = "Gebruiksoppervlak")]
        public decimal? dUseSurface { get; set; }

        [Display(Name = "BVO gereed")]
        public decimal? dBusinessFloorDone { get; set; }

        [Display(Name = "BVO gereed")]
        public decimal? dBusinessFloorToBeRealised { get; set; }

        public DispensingUnit DispensingUnit { get; set; }

        public WeqCategory WeqCategory { get; set; }

        public ProjectInfo ProjectInfo { get; set; }

    }
}