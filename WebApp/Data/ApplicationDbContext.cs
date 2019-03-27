using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models;
using PmsEteck.Data.Models.Results;

namespace WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Financing>().Property(x => x.dInterest).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<ProjectInfo>().Property(x => x.dDiscountRate).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<ProjectInfo>().Property(x => x.dLongitude).HasColumnType("decimal(10, 7)");
            modelBuilder.Entity<ProjectInfo>().Property(x => x.dLatitude).HasColumnType("decimal(9, 7)");
            modelBuilder.Entity<ProjectInfo>().Property(x => x.PercentageDeviationFromAverage).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<Address>().Property(x => x.dLongitude).HasColumnType("decimal(10, 7)");
            modelBuilder.Entity<Address>().Property(x => x.dLatitude).HasColumnType("decimal(9, 7)");
            modelBuilder.Entity<Address>().Property(p => p.PercentLossOfDistribution).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<Address>().Property(p => p.MaxLossOfDistribution).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<Address>().Property(p => p.PercentPollTax).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<Address>().Property(p => p.MaxPollTax).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<RateCardRow>().Property(x => x.dVAT).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<RateCardRow>().Property(p => p.dAmount).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<RateCardScaleHistory>().Property(p => p.Consumed).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<Consumption>().Property(p => p.dConsumption).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<Consumption>().Property(p => p.dEndPosition).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<ReferenceProjectRow>().Property(p => p.dDistributionEfficiency).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<ReferenceProjectRow>().Property(p => p.dGenerationEfficiency).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<CO2ConstantRow>().Property(p => p.dCO2ConstantRowValue).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<CounterChangeLog>().Property(p => p.dEndPosition).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<InvoiceLine>().Property(p => p.dAmount).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<InvoiceLine>().Property(p => p.dConsumption).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<InvoiceLine>().Property(p => p.dEndPosition).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<InvoiceLine>().Property(p => p.dQuantity).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<InvoiceLine>().Property(p => p.dStartPosition).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<InvoiceLine>().Property(p => p.dTotalAmount).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<InvoiceLine>().Property(p => p.dUnitPrice).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<Deposit>().Property(p => p.dAmountexVAT).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<Default>().Property(p => p.dValue).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<Opportunity>().Property(p => p.Chance).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<Opportunity>().Property(p => p.FinancingPercentage).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<Opportunity>().Property(p => p.BuildingFinancingPercentage).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<Opportunity>().Property(p => p.EquityPercentage).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<OpportunityDefault>().Property(p => p.dValue).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<ReportValidationSetLine>().Property(p => p.Value).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<CostlierValue>().Property(p => p.TotalConsumption).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<CostlierValue>().Property(p => p.TotalCostlier).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<CostlierValue>().Property(p => p.PollTax).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<CostlierValue>().Property(p => p.LossOfDistribution).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<CounterYearConsumption>().Property(p => p.Consumption).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<Counter>().Property(x => x.PercentageDeviationFromAverage).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<SeasonalPattern>().Property(x => x.PercentFootHold).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<PeriodPercentage>().Property(x => x.Percentage).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<BudgetDimensionRule>().Property(x => x.PercentJanuary).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<BudgetDimensionRule>().Property(x => x.PercentFebruary).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<BudgetDimensionRule>().Property(x => x.PercentMarch).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<BudgetDimensionRule>().Property(x => x.PercentApril).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<BudgetDimensionRule>().Property(x => x.PercentMay).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<BudgetDimensionRule>().Property(x => x.PercentJune).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<BudgetDimensionRule>().Property(x => x.PercentJuly).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<BudgetDimensionRule>().Property(x => x.PercentAugust).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<BudgetDimensionRule>().Property(x => x.PercentSeptember).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<BudgetDimensionRule>().Property(x => x.PercentOctober).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<BudgetDimensionRule>().Property(x => x.PercentNovember).HasColumnType("decimal(18, 5)");
            modelBuilder.Entity<BudgetDimensionRule>().Property(x => x.PercentDecember).HasColumnType("decimal(18, 5)");

            modelBuilder.Entity<ApplicationRoleGroup>().HasKey(t => new { t.RoleId, t.RoleGroupId });
            modelBuilder.Entity<ApplicationUserRoleGroup>().HasKey(t => new { t.RoleGroupId, t.UserId });
            modelBuilder.Entity<ApplicationUserGroup>().HasKey(t => new { t.UserId, t.GroupId }); modelBuilder.Entity<AddressRateCard>().HasKey(t => new { t.iAddressKey, t.iRateCardKey });
            modelBuilder.Entity<TicketLabel>().HasKey(t => new { t.TicketId, t.LabelId });
            modelBuilder.Entity<OtherDeliveryProjectInfo>().HasKey(t => new { t.OtherDeliveryId, t.ProjectInfoId });
            modelBuilder.Entity<ProjectInfoUserGroup>().HasKey(t => new { t.UserGroupId, t.ProjectInfoId });
            modelBuilder.Entity<ProjectInfoPurchDeliveryType>().HasKey(t => new { t.PurchaseDeliveryTypeId, t.ProjectInfoPurchId });
            modelBuilder.Entity<ProjectInfoSalesDeliveryType>().HasKey(t => new { t.SalesDeliveryTypeId, t.ProjectInfoId });
            modelBuilder.Entity<AddressRateCard>().HasKey(t => new { t.iAddressKey, t.iRateCardKey });

            modelBuilder.Entity<MaintenanceContact>().HasMany(m => m.Projects).WithOne(w => w.MaintenanceContact).HasForeignKey(h => h.iMaintenanceContactKey);
            modelBuilder.Entity<ServiceInvoiceLineInput>().HasMany(m => m.MaintenanceContactInputLines).WithOne(w => w.MaintenanceContactInput).HasForeignKey(h => h.MaintenanceContactInputID);
            modelBuilder.Entity<ServiceInvoiceLineInput>().HasMany(m => m.AssetmanagerInputLines).WithOne(w => w.AssetManagerInput).HasForeignKey(h => h.AssetManagerInputID);
            modelBuilder.Entity<ServiceInvoiceLineInput>().HasMany(m => m.CoordinatorInputLines).WithOne(w => w.CoordinatorInput).HasForeignKey(h => h.CoordinatorInputID);
            modelBuilder.Entity<ServiceInvoiceLineInput>().HasMany(m => m.OwnerInputLines).WithOne(w => w.OwnerInput).HasForeignKey(h => h.OwnerInputID);
            modelBuilder.Entity<ResponseConcept>().HasOne(m => m.Ticket).WithOne(h => h.ResponseConcept).HasForeignKey<Ticket>(c => c.iTicketID);
            modelBuilder.Entity<ServiceTicket>().HasOne(m => m.Ticket).WithOne(h => h.ServiceTicket).HasPrincipalKey<Ticket>(c => c.iTicketID);

            modelBuilder.Entity<Occupant>().HasOne(m => m.Title).WithMany().HasForeignKey(h => h.iTitleID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Occupant>().HasOne(m => m.Debtor).WithMany().HasForeignKey(h => h.iDebtorID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PaymentTermHistory>().HasOne(m => m.PaymentTerm).WithMany().HasForeignKey(h => h.iPaymentTermID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProjectReportValidationSetLog>().HasOne(m => m.ReportValidationSet).WithMany().HasForeignKey(h => h.ReportValidationSetID).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplicationUserRoleGroup>().HasOne(m => m.ApplicationUser).WithMany(c => c.RoleGroups).HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<ApplicationUserRoleGroup>().HasOne(m => m.RoleGroup).WithMany(c => c.Users).HasForeignKey(cb => cb.RoleGroupId);

            modelBuilder.Entity<ApplicationRoleGroup>().HasOne(m => m.ApplicationRole).WithMany(c => c.RoleGroups).HasForeignKey(cb => cb.RoleId);
            modelBuilder.Entity<ApplicationRoleGroup>().HasOne(m => m.RoleGroup).WithMany(c => c.Roles).HasForeignKey(cb => cb.RoleGroupId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("pms");
        }

        public DbSet<Operator> Operators { get; set; }
        public DbSet<WaterType> WaterTypes { get; set; }
        public DbSet<WeqCategory> WeqCategories { get; set; }
        public DbSet<BaseModel> BaseModels { get; set; }
        public DbSet<BillingType> BillingTypes { get; set; }
        public DbSet<CalcCategory> CalcCategories { get; set; }
        public DbSet<ChangeReason> ChangeReasons { get; set; }
        public DbSet<CommunicationType> CommunicationTypes { get; set; }
        public DbSet<ConnectionType> ConnectionTypes { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Financer> Financers { get; set; }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }
        public DbSet<Frequency> Frequencies { get; set; }
        public DbSet<Ledger> Ldgers { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<LostToCategory> LostToCategories { get; set; }
        public DbSet<Meter> Meters { get; set; }
        public DbSet<Month> Months { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<RubricType> RubricTypes { get; set; }
        public DbSet<RubricGroup> RubricGroups { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Demarcation> Demarcations { get; set; }
        public DbSet<Default> Defaults { get; set; }
        public DbSet<DebtorType> DebtorTypes { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<ValueOperator> ValueOperators { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<TransactionMode> TransactionModes { get; set; }
        public DbSet<TemperatureRange> TemperatureRanges { get; set; }
        public DbSet<SupplyWaterType> SupplyWaterTypes { get; set; }
        public DbSet<SubsidyCategory> SubsidyCategories { get; set; }
        public DbSet<ShippingProfile> ShippingProfiles { get; set; }
        public DbSet<ReportColumn> ReportColumns { get; set; }
        public DbSet<RateCardType> RateCardTypes { get; set; }
        public DbSet<MeasuringOfficer> MeasuringOfficers { get; set; }
        public DbSet<BudgetBaseType> BudgetBaseTypes { get; set; }
        public DbSet<BudgetReference> BudgetReferences { get; set; }
        public DbSet<CO2Constant> CO2Constants { get; set; }
        public DbSet<CO2ConstantRow> CO2ConstantRows { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerDocument> CustomerDocuments { get; set; }
        public DbSet<CustomerInfo> CustomerInfos { get; set; }
        public DbSet<AssetManager> AssetManagers { get; set; }
        public DbSet<ServiceRun> ServiceRuns { get; set; }
        public DbSet<ServiceRunError> ServiceRunErrors { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressDebtor> AddressDebtors { get; set; }
        public DbSet<AddressOccupant> AddressOccupants { get; set; }
        public DbSet<AddressRateCard> AddressRateCards { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<BlindConsumption> BlindConsumptions { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetBase> BudgetBases { get; set; }
        public DbSet<BudgetDimension> BudgetDimensions { get; set; }
        public DbSet<BudgetDimensionRule> BudgetDimensionRules { get; set; }
        public DbSet<BudgetDimensionType> BudgetDimensionTypes { get; set; }
        public DbSet<BudgetSectionIndex> BudgetSectionIndex { get; set; }
        public DbSet<BudgetSetting> BudgetSettings { get; set; }
        public DbSet<CalcMutation> CalcMutations { get; set; }
        public DbSet<CalcRule> CalcRules { get; set; }
        public DbSet<CalculationType> CalculationTypes { get; set; }
        public DbSet<ProjectType> SoortProjectTypes { get; set; }
        public DbSet<BuildingManagementSystem> BuildingManagementSystems { get; set; }
        public DbSet<Consumption> Consumption { get; set; }
        public DbSet<ConsumptionMeter> ConsumptionMeters { get; set; }
        public DbSet<ConsumptionMeterSupplier> ConsumptionMeterSuppliers { get; set; }
        public DbSet<ConsumptionUnvalidated> ConsumptionUnvalidated { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<CostlierValue> CostlierValues { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<CounterChangeLog> CounterChangeLogs { get; set; }
        public DbSet<OldCounterStatus> OldCounterStatus { get; set; }
        public DbSet<CounterStatus> CounterStatus { get; set; }
        public DbSet<CounterType> CounterTypes { get; set; }
        public DbSet<CounterTypeYearSeasonPattern> CounterTypeYearCurves { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<CustomerInfo> CustomerInfo { get; set; }
        public DbSet<Debtor> Debtors { get; set; }
        public DbSet<DebtorFile> DebtorFiles { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<DispensingUnit> DispensingUnits { get; set; }
        public DbSet<DistributionNetwork> DistributionNetWorks { get; set; }
        public DbSet<DocumentCategory> DocumentCategories { get; set; }
        public DbSet<DsraDeposit> DsraDeposits { get; set; }
        public DbSet<EmailAddress> EmailAddresses { get; set; }
        public DbSet<EnergyConsumption> EnergyConsumption { get; set; }
        public DbSet<EnergySupplier> EnergySuppliers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<ExchangeForm> ExchangeForms { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Financing> Financings { get; set; }
        public DbSet<Hyperlink> Hyperlinks { get; set; }
        public DbSet<InstallationPartner> InstallationPartners { get; set; }
        public DbSet<InstallationPartnerProcess> InstallationPartnerProcesses { get; set; }
        public DbSet<InvestmentProposal> InvestmentProposals { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceBatch> InvoiceBatches { get; set; }
        public DbSet<InvoiceBatchStatus> InvoiceBatchStatuses { get; set; }
        public DbSet<InvoiceCheckOption> InvoiceCheckOptions { get; set; }
        public DbSet<InvoiceLine> InvoiceLines { get; set; }
        public DbSet<InvoicePeriod> InvoicePeriods { get; set; }
        public DbSet<InvoiceStatus> InvoiceStatuses { get; set; }
        public DbSet<OldInvoiceStatus> OldInvoiceStatuses { get; set; }
        public DbSet<InvoiceType> InvoiceTypes { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<MaintenanceContact> MaintenanceContacts { get; set; }
        public DbSet<MailAttachment> MailAttachments { get; set; }
        public DbSet<MailConfig> MailConfigs { get; set; }
        public DbSet<MaximumPower> MaximumPowers { get; set; }
        public DbSet<MeterChangeLog> MeterChangeLogs { get; set; }
        public DbSet<MeterType> MeterTypes { get; set; }
        public DbSet<MonthDegreeDayIndex> MonthDegreeDayIndex { get; set; }
        public DbSet<NoInvoicePeriods> NoInvoicePeriods { get; set; }
        public DbSet<Occupant> Occupants { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<OpportunityDefault> OpportunityDefaults { get; set; }
        public DbSet<OpportunityKind> OpportunityKinds { get; set; }
        public DbSet<OpportunityNote> OpportunityNotes { get; set; }
        public DbSet<OpportunityStatus> OpportunityStatus { get; set; }
        public DbSet<OpportunityType> OpportunityTypes { get; set; }
        public DbSet<OpportunityValueLog> OpportunityValueLogs { get; set; }
        public DbSet<OtherDelivery> OtherDeliveries { get; set; }
        public DbSet<PaymentHistory> PaymentHistory { get; set; }
        public DbSet<PaymentTermHistory> PaymentTermHistories { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<PeriodPercentage> PeriodPercentages { get; set; }
        public DbSet<ProjectBase> ProjectBases { get; set; }
        public DbSet<ProjectInfo> ProjectInfo { get; set; }
        public DbSet<ProjectReportValidationSetLog> ProjectReportValidationSetLogs { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<ProjectYearDetail> ProjectYearDetails { get; set; }
        public DbSet<RateCard> RateCards { get; set; }
        public DbSet<PurchaseDeliveryType> PurchaseDeliveryTypes { get; set; }
        public DbSet<RateCardRow> RateCardRows { get; set; }
        public DbSet<RateCardScale> RateCardScales { get; set; }
        public DbSet<RateCardScaleHistory> RateCardScaleHistory { get; set; }
        public DbSet<RateCardScaleRow> RateCardScaleRows { get; set; }
        public DbSet<RateCardYear> RateCardYears { get; set; }
        public DbSet<ReferenceProject> ReferenceProjects { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportingStructure> ReportingStructure { get; set; }
        public DbSet<ReportPeriod> ReportPeriods { get; set; }
        public DbSet<ReportValidationSet> ReportValidationSets { get; set; }
        public DbSet<ReportValidationSetLine> ReportValidationSetLines { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<ResponseConcept> ResponseConcepts { get; set; }
        public DbSet<ResponseText> ResponseTexts { get; set; }
        public DbSet<ResponseType> ResponseTypes { get; set; }
        public DbSet<Resultaatoverzicht> Resultaatoverzicht { get; set; }
        public DbSet<RoleGroup> RoleGroups { get; set; }
        public DbSet<Rubric> Rubrics { get; set; }
        public DbSet<SalesDeliveryType> SalesDeliveryTypes { get; set; }
        public DbSet<SeasonalPattern> SeasonalPatterns { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceInvoice> ServiceInvoices { get; set; }
        public DbSet<ServiceInvoiceLine> ServiceInvoiceLines { get; set; }
        public DbSet<ServiceInvoiceLineInput> ServiceInvoiceLineInputs { get; set; }
        public DbSet<ServiceInvoiceLineStatus> ServiceInvoiceLineStatuses { get; set; }
        public DbSet<ServiceInvoiceStatus> ServiceInvoiceStatuses { get; set; }
        public DbSet<ServiceTicketType> ServiceTicketTypes { get; set; }
        public DbSet<ServiceMessage> ServiceMessages { get; set; }
        public DbSet<ServiceTicket> ServiceTickets { get; set; }
        public DbSet<ServiceTicketStatus> ServiceTicketStatuses { get; set; }
        public DbSet<SolutionCategory> SolutionCategories { get; set; }
        public DbSet<Subsidy> Subsidies { get; set; }
        public DbSet<VatCondition> VatConditions { get; set; }
        public DbSet<TechnicalPrincipal> TechnicalPrincipals { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketLog> TicketLogs { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<WebserviceConnection> WebserviceConnections { get; set; }
        public DbSet<WeqMutation> WeqMutations { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<WorkOrderStatus> WorkOrderStatuses { get; set; }
        public DbSet<CounterYearConsumption> YearConsumptions { get; set; }
        public DbSet<YearDegreeDayIndex> YearDegreeDayIndex { get; set; }

        //UserRoles
        public DbSet<ApplicationUserRoleGroup> ApplicationUserRoleGroup { get; set; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { get; set; }
    }
}
