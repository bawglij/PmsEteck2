using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Investments")]
    public class Investment
    {
        #region Keys
        [Key]
        public int iInvestmentKey { get; set; }

        public int iProjectKey { get; set; }
        
        [Display(Name ="Afschrijvingstermijn")]
        public int iPeriodKey { get; set; }
        #endregion

        #region Fields
        [Display(Name ="Bedrag")]
        public decimal dAmount { get; set; }

        [Display(Name="Restbedrag na looptijd")]
        public decimal dResdualAmount { get; set; }

        [Display(Name ="Startdatum")]
        public DateTime dtStartDate { get; set; }

        [Display(Name ="Einddatum")]
        public DateTime dtEndDate { get; set; }

        [MaxLength(250)]
        [Display(Name ="Omschrijving")]
        public string sDescription { get; set; }

        #endregion

        #region Single References
        public Period Period { get; set; }
        #endregion
    }
}