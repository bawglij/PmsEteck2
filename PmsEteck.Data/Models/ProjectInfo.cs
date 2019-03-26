using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PmsEteck.Data.Models
{
    [Table("ProjectInfo")]
    public class ProjectInfo
    {
        public enum ProjectRapportage
        {
            Maandrapportage = 1,
            Kwartaalrapportage = 2,
            Jaarrapportage = 3
        }

        #region Constructor
        private PmsEteckContext db = new PmsEteckContext();
        #endregion

        #region Keys
        [Key, ForeignKey("ProjectBase")]
        public int iProjectKey { get; set; }

        [Display(Name = "Assetmanager")]
        public int? iAssetManagerKey { get; set; }

        [ForeignKey("User")]
        public string AssetManagerID { get; set; }

        [ForeignKey("TechnicalPrincipalMain")]
        [Display(Name = "Hoofdprincipe")]
        public int? iTechnicalPrincipalMainKey { get; set; }

        [ForeignKey("TechnicalPrincipalSub1")]
        [Display(Name = "Nevenprincipe 1")]
        public int? iTechnicalPrincipalSub1Key { get; set; }

        [ForeignKey("TechnicalPrincipalSub2")]
        [Display(Name = "Nevenprincipe 2")]
        public int? iTechnicalPrincipalSub2Key { get; set; }

        [Display(Name = "Opwekking")]
        public int? iDemarcationKey { get; set; }

        [Display(Name = "Bemetering")]
        public int? iMeterKey { get; set; }

        [Display(Name = "Financieel project")]
        public int? iFinProjectKey { get; set; }

        [Display(Name = "Temperatuurtraject Warmte")]
        public int? iTemperatureRangeKey { get; set; }

        [Display(Name = "Temperatuurtraject Koude")]
        public int? iColdTemperatureRangeKey { get; set; }

        [Display(Name = "Distributie net")]
        public int? iDistributionNetWorkKey { get; set; }

        [Display(Name = "Distributie water")]
        public int? iWaterTypeKey { get; set; }

        [ForeignKey("MaintenanceContact")]
        [Display(Name = "Onderhoudspartij centrale installatie")]
        public int? iMaintenanceContactKey { get; set; }

        [ForeignKey("HomeMaintenanceContact")]
        [Display(Name = "Onderhoudspartij woningaansluitingen")]
        public int? iHomeMaintenanceContactKey { get; set; }

        [Display(Name = "Levering tapwater")]
        public int? iSupplyWaterTypeKey { get; set; }

        [Display(Name = "Referentieproject (CO2-Reductie)")]
        public int? iReferenceProjectKey { get; set; }

        [Display(Name = "Status")]
        public int iProjectStatusID { get; set; }

        [Display(Name = "Standaard projectdebiteur")]
        [ForeignKey("Debtor")]
        public int? iDefaultDebtorID { get; set; }

        [Display(Name = "Transactiewijze")]
        public int? iTransactionModeID { get; set; }

        [Display(Name = "Validatieprofiel")]
        public int? ReportValidationSetID { get; set; }

        [Display(Name = "Facturatie via eigen incasso BV")]
        public bool InvoiceViaOwnCollection { get; set; }

        private int? mailConfigID;
        [Display(Name = "Mail Configuratie")]
        [ForeignKey("MailConfig")]
        public int? MailConfigID
        {
            get => mailConfigID ?? 1;
            set { mailConfigID = value; }
        }

        [ForeignKey("DebtCollectionCustomer")]
        public int? DebtCollectionCustomerID { get; set; }

        #endregion

        #region Fields

        #region Default

        [MaxLength(500)]
        [Display(Name = "Alias")]
        public string sProjectAlias { get; set; }

        [MaxLength(150)]
        [Display(Name = "Entiteit")]
        public string sCustomerName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Straat")]
        public string sStreetName { get; set; }

        [Display(Name = "Nummer")]
        [MaxLength(50)]
        public string iNumber { get; set; }

        [MaxLength(150)]
        [Display(Name = "Plaatsnaam")]
        public string sCity { get; set; }

        [MaxLength(100)]
        [Display(Name = "Postcodegebied")]
        [DataType(DataType.PostalCode, ErrorMessage = "Voer een geldige postcode in.")]
        public string sPostalcodeArea { get; set; }

        [Display(Name = "Latitude")]
        public decimal? dLatitude { get; set; }

        [Display(Name = "Longitude")]
        public decimal? dLongitude { get; set; }

        [Display(Name = "Extern toegankelijk")]
        public bool bExternalAccess { get; set; }

        [Display(Name = "Projectafbeelding")]
        [StringLength(255, ErrorMessage = "{0} mag maximaal {1} tekens bevatten.")]
        public string sProjectImage { get; set; }

        #endregion

        #region Details

        [Display(Name = "Referentieproject niet van toepassing")]
        public bool bReferenProjectDoesNotApply { get; set; }

        [Display(Name = "Levering opwarming tapwater")]
        public bool bHotWater { get; set; }

        [Display(Name = "Levering tapwater warm")]
        public bool bDrinkWater { get; set; }

        [Display(Name = "Levering koeling")]
        public bool bCooling { get; set; }

        [Display(Name = "Bouwjaar (centrale) installatie")]
        public int? iYearCentralInstallation { get; set; }

        [Display(Name = "WION registratie")]
        public bool bWIONRegistration { get; set; }

        [Display(Name = "Vergunning waterwet")]
        [MaxLength(100)]
        public string sLicenseWaterLaw { get; set; }

        [Display(Name = "Levering van koud tapwater")]
        public bool bDeliverCoolWater { get; set; }

        [Display(Name = "AVF Quickscan")]
        public bool bAVFQuickscanExecuted { get; set; }

        [Display(Name = "Laatste controle")]
        public DateTime? dtLastAVFQuickScan { get; set; }

        [Display(Name = "warmtewet van toepassing")]
        public bool bAppliesHeatLaw { get; set; }
        
        [Display(Name = "Soort project")]
        [ForeignKey("ProjectType")]
        public int? ProjectTypeID { get; set; }
        public virtual ProjectType ProjectType { get; set; }
        #endregion

        #region Installed Capacity

        [Display(Name = "Warmtepomp(en)")]
        public decimal? dHeatPumps { get; set; }

        [Display(Name = "CV-Ketels tbv ruimteverwarming")]
        public decimal? dCentralHeatingRooms { get; set; }

        [Display(Name = "Tevens voor bereiding tapwater")]
        public bool bCentralHeatingRoomsForWater { get; set; }

        [Display(Name = "CV-Ketels tbv tapwater")]
        public decimal? dCentralHeatingHotWater { get; set; }

        [Display(Name = "Bronvermogen warmte (kW)")]
        [DisplayFormat(DataFormatString = "{0:0.## kW}")]
        public decimal? dSourcePowerHotKWh { get; set; }

        [Display(Name = "Bronvermogen warmte (m\xB3)")]
        [DisplayFormat(DataFormatString = "{0:0.## m\xB3}")]
        public decimal? dSourcePowerHotM3 { get; set; }

        [Display(Name = "Bronvermogen koude (kW)")]
        [DisplayFormat(DataFormatString = "{0:0.## kW}")]
        public decimal? dSourcePowerCoolKWh { get; set; }

        [Display(Name = "Bronvermogen koude (m\xB3)")]
        [DisplayFormat(DataFormatString = "{0:0.## m\xB3}")]
        public decimal? dSourcePowerCoolM3 { get; set; }

        [Display(Name = "Stadsverwarming / restwarmte")]
        [DisplayFormat(DataFormatString = "{0:0.## kW}")]
        public decimal? dWasteHeat { get; set; }

        [Display(Name = "Houtpelletketel")]
        [DisplayFormat(DataFormatString = "{0:0.## kW}")]
        public decimal? dWoodPelletStove { get; set; }

        [Display(Name = "Gasboiler tbv tapwater")]
        [DisplayFormat(DataFormatString = "{0:0.## kW}")]
        public decimal? dGasBoiler { get; set; }

        [Display(Name = "Energiedak")]
        [DisplayFormat(DataFormatString = "{0:0.## kW}")]
        public decimal? dEnergyRoof { get; set; }

        [Display(Name = "Drycooler")]
        [DisplayFormat(DataFormatString = "{0:0.## kW}")]
        public decimal? dDryCooler { get; set; }

        [Display(Name = "Oppervlaktewater regeneratie (kW)")]
        [DisplayFormat(DataFormatString = "{0:0.## kW}")]
        public decimal? dSurfaceGenerationKWh { get; set; }

        [Display(Name = "Oppervlaktewater regeneratie (m\xB3/h)")]
        [DisplayFormat(DataFormatString = "{0:0.## m\xB3/h}")]
        public decimal? dSurfaceGenerationM3 { get; set; }

        [Display(Name = "Zonnecollectoren (thermisch)")]
        [DisplayFormat(DataFormatString = "{0:0.## kW}")]
        public decimal? dSolarCollectorsThermally { get; set; }

        [Display(Name = "Zonnecollectoren (elektrisch)")]
        [DisplayFormat(DataFormatString = "{0:0.## kWp}")]
        public decimal? dSolarCollectorsElectricKWh { get; set; }

        [Display(Name = "Aantal zonnecollectoren (elektrisch)")]
        [DisplayFormat(DataFormatString = "{0:0.## stuks}")]
        public decimal? dNumberOfSolarCollectorsElectric { get; set; }

        [Display(Name = "Inhoud distributiesysteem")]
        [DisplayFormat(DataFormatString = "{0:0.## m\xB3}")]
        public decimal? dDistribution { get; set; }

        [Display(Name = "Koelmachine")]
        [DisplayFormat(DataFormatString = "{0:0.## kW}")]
        public decimal? dChiller { get; set; }

        [Display(Name = "Eindtemperatuur opwarming")]
        [DisplayFormat(DataFormatString = "{0:0.## °C}")]
        public decimal? dTemperatureHotWater { get; set; }

        [Display(Name = "WP Lucht/water verwarmen (kW)")]
        [DisplayFormat(DataFormatString = "{0:0.## kW}")]
        public decimal? dAirWaterHotKWh { get; set; }

        [Display(Name = "WP Lucht/water koelen (kW)")]
        [DisplayFormat(DataFormatString = "{0:0.## kW}")]
        public decimal? dAirWaterCoolKWh { get; set; }

        public bool bHeatPumpsActive { get; set; }

        public bool bChillerActive { get; set; }

        public bool bCentralHeatingRoomsActive { get; set; }

        public bool bCentralHeatingHotWaterActive { get; set; }

        public bool bGasBoilerActive { get; set; }

        public bool bSourcePowerHotKWhActive { get; set; }

        public bool bSourcePowerHotM3Active { get; set; }

        public bool bSourcePowerCoolKWhActive { get; set; }

        public bool bSourcePowerCoolM3Active { get; set; }

        public bool bWasteHeatActive { get; set; }

        public bool bWoodPelletStoveActive { get; set; }

        public bool bEnergyRoofActive { get; set; }

        public bool bDryCoolerActive { get; set; }

        public bool bSurfaceGenerationKWhActive { get; set; }

        public bool bSurfaceGenerationM3Active { get; set; }

        public bool bSolarCollectorsThermallyActive { get; set; }

        public bool bNumberOfSolarCollectorsElectricActive { get; set; }

        public bool bSolarCollectorsElectricKWhActive { get; set; }

        public bool bAirWaterHotKWhActive { get; set; }

        public bool bAirWaterCoolKWhActive { get; set; }

        [Display(Name = "Informatie bij verhuizing")]
        [MaxLength(1000)]
        public string MovingMessage { get; set; }

        #endregion

        #region Exploitation
        [Display(Name = "Start exploitatie")]
        public DateTime? dtStartDateExploitation { get; set; }

        [Display(Name = "Datum overname")]
        public DateTime? dtDateTakeOver { get; set; }

        [Display(Name = "Looptijd exploitatie")]
        public DateTime? dtEndDateExploitation { get; set; }

        [Display(Name = "Start technische exploitatie")]
        public DateTime? dtStartDateTechnicalExploitation { get; set; }

        [Display(Name = "Jaar herinvestering")]
        public DateTime? dtDateReinvestment { get; set; }
        #endregion

        #region Ownership
        [Display(Name = "Opstalrecht")]
        public bool bShelterLaw { get; set; }

        [Display(Name = "Kwalitatieve verplichting")]
        public bool bQualitativeObligation { get; set; }

        [Display(Name = "Kettingbeding")]
        public bool bPerpetualClause { get; set; }

        [Display(Name = "Huursituatie")]
        public bool bRentalCase { get; set; }
        #endregion

        #region Financial details
        [Display(Name = "Disconteringsvoet")]
        public decimal? dDiscountRate { get; set; }

        [MaxLength(250)]
        [Display(Name = "Upsides/Downsides toelichting")]
        public string sUpDownsides { get; set; }

        [Display(Name = "Upsides/Downsides in €")]
        public decimal dUpDownsides { get; set; }

        #region Fire Insurance

        [Display(Name = "Herbouwwaarde installatie")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? dReinstatementInstallation { get; set; }

        [Display(Name = "Bijzondere kosten")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal? dSpecialCharges { get; set; }

        #endregion

        #endregion

        #region SLA

        [Display(Name = "Responsetijd terugbellen in uren")]
        public decimal? dResponseTimeCallBack { get; set; }

        [Display(Name = "Responsetijd aanwezig")]
        public decimal? dResponseTimeOnSite { get; set; }

        [Display(Name = "Responsetijd oplossing")]
        public decimal? dResponseTimeSolution { get; set; }

        [Display(Name = "SLA Document")]
        [MaxLength(2000, ErrorMessage = "{0} mag maximaal {1} tekens bevatten.")]
        public string sSlaDocument { get; set; }

        #endregion

        #region EMDetails

        public ProjectRapportage? iProjectReportPeriod { get; set; }

        [Display(Name = "EM rapportage verplicht?")]
        public bool EMReportRequired { get; set; }

        [Display(Name = "COP wp bij 15-35 (Ruimteverwarming)")]
        [DisplayFormat(DataFormatString = "{0:N0} GJ")]
        public decimal? dCOPRoomHeating { get; set; }

        [Display(Name = "COP wp bij 15-60 (Tapwaterverwarming)")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal? dCOPWaterHeating { get; set; }

        [Display(Name = "SPF BodemEnergie Systemen")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal? dSPFbes { get; set; }

        [Display(Name = "Energievraag ruimteverwarming")]
        [DisplayFormat(DataFormatString = "{0:N} GJ")]
        public decimal? dEnergyDemandRoomHeating { get; set; }

        [Display(Name = "Energievraag tapwaterverwarming")]
        [DisplayFormat(DataFormatString = "{0:N} GJ")]
        public decimal? dEnergyDemandWater { get; set; }

        [Display(Name = "Stookgrens")]
        [DisplayFormat(DataFormatString = "{0:N1} °C")]
        public decimal? dStokeLimit { get; set; }

        [Display(Name = "Koelgrens")]
        [DisplayFormat(DataFormatString = "{0:N1} °C")]
        public decimal? dCoolLimit { get; set; }

        [Display(Name = "Referentiewaarde verkoop koude")]
        [DisplayFormat(DataFormatString = "{0:N2} GJ/jaar/woning")]
        public decimal? dReferenceDeliverCooling { get; set; }

        [ForeignKey("EnergyManager")]
        [Display(Name = "Verantwoordelijke energiemanager")]
        public string sEnergyManagerID { get; set; }

        [Display(Name = "Instructies inkoop")]
        public string sPurchaseInstruction { get; set; }

        [Display(Name = "Instructies verkoop")]
        public string sSalesInstruction { get; set; }

        [Display(Name = "Variabel deel inkoop bij maandrapportage")]
        [ForeignKey("CalculationTypePurchase")]
        public int? iCalculationTypePurchaseID { get; set; }

        [Display(Name = "Variabel deel verkoop bij maandrapportage")]
        [ForeignKey("CalculationTypeSales")]
        public int? iCalculationTypeSalesID { get; set; }

        [Display(Name = "Gebruik standaard voor ruimteverwarming")]
        public bool bDefaultCOPRoomHeating { get; set; }

        [Display(Name = "Gebruik standaard voor tapwaterverwarming")]
        public bool bDefaultCOPWaterHeating { get; set; }

        [Display(Name = "Gebruik standaard voor SPF BodemEnergie Systemen")]
        public bool bDefaultSPFbes { get; set; }

        [Display(Name = "Gebruik standaard voor energievraag ruimteverwarming")]
        public bool bDefaultEnergyDemandRoomHeating { get; set; }

        [Display(Name = "Gebruik standaard voor energievraag tapwaterverwarming")]
        public bool bDefaultEnergyDemandWaterHeating { get; set; }

        [Display(Name = "Gebruik standaard voor referentiewaarde verkoop koude")]
        public bool bDefaultReferenceSalesCooling { get; set; }

        [Display(Name = "Gebruik standaard voor stookgrens")]
        public bool bDefaultStokeLimit { get; set; }

        [Display(Name = "Gebruik standaard voor koelgrens")]
        public bool bDefaultCoolLimit { get; set; }

        [Display(Name = "Automatisch rapporteren")]
        public bool AutomaticReport { get; set; }

        [Display(Name = "Procentuele afwijking van gemiddelde verbruik")]
        [DisplayFormat(DataFormatString = "{0:P2}")]
        public decimal PercentageDeviationFromAverage { get; set; }

        [Display(Name = "Gebruik standaard voor referentiewaarde verkoop tapwater")]
        public bool DefaultReferenceDeliveryWater { get; set; }

        [Display(Name = "Referentiewaarde verkoop tapwater")]
        [DisplayFormat(DataFormatString = "{0:N2} m\xB3/jaar/woning")]
        public decimal? ReferenceDeliveryWater { get; set; }

        [Display(Name = "Jaardetails")]
        public ICollection<ProjectYearDetail> ProjectYearDetails { get; set; }

        [Display(Name = "Gebouwbeheersystemen (GBS)")]
        public ICollection<BuildingManagementSystem> BuildingManagementSystems { get; set; }

        #endregion

        #region CustomerDetails
        [Display(Name = "Facturatie via standaard BV")]
        public bool InvoicedByDefaultCustomer { get; set; }

        [Display(Name = "Automatisch factureren")]
        public bool AutomaticInvoicing { get; set; }

        [Display(Name = "Algemene mededeling")]
        public string CustomerAnnouncement { get; set; }
        #endregion
        #region ServiceDetails
        [Display(Name = "Algemene mededeling")]
        public string ServiceAnnouncement { get; set; }
        #endregion

        #endregion

        #region Single References

        public virtual ProjectBase ProjectBase { get; set; }

        //public ProjectBase FinProject { get; set; }

        public virtual AssetManager AssetManager { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Demarcation Demarcation { get; set; }

        public virtual DistributionNetwork DistributionNetwork { get; set; }

        public virtual TechnicalPrincipal TechnicalPrincipalMain { get; set; }

        public virtual TechnicalPrincipal TechnicalPrincipalSub1 { get; set; }

        public virtual TechnicalPrincipal TechnicalPrincipalSub2 { get; set; }

        public virtual Meter Meter { get; set; }

        public virtual CalcRule CalcRule { get; set; }

        [ForeignKey("iTemperatureRangeKey")]
        public virtual TemperatureRange HeatTemperatureRange { get; set; }

        [ForeignKey("iColdTemperatureRangeKey")]
        public virtual TemperatureRange ColdTemperatureRange { get; set; }

        public virtual MaintenanceContact MaintenanceContact { get; set; }

        public virtual MaintenanceContact HomeMaintenanceContact { get; set; }

        public virtual SupplyWaterType SupplyWaterType { get; set; }

        public virtual WaterType WaterType { get; set; }

        public virtual ReferenceProject ReferenceProject { get; set; }

        [Display(Name = "Status")]
        public virtual ProjectStatus ProjectStatus { get; set; }

        public virtual ApplicationUser EnergyManager { get; set; }

        public virtual Debtor Debtor { get; set; }

        public virtual TransactionMode TransactionMode { get; set; }

        public virtual CalculationType CalculationTypePurchase { get; set; }

        public virtual CalculationType CalculationTypeSales { get; set; }

        public virtual ReportValidationSet ReportValidationSet { get; set; }

        public virtual MailConfig MailConfig { get; set; }

        public virtual Customer DebtCollectionCustomer { get; set; }
        #endregion

        #region List References
        public ICollection<WeqMutation> WeqMutations { get; set; }

        public ICollection<Financing> Financings { get; set; }

        public ICollection<Investment> Investments { get; set; }

        public ICollection<CalcMutation> CalcMutations { get; set; }

        public ICollection<Subsidy> Subsidies { get; set; }

        public ICollection<EnergyConsumption> EnergyConsumption { get; set; }

        public ICollection<Hyperlink> Hyperlinks { get; set; }

        public ICollection<Contact> Contacts { get; set; }

        //public ICollection<OtherDelivery> OtherDeliveries { get; set; }
        public ICollection<OtherDeliveryProjectInfo> OtherDeliveries { get; set; }

        public ICollection<Address> Addresses { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public List<Report> Reports { get; set; }

        [Display(Name = "Usergroups")]
        //public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<ProjectInfoUserGroup> UserGroups { get; set; }
        //public ICollection<PurchaseDeliveryType> PurchaseTypes { get; set; }
        public ICollection<ProjectInfoPurchDeliveryType> PurchaseTypes { get; set; }

        //public ICollection<SalesDeliveryType> SalesTypes { get; set; }
        public ICollection<ProjectInfoSalesDeliveryType> SalesTypes { get; set; }

        [Display(Name = "Historie validatieprofielen")]
        public List<ProjectReportValidationSetLog> ReportValidationSetLog { get; set; }

        public ICollection<ServiceTicket> ServiceTickets { get; set; }

        public ICollection<ServiceInvoice> ServiceInvoices { get; set; }

        public ICollection<AverageMonthConsumption> AverageMonthConsumptions { get; set; }
        #endregion

        public bool ProjectHasError
        {
            get
            {
                bool hasError = false;
                ProjectInfo project = db.ProjectInfo
                                                                        .Include(i => i.Reports)
                                                                        .Include(i => i.Addresses)
                                                                        .Include(i => i.Addresses.Select(s => s.ConnectionType))
                                                                        .Include(i => i.Addresses.Select(s => s.ConsumptionMeters))
                                                                        .Include(i => i.Addresses.Select(s => s.ConsumptionMeters.Select(c => c.Counters)))
                                                                        .Include(i => i.Addresses.Select(s => s.ConsumptionMeters.Select(c => c.Counters.Select(co => co.CounterStatus))))
                                                                        .Include(i => i.Addresses.Select(s => s.AddressRateCards))
                                                                        .FirstOrDefault(f => f.iProjectKey == iProjectKey);
                hasError = project.ProjectIsActive && project.Addresses.Any(a => a.ConnectionType.bConsumptionMeterRequired ?
                                                            a.ConsumptionMeters.Count() != 0 ?
                                                                a.ConsumptionMeters.Any(u => u.Counters.Where(w => w.bActive).Count() == 0) ?
                                                                    true
                                                                : a.ConsumptionMeters.Any(c => c.Counters.Where(w => w.bActive).Any(co => co.OldCounterStatus.Count > 0 && co.OldCounterStatus.FirstOrDefault().bHasError))
                                                            : true
                                                        : a.AddressRateCards.Count() == 0);
                return hasError;
            }
        }
        
        #region Methods

        public bool IsProjectClosed(int ProjectKey)
        {
            bool projectIsClosed = false;

            ProjectInfo project = db.ProjectInfo.Find(ProjectKey);

            if (project != null)
            {
                projectIsClosed = project.dtEndDateExploitation.HasValue && project.dtEndDateExploitation.Value < DateTime.Now ? true : false;
            }

            return projectIsClosed;
        }

        public async Task<bool> IsProjectClosedAsync(int ProjectKey)
        {
            bool projectIsClosed = false;

            ProjectInfo project = await db.ProjectInfo.FindAsync(ProjectKey);

            if (project != null)
            {
                projectIsClosed = project.dtEndDateExploitation.HasValue && project.dtEndDateExploitation.Value < DateTime.Now ? true : false;
            }

            return projectIsClosed;
        }

        public bool ProjectIsActive
        {
            get
            {
                DateTime today = DateTime.Today;
                return dtStartDateExploitation <= today &&  (!dtEndDateExploitation.HasValue || dtEndDateExploitation > today);
            }
        }

        #endregion
    }
}