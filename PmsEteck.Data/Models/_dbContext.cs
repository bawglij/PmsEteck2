
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PmsEteck.Data.Models
{
    public class PmsEteckContext : IdentityDbContext<ApplicationUser>
    {
        #region Constructor

        public PmsEteckContext()
        {
        }

        public static PmsEteckContext Create()
        {
            return new PmsEteckContext();
        }

        #endregion

        #region Properties

        public DbSet<Address> Addresses { get; set; }

        public DbSet<AddressDebtor> AddressDebtors { get; set; }

        public DbSet<AddressOccupant> AddressOccupants { get; set; }

        public DbSet<AddressRateCard> AddressRateCards { get; set; }

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<AssetManager> AssetManagers { get; set; }

        public DbSet<BillingType> BillingTypes { get; set; }

        public DbSet<BlindConsumption> BlindConsumptions { get; set; }

        public DbSet<Budget> Budgets { get; set; }

        public DbSet<BudgetBase> BudgetBases { get; set; }

        public DbSet<BudgetReference> BudgetReferences { get; set; }

        public DbSet<BudgetBaseType> BudgetBaseTypes { get; set; }

        public DbSet<BudgetDimension> BudgetDimensions { get; set; }

        public DbSet<BudgetDimensionRule> BudgetDimensionRules { get; set; }

        public DbSet<BudgetDimensionType> BudgetDimensionTypes { get; set; }

        public DbSet<BudgetSectionIndex> BudgetSectionIndex { get; set; }

        public DbSet<BudgetSetting> BudgetSettings { get; set; }

        public DbSet<CalcCategory> CalcCategories { get; set; }

        public DbSet<CalcMutation> CalcMutations { get; set; }

        public DbSet<CalcRule> CalcRules { get; set; }

        public DbSet<CalculationType> CalculationTypes { get; set; }

        public DbSet<ChangeReason> ChangeReasons { get; set; }

        public DbSet<CO2Constant> CO2Constants { get; set; }

        public DbSet<CommunicationType> CommunicationTypes { get; set; }

        public DbSet<ProjectType> SoortProjectTypes { get; set; }
        
        public DbSet<BuildingManagementSystem> BuildingManagementSystems { get; set; }

        public DbSet<CO2ConstantRow> CO2ConstantRows { get; set; }

        public DbSet<ConnectionType> ConnectionTypes { get; set; }

        public DbSet<Consumption> Consumption { get; set; }

        public DbSet<ConsumptionMeter> ConsumptionMeters { get; set; }

        public DbSet<ConsumptionMeterSupplier> ConsumptionMeterSuppliers { get; set; }

        public DbSet<ConsumptionUnvalidated> ConsumptionUnvalidated { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ContactType> ContactTypes { get; set; }

        public DbSet<CostlierValue> CostlierValues { get; set; }

        public DbSet<Counter> Counters { get; set; }

        public DbSet<CounterChangeLog> CounterChangeLogs { get; set; }

        public DbSet<OldCounterStatus> OldCounterStatus { get; set; }

        public DbSet<CounterStatus> CounterStatus { get; set; }

        public DbSet<CounterType> CounterTypes { get; set; }

        public DbSet<CounterTypeYearSeasonPattern> CounterTypeYearCurves { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerAccount> CustomerAccounts { get; set; }

        public DbSet<CustomerDocument> CustomerDocuments { get; set; }

        public DbSet<CustomerInfo> CustomerInfo { get; set; }

        public DbSet<Debtor> Debtors { get; set; }

        public DbSet<DebtorFile> DebtorFiles { get; set; }

        public DbSet<DebtorType> DebtorTypes { get; set; }

        public DbSet<Default> Defaults { get; set; }

        public DbSet<Demarcation> Demarcations { get; set; }

        public DbSet<Deposit> Deposits { get; set; }

        public DbSet<DispensingUnit> DispensingUnits { get; set; }

        public DbSet<DistributionNetwork> DistributionNetWorks { get; set; }

        public DbSet<DocumentCategory> DocumentCategories { get; set; }

        public DbSet<DsraDeposit> DsraDeposits { get; set; }

        public DbSet<EmailAddress> EmailAddresses { get; set; }

        public DbSet<EnergyConcept> EnergyConcept { get; set; }

        public DbSet<EnergyConsumption> EnergyConsumption { get; set; }

        public DbSet<EnergySupplier> EnergySuppliers { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<ExchangeForm> ExchangeForms { get; set; }

        public DbSet<File> Files { get; set; }

        public DbSet<Financer> Financers { get; set; }

        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }

        public DbSet<Financing> Financings { get; set; }

        public DbSet<Hyperlink> Hyperlinks { get; set; }

        public DbSet<InstallationPartner> InstallationPartners { get; set; }

        public DbSet<InstallationPartnerProcess> InstallationPartnerProcesses { get; set; }

        public DbSet<Investment> Investments { get; set; }

        public DbSet<InvestmentProposal> InvestmentProposals { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceBatch> InvoiceBatches { get; set; }

        public DbSet<InvoiceBatchStatus> InvoiceBatchStatuses { get; set; }

        public DbSet<InvoiceCheckOption> InvoiceCheckOptions { get; set; }

        public DbSet<InvoiceCheck> InvoiceChecks { get; set; }

        public DbSet<InvoiceLine> InvoiceLines { get; set; }

        public DbSet<InvoicePeriod> InvoicePeriods { get; set; }

        public DbSet<InvoiceStatus> InvoiceStatuses { get; set; }

        public DbSet<OldInvoiceStatus> OldInvoiceStatuses { get; set; }

        public DbSet<InvoiceType> InvoiceTypes { get; set; }

        public DbSet<Label> Labels { get; set; }

        public DbSet<Log> Logs { get; set; }
       
        public DbSet<LostToCategory> LostToCategories { get; set; }
   
        public DbSet<MaintenanceContact> MaintenanceContacts { get; set; }

        public DbSet<MailAttachment> MailAttachments { get; set; }

        public DbSet<MailConfig> MailConfigs { get; set; }

        public DbSet<MaximumPower> MaximumPowers { get; set; }

        public DbSet<MeasuringOfficer> MeasuringOfficers { get; set; }

        public DbSet<MeterChangeLog> MeterChangeLogs { get; set; }

        public DbSet<Meter> Meters { get; set; }

        public DbSet<MeterType> MeterTypes { get; set; }

        public DbSet<Month> Months { get; set; }

        public DbSet<MonthDegreeDayIndex> MonthDegreeDayIndex { get; set; }

        public DbSet<NoInvoicePeriods> NoInvoicePeriods { get; set; }

        public DbSet<Occupant> Occupants { get; set; }

        public DbSet<Operator> Operators { get; set; }

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

        public DbSet<Period> Periods { get; set; }

        public DbSet<PeriodPercentage> PeriodPercentages { get; set; }

        public DbSet<ProjectBase> ProjectBases { get; set; }

        public DbSet<ProjectInfo> ProjectInfo { get; set; }

        public DbSet<ProjectReportValidationSetLog> ProjectReportValidationSetLogs { get; set; }

        public DbSet<ProjectStatus> ProjectStatuses { get; set; }

        public DbSet<ProjectYearDetail> ProjectYearDetails { get; set; }

        public DbSet<RateCard> RateCards { get; set; }

        public DbSet<Frequency> Frequencies { get; set; }

        public DbSet<PurchaseDeliveryType> PurchaseDeliveryTypes { get; set; }

        public DbSet<RateCardRow> RateCardRows { get; set; }

        public DbSet<RateCardScale> RateCardScales { get; set; }

        public DbSet<RateCardType> RateCardTypes { get; set; }

        public DbSet<RateCardScaleHistory> RateCardScaleHistory { get; set; }

        public DbSet<RateCardScaleRow> RateCardScaleRows { get; set; }

        public DbSet<RateCardYear> RateCardYears { get; set; }

        public DbSet<ReferenceProject> ReferenceProjects { get; set; }

        public DbSet<ReferenceProjectRow> ReferenceProjectRows { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<ReportColumn> ReportColumns { get; set; }

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

        public DbSet<RubricGroup> RubricGroups { get; set; }

        public DbSet<RubricType> RubricTypes { get; set; }

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

        public DbSet<ServiceRun> ServiceRuns { get; set; }

        public DbSet<ServiceRunError> ServiceRunErrors { get; set; }

        public DbSet<ServiceTicket> ServiceTickets { get; set; }

        public DbSet<ServiceTicketStatus> ServiceTicketStatuses { get; set; }

        public DbSet<ShippingProfile> ShippingProfiles { get; set; }

        public DbSet<SolutionCategory> SolutionCategories { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Subsidy> Subsidies { get; set; }

        public DbSet<SubsidyCategory> SubsidyCategories { get; set; }

        public DbSet<SupplyWaterType> SupplyWaterTypes { get; set; }

        public DbSet<VatCondition> VatConditions { get; set; }

        public DbSet<TechnicalPrincipal> TechnicalPrincipals { get; set; }

        public DbSet<TemperatureRange> TemperatureRanges { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<TicketLog> TicketLogs { get; set; }

        public DbSet<TicketStatus> TicketStatuses { get; set; }

        public DbSet<TicketType> TicketTypes { get; set; }

        public DbSet<Title> Titles { get; set; }

        public DbSet<TransactionMode> TransactionModes { get; set; }

        public DbSet<Unit> Units { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }

        public DbSet<ValueOperator> ValueOperators { get; set; }

        public DbSet<WaterType> WaterTypes { get; set; }

        public DbSet<WebserviceConnection> WebserviceConnections { get; set; }

        public DbSet<WeqCategory> WeqCategories { get; set; }

        public DbSet<WeqMutation> WeqMutations { get; set; }

        public DbSet<WorkOrder> WorkOrders { get; set; }

        public DbSet<WorkOrderStatus> WorkOrderStatuses { get; set; }

        public DbSet<CounterYearConsumption> YearConsumptions { get; set; }

        public DbSet<YearDegreeDayIndex> YearDegreeDayIndex { get; set; }

        #endregion

        #region Methods
        public int SaveChanges(string userID)
        {

            foreach (EntityEntry ent in ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
            {
                // For each changed record, get the audit record entries and add them
                foreach (Log x in GetAuditRecordsForChange(ent, userID))
                {
                    Logs.Add(x);
                }
            }

            return base.SaveChanges();
        }

        public async Task SaveChangesAsync(string userID)
        {
            // Get all Added/Deleted/Modified entities (not Unmodified or Detached)
            foreach (EntityEntry ent in ChangeTracker.Entries().Where(p => p.State == EntityState.Added || p.State == EntityState.Deleted || p.State == EntityState.Modified))
            {
                // For each changed record, get the audit record entries and add them
                foreach (Log x in GetAuditRecordsForChange(ent, userID))
                {
                    Logs.Add(x);
                }
            }

            // Call the original SaveChanges(), which will save both the changes made and the audit records
            await base.SaveChangesAsync();
        }

        private List<Log> GetAuditRecordsForChange(EntityEntry dbEntry, string userId)
        {
            List<Log> result = new List<Log>();

            DateTime changeTime = DateTime.UtcNow;

            // Get the Table() attribute, if one exists
            TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;

            // Get table name (if it has a Table attribute, use that, otherwise get the pluralized name)
            string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;

            // Get primary key value (If you have more than one key column, this will need to be adjusted)
            string keyName;
            if (dbEntry.Entity.GetType().GetProperties().Where(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).Count() > 0)
                keyName = dbEntry.Entity.GetType().GetProperties().Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).Name;
            else
                keyName = "0";

            // Oud
            //string keyName = dbEntry.Entity.GetType().GetProperties().Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).Name;

            if (dbEntry.State == EntityState.Added)
            {
                // For Inserts, just add the whole record
                // If the entity implements IDescribableEntity, use the description from Describe(), otherwise use ToString()
                result.Add(new Log()
                {
                    LogID = Guid.NewGuid(),
                    EventType = "Add", // Added
                    TableName = tableName,
                    RecordID = keyName != "0" ? dbEntry.CurrentValues.GetValue<object>(keyName).ToString() : keyName,  // Again, adjust this if you have a multi-column key
                    ColumnName = "*ALL",    // Or make it nullable, whatever you want
                    //NewValue = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ? null : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString(),
                    CreatedByUserID = userId,
                    CreateDateTime = changeTime
                }
                    );
            }
            else if (dbEntry.State == EntityState.Deleted)
            {
                // Same with deletes, do the whole record, and use either the description from Describe() or ToString()
                result.Add(new Log()
                {
                    LogID = Guid.NewGuid(),
                    EventType = "Delete", // Deleted
                    TableName = tableName,
                    RecordID = keyName != "0" ? dbEntry.OriginalValues.GetValue<object>(keyName).ToString() : keyName,
                    ColumnName = "*ALL",
                    //NewValue = (dbEntry.OriginalValues.ToObject() is IDescribableEntity) ? (dbEntry.OriginalValues.ToObject() as IDescribableEntity).Describe() : dbEntry.OriginalValues.ToObject().ToString(),
                    CreatedByUserID = userId,
                    CreateDateTime = changeTime
                }
                    );
            }
            else if (dbEntry.State == EntityState.Modified)
            {
                /*
                foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
                {
                    // For updates, we only want to capture the columns that actually changed
                    if (!Equals(dbEntry.OriginalValues.GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))
                    {
                        result.Add(new Log()
                        {
                            LogID = Guid.NewGuid(),
                            EventType = "Modified",    // Modified
                            TableName = tableName,
                            RecordID = keyName != "0" ? dbEntry.OriginalValues.GetValue<object>(keyName).ToString() : keyName,
                            ColumnName = propertyName,
                            OriginalValue = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ? null : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString(),
                            NewValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ? null : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString(),
                            CreatedByUserID = userId,
                            CreateDateTime = changeTime
                        }
                            );
                    }
                }
                */
                
            }
            // Otherwise, don't do anything, we don't care about Unchanged or Detached entities

            return result;
        }
     
#endregion
    }

}