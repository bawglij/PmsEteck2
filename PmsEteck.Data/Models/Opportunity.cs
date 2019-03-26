using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Opportunities")]
    public class Opportunity
    {
        [Key, ForeignKey("ProjectBase")]
        public int iOpportunityID { get; set; }

        #region ForeignKeys
        [ForeignKey("AccountManager")]
        [Display(Name = "Assetmanager")]
        public string AccountManagerID { get; set; }

        [ForeignKey("MaintenanceContact")]
        [Display(Name = "MaintenanceContact")]
        public int? MaintenanceContactID { get; set; }

        [Display(Name = "Projecttype")]
        public int OpportunityTypeID { get; set; }

        [Display(Name = "Ontwikkelingsfase")]
        public int OpportunityStatusID { get; set; }

        [ForeignKey("OpportunityKind")]
        [Display(Name = "Kind")]
        public int? OpportunityKindID { get; set; }

        [Display(Name = "Investeringsvoorstel")]
        public int? InvestmentProposalID { get; set; }

        [Display(Name = "Type Energie Concept")]
        public int? EnergyConceptID { get; set; }

        [ForeignKey("ProjectInfo")]
        [Display(Name = "Project")]
        public int? ProjectInfoID { get; set; }

        [ForeignKey("TechnicalPrincipal")]
        [Display(Name = "Technisch hoofdprincipe")]
        public int? TechnicalPrincipalID { get; set; }

        [ForeignKey("Developer")]
        [Display(Name = "Ontwikkelaar")]
        public string DeveloperID { get; set; }

        [ForeignKey("ProjectManager")]
        [Display(Name = "Projectmanager")]
        public string ProjectManagerID { get; set; }

        [Display(Name = "Installatiepartner")]
        public int? InstallationPartnerID { get; set; }

        [Display(Name = "Installatiepartnerproces")]
        public int? InstallationPartnerProcessID { get; set; }
        #endregion

        #region Properties
        [Display(Name = "Customer")]
        [StringLength(200, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string Customer { get; set; }

        [Display(Name = "Datum aanvraag")]
        public DateTime? RequestDate { get; set; }

        [Display(Name = "Aantal woningen")]
        public decimal ResidenceAmount { get; set; }

        [Display(Name = "Aantal commercieel")]
        public decimal CommercialAmount { get; set; }

        [Display(Name = "Bedrijfsvloeroppervlak")]
        public decimal BusinessFloorSurface { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "WEQ")]
        public decimal WEQ { get; set; }

        [Display(Name = "Chance")]
        public decimal Chance { get; set; }

        [Display(Name = "Contract date")]
        public DateTime? ContractDate { get; set; }

        [Display(Name = "Starting date")]
        public DateTime? StartingDate { get; set; }

        [Display(Name = "Planning goedkeuringsdatum")]
        public DateTime? PlanningApprovalDate { get; set; }

        [Display(Name = "Startdatum realisatie")]
        public DateTime? StartDateRealisation { get; set; }

        [Display(Name = "Jaarlijkse omzet")]
        public decimal Turnover { get; set; }

        [Display(Name = "Jaarlijkse operationele kosten")]
        public decimal Costs { get; set; }

        [Display(Name = "Aansluitkosten (BAK)")]
        public decimal ConnectionFee { get; set; }

        [Display(Name = "Installing price")]
        public decimal InstallingPrice { get; set; }

        [Display(Name = "EBITDA")]
        public decimal EBITDA { get; set; }

        [Display(Name = "Purchase price SPV")]
        public decimal PurchasePriceSPV { get; set; }

        [Display(Name = "Equity")]
        public decimal Equity { get; set; }

        [Display(Name = "Purchase")]
        public decimal Purchase { get; set; }

        [Display(Name = "Eenmalige externe kosten")]
        public decimal ExternalCosts { get; set; }

        [Display(Name = "Externe opbrengsten (subsidies)")]
        public decimal ExternalRevenues { get; set; }

        [Display(Name = "Ontwikkelkosten")]
        public decimal DevelopmentCosts { get; set; }

        [Display(Name = "Margin")]
        public decimal Margin { get; set; }

        [Display(Name = "Cash flow")]
        public decimal CashFlow { get; set; }

        [Display(Name = "Taken turnover")]
        public decimal TakenTurnover { get; set; }

        [Display(Name = "Portefeuille")]
        public decimal Portfolio { get; set; }

        [Display(Name = "WA EBITDA")]
        public decimal WAEBITDA { get; set; }

        [Display(Name = "WA SPV")]
        public decimal WASPV { get; set; }

        [Display(Name = "Contractduur")]
        [Range(1, 100)]
        public int? ContractDuration { get; set; }

        [Display(Name = "Einde bouw")]
        public DateTime? EndOfConstruction { get; set; }

        [Display(Name = "Bouwtijd")]
        [Range(0, 100)]
        public int? ConstructionTime { get; set; }

        [Display(Name = "Financiering percentage")]
        [Range(0, 1)]
        public decimal? FinancingPercentage { get; set; }

        [Display(Name = "Bouwfinanciering percentage")]
        [Range(0, 1)]
        public decimal? BuildingFinancingPercentage { get; set; }

        [Display(Name = "Eigen vermogen percentage")]
        [Range(0, 1)]
        public decimal? EquityPercentage { get; set; }

        [Display(Name = "Percentage genomen ontwikkelomzet")]
        [Range(0, 1)]
        public decimal? TakenTurnoverPercentage { get; set; }

        [Display(Name = "IRR 15 jaar")]
        [Range(0, 1)]
        public decimal IRRFifteen { get; set; }

        [Display(Name = "IRR 30 jaar")]
        [Range(0, 1)]
        public decimal IRRThirty { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Multiple")]
        public decimal PurchaseFactor { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Actiedatum")]
        public DateTime ActionDate { get; set; }

        [Display(Name = "Verloren aan")]
        public int? LostToCategoryID { get; set; }

        [Display(Name = "Omschrijving")]
        [StringLength(200, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string LostTo { get; set; }
  
        [Display(Name = "Toegevoegd aan pipeline")]
        public DateTime CreatedOn { get; set; }

        #endregion

        #region Read-Only Properties
        [Display(Name = "Jaarlijkse omzet per WEQ")]
        public decimal TurnoverPerWEQ
            => DivideByWEQ(Turnover);

        [Display(Name = "Jaarlijkse operationele kosten per WEQ")]
        public decimal CostsPerWEQ
            => DivideByWEQ(Costs);

        [Display(Name = "EBITDA per WEQ")]
        public decimal EBITDAPerWEQ
            => DivideByWEQ(EBITDA);

        [Display(Name = "Koopsom extern per WEQ")]
        public decimal PurchasePerWEQ
            => DivideByWEQ(Purchase);

        [Display(Name = "Aansluitkosten (BAK) per WEQ")]
        public decimal ConnectionFeePerWEQ
            => DivideByWEQ(ConnectionFee);

        [Display(Name = "Stichtingskosten installaties (STIKO) per WEQ")]
        public decimal InstallingPricePerWEQ
            => DivideByWEQ(InstallingPrice);

        public bool HigherThanFiveMill
            => PurchasePriceSPV >= 5000000;
        #endregion

        #region Single References
        [Display(Name = "Accountmanager")]
        [NotMapped]
        public virtual ApplicationUser AccountManager { get; set; }

        [Display(Name = "OnderhoudsPartij")]
        public virtual MaintenanceContact MaintenanceContact { get; set; }

        [Display(Name = "Type")]
        public virtual OpportunityType OpportunityType { get; set; }

        [Display(Name = "Status")]
        public virtual OpportunityStatus OpportunityStatus { get; set; }

        [Display(Name = "Kind")]
        public virtual OpportunityKind OpportunityKind { get; set; }

        [Display(Name = "Investeringsvoorstel")]
        public virtual InvestmentProposal InvestmentProposal { get; set; }

        [Display(Name = "ProjectBase")]
        public virtual ProjectBase ProjectBase { get; set; }

        [Display(Name = "ProjectInfo")]
        public virtual ProjectInfo ProjectInfo { get; set; }

        [Display(Name = "Technisch hoofdprincipe")]
        public virtual TechnicalPrincipal TechnicalPrincipal { get; set; }
        [NotMapped]
        public virtual ApplicationUser Developer { get; set; }
        [NotMapped]
        public virtual ApplicationUser ProjectManager { get; set; }

        public virtual InstallationPartner InstallationPartner { get; set; }

        public virtual InstallationPartnerProcess InstallationPartnerProcess { get; set; }

        public virtual EnergyConcept EnergyConcept { get; set; }

        [Display(Name = "Verloren aan")]
        public virtual LostToCategory LostToCategory { get; set; }

        #endregion

        #region List References

        [Display(Name = "Notes")]
        public List<OpportunityNote> OpportunityNotes { get; set; }

        [Display(Name = "Changelog")]
        public List<OpportunityValueLog> OpportunityValueLogs { get; set; }

        [Display(Name = "Bestanden")]
        public ICollection<File> Documents { get; set; }
        #endregion

        #region Methods
        private decimal DivideByWEQ(decimal value)
            => WEQ > 0 ? decimal.Divide(value, WEQ) : 0;
        #endregion
    }
}