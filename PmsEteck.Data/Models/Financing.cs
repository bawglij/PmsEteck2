using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Financings")]
    public class Financing
    {
        #region Keys
        [Key]
        public int iFinancingKey { get; set; }

        public int iProjectKey { get; set; }

        [ForeignKey("Financer")]
        [Display(Name ="Eerste financier")]
        public int iFinancerKey { get; set; }

        [ForeignKey("SubFinancer")]
        [Display(Name="Tweede financier")]
        public int? iSubFinancerKey { get; set; }

        public bool bSubordinatedLoan { get; set; }

        [Display(Name ="Aflossingstermijn")]
        public int iPeriodKey { get; set; }

        [Display(Name ="DSRA Storting")]
        public int iDsraDepositKey { get; set; }
        #endregion
        
        #region Fields
        [Display(Name ="Bedrag")]
        public decimal dAmount { get; set; }

        [Display(Name ="DSRA Bedrag")]
        public decimal? dDsraAmount { get; set; }

        [Display(Name ="Bedrag na looptijd")]
        public decimal dResdualAmount { get; set; }

        [Display(Name ="Startdatum")]
        public DateTime dtStartDate { get; set; }

        [Display(Name ="Einddatum")]
        public DateTime dtEndDate { get; set; }

        [Display(Name ="Rentepercentage")]
        public Decimal dInterest { get; set; }

        [MaxLength(250)]
        [Display(Name ="Omschrijving")]
        public string sDescription { get; set; }
        #endregion

        #region Single References
        public ProjectInfo ProjectInfo { get; set; }

        public Financer Financer { get; set; }

        public Financer SubFinancer { get; set; }

        public Period Period { get; set; }

        public DsraDeposit DsraDeposit { get; set; }
        #endregion
    }
}