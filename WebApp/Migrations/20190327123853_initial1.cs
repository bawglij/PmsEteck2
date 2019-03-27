using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PmsEteck.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "pms");

            migrationBuilder.EnsureSchema(
                name: "invoice");

            migrationBuilder.EnsureSchema(
                name: "meter");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.EnsureSchema(
                name: "budget");

            migrationBuilder.EnsureSchema(
                name: "astonmartin");

            migrationBuilder.EnsureSchema(
                name: "service");

            migrationBuilder.EnsureSchema(
                name: "donkervoort");

            migrationBuilder.CreateTable(
                name: "DimCustomers",
                schema: "astonmartin",
                columns: table => new
                {
                    iCustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iPartnerID = table.Column<int>(nullable: false),
                    iGroupID = table.Column<int>(nullable: false),
                    sName = table.Column<string>(maxLength: 100, nullable: false),
                    sClientcode = table.Column<string>(maxLength: 100, nullable: true),
                    sCocnumber = table.Column<string>(maxLength: 100, nullable: true),
                    sSbicode = table.Column<string>(maxLength: 100, nullable: true),
                    sAddress = table.Column<string>(maxLength: 100, nullable: true),
                    sPostalcode = table.Column<string>(maxLength: 20, nullable: true),
                    sCity = table.Column<string>(maxLength: 100, nullable: true),
                    sCountry = table.Column<string>(maxLength: 50, nullable: true),
                    sDescription = table.Column<string>(nullable: true),
                    bParent = table.Column<bool>(nullable: false),
                    bActive = table.Column<bool>(nullable: false),
                    NavisionPrefix = table.Column<string>(maxLength: 30, nullable: true),
                    CustomerMetacom = table.Column<bool>(nullable: false),
                    CustomerNavision = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimCustomers", x => x.iCustomerID);
                });

            migrationBuilder.CreateTable(
                name: "BudgetBaseTypes",
                schema: "budget",
                columns: table => new
                {
                    iBudgetBaseTypeKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sBudgetBaseTypeName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetBaseTypes", x => x.iBudgetBaseTypeKey);
                });

            migrationBuilder.CreateTable(
                name: "BudgetDimensionTypes",
                schema: "budget",
                columns: table => new
                {
                    iBudgetDimensionTypeKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sBudgetDimensionTypeName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetDimensionTypes", x => x.iBudgetDimensionTypeKey);
                });

            migrationBuilder.CreateTable(
                name: "BudgetSettings",
                schema: "budget",
                columns: table => new
                {
                    iBudgetSettingKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dtCreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetSettings", x => x.iBudgetSettingKey);
                });

            migrationBuilder.CreateTable(
                name: "DimLedger",
                schema: "dbo",
                columns: table => new
                {
                    iLedgerKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iPartnerID = table.Column<int>(nullable: true),
                    iCustomerID = table.Column<int>(nullable: true),
                    sLedgerNumber = table.Column<string>(maxLength: 40, nullable: true),
                    sLedgerName = table.Column<string>(maxLength: 60, nullable: true),
                    sLedgerType = table.Column<string>(maxLength: 20, nullable: true),
                    sAccountCategoryCode = table.Column<string>(maxLength: 40, nullable: true),
                    sAccountCategory = table.Column<string>(maxLength: 100, nullable: true),
                    sReportingCode = table.Column<int>(nullable: true),
                    sReportingCode_old = table.Column<int>(nullable: true),
                    iUserKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimLedger", x => x.iLedgerKey);
                });

            migrationBuilder.CreateTable(
                name: "FactFinancialTransactions",
                schema: "dbo",
                columns: table => new
                {
                    iFinancialTransactionKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iPartnerID = table.Column<int>(nullable: false),
                    iCustomerID = table.Column<int>(nullable: false),
                    sIncoiceNumber = table.Column<string>(maxLength: 50, nullable: true),
                    sEntryNumber = table.Column<string>(maxLength: 20, nullable: true),
                    iRelationKey = table.Column<int>(nullable: true),
                    iVATKey = table.Column<int>(nullable: true),
                    sVATCode = table.Column<string>(maxLength: 15, nullable: true),
                    iJournalKey = table.Column<int>(nullable: true),
                    iLedgerKey = table.Column<int>(nullable: true),
                    sDesc = table.Column<string>(maxLength: 255, nullable: true),
                    iCostUnitKey = table.Column<int>(nullable: true),
                    iProjectKey = table.Column<int>(nullable: true),
                    sPeriod = table.Column<string>(maxLength: 10, nullable: true),
                    iTDDateKey = table.Column<int>(nullable: true),
                    iEDDateKey = table.Column<int>(nullable: true),
                    iCurrencyKey = table.Column<int>(nullable: true),
                    iDiscount = table.Column<int>(nullable: true),
                    dcAmountExVat = table.Column<decimal>(nullable: true),
                    dcAmountInclVat = table.Column<decimal>(nullable: true),
                    dcAmountVat = table.Column<decimal>(nullable: true),
                    iCountRecord = table.Column<int>(nullable: true),
                    iSUFKey = table.Column<int>(nullable: false),
                    iUserKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactFinancialTransactions", x => x.iFinancialTransactionKey);
                });

            migrationBuilder.CreateTable(
                name: "Resultaatoverzicht",
                schema: "dbo",
                columns: table => new
                {
                    iCustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecNo = table.Column<int>(nullable: false),
                    sSpatie = table.Column<string>(nullable: true),
                    sDescription = table.Column<string>(nullable: true),
                    iSubTotal = table.Column<int>(nullable: true),
                    SaldoRealisatie = table.Column<decimal>(nullable: true),
                    SaldoBudget = table.Column<decimal>(nullable: true),
                    VastVariabel = table.Column<int>(nullable: true),
                    Berekenen = table.Column<int>(nullable: true),
                    sOpmaak = table.Column<string>(nullable: true),
                    iYear = table.Column<int>(nullable: true),
                    iPeriod = table.Column<int>(nullable: true),
                    iProjectKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resultaatoverzicht", x => x.iCustomerID);
                });

            migrationBuilder.CreateTable(
                name: "ReportingStructure",
                schema: "donkervoort",
                columns: table => new
                {
                    iReportingStructureKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecNo = table.Column<int>(nullable: true),
                    RcStart = table.Column<int>(nullable: true),
                    RcEnd = table.Column<int>(nullable: true),
                    sSpatie = table.Column<string>(maxLength: 50, nullable: true),
                    sDescription = table.Column<string>(maxLength: 255, nullable: true),
                    isFlipCode = table.Column<int>(nullable: true),
                    iBalResKey = table.Column<int>(nullable: true),
                    Berekenen = table.Column<int>(nullable: true),
                    Niveau = table.Column<string>(maxLength: 50, nullable: true),
                    RS_JaarTerug = table.Column<int>(nullable: true),
                    RS_VordSchuld = table.Column<int>(nullable: true),
                    RS_IsStand = table.Column<int>(nullable: true),
                    IsDirect = table.Column<int>(nullable: true),
                    Bedrijfsresultaat = table.Column<int>(nullable: true),
                    KostenTotaal = table.Column<int>(nullable: true),
                    Leningen = table.Column<int>(nullable: true),
                    KortlSchuldenVorderingen = table.Column<int>(nullable: true),
                    Personeelskosten = table.Column<int>(nullable: true),
                    bk_Splitsing = table.Column<int>(nullable: true),
                    Kosten = table.Column<int>(nullable: true),
                    Omzet = table.Column<int>(nullable: true),
                    KostprijsOmzet = table.Column<int>(nullable: true),
                    Marge = table.Column<int>(nullable: true),
                    wk_Debiteuren = table.Column<int>(nullable: true),
                    wk_Crediteuren = table.Column<int>(nullable: true),
                    wk_LiquideMiddelen = table.Column<int>(nullable: true),
                    wk_Belastingen = table.Column<int>(nullable: true),
                    EigenVermogen = table.Column<int>(nullable: true),
                    TotaalVermogen = table.Column<int>(nullable: true),
                    CurRat_Activa = table.Column<int>(nullable: true),
                    CurRat_Passiva = table.Column<int>(nullable: true),
                    sCalculation = table.Column<string>(maxLength: 50, nullable: true),
                    iCalculation = table.Column<int>(nullable: true),
                    VastVariabel = table.Column<int>(nullable: true),
                    Factor = table.Column<int>(nullable: true),
                    sOpmaak = table.Column<string>(maxLength: 50, nullable: true),
                    PercTonen = table.Column<int>(nullable: true),
                    iProjectrapportagType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportingStructure", x => x.iReportingStructureKey);
                });

            migrationBuilder.CreateTable(
                name: "BillingTypes",
                schema: "invoice",
                columns: table => new
                {
                    BillingTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 150, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingTypes", x => x.BillingTypeID);
                });

            migrationBuilder.CreateTable(
                name: "DebtorTypes",
                schema: "invoice",
                columns: table => new
                {
                    iDebtorTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebtorTypes", x => x.iDebtorTypeID);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceCheckOptions",
                schema: "invoice",
                columns: table => new
                {
                    InvoiceCheckOptionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceCheckOptions", x => x.InvoiceCheckOptionID);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePeriods",
                schema: "invoice",
                columns: table => new
                {
                    InvoicePeriodID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePeriods", x => x.InvoicePeriodID);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceStatuses",
                schema: "invoice",
                columns: table => new
                {
                    iStatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sStatus = table.Column<string>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceStatuses", x => x.iStatusID);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceTypes",
                schema: "invoice",
                columns: table => new
                {
                    iInvoiceTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sInvoiceTypeName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTypes", x => x.iInvoiceTypeID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTerms",
                schema: "invoice",
                columns: table => new
                {
                    iPaymentTermID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sCode = table.Column<string>(maxLength: 20, nullable: false),
                    sDescription = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTerms", x => x.iPaymentTermID);
                });

            migrationBuilder.CreateTable(
                name: "ShippingProfiles",
                schema: "invoice",
                columns: table => new
                {
                    iShippingProfileID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sCode = table.Column<string>(maxLength: 20, nullable: false),
                    sDescription = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingProfiles", x => x.iShippingProfileID);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                schema: "invoice",
                columns: table => new
                {
                    iTitleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sTitleName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.iTitleID);
                });

            migrationBuilder.CreateTable(
                name: "ChangeReasons",
                schema: "meter",
                columns: table => new
                {
                    iChangeReasonKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sDescription = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeReasons", x => x.iChangeReasonKey);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionTypes",
                schema: "meter",
                columns: table => new
                {
                    sConnectionTypeKey = table.Column<string>(maxLength: 2, nullable: false),
                    sConnectionTypeDescription = table.Column<string>(maxLength: 100, nullable: false),
                    bConsumptionMeterRequired = table.Column<bool>(nullable: false),
                    bActive = table.Column<bool>(nullable: false),
                    IsStandingRight = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionTypes", x => x.sConnectionTypeKey);
                });

            migrationBuilder.CreateTable(
                name: "ConsumptionMeterSuppliers",
                schema: "meter",
                columns: table => new
                {
                    iConsumptionMeterSupplierKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sConsumptionMeterSupplier = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptionMeterSuppliers", x => x.iConsumptionMeterSupplierKey);
                });

            migrationBuilder.CreateTable(
                name: "CounterTypes",
                schema: "meter",
                columns: table => new
                {
                    iCounterTypeKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sCounterTypeDescription = table.Column<string>(maxLength: 100, nullable: false),
                    bActive = table.Column<bool>(nullable: false),
                    iOrder = table.Column<int>(nullable: false),
                    bCanExchange = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CounterTypes", x => x.iCounterTypeKey);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                schema: "meter",
                columns: table => new
                {
                    iEventKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sEventDescription = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.iEventKey);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeForms",
                schema: "meter",
                columns: table => new
                {
                    iExchangeFormKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sFileName = table.Column<string>(maxLength: 255, nullable: true),
                    sContentType = table.Column<string>(maxLength: 100, nullable: true),
                    bContent = table.Column<byte[]>(nullable: true),
                    FileType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeForms", x => x.iExchangeFormKey);
                });

            migrationBuilder.CreateTable(
                name: "Frequencies",
                schema: "meter",
                columns: table => new
                {
                    iFrequencyKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sFrequencyDescription = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequencies", x => x.iFrequencyKey);
                });

            migrationBuilder.CreateTable(
                name: "MeterTypes",
                schema: "meter",
                columns: table => new
                {
                    iMeterTypeKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sDescription = table.Column<string>(maxLength: 50, nullable: false),
                    sPurchaseOrSale = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeterTypes", x => x.iMeterTypeKey);
                });

            migrationBuilder.CreateTable(
                name: "RateCardTypes",
                schema: "meter",
                columns: table => new
                {
                    iRateCardTypeKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sDescription = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateCardTypes", x => x.iRateCardTypeKey);
                });

            migrationBuilder.CreateTable(
                name: "ReportPeriods",
                schema: "meter",
                columns: table => new
                {
                    iReportPeriodKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iYear = table.Column<int>(nullable: false),
                    iPeriod = table.Column<int>(nullable: false),
                    dtPeriodDate = table.Column<DateTime>(nullable: false),
                    bBlocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportPeriods", x => x.iReportPeriodKey);
                });

            migrationBuilder.CreateTable(
                name: "RubricGroups",
                schema: "meter",
                columns: table => new
                {
                    iRubricGroupKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sRubricGroupDescription = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubricGroups", x => x.iRubricGroupKey);
                });

            migrationBuilder.CreateTable(
                name: "RubricTypes",
                schema: "meter",
                columns: table => new
                {
                    iRubricTypeKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sRubricTypeDescription = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubricTypes", x => x.iRubricTypeKey);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "meter",
                columns: table => new
                {
                    iServiceKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sServiceName = table.Column<string>(maxLength: 100, nullable: false),
                    dtNextServiceRun = table.Column<DateTime>(nullable: true),
                    sServiceType = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.iServiceKey);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                schema: "meter",
                columns: table => new
                {
                    iUnitKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sDescription = table.Column<string>(maxLength: 150, nullable: false),
                    sUnit = table.Column<string>(maxLength: 20, nullable: false),
                    bActive = table.Column<bool>(nullable: false),
                    iOrder = table.Column<int>(nullable: false),
                    bUsedForCounter = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.iUnitKey);
                });

            migrationBuilder.CreateTable(
                name: "VatConditions",
                schema: "meter",
                columns: table => new
                {
                    VatConditionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VatConditions", x => x.VatConditionID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleGroups",
                schema: "pms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "pms",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserGroups",
                schema: "pms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetManager",
                schema: "pms",
                columns: table => new
                {
                    iAssetManagerKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sFirstName = table.Column<string>(maxLength: 150, nullable: false),
                    sLastName = table.Column<string>(maxLength: 150, nullable: false),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetManager", x => x.iAssetManagerKey);
                });

            migrationBuilder.CreateTable(
                name: "CalcCategories",
                schema: "pms",
                columns: table => new
                {
                    iCalcCategoryKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sCalcCategory = table.Column<string>(maxLength: 100, nullable: false),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalcCategories", x => x.iCalcCategoryKey);
                });

            migrationBuilder.CreateTable(
                name: "CalculationType",
                schema: "pms",
                columns: table => new
                {
                    iCalculationTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationType", x => x.iCalculationTypeID);
                });

            migrationBuilder.CreateTable(
                name: "CO2Constants",
                schema: "pms",
                columns: table => new
                {
                    iCO2ConstantKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dtCreateDateTime = table.Column<DateTime>(nullable: false),
                    sCreatedBy = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CO2Constants", x => x.iCO2ConstantKey);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationTypes",
                schema: "pms",
                columns: table => new
                {
                    ComunicationTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationTypes", x => x.ComunicationTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ContactTypes",
                schema: "pms",
                columns: table => new
                {
                    iContactTypeKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sContactTypeName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.iContactTypeKey);
                });

            migrationBuilder.CreateTable(
                name: "Defaults",
                schema: "pms",
                columns: table => new
                {
                    iDefaultID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sName = table.Column<string>(maxLength: 150, nullable: true),
                    dValue = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Year = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Defaults", x => x.iDefaultID);
                });

            migrationBuilder.CreateTable(
                name: "Demarcations",
                schema: "pms",
                columns: table => new
                {
                    iDemarcationKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sDemarcation = table.Column<string>(maxLength: 100, nullable: false),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demarcations", x => x.iDemarcationKey);
                });

            migrationBuilder.CreateTable(
                name: "DispensingUnits",
                schema: "pms",
                columns: table => new
                {
                    iDispensingUnitKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sDispensingUnitName = table.Column<string>(maxLength: 100, nullable: true),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DispensingUnits", x => x.iDispensingUnitKey);
                });

            migrationBuilder.CreateTable(
                name: "DistributionNetWorks",
                schema: "pms",
                columns: table => new
                {
                    iDistributionNetWorkKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sDistributionNetWorkName = table.Column<string>(maxLength: 25, nullable: true),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributionNetWorks", x => x.iDistributionNetWorkKey);
                });

            migrationBuilder.CreateTable(
                name: "DocumentCategories",
                schema: "pms",
                columns: table => new
                {
                    iDocumentCategoryKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sName = table.Column<string>(maxLength: 50, nullable: false),
                    sExplanation = table.Column<string>(maxLength: 250, nullable: true),
                    iSorting = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentCategories", x => x.iDocumentCategoryKey);
                });

            migrationBuilder.CreateTable(
                name: "DsraDeposits",
                schema: "pms",
                columns: table => new
                {
                    iDsraDepositKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sDsraDepositName = table.Column<string>(maxLength: 150, nullable: false),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsraDeposits", x => x.iDsraDepositKey);
                });

            migrationBuilder.CreateTable(
                name: "EnergyConcepts",
                schema: "pms",
                columns: table => new
                {
                    EnergyConceptID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    EnglishDescription = table.Column<string>(maxLength: 250, nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyConcepts", x => x.EnergyConceptID);
                });

            migrationBuilder.CreateTable(
                name: "EnergySuppliers",
                schema: "pms",
                columns: table => new
                {
                    iEnergySupplierID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sName = table.Column<string>(maxLength: 100, nullable: true),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergySuppliers", x => x.iEnergySupplierID);
                });

            migrationBuilder.CreateTable(
                name: "Financers",
                schema: "pms",
                columns: table => new
                {
                    iFinancerKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sFinancer = table.Column<string>(maxLength: 100, nullable: false),
                    sLedgerNumberInterest = table.Column<string>(maxLength: 50, nullable: true),
                    sLedgerNumberAmortization = table.Column<string>(maxLength: 50, nullable: true),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financers", x => x.iFinancerKey);
                });

            migrationBuilder.CreateTable(
                name: "InstallationPartnerProcesses",
                schema: "pms",
                columns: table => new
                {
                    InstallationPartnerProcessID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    EnglishDescription = table.Column<string>(maxLength: 250, nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallationPartnerProcesses", x => x.InstallationPartnerProcessID);
                });

            migrationBuilder.CreateTable(
                name: "InstallationPartners",
                schema: "pms",
                columns: table => new
                {
                    InstallationPartnerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallationPartners", x => x.InstallationPartnerID);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentProposals",
                schema: "pms",
                columns: table => new
                {
                    InvestmentProposalID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    EnglishDescription = table.Column<string>(maxLength: 100, nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentProposals", x => x.InvestmentProposalID);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                schema: "pms",
                columns: table => new
                {
                    LogID = table.Column<Guid>(nullable: false),
                    EventType = table.Column<string>(nullable: false),
                    TableName = table.Column<string>(nullable: false),
                    RecordID = table.Column<string>(nullable: false),
                    ColumnName = table.Column<string>(nullable: true),
                    OriginalValue = table.Column<string>(nullable: true),
                    NewValue = table.Column<string>(nullable: true),
                    CreatedByUserID = table.Column<string>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.LogID);
                });

            migrationBuilder.CreateTable(
                name: "LostToCategories",
                schema: "pms",
                columns: table => new
                {
                    LostToCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 128, nullable: true),
                    EnglishDescription = table.Column<string>(maxLength: 128, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LostToCategories", x => x.LostToCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "MailConfigs",
                schema: "pms",
                columns: table => new
                {
                    MailConfigID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Mailbox = table.Column<string>(nullable: true),
                    ReadMailbox = table.Column<bool>(nullable: false),
                    ReadFolder = table.Column<string>(nullable: true),
                    ArchiveFolder = table.Column<string>(nullable: true),
                    ArchiveMail = table.Column<bool>(nullable: false),
                    ServiceUrl = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailConfigs", x => x.MailConfigID);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceContacts",
                schema: "pms",
                columns: table => new
                {
                    iMaintenanceContactKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sOrganisation = table.Column<string>(maxLength: 50, nullable: false),
                    sErrorNumber1 = table.Column<string>(maxLength: 15, nullable: false),
                    sErrorNumber2 = table.Column<string>(maxLength: 15, nullable: true),
                    sEmail = table.Column<string>(maxLength: 50, nullable: true),
                    sContactName = table.Column<string>(maxLength: 50, nullable: true),
                    sContactPhone = table.Column<string>(maxLength: 15, nullable: true),
                    sContactEmail = table.Column<string>(maxLength: 50, nullable: true),
                    UnitPriceHourlyRate = table.Column<decimal>(nullable: true),
                    UnitPriceCallOutHours = table.Column<decimal>(nullable: true),
                    UnitPriceCallOutKilometers = table.Column<decimal>(nullable: true),
                    ActiveService = table.Column<bool>(nullable: false),
                    VendorCode = table.Column<string>(maxLength: 20, nullable: true),
                    MaintenanceContactCommunicationType = table.Column<int>(nullable: false),
                    GlobalLocationNumber = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceContacts", x => x.iMaintenanceContactKey);
                });

            migrationBuilder.CreateTable(
                name: "MeasuringOfficers",
                schema: "pms",
                columns: table => new
                {
                    iMeasuringOfficerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sName = table.Column<string>(maxLength: 100, nullable: true),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasuringOfficers", x => x.iMeasuringOfficerID);
                });

            migrationBuilder.CreateTable(
                name: "Meters",
                schema: "pms",
                columns: table => new
                {
                    iMeterKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sMeter = table.Column<string>(maxLength: 100, nullable: false),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meters", x => x.iMeterKey);
                });

            migrationBuilder.CreateTable(
                name: "Months",
                schema: "pms",
                columns: table => new
                {
                    iMonthKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sMonthName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.iMonthKey);
                });

            migrationBuilder.CreateTable(
                name: "Operators",
                schema: "pms",
                columns: table => new
                {
                    iOperatorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sName = table.Column<string>(maxLength: 100, nullable: true),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.iOperatorID);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityDefaults",
                schema: "pms",
                columns: table => new
                {
                    iOpportunityDefaultID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sName = table.Column<string>(maxLength: 50, nullable: false),
                    dValue = table.Column<decimal>(type: "decimal(18, 5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityDefaults", x => x.iOpportunityDefaultID);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityKinds",
                schema: "pms",
                columns: table => new
                {
                    OpportunityKindID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    EnglishDescription = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityKinds", x => x.OpportunityKindID);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityStatus",
                schema: "pms",
                columns: table => new
                {
                    OpportunityStatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    EnglishDescription = table.Column<string>(maxLength: 100, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    ShowInTimeline = table.Column<bool>(nullable: false),
                    TimespanFromRequestDate = table.Column<int>(nullable: false),
                    TimespanFromPreviousStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityStatus", x => x.OpportunityStatusID);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityTypes",
                schema: "pms",
                columns: table => new
                {
                    OpportunityTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: false),
                    EnglishDescription = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityTypes", x => x.OpportunityTypeID);
                });

            migrationBuilder.CreateTable(
                name: "OtherDeliveries",
                schema: "pms",
                columns: table => new
                {
                    iOtherDeliveryKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sOtherDelivery = table.Column<string>(maxLength: 50, nullable: true),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherDeliveries", x => x.iOtherDeliveryKey);
                });

            migrationBuilder.CreateTable(
                name: "Periods",
                schema: "pms",
                columns: table => new
                {
                    iPeriodKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sPeriod = table.Column<string>(maxLength: 100, nullable: false),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.iPeriodKey);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStatuses",
                schema: "pms",
                columns: table => new
                {
                    iProjectStatusID = table.Column<int>(nullable: false),
                    sStatusDescription = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStatuses", x => x.iProjectStatusID);
                });

            migrationBuilder.CreateTable(
                name: "ProjectType",
                schema: "pms",
                columns: table => new
                {
                    ProjectTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectType", x => x.ProjectTypeID);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDeliveryTypes",
                schema: "pms",
                columns: table => new
                {
                    iPurchaseDeliveryTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sDescription = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDeliveryTypes", x => x.iPurchaseDeliveryTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceProjects",
                schema: "pms",
                columns: table => new
                {
                    iReferenceProjectKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sReferenProjectName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceProjects", x => x.iReferenceProjectKey);
                });

            migrationBuilder.CreateTable(
                name: "ReportColumns",
                schema: "pms",
                columns: table => new
                {
                    iReportColumnKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sReportingColumnName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportColumns", x => x.iReportColumnKey);
                });

            migrationBuilder.CreateTable(
                name: "SalesDeliveryTypes",
                schema: "pms",
                columns: table => new
                {
                    iSalesDeliveryTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sDescription = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesDeliveryTypes", x => x.iSalesDeliveryTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                schema: "pms",
                columns: table => new
                {
                    StatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 128, nullable: true),
                    StatusCode = table.Column<int>(nullable: false),
                    StatusColor = table.Column<string>(maxLength: 50, nullable: true),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "SubsidyCategories",
                schema: "pms",
                columns: table => new
                {
                    iSubsidyCategoryKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sSubsidyCategory = table.Column<string>(maxLength: 150, nullable: false),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubsidyCategories", x => x.iSubsidyCategoryKey);
                });

            migrationBuilder.CreateTable(
                name: "SupplyWaterTypes",
                schema: "pms",
                columns: table => new
                {
                    iSupplyWaterTypeKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sSupplyWaterType = table.Column<string>(maxLength: 50, nullable: true),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyWaterTypes", x => x.iSupplyWaterTypeKey);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalPrincipals",
                schema: "pms",
                columns: table => new
                {
                    iTechnicalPrincipalKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sTechnicalPrincipal = table.Column<string>(maxLength: 150, nullable: false),
                    EnglishDescription = table.Column<string>(maxLength: 150, nullable: false),
                    bIsGas = table.Column<bool>(nullable: false),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalPrincipals", x => x.iTechnicalPrincipalKey);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureRanges",
                schema: "pms",
                columns: table => new
                {
                    iTemperatureRangeKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sTemperatureRange = table.Column<string>(maxLength: 100, nullable: true),
                    bActive = table.Column<bool>(nullable: false),
                    sHeatOrCold = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureRanges", x => x.iTemperatureRangeKey);
                });

            migrationBuilder.CreateTable(
                name: "TransactionModes",
                schema: "pms",
                columns: table => new
                {
                    iTransactionModeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sCodePerson = table.Column<string>(maxLength: 20, nullable: false),
                    sCodeBusiness = table.Column<string>(maxLength: 20, nullable: false),
                    sCodeNonIncasso = table.Column<string>(maxLength: 20, nullable: false),
                    sDescription = table.Column<string>(maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionModes", x => x.iTransactionModeID);
                });

            migrationBuilder.CreateTable(
                name: "ValueOperators",
                schema: "pms",
                columns: table => new
                {
                    ValueOperatorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Operator = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueOperators", x => x.ValueOperatorID);
                });

            migrationBuilder.CreateTable(
                name: "WaterTypes",
                schema: "pms",
                columns: table => new
                {
                    iWaterTypeKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sWaterType = table.Column<string>(maxLength: 100, nullable: true),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterTypes", x => x.iWaterTypeKey);
                });

            migrationBuilder.CreateTable(
                name: "WeqCategories",
                schema: "pms",
                columns: table => new
                {
                    iWeqCategoryKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sWeqCategory = table.Column<string>(maxLength: 100, nullable: false),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeqCategories", x => x.iWeqCategoryKey);
                });

            migrationBuilder.CreateTable(
                name: "Labels",
                schema: "service",
                columns: table => new
                {
                    iLabelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sDescription = table.Column<string>(maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.iLabelID);
                });

            migrationBuilder.CreateTable(
                name: "ResponseTypes",
                schema: "service",
                columns: table => new
                {
                    iResponseTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sName = table.Column<string>(maxLength: 50, nullable: false),
                    sDisplayName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseTypes", x => x.iResponseTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTicketTypes",
                schema: "service",
                columns: table => new
                {
                    ServiceTicketTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTicketTypes", x => x.ServiceTicketTypeID);
                });

            migrationBuilder.CreateTable(
                name: "SolutionCategories",
                schema: "service",
                columns: table => new
                {
                    SolutionCategoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolutionCategories", x => x.SolutionCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "TicketStatuses",
                schema: "service",
                columns: table => new
                {
                    iTicketStatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatuses", x => x.iTicketStatusID);
                });

            migrationBuilder.CreateTable(
                name: "TicketTypes",
                schema: "service",
                columns: table => new
                {
                    iTicketTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketTypes", x => x.iTicketTypeID);
                });

            migrationBuilder.CreateTable(
                name: "DimProject",
                schema: "dbo",
                columns: table => new
                {
                    iProjectKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iCustomerID = table.Column<int>(nullable: false),
                    iPartnerID = table.Column<int>(nullable: false),
                    sProjectCode = table.Column<string>(maxLength: 50, nullable: true),
                    sProjectDescription = table.Column<string>(maxLength: 255, nullable: true),
                    sProjectCategoryCode = table.Column<string>(maxLength: 50, nullable: true),
                    sProjectCategoryDescription = table.Column<string>(maxLength: 255, nullable: true),
                    dStartDate = table.Column<string>(maxLength: 255, nullable: true),
                    dEndDate = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimProject", x => x.iProjectKey);
                    table.ForeignKey(
                        name: "FK_DimProject_DimCustomers_iCustomerID",
                        column: x => x.iCustomerID,
                        principalSchema: "astonmartin",
                        principalTable: "DimCustomers",
                        principalColumn: "iCustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerInfo",
                schema: "pms",
                columns: table => new
                {
                    iCustomerKey = table.Column<int>(nullable: false),
                    sCocNumber = table.Column<string>(maxLength: 8, nullable: false),
                    sEstablishmentNumber = table.Column<string>(maxLength: 12, nullable: true),
                    sSbiCode = table.Column<string>(maxLength: 250, nullable: true),
                    sBtwNumber = table.Column<string>(maxLength: 14, nullable: true),
                    dtFoundationDate = table.Column<DateTime>(nullable: false),
                    dtInControlDate = table.Column<DateTime>(nullable: false),
                    bExcludeForProjectBase = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInfo", x => x.iCustomerKey);
                    table.ForeignKey(
                        name: "FK_CustomerInfo_DimCustomers_iCustomerKey",
                        column: x => x.iCustomerKey,
                        principalSchema: "astonmartin",
                        principalTable: "DimCustomers",
                        principalColumn: "iCustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetBases",
                schema: "budget",
                columns: table => new
                {
                    iBudgetBaseKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iCustomerID = table.Column<int>(nullable: false),
                    iBudgetBaseTypeKey = table.Column<int>(nullable: false),
                    iYear = table.Column<int>(nullable: false),
                    sDescription = table.Column<string>(maxLength: 250, nullable: true),
                    bActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetBases", x => x.iBudgetBaseKey);
                    table.ForeignKey(
                        name: "FK_BudgetBases_BudgetBaseTypes_iBudgetBaseTypeKey",
                        column: x => x.iBudgetBaseTypeKey,
                        principalSchema: "budget",
                        principalTable: "BudgetBaseTypes",
                        principalColumn: "iBudgetBaseTypeKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetBases_DimCustomers_iCustomerID",
                        column: x => x.iCustomerID,
                        principalSchema: "astonmartin",
                        principalTable: "DimCustomers",
                        principalColumn: "iCustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YearDegreeDayIndex",
                schema: "budget",
                columns: table => new
                {
                    iYearDegreeIndexKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iBudgetSettingKey = table.Column<int>(nullable: false),
                    iYear = table.Column<int>(nullable: false),
                    dDegreeDayIndex = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearDegreeDayIndex", x => x.iYearDegreeIndexKey);
                    table.ForeignKey(
                        name: "FK_YearDegreeDayIndex_BudgetSettings_iBudgetSettingKey",
                        column: x => x.iBudgetSettingKey,
                        principalSchema: "budget",
                        principalTable: "BudgetSettings",
                        principalColumn: "iBudgetSettingKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetSectionIndex",
                schema: "budget",
                columns: table => new
                {
                    iBudgetSectionIndexKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iBudgetSettingKey = table.Column<int>(nullable: false),
                    iReportingStructureKey = table.Column<int>(nullable: false),
                    iRecNo = table.Column<int>(nullable: false),
                    bVariable = table.Column<bool>(nullable: false),
                    dSectionIndex = table.Column<decimal>(nullable: false),
                    bSpatie = table.Column<bool>(nullable: false),
                    bSubtotaal = table.Column<bool>(nullable: false),
                    bFixedPart = table.Column<bool>(nullable: false),
                    dFixedPart = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetSectionIndex", x => x.iBudgetSectionIndexKey);
                    table.ForeignKey(
                        name: "FK_BudgetSectionIndex_BudgetSettings_iBudgetSettingKey",
                        column: x => x.iBudgetSettingKey,
                        principalSchema: "budget",
                        principalTable: "BudgetSettings",
                        principalColumn: "iBudgetSettingKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetSectionIndex_ReportingStructure_iReportingStructureKey",
                        column: x => x.iReportingStructureKey,
                        principalSchema: "donkervoort",
                        principalTable: "ReportingStructure",
                        principalColumn: "iReportingStructureKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Debtors",
                schema: "invoice",
                columns: table => new
                {
                    iDebtorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iDebtorCode = table.Column<int>(nullable: false),
                    iShippingProfileID = table.Column<int>(nullable: false),
                    sCustomerPostingGroup = table.Column<string>(maxLength: 10, nullable: false),
                    iPaymentTermID = table.Column<int>(nullable: false),
                    sReminderTermsCode = table.Column<string>(maxLength: 10, nullable: false),
                    sVATBusPostingGroup = table.Column<string>(maxLength: 10, nullable: false),
                    iPartnerType = table.Column<int>(nullable: false),
                    iTitleID = table.Column<int>(nullable: false),
                    sInitials = table.Column<string>(maxLength: 50, nullable: true),
                    sFirstName = table.Column<string>(maxLength: 250, nullable: true),
                    sLastName = table.Column<string>(maxLength: 250, nullable: false),
                    sPhoneNumber = table.Column<string>(maxLength: 30, nullable: true),
                    sEmailAddress = table.Column<string>(maxLength: 80, nullable: true),
                    sRemark = table.Column<string>(maxLength: 250, nullable: true),
                    sVATNumber = table.Column<string>(maxLength: 80, nullable: true),
                    sIBANNumber = table.Column<string>(maxLength: 50, nullable: true),
                    sSWIFTCode = table.Column<string>(maxLength: 20, nullable: true),
                    sInvoiceRemark = table.Column<string>(maxLength: 50, nullable: true),
                    bIsVacancy = table.Column<bool>(nullable: false),
                    bIsBlocked = table.Column<bool>(nullable: false),
                    sBillingName = table.Column<string>(maxLength: 50, nullable: false),
                    sBillingAddress = table.Column<string>(maxLength: 50, nullable: false),
                    sBillingPostalCode = table.Column<string>(maxLength: 20, nullable: false),
                    sBillingCity = table.Column<string>(maxLength: 30, nullable: false),
                    sBillingCountry = table.Column<string>(maxLength: 10, nullable: false),
                    iDebtorTypeID = table.Column<int>(nullable: false),
                    bWaitingForImport = table.Column<bool>(nullable: false),
                    dtDateOfBirth = table.Column<DateTime>(nullable: true),
                    bMailboxAddress = table.Column<bool>(nullable: false),
                    bNoCombinedInvoice = table.Column<bool>(nullable: false),
                    ReceiveNewsletter = table.Column<bool>(nullable: false),
                    dtNoInvoicePeriod = table.Column<DateTime>(nullable: true),
                    InvoicePeriodID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debtors", x => x.iDebtorID);
                    table.ForeignKey(
                        name: "FK_Debtors_InvoicePeriods_InvoicePeriodID",
                        column: x => x.InvoicePeriodID,
                        principalSchema: "invoice",
                        principalTable: "InvoicePeriods",
                        principalColumn: "InvoicePeriodID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Debtors_DebtorTypes_iDebtorTypeID",
                        column: x => x.iDebtorTypeID,
                        principalSchema: "invoice",
                        principalTable: "DebtorTypes",
                        principalColumn: "iDebtorTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Debtors_PaymentTerms_iPaymentTermID",
                        column: x => x.iPaymentTermID,
                        principalSchema: "invoice",
                        principalTable: "PaymentTerms",
                        principalColumn: "iPaymentTermID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Debtors_ShippingProfiles_iShippingProfileID",
                        column: x => x.iShippingProfileID,
                        principalSchema: "invoice",
                        principalTable: "ShippingProfiles",
                        principalColumn: "iShippingProfileID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Debtors_Titles_iTitleID",
                        column: x => x.iTitleID,
                        principalSchema: "invoice",
                        principalTable: "Titles",
                        principalColumn: "iTitleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RateCards",
                schema: "meter",
                columns: table => new
                {
                    iRateCardKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iRateCardTypeKey = table.Column<int>(nullable: true),
                    sDescription = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateCards", x => x.iRateCardKey);
                    table.ForeignKey(
                        name: "FK_RateCards_RateCardTypes_iRateCardTypeKey",
                        column: x => x.iRateCardTypeKey,
                        principalSchema: "meter",
                        principalTable: "RateCardTypes",
                        principalColumn: "iRateCardTypeKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rubrics",
                schema: "meter",
                columns: table => new
                {
                    iRubricKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sRubricDescription = table.Column<string>(maxLength: 150, nullable: false),
                    iRowNo = table.Column<int>(nullable: false),
                    iRowNoStart = table.Column<int>(nullable: false),
                    iRowNoEnd = table.Column<int>(nullable: false),
                    bTotal = table.Column<bool>(nullable: false),
                    iReportingCode = table.Column<int>(nullable: false),
                    iRubricGroupKey = table.Column<int>(nullable: false),
                    iAccountNumber = table.Column<int>(nullable: false),
                    iCounterAccountNumber = table.Column<int>(nullable: false),
                    iRubricTypeKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubrics", x => x.iRubricKey);
                    table.ForeignKey(
                        name: "FK_Rubrics_RubricGroups_iRubricGroupKey",
                        column: x => x.iRubricGroupKey,
                        principalSchema: "meter",
                        principalTable: "RubricGroups",
                        principalColumn: "iRubricGroupKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rubrics_RubricTypes_iRubricTypeKey",
                        column: x => x.iRubricTypeKey,
                        principalSchema: "meter",
                        principalTable: "RubricTypes",
                        principalColumn: "iRubricTypeKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRuns",
                schema: "meter",
                columns: table => new
                {
                    iServiceRunKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iServiceKey = table.Column<int>(nullable: false),
                    dtServiceRunStartDate = table.Column<DateTime>(nullable: false),
                    dtServiceRunEndDate = table.Column<DateTime>(nullable: true),
                    iServiceRunStatus = table.Column<int>(nullable: false),
                    iServiceRunRowsUpdated = table.Column<int>(nullable: false),
                    sServiceRunMessage = table.Column<string>(maxLength: 1000, nullable: true),
                    sFileName = table.Column<string>(maxLength: 250, nullable: true),
                    bInQueue = table.Column<bool>(nullable: false),
                    iNumberOfReprocessed = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRuns", x => x.iServiceRunKey);
                    table.ForeignKey(
                        name: "FK_ServiceRuns_Services_iServiceKey",
                        column: x => x.iServiceKey,
                        principalSchema: "meter",
                        principalTable: "Services",
                        principalColumn: "iServiceKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RateCardScales",
                schema: "meter",
                columns: table => new
                {
                    iRateCardScaleKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sRateCardScaleDescription = table.Column<string>(maxLength: 100, nullable: false),
                    iUnitKey = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateCardScales", x => x.iRateCardScaleKey);
                    table.ForeignKey(
                        name: "FK_RateCardScales_Units_iUnitKey",
                        column: x => x.iUnitKey,
                        principalSchema: "meter",
                        principalTable: "Units",
                        principalColumn: "iUnitKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "pms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "pms",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRolegroupRoles",
                schema: "pms",
                columns: table => new
                {
                    RoleId = table.Column<string>(nullable: false),
                    RoleGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRolegroupRoles", x => new { x.RoleId, x.RoleGroupId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRolegroupRoles_AspNetRoleGroups_RoleGroupId",
                        column: x => x.RoleGroupId,
                        principalSchema: "pms",
                        principalTable: "AspNetRoleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRolegroupRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "pms",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CO2ConstantRows",
                schema: "pms",
                columns: table => new
                {
                    iCO2ConstantRowKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iCO2ConstantKey = table.Column<int>(nullable: false),
                    sCO2ConstantRowName = table.Column<string>(maxLength: 100, nullable: false),
                    dCO2ConstantRowValue = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    sCO2ConstantRowUnit = table.Column<string>(maxLength: 30, nullable: false),
                    sCO2ConstantRowSource = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CO2ConstantRows", x => x.iCO2ConstantRowKey);
                    table.ForeignKey(
                        name: "FK_CO2ConstantRows_CO2Constants_iCO2ConstantKey",
                        column: x => x.iCO2ConstantKey,
                        principalSchema: "pms",
                        principalTable: "CO2Constants",
                        principalColumn: "iCO2ConstantKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "pms",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    sFirstName = table.Column<string>(maxLength: 150, nullable: true),
                    sLastName = table.Column<string>(maxLength: 150, nullable: true),
                    IsLocked = table.Column<bool>(nullable: true),
                    IsDuoAuthenticatorEnabled = table.Column<bool>(nullable: false),
                    DuoAuthenticatorSecretKey = table.Column<string>(nullable: true),
                    MaintenanceContactID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_MaintenanceContacts_MaintenanceContactID",
                        column: x => x.MaintenanceContactID,
                        principalSchema: "pms",
                        principalTable: "MaintenanceContacts",
                        principalColumn: "iMaintenanceContactKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MonthDegreeDayIndex",
                schema: "budget",
                columns: table => new
                {
                    iMonthDegreeDayIndexKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iBudgetSettingKey = table.Column<int>(nullable: false),
                    iMonthKey = table.Column<int>(nullable: false),
                    dDegreeDayIndex = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthDegreeDayIndex", x => x.iMonthDegreeDayIndexKey);
                    table.ForeignKey(
                        name: "FK_MonthDegreeDayIndex_BudgetSettings_iBudgetSettingKey",
                        column: x => x.iBudgetSettingKey,
                        principalSchema: "budget",
                        principalTable: "BudgetSettings",
                        principalColumn: "iBudgetSettingKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonthDegreeDayIndex_Months_iMonthKey",
                        column: x => x.iMonthKey,
                        principalSchema: "pms",
                        principalTable: "Months",
                        principalColumn: "iMonthKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceProjectRows",
                schema: "pms",
                columns: table => new
                {
                    iReferenceProjectRowKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iReferenceProjectKey = table.Column<int>(nullable: false),
                    iCounterTypeKey = table.Column<int>(nullable: false),
                    sConnectionTypeKey = table.Column<string>(maxLength: 2, nullable: false),
                    dDistributionEfficiency = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    dGenerationEfficiency = table.Column<decimal>(type: "decimal(18, 5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceProjectRows", x => x.iReferenceProjectRowKey);
                    table.ForeignKey(
                        name: "FK_ReferenceProjectRows_CounterTypes_iCounterTypeKey",
                        column: x => x.iCounterTypeKey,
                        principalSchema: "meter",
                        principalTable: "CounterTypes",
                        principalColumn: "iCounterTypeKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReferenceProjectRows_ReferenceProjects_iReferenceProjectKey",
                        column: x => x.iReferenceProjectKey,
                        principalSchema: "pms",
                        principalTable: "ReferenceProjects",
                        principalColumn: "iReferenceProjectKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReferenceProjectRows_ConnectionTypes_sConnectionTypeKey",
                        column: x => x.sConnectionTypeKey,
                        principalSchema: "meter",
                        principalTable: "ConnectionTypes",
                        principalColumn: "sConnectionTypeKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResponseConcepts",
                schema: "service",
                columns: table => new
                {
                    iTicketID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    dtDateTimeLastEdited = table.Column<DateTime>(nullable: false),
                    sUserID = table.Column<string>(nullable: true),
                    iResponseTypeID = table.Column<int>(nullable: false),
                    iTicketStatusID = table.Column<int>(nullable: false),
                    sToEmail = table.Column<string>(maxLength: 150, nullable: true),
                    CCList = table.Column<string>(nullable: true),
                    BCCList = table.Column<string>(nullable: true),
                    sMessage = table.Column<string>(nullable: true),
                    sSolution = table.Column<string>(nullable: true),
                    MailHistory = table.Column<bool>(nullable: false),
                    PhoneHistory = table.Column<bool>(nullable: false),
                    NoteHistory = table.Column<bool>(nullable: false),
                    SolutionHistory = table.Column<bool>(nullable: false),
                    PortalHistory = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseConcepts", x => x.iTicketID);
                    table.ForeignKey(
                        name: "FK_ResponseConcepts_ResponseTypes_iResponseTypeID",
                        column: x => x.iResponseTypeID,
                        principalSchema: "service",
                        principalTable: "ResponseTypes",
                        principalColumn: "iResponseTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResponseConcepts_TicketStatuses_iTicketStatusID",
                        column: x => x.iTicketStatusID,
                        principalSchema: "service",
                        principalTable: "TicketStatuses",
                        principalColumn: "iTicketStatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResponseTexts",
                schema: "service",
                columns: table => new
                {
                    iResponseTextID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iTicketTypeID = table.Column<int>(nullable: false),
                    sTitle = table.Column<string>(maxLength: 50, nullable: false),
                    sMessage = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponseTexts", x => x.iResponseTextID);
                    table.ForeignKey(
                        name: "FK_ResponseTexts_TicketTypes_iTicketTypeID",
                        column: x => x.iTicketTypeID,
                        principalSchema: "service",
                        principalTable: "TicketTypes",
                        principalColumn: "iTicketTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAccounts",
                schema: "pms",
                columns: table => new
                {
                    iCustomerAccountID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sBankName = table.Column<string>(maxLength: 200, nullable: true),
                    sIBANNumber = table.Column<string>(maxLength: 50, nullable: true),
                    sSWIFTCode = table.Column<string>(maxLength: 20, nullable: true),
                    sAccountType = table.Column<string>(maxLength: 100, nullable: true),
                    iCustomerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAccounts", x => x.iCustomerAccountID);
                    table.ForeignKey(
                        name: "FK_CustomerAccounts_CustomerInfo_iCustomerID",
                        column: x => x.iCustomerID,
                        principalSchema: "pms",
                        principalTable: "CustomerInfo",
                        principalColumn: "iCustomerKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDocuments",
                schema: "pms",
                columns: table => new
                {
                    iCustomerDocumentKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iCustomerKey = table.Column<int>(nullable: false),
                    iDocumentCategoryKey = table.Column<int>(nullable: false),
                    sDocumentName = table.Column<string>(maxLength: 50, nullable: false),
                    sUrl = table.Column<string>(maxLength: 2000, nullable: false),
                    bDownloaded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerDocuments", x => x.iCustomerDocumentKey);
                    table.ForeignKey(
                        name: "FK_CustomerDocuments_CustomerInfo_iCustomerKey",
                        column: x => x.iCustomerKey,
                        principalSchema: "pms",
                        principalTable: "CustomerInfo",
                        principalColumn: "iCustomerKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerDocuments_DocumentCategories_iDocumentCategoryKey",
                        column: x => x.iDocumentCategoryKey,
                        principalSchema: "pms",
                        principalTable: "DocumentCategories",
                        principalColumn: "iDocumentCategoryKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DebtorFiles",
                schema: "invoice",
                columns: table => new
                {
                    iDebtorFileID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sDisplayName = table.Column<string>(maxLength: 150, nullable: false),
                    sFileName = table.Column<string>(maxLength: 250, nullable: false),
                    sContentType = table.Column<string>(maxLength: 100, nullable: false),
                    bByteArray = table.Column<byte[]>(nullable: false),
                    iDebtorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebtorFiles", x => x.iDebtorFileID);
                    table.ForeignKey(
                        name: "FK_DebtorFiles_Debtors_iDebtorID",
                        column: x => x.iDebtorID,
                        principalSchema: "invoice",
                        principalTable: "Debtors",
                        principalColumn: "iDebtorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Occupants",
                schema: "invoice",
                columns: table => new
                {
                    iOccupantID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iDebtorID = table.Column<int>(nullable: false),
                    sCompanyName = table.Column<string>(maxLength: 150, nullable: true),
                    iTitleID = table.Column<int>(nullable: false),
                    sInitials = table.Column<string>(maxLength: 50, nullable: true),
                    sFirstName = table.Column<string>(maxLength: 250, nullable: true),
                    sLastName = table.Column<string>(maxLength: 250, nullable: false),
                    sPhoneNumber = table.Column<string>(maxLength: 30, nullable: true),
                    sEmailAddress = table.Column<string>(maxLength: 80, nullable: true),
                    sRemark = table.Column<string>(maxLength: 250, nullable: true),
                    bIsVacancy = table.Column<bool>(nullable: false),
                    OccupantToken = table.Column<Guid>(nullable: true),
                    ReceiveNewsletter = table.Column<bool>(nullable: false),
                    CustomerSatisfactionResearch = table.Column<bool>(nullable: false),
                    DebtoriDebtorID = table.Column<int>(nullable: true),
                    TitleiTitleID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occupants", x => x.iOccupantID);
                    table.ForeignKey(
                        name: "FK_Occupants_Debtors_DebtoriDebtorID",
                        column: x => x.DebtoriDebtorID,
                        principalSchema: "invoice",
                        principalTable: "Debtors",
                        principalColumn: "iDebtorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Occupants_Titles_TitleiTitleID",
                        column: x => x.TitleiTitleID,
                        principalSchema: "invoice",
                        principalTable: "Titles",
                        principalColumn: "iTitleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Occupants_Debtors_iDebtorID",
                        column: x => x.iDebtorID,
                        principalSchema: "invoice",
                        principalTable: "Debtors",
                        principalColumn: "iDebtorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Occupants_Titles_iTitleID",
                        column: x => x.iTitleID,
                        principalSchema: "invoice",
                        principalTable: "Titles",
                        principalColumn: "iTitleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RateCardYears",
                schema: "meter",
                columns: table => new
                {
                    iRateCardYearKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(nullable: true),
                    iRateCardKey = table.Column<int>(nullable: false),
                    iYear = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateCardYears", x => x.iRateCardYearKey);
                    table.ForeignKey(
                        name: "FK_RateCardYears_RateCards_iRateCardKey",
                        column: x => x.iRateCardKey,
                        principalSchema: "meter",
                        principalTable: "RateCards",
                        principalColumn: "iRateCardKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRunErrors",
                schema: "meter",
                columns: table => new
                {
                    iServiceRunErrorKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iServiceRunKey = table.Column<int>(nullable: false),
                    sConsumptionMeterNumber = table.Column<string>(maxLength: 100, nullable: true),
                    sProjectNumber = table.Column<string>(maxLength: 100, nullable: true),
                    iStatusCode = table.Column<int>(nullable: false),
                    sErrorMessage = table.Column<string>(maxLength: 1000, nullable: false),
                    dtEffectiveDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRunErrors", x => x.iServiceRunErrorKey);
                    table.ForeignKey(
                        name: "FK_ServiceRunErrors_ServiceRuns_iServiceRunKey",
                        column: x => x.iServiceRunKey,
                        principalSchema: "meter",
                        principalTable: "ServiceRuns",
                        principalColumn: "iServiceRunKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RateCardScaleRows",
                schema: "meter",
                columns: table => new
                {
                    iRateCardScaleRowKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iRateCardScaleKey = table.Column<int>(nullable: false),
                    sDescription = table.Column<string>(maxLength: 150, nullable: true),
                    iRowStart = table.Column<int>(nullable: false),
                    iRowEnd = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateCardScaleRows", x => x.iRateCardScaleRowKey);
                    table.ForeignKey(
                        name: "FK_RateCardScaleRows_RateCardScales_iRateCardScaleKey",
                        column: x => x.iRateCardScaleKey,
                        principalSchema: "meter",
                        principalTable: "RateCardScales",
                        principalColumn: "iRateCardScaleKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTermHistory",
                schema: "invoice",
                columns: table => new
                {
                    iPaymentTermHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iPaymentTermID = table.Column<int>(nullable: false),
                    iDebtorID = table.Column<int>(nullable: false),
                    sUserID = table.Column<string>(nullable: true),
                    dtStartDate = table.Column<DateTime>(nullable: false),
                    dtEndDate = table.Column<DateTime>(nullable: true),
                    PaymentTermiPaymentTermID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTermHistory", x => x.iPaymentTermHistoryID);
                    table.ForeignKey(
                        name: "FK_PaymentTermHistory_PaymentTerms_PaymentTermiPaymentTermID",
                        column: x => x.PaymentTermiPaymentTermID,
                        principalSchema: "invoice",
                        principalTable: "PaymentTerms",
                        principalColumn: "iPaymentTermID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentTermHistory_Debtors_iDebtorID",
                        column: x => x.iDebtorID,
                        principalSchema: "invoice",
                        principalTable: "Debtors",
                        principalColumn: "iDebtorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentTermHistory_PaymentTerms_iPaymentTermID",
                        column: x => x.iPaymentTermID,
                        principalSchema: "invoice",
                        principalTable: "PaymentTerms",
                        principalColumn: "iPaymentTermID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentTermHistory_AspNetUsers_sUserID",
                        column: x => x.sUserID,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserGroup",
                schema: "pms",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    UserGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserGroup", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserGroup_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserGroup_AspNetUserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalSchema: "pms",
                        principalTable: "AspNetUserGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "pms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "pms",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoleGroups",
                schema: "pms",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoleGroups", x => new { x.RoleGroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoleGroups_AspNetRoleGroups_RoleGroupId",
                        column: x => x.RoleGroupId,
                        principalSchema: "pms",
                        principalTable: "AspNetRoleGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoleGroups_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "pms",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "pms",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "pms",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportValidationSets",
                schema: "pms",
                columns: table => new
                {
                    ReportValidationSetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MasterSet = table.Column<bool>(nullable: false),
                    ReportValidationSetName = table.Column<string>(maxLength: 150, nullable: true),
                    CreateByUserID = table.Column<string>(nullable: false),
                    ChangeByUserID = table.Column<string>(nullable: true),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    ChangeDateTime = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportValidationSets", x => x.ReportValidationSetID);
                    table.ForeignKey(
                        name: "FK_ReportValidationSets_AspNetUsers_ChangeByUserID",
                        column: x => x.ChangeByUserID,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportValidationSets_AspNetUsers_CreateByUserID",
                        column: x => x.CreateByUserID,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceInvoiceLineInputs",
                schema: "service",
                columns: table => new
                {
                    ServiceInvoiceLineInputID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    MaterialCost = table.Column<decimal>(nullable: true),
                    WorkingHours = table.Column<decimal>(nullable: true),
                    WorkingHours25 = table.Column<decimal>(nullable: true),
                    WorkingHours50 = table.Column<decimal>(nullable: true),
                    WorkingHours100 = table.Column<decimal>(nullable: true),
                    CallOutHours = table.Column<decimal>(nullable: true),
                    CallOutKilometers = table.Column<decimal>(nullable: true),
                    TotalCosts = table.Column<decimal>(nullable: false),
                    Note = table.Column<string>(maxLength: 1000, nullable: true),
                    ValidatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceInvoiceLineInputs", x => x.ServiceInvoiceLineInputID);
                    table.ForeignKey(
                        name: "FK_ServiceInvoiceLineInputs_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RateCardRows",
                schema: "meter",
                columns: table => new
                {
                    iRateCardRowKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sDescription = table.Column<string>(maxLength: 150, nullable: false),
                    iRateCardYearKey = table.Column<int>(nullable: false),
                    iRubricKey = table.Column<int>(nullable: false),
                    iCounterTypeKey = table.Column<int>(nullable: true),
                    iRateCardScaleRowKey = table.Column<int>(nullable: true),
                    iUnitKey = table.Column<int>(nullable: false),
                    dAmount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    dVAT = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    bIndexed = table.Column<bool>(nullable: false),
                    Discount = table.Column<bool>(nullable: false),
                    VatConditionID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateCardRows", x => x.iRateCardRowKey);
                    table.ForeignKey(
                        name: "FK_RateCardRows_VatConditions_VatConditionID",
                        column: x => x.VatConditionID,
                        principalSchema: "meter",
                        principalTable: "VatConditions",
                        principalColumn: "VatConditionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateCardRows_CounterTypes_iCounterTypeKey",
                        column: x => x.iCounterTypeKey,
                        principalSchema: "meter",
                        principalTable: "CounterTypes",
                        principalColumn: "iCounterTypeKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateCardRows_RateCardScaleRows_iRateCardScaleRowKey",
                        column: x => x.iRateCardScaleRowKey,
                        principalSchema: "meter",
                        principalTable: "RateCardScaleRows",
                        principalColumn: "iRateCardScaleRowKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateCardRows_RateCardYears_iRateCardYearKey",
                        column: x => x.iRateCardYearKey,
                        principalSchema: "meter",
                        principalTable: "RateCardYears",
                        principalColumn: "iRateCardYearKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RateCardRows_Rubrics_iRubricKey",
                        column: x => x.iRubricKey,
                        principalSchema: "meter",
                        principalTable: "Rubrics",
                        principalColumn: "iRubricKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RateCardRows_Units_iUnitKey",
                        column: x => x.iUnitKey,
                        principalSchema: "meter",
                        principalTable: "Units",
                        principalColumn: "iUnitKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectInfo",
                schema: "pms",
                columns: table => new
                {
                    iProjectKey = table.Column<int>(nullable: false),
                    iAssetManagerKey = table.Column<int>(nullable: true),
                    AssetManagerID = table.Column<string>(nullable: true),
                    iTechnicalPrincipalMainKey = table.Column<int>(nullable: true),
                    iTechnicalPrincipalSub1Key = table.Column<int>(nullable: true),
                    iTechnicalPrincipalSub2Key = table.Column<int>(nullable: true),
                    iDemarcationKey = table.Column<int>(nullable: true),
                    iMeterKey = table.Column<int>(nullable: true),
                    iFinProjectKey = table.Column<int>(nullable: true),
                    iTemperatureRangeKey = table.Column<int>(nullable: true),
                    iColdTemperatureRangeKey = table.Column<int>(nullable: true),
                    iDistributionNetWorkKey = table.Column<int>(nullable: true),
                    iWaterTypeKey = table.Column<int>(nullable: true),
                    iMaintenanceContactKey = table.Column<int>(nullable: true),
                    iHomeMaintenanceContactKey = table.Column<int>(nullable: true),
                    iSupplyWaterTypeKey = table.Column<int>(nullable: true),
                    iReferenceProjectKey = table.Column<int>(nullable: true),
                    iProjectStatusID = table.Column<int>(nullable: false),
                    iDefaultDebtorID = table.Column<int>(nullable: true),
                    iTransactionModeID = table.Column<int>(nullable: true),
                    ReportValidationSetID = table.Column<int>(nullable: true),
                    InvoiceViaOwnCollection = table.Column<bool>(nullable: false),
                    MailConfigID = table.Column<int>(nullable: true),
                    DebtCollectionCustomerID = table.Column<int>(nullable: true),
                    sProjectAlias = table.Column<string>(maxLength: 500, nullable: true),
                    sCustomerName = table.Column<string>(maxLength: 150, nullable: true),
                    sStreetName = table.Column<string>(maxLength: 100, nullable: true),
                    iNumber = table.Column<string>(maxLength: 50, nullable: true),
                    sCity = table.Column<string>(maxLength: 150, nullable: true),
                    sPostalcodeArea = table.Column<string>(maxLength: 100, nullable: true),
                    dLatitude = table.Column<decimal>(type: "decimal(9, 7)", nullable: true),
                    dLongitude = table.Column<decimal>(type: "decimal(10, 7)", nullable: true),
                    bExternalAccess = table.Column<bool>(nullable: false),
                    sProjectImage = table.Column<string>(maxLength: 255, nullable: true),
                    bReferenProjectDoesNotApply = table.Column<bool>(nullable: false),
                    bHotWater = table.Column<bool>(nullable: false),
                    bDrinkWater = table.Column<bool>(nullable: false),
                    bCooling = table.Column<bool>(nullable: false),
                    iYearCentralInstallation = table.Column<int>(nullable: true),
                    bWIONRegistration = table.Column<bool>(nullable: false),
                    sLicenseWaterLaw = table.Column<string>(maxLength: 100, nullable: true),
                    bDeliverCoolWater = table.Column<bool>(nullable: false),
                    bAVFQuickscanExecuted = table.Column<bool>(nullable: false),
                    dtLastAVFQuickScan = table.Column<DateTime>(nullable: true),
                    bAppliesHeatLaw = table.Column<bool>(nullable: false),
                    ProjectTypeID = table.Column<int>(nullable: true),
                    dHeatPumps = table.Column<decimal>(nullable: true),
                    dCentralHeatingRooms = table.Column<decimal>(nullable: true),
                    bCentralHeatingRoomsForWater = table.Column<bool>(nullable: false),
                    dCentralHeatingHotWater = table.Column<decimal>(nullable: true),
                    dSourcePowerHotKWh = table.Column<decimal>(nullable: true),
                    dSourcePowerHotM3 = table.Column<decimal>(nullable: true),
                    dSourcePowerCoolKWh = table.Column<decimal>(nullable: true),
                    dSourcePowerCoolM3 = table.Column<decimal>(nullable: true),
                    dWasteHeat = table.Column<decimal>(nullable: true),
                    dWoodPelletStove = table.Column<decimal>(nullable: true),
                    dGasBoiler = table.Column<decimal>(nullable: true),
                    dEnergyRoof = table.Column<decimal>(nullable: true),
                    dDryCooler = table.Column<decimal>(nullable: true),
                    dSurfaceGenerationKWh = table.Column<decimal>(nullable: true),
                    dSurfaceGenerationM3 = table.Column<decimal>(nullable: true),
                    dSolarCollectorsThermally = table.Column<decimal>(nullable: true),
                    dSolarCollectorsElectricKWh = table.Column<decimal>(nullable: true),
                    dNumberOfSolarCollectorsElectric = table.Column<decimal>(nullable: true),
                    dDistribution = table.Column<decimal>(nullable: true),
                    dChiller = table.Column<decimal>(nullable: true),
                    dTemperatureHotWater = table.Column<decimal>(nullable: true),
                    dAirWaterHotKWh = table.Column<decimal>(nullable: true),
                    dAirWaterCoolKWh = table.Column<decimal>(nullable: true),
                    bHeatPumpsActive = table.Column<bool>(nullable: false),
                    bChillerActive = table.Column<bool>(nullable: false),
                    bCentralHeatingRoomsActive = table.Column<bool>(nullable: false),
                    bCentralHeatingHotWaterActive = table.Column<bool>(nullable: false),
                    bGasBoilerActive = table.Column<bool>(nullable: false),
                    bSourcePowerHotKWhActive = table.Column<bool>(nullable: false),
                    bSourcePowerHotM3Active = table.Column<bool>(nullable: false),
                    bSourcePowerCoolKWhActive = table.Column<bool>(nullable: false),
                    bSourcePowerCoolM3Active = table.Column<bool>(nullable: false),
                    bWasteHeatActive = table.Column<bool>(nullable: false),
                    bWoodPelletStoveActive = table.Column<bool>(nullable: false),
                    bEnergyRoofActive = table.Column<bool>(nullable: false),
                    bDryCoolerActive = table.Column<bool>(nullable: false),
                    bSurfaceGenerationKWhActive = table.Column<bool>(nullable: false),
                    bSurfaceGenerationM3Active = table.Column<bool>(nullable: false),
                    bSolarCollectorsThermallyActive = table.Column<bool>(nullable: false),
                    bNumberOfSolarCollectorsElectricActive = table.Column<bool>(nullable: false),
                    bSolarCollectorsElectricKWhActive = table.Column<bool>(nullable: false),
                    bAirWaterHotKWhActive = table.Column<bool>(nullable: false),
                    bAirWaterCoolKWhActive = table.Column<bool>(nullable: false),
                    MovingMessage = table.Column<string>(maxLength: 1000, nullable: true),
                    dtStartDateExploitation = table.Column<DateTime>(nullable: true),
                    dtDateTakeOver = table.Column<DateTime>(nullable: true),
                    dtEndDateExploitation = table.Column<DateTime>(nullable: true),
                    dtStartDateTechnicalExploitation = table.Column<DateTime>(nullable: true),
                    dtDateReinvestment = table.Column<DateTime>(nullable: true),
                    bShelterLaw = table.Column<bool>(nullable: false),
                    bQualitativeObligation = table.Column<bool>(nullable: false),
                    bPerpetualClause = table.Column<bool>(nullable: false),
                    bRentalCase = table.Column<bool>(nullable: false),
                    dDiscountRate = table.Column<decimal>(type: "decimal(18, 5)", nullable: true),
                    sUpDownsides = table.Column<string>(maxLength: 250, nullable: true),
                    dUpDownsides = table.Column<decimal>(nullable: false),
                    dReinstatementInstallation = table.Column<decimal>(nullable: true),
                    dSpecialCharges = table.Column<decimal>(nullable: true),
                    dResponseTimeCallBack = table.Column<decimal>(nullable: true),
                    dResponseTimeOnSite = table.Column<decimal>(nullable: true),
                    dResponseTimeSolution = table.Column<decimal>(nullable: true),
                    sSlaDocument = table.Column<string>(maxLength: 2000, nullable: true),
                    iProjectReportPeriod = table.Column<int>(nullable: true),
                    EMReportRequired = table.Column<bool>(nullable: false),
                    dCOPRoomHeating = table.Column<decimal>(nullable: true),
                    dCOPWaterHeating = table.Column<decimal>(nullable: true),
                    dSPFbes = table.Column<decimal>(nullable: true),
                    dEnergyDemandRoomHeating = table.Column<decimal>(nullable: true),
                    dEnergyDemandWater = table.Column<decimal>(nullable: true),
                    dStokeLimit = table.Column<decimal>(nullable: true),
                    dCoolLimit = table.Column<decimal>(nullable: true),
                    dReferenceDeliverCooling = table.Column<decimal>(nullable: true),
                    sEnergyManagerID = table.Column<string>(nullable: true),
                    sPurchaseInstruction = table.Column<string>(nullable: true),
                    sSalesInstruction = table.Column<string>(nullable: true),
                    iCalculationTypePurchaseID = table.Column<int>(nullable: true),
                    iCalculationTypeSalesID = table.Column<int>(nullable: true),
                    bDefaultCOPRoomHeating = table.Column<bool>(nullable: false),
                    bDefaultCOPWaterHeating = table.Column<bool>(nullable: false),
                    bDefaultSPFbes = table.Column<bool>(nullable: false),
                    bDefaultEnergyDemandRoomHeating = table.Column<bool>(nullable: false),
                    bDefaultEnergyDemandWaterHeating = table.Column<bool>(nullable: false),
                    bDefaultReferenceSalesCooling = table.Column<bool>(nullable: false),
                    bDefaultStokeLimit = table.Column<bool>(nullable: false),
                    bDefaultCoolLimit = table.Column<bool>(nullable: false),
                    AutomaticReport = table.Column<bool>(nullable: false),
                    PercentageDeviationFromAverage = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    DefaultReferenceDeliveryWater = table.Column<bool>(nullable: false),
                    ReferenceDeliveryWater = table.Column<decimal>(nullable: true),
                    InvoicedByDefaultCustomer = table.Column<bool>(nullable: false),
                    AutomaticInvoicing = table.Column<bool>(nullable: false),
                    CustomerAnnouncement = table.Column<string>(nullable: true),
                    ServiceAnnouncement = table.Column<string>(nullable: true),
                    AssetManageriAssetManagerKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectInfo", x => x.iProjectKey);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_AspNetUsers_AssetManagerID",
                        column: x => x.AssetManagerID,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_AssetManager_AssetManageriAssetManagerKey",
                        column: x => x.AssetManageriAssetManagerKey,
                        principalSchema: "pms",
                        principalTable: "AssetManager",
                        principalColumn: "iAssetManagerKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_DimCustomers_DebtCollectionCustomerID",
                        column: x => x.DebtCollectionCustomerID,
                        principalSchema: "astonmartin",
                        principalTable: "DimCustomers",
                        principalColumn: "iCustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_MailConfigs_MailConfigID",
                        column: x => x.MailConfigID,
                        principalSchema: "pms",
                        principalTable: "MailConfigs",
                        principalColumn: "MailConfigID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_ProjectType_ProjectTypeID",
                        column: x => x.ProjectTypeID,
                        principalSchema: "pms",
                        principalTable: "ProjectType",
                        principalColumn: "ProjectTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_ReportValidationSets_ReportValidationSetID",
                        column: x => x.ReportValidationSetID,
                        principalSchema: "pms",
                        principalTable: "ReportValidationSets",
                        principalColumn: "ReportValidationSetID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_CalculationType_iCalculationTypePurchaseID",
                        column: x => x.iCalculationTypePurchaseID,
                        principalSchema: "pms",
                        principalTable: "CalculationType",
                        principalColumn: "iCalculationTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_CalculationType_iCalculationTypeSalesID",
                        column: x => x.iCalculationTypeSalesID,
                        principalSchema: "pms",
                        principalTable: "CalculationType",
                        principalColumn: "iCalculationTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_TemperatureRanges_iColdTemperatureRangeKey",
                        column: x => x.iColdTemperatureRangeKey,
                        principalSchema: "pms",
                        principalTable: "TemperatureRanges",
                        principalColumn: "iTemperatureRangeKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_Debtors_iDefaultDebtorID",
                        column: x => x.iDefaultDebtorID,
                        principalSchema: "invoice",
                        principalTable: "Debtors",
                        principalColumn: "iDebtorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_Demarcations_iDemarcationKey",
                        column: x => x.iDemarcationKey,
                        principalSchema: "pms",
                        principalTable: "Demarcations",
                        principalColumn: "iDemarcationKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_DistributionNetWorks_iDistributionNetWorkKey",
                        column: x => x.iDistributionNetWorkKey,
                        principalSchema: "pms",
                        principalTable: "DistributionNetWorks",
                        principalColumn: "iDistributionNetWorkKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_MaintenanceContacts_iHomeMaintenanceContactKey",
                        column: x => x.iHomeMaintenanceContactKey,
                        principalSchema: "pms",
                        principalTable: "MaintenanceContacts",
                        principalColumn: "iMaintenanceContactKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_MaintenanceContacts_iMaintenanceContactKey",
                        column: x => x.iMaintenanceContactKey,
                        principalSchema: "pms",
                        principalTable: "MaintenanceContacts",
                        principalColumn: "iMaintenanceContactKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_Meters_iMeterKey",
                        column: x => x.iMeterKey,
                        principalSchema: "pms",
                        principalTable: "Meters",
                        principalColumn: "iMeterKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_DimProject_iProjectKey",
                        column: x => x.iProjectKey,
                        principalSchema: "dbo",
                        principalTable: "DimProject",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_ProjectStatuses_iProjectStatusID",
                        column: x => x.iProjectStatusID,
                        principalSchema: "pms",
                        principalTable: "ProjectStatuses",
                        principalColumn: "iProjectStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_ReferenceProjects_iReferenceProjectKey",
                        column: x => x.iReferenceProjectKey,
                        principalSchema: "pms",
                        principalTable: "ReferenceProjects",
                        principalColumn: "iReferenceProjectKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_SupplyWaterTypes_iSupplyWaterTypeKey",
                        column: x => x.iSupplyWaterTypeKey,
                        principalSchema: "pms",
                        principalTable: "SupplyWaterTypes",
                        principalColumn: "iSupplyWaterTypeKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_TechnicalPrincipals_iTechnicalPrincipalMainKey",
                        column: x => x.iTechnicalPrincipalMainKey,
                        principalSchema: "pms",
                        principalTable: "TechnicalPrincipals",
                        principalColumn: "iTechnicalPrincipalKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_TechnicalPrincipals_iTechnicalPrincipalSub1Key",
                        column: x => x.iTechnicalPrincipalSub1Key,
                        principalSchema: "pms",
                        principalTable: "TechnicalPrincipals",
                        principalColumn: "iTechnicalPrincipalKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_TechnicalPrincipals_iTechnicalPrincipalSub2Key",
                        column: x => x.iTechnicalPrincipalSub2Key,
                        principalSchema: "pms",
                        principalTable: "TechnicalPrincipals",
                        principalColumn: "iTechnicalPrincipalKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_TemperatureRanges_iTemperatureRangeKey",
                        column: x => x.iTemperatureRangeKey,
                        principalSchema: "pms",
                        principalTable: "TemperatureRanges",
                        principalColumn: "iTemperatureRangeKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_TransactionModes_iTransactionModeID",
                        column: x => x.iTransactionModeID,
                        principalSchema: "pms",
                        principalTable: "TransactionModes",
                        principalColumn: "iTransactionModeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_WaterTypes_iWaterTypeKey",
                        column: x => x.iWaterTypeKey,
                        principalSchema: "pms",
                        principalTable: "WaterTypes",
                        principalColumn: "iWaterTypeKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfo_AspNetUsers_sEnergyManagerID",
                        column: x => x.sEnergyManagerID,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReportValidationSetLines",
                schema: "pms",
                columns: table => new
                {
                    ReportValidationSetLineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReportValidationSetID = table.Column<int>(nullable: false),
                    RubricID = table.Column<int>(nullable: true),
                    ReportValidationSetLineName = table.Column<string>(maxLength: 250, nullable: false),
                    ValueOperatorID = table.Column<int>(nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    FormatString = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportValidationSetLines", x => x.ReportValidationSetLineID);
                    table.ForeignKey(
                        name: "FK_ReportValidationSetLines_ReportValidationSets_ReportValidationSetID",
                        column: x => x.ReportValidationSetID,
                        principalSchema: "pms",
                        principalTable: "ReportValidationSets",
                        principalColumn: "ReportValidationSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportValidationSetLines_Rubrics_RubricID",
                        column: x => x.RubricID,
                        principalSchema: "meter",
                        principalTable: "Rubrics",
                        principalColumn: "iRubricKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportValidationSetLines_ValueOperators_ValueOperatorID",
                        column: x => x.ValueOperatorID,
                        principalSchema: "pms",
                        principalTable: "ValueOperators",
                        principalColumn: "ValueOperatorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceBatches",
                schema: "invoice",
                columns: table => new
                {
                    iInvoiceBatchID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iInvoiceTypeID = table.Column<int>(nullable: false),
                    InvoicePeriodID = table.Column<int>(nullable: true),
                    iProjectID = table.Column<int>(nullable: false),
                    iYear = table.Column<int>(nullable: false),
                    iPeriod = table.Column<int>(nullable: false),
                    dtDateTime = table.Column<DateTime>(nullable: false),
                    userID = table.Column<string>(nullable: false),
                    iNumberOfAdresses = table.Column<int>(nullable: false),
                    iStatusID = table.Column<int>(nullable: false),
                    dtInvoiceDateTime = table.Column<DateTime>(nullable: false),
                    ScheduledDate = table.Column<DateTime>(nullable: true),
                    StatusID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceBatches", x => x.iInvoiceBatchID);
                    table.ForeignKey(
                        name: "FK_InvoiceBatches_InvoicePeriods_InvoicePeriodID",
                        column: x => x.InvoicePeriodID,
                        principalSchema: "invoice",
                        principalTable: "InvoicePeriods",
                        principalColumn: "InvoicePeriodID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceBatches_Statuses_StatusID",
                        column: x => x.StatusID,
                        principalSchema: "pms",
                        principalTable: "Statuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceBatches_InvoiceTypes_iInvoiceTypeID",
                        column: x => x.iInvoiceTypeID,
                        principalSchema: "invoice",
                        principalTable: "InvoiceTypes",
                        principalColumn: "iInvoiceTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceBatches_ProjectInfo_iProjectID",
                        column: x => x.iProjectID,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceBatches_InvoiceStatuses_iStatusID",
                        column: x => x.iStatusID,
                        principalSchema: "invoice",
                        principalTable: "InvoiceStatuses",
                        principalColumn: "iStatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AverageMonthConsumptions",
                schema: "meter",
                columns: table => new
                {
                    AverageMonthConsumptionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    CounterTypeID = table.Column<int>(nullable: false),
                    UnitID = table.Column<int>(nullable: false),
                    ProjectID = table.Column<int>(nullable: false),
                    Consumption = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AverageMonthConsumptions", x => x.AverageMonthConsumptionID);
                    table.ForeignKey(
                        name: "FK_AverageMonthConsumptions_CounterTypes_CounterTypeID",
                        column: x => x.CounterTypeID,
                        principalSchema: "meter",
                        principalTable: "CounterTypes",
                        principalColumn: "iCounterTypeKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AverageMonthConsumptions_ProjectInfo_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AverageMonthConsumptions_Units_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "meter",
                        principalTable: "Units",
                        principalColumn: "iUnitKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingManagementSystems",
                schema: "meter",
                columns: table => new
                {
                    BuildingManagementSystemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectID = table.Column<int>(nullable: false),
                    TypeManager = table.Column<string>(maxLength: 250, nullable: true),
                    ProjectName = table.Column<string>(maxLength: 250, nullable: true),
                    IpPhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    Credentials = table.Column<string>(maxLength: 250, nullable: true),
                    CommunicationTypeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingManagementSystems", x => x.BuildingManagementSystemID);
                    table.ForeignKey(
                        name: "FK_BuildingManagementSystems_CommunicationTypes_CommunicationTypeID",
                        column: x => x.CommunicationTypeID,
                        principalSchema: "pms",
                        principalTable: "CommunicationTypes",
                        principalColumn: "ComunicationTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BuildingManagementSystems_ProjectInfo_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                schema: "meter",
                columns: table => new
                {
                    iReportKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iProjectKey = table.Column<int>(nullable: false),
                    bConcept = table.Column<bool>(nullable: false),
                    bBooked = table.Column<bool>(nullable: false),
                    iReportPeriodKey = table.Column<int>(nullable: false),
                    sValidationNote = table.Column<string>(maxLength: 500, nullable: true),
                    sReportNote = table.Column<string>(maxLength: 500, nullable: true),
                    sCreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    sLastEditedBy = table.Column<string>(maxLength: 128, nullable: false),
                    dtDateTimeCreated = table.Column<DateTime>(nullable: false),
                    dtDateTimeLastEdited = table.Column<DateTime>(nullable: false),
                    ReportValidationSetID = table.Column<int>(nullable: true),
                    ReportOnBudget = table.Column<bool>(nullable: false),
                    AutomaticReport = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.iReportKey);
                    table.ForeignKey(
                        name: "FK_Reports_ReportValidationSets_ReportValidationSetID",
                        column: x => x.ReportValidationSetID,
                        principalSchema: "pms",
                        principalTable: "ReportValidationSets",
                        principalColumn: "ReportValidationSetID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reports_ProjectInfo_iProjectKey",
                        column: x => x.iProjectKey,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_ReportPeriods_iReportPeriodKey",
                        column: x => x.iReportPeriodKey,
                        principalSchema: "meter",
                        principalTable: "ReportPeriods",
                        principalColumn: "iReportPeriodKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "pms",
                columns: table => new
                {
                    iAddressKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CollectiveAddressID = table.Column<int>(nullable: true),
                    IsCollectiveAddress = table.Column<bool>(nullable: false),
                    CostlierServiceActive = table.Column<bool>(nullable: false),
                    sFineCode = table.Column<string>(maxLength: 10, nullable: true),
                    sStreetName = table.Column<string>(maxLength: 150, nullable: false),
                    iNumber = table.Column<int>(nullable: false),
                    sNumberAddition = table.Column<string>(maxLength: 10, nullable: true),
                    sPostalCode = table.Column<string>(maxLength: 7, nullable: false),
                    sCity = table.Column<string>(maxLength: 100, nullable: false),
                    iProjectKey = table.Column<int>(nullable: false),
                    sConnectionTypeKey = table.Column<string>(maxLength: 2, nullable: false),
                    dLatitude = table.Column<decimal>(type: "decimal(9, 7)", nullable: true),
                    dLongitude = table.Column<decimal>(type: "decimal(10, 7)", nullable: true),
                    sAddressComment = table.Column<string>(maxLength: 150, nullable: true),
                    iServiceRunKey = table.Column<int>(nullable: true),
                    dColdElectricPower = table.Column<decimal>(nullable: true),
                    dHeatElectricPower = table.Column<decimal>(nullable: true),
                    dTapWaterPower = table.Column<decimal>(nullable: true),
                    dBVO = table.Column<decimal>(nullable: true),
                    dContractedCapacityEnergy = table.Column<decimal>(nullable: true),
                    dContractedCapacityGas = table.Column<decimal>(nullable: true),
                    iCategory = table.Column<int>(nullable: true),
                    iAddressType = table.Column<int>(nullable: true),
                    IsIndoorInstallation = table.Column<bool>(nullable: false),
                    ObjectID = table.Column<int>(nullable: true),
                    Closed = table.Column<bool>(nullable: false),
                    PercentLossOfDistribution = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    MaxLossOfDistribution = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    PercentPollTax = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    MaxPollTax = table.Column<decimal>(type: "decimal(18, 5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.iAddressKey);
                    table.ForeignKey(
                        name: "FK_Addresses_Addresses_CollectiveAddressID",
                        column: x => x.CollectiveAddressID,
                        principalSchema: "pms",
                        principalTable: "Addresses",
                        principalColumn: "iAddressKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_ProjectInfo_iProjectKey",
                        column: x => x.iProjectKey,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_ServiceRuns_iServiceRunKey",
                        column: x => x.iServiceRunKey,
                        principalSchema: "meter",
                        principalTable: "ServiceRuns",
                        principalColumn: "iServiceRunKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_ConnectionTypes_sConnectionTypeKey",
                        column: x => x.sConnectionTypeKey,
                        principalSchema: "meter",
                        principalTable: "ConnectionTypes",
                        principalColumn: "sConnectionTypeKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaseModels",
                schema: "pms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Code = table.Column<int>(nullable: true),
                    Order = table.Column<int>(nullable: true),
                    CounterTypeId = table.Column<int>(nullable: true),
                    ProjectYearDetailId = table.Column<Guid>(nullable: true),
                    SeasonalPatternId = table.Column<Guid>(nullable: true),
                    DebtorId = table.Column<int>(nullable: true),
                    BlockedPeriod = table.Column<DateTime>(nullable: true),
                    PeriodPercentage_SeasonalPatternId = table.Column<Guid>(nullable: true),
                    PeriodNumber = table.Column<int>(nullable: true),
                    Percentage = table.Column<decimal>(type: "decimal(18, 5)", nullable: true),
                    ProjectId = table.Column<int>(nullable: true),
                    Year = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    PatternType = table.Column<int>(nullable: true),
                    PercentFootHold = table.Column<decimal>(type: "decimal(18, 5)", nullable: true),
                    Calculated = table.Column<bool>(nullable: true),
                    MaintenanceContactID = table.Column<int>(nullable: true),
                    MaintenanceContactCommunicationType = table.Column<int>(nullable: true),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    Domain = table.Column<string>(maxLength: 256, nullable: true),
                    BaseUrl = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseModels_CounterTypes_CounterTypeId",
                        column: x => x.CounterTypeId,
                        principalSchema: "meter",
                        principalTable: "CounterTypes",
                        principalColumn: "iCounterTypeKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseModels_BaseModels_ProjectYearDetailId",
                        column: x => x.ProjectYearDetailId,
                        principalSchema: "pms",
                        principalTable: "BaseModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseModels_BaseModels_SeasonalPatternId",
                        column: x => x.SeasonalPatternId,
                        principalSchema: "pms",
                        principalTable: "BaseModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseModels_Debtors_DebtorId",
                        column: x => x.DebtorId,
                        principalSchema: "invoice",
                        principalTable: "Debtors",
                        principalColumn: "iDebtorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseModels_BaseModels_PeriodPercentage_SeasonalPatternId",
                        column: x => x.PeriodPercentage_SeasonalPatternId,
                        principalSchema: "pms",
                        principalTable: "BaseModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseModels_ProjectInfo_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseModels_MaintenanceContacts_MaintenanceContactID",
                        column: x => x.MaintenanceContactID,
                        principalSchema: "pms",
                        principalTable: "MaintenanceContacts",
                        principalColumn: "iMaintenanceContactKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalcMutations",
                schema: "pms",
                columns: table => new
                {
                    iCalcMutationKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iProjectKey = table.Column<int>(nullable: false),
                    iCalcCategoryKey = table.Column<int>(nullable: false),
                    iYear = table.Column<int>(nullable: false),
                    iPeriod = table.Column<int>(nullable: false),
                    dAmount = table.Column<decimal>(nullable: false),
                    dtDate = table.Column<DateTime>(nullable: false),
                    sDescription = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalcMutations", x => x.iCalcMutationKey);
                    table.ForeignKey(
                        name: "FK_CalcMutations_CalcCategories_iCalcCategoryKey",
                        column: x => x.iCalcCategoryKey,
                        principalSchema: "pms",
                        principalTable: "CalcCategories",
                        principalColumn: "iCalcCategoryKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalcMutations_ProjectInfo_iProjectKey",
                        column: x => x.iProjectKey,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalcRules",
                schema: "pms",
                columns: table => new
                {
                    iProjectKey = table.Column<int>(nullable: false),
                    dEnergyCostsHeating = table.Column<decimal>(nullable: false),
                    dEnergyCostsCooling = table.Column<decimal>(nullable: false),
                    dIndexEnergyCosts = table.Column<decimal>(nullable: false),
                    dIndexOtherCosts = table.Column<decimal>(nullable: false),
                    dIndexEnergySales = table.Column<decimal>(nullable: false),
                    dIndexStandingCharge = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalcRules", x => x.iProjectKey);
                    table.ForeignKey(
                        name: "FK_CalcRules_ProjectInfo_iProjectKey",
                        column: x => x.iProjectKey,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "pms",
                columns: table => new
                {
                    iContactKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iProjectKey = table.Column<int>(nullable: false),
                    iContactTypeKey = table.Column<int>(nullable: false),
                    sOrganisation = table.Column<string>(maxLength: 50, nullable: true),
                    sTitle = table.Column<string>(maxLength: 50, nullable: true),
                    sContactName = table.Column<string>(maxLength: 50, nullable: false),
                    sEmail = table.Column<string>(maxLength: 100, nullable: true),
                    sTelephone = table.Column<string>(maxLength: 50, nullable: true),
                    sDescription = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.iContactKey);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactTypes_iContactTypeKey",
                        column: x => x.iContactTypeKey,
                        principalSchema: "pms",
                        principalTable: "ContactTypes",
                        principalColumn: "iContactTypeKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contacts_ProjectInfo_iProjectKey",
                        column: x => x.iProjectKey,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergyConsumption",
                schema: "pms",
                columns: table => new
                {
                    iEnergyConsumptionKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iProjectKey = table.Column<int>(nullable: false),
                    gGroupKey = table.Column<Guid>(nullable: false),
                    iPeriodKey = table.Column<int>(nullable: false),
                    iPeriod = table.Column<int>(nullable: false),
                    iQuarter = table.Column<int>(nullable: false),
                    iYear = table.Column<int>(nullable: false),
                    dHeatingRoomsGJ = table.Column<decimal>(nullable: false),
                    dHeatingHotWaterGJ = table.Column<decimal>(nullable: false),
                    dCoolingGJ = table.Column<decimal>(nullable: false),
                    dWaterM3 = table.Column<decimal>(nullable: false),
                    dElectricityKwh = table.Column<decimal>(nullable: false),
                    dGasM3 = table.Column<decimal>(nullable: false),
                    dHeatingGJ = table.Column<decimal>(nullable: false),
                    dOthersT = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergyConsumption", x => x.iEnergyConsumptionKey);
                    table.ForeignKey(
                        name: "FK_EnergyConsumption_Periods_iPeriodKey",
                        column: x => x.iPeriodKey,
                        principalSchema: "pms",
                        principalTable: "Periods",
                        principalColumn: "iPeriodKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnergyConsumption_ProjectInfo_iProjectKey",
                        column: x => x.iProjectKey,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Financings",
                schema: "pms",
                columns: table => new
                {
                    iFinancingKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iProjectKey = table.Column<int>(nullable: false),
                    iFinancerKey = table.Column<int>(nullable: false),
                    iSubFinancerKey = table.Column<int>(nullable: true),
                    bSubordinatedLoan = table.Column<bool>(nullable: false),
                    iPeriodKey = table.Column<int>(nullable: false),
                    iDsraDepositKey = table.Column<int>(nullable: false),
                    dAmount = table.Column<decimal>(nullable: false),
                    dDsraAmount = table.Column<decimal>(nullable: true),
                    dResdualAmount = table.Column<decimal>(nullable: false),
                    dtStartDate = table.Column<DateTime>(nullable: false),
                    dtEndDate = table.Column<DateTime>(nullable: false),
                    dInterest = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    sDescription = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financings", x => x.iFinancingKey);
                    table.ForeignKey(
                        name: "FK_Financings_DsraDeposits_iDsraDepositKey",
                        column: x => x.iDsraDepositKey,
                        principalSchema: "pms",
                        principalTable: "DsraDeposits",
                        principalColumn: "iDsraDepositKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Financings_Financers_iFinancerKey",
                        column: x => x.iFinancerKey,
                        principalSchema: "pms",
                        principalTable: "Financers",
                        principalColumn: "iFinancerKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Financings_Periods_iPeriodKey",
                        column: x => x.iPeriodKey,
                        principalSchema: "pms",
                        principalTable: "Periods",
                        principalColumn: "iPeriodKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Financings_ProjectInfo_iProjectKey",
                        column: x => x.iProjectKey,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Financings_Financers_iSubFinancerKey",
                        column: x => x.iSubFinancerKey,
                        principalSchema: "pms",
                        principalTable: "Financers",
                        principalColumn: "iFinancerKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hyperlinks",
                schema: "pms",
                columns: table => new
                {
                    iHyperlinkKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iProjectKey = table.Column<int>(nullable: false),
                    sLinkName = table.Column<string>(maxLength: 50, nullable: false),
                    sUrl = table.Column<string>(maxLength: 2000, nullable: false),
                    bVisible = table.Column<bool>(nullable: false),
                    VisibleForMechanic = table.Column<bool>(nullable: false),
                    bDownloaded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hyperlinks", x => x.iHyperlinkKey);
                    table.ForeignKey(
                        name: "FK_Hyperlinks_ProjectInfo_iProjectKey",
                        column: x => x.iProjectKey,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Investments",
                schema: "pms",
                columns: table => new
                {
                    iInvestmentKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iProjectKey = table.Column<int>(nullable: false),
                    iPeriodKey = table.Column<int>(nullable: false),
                    dAmount = table.Column<decimal>(nullable: false),
                    dResdualAmount = table.Column<decimal>(nullable: false),
                    dtStartDate = table.Column<DateTime>(nullable: false),
                    dtEndDate = table.Column<DateTime>(nullable: false),
                    sDescription = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investments", x => x.iInvestmentKey);
                    table.ForeignKey(
                        name: "FK_Investments_Periods_iPeriodKey",
                        column: x => x.iPeriodKey,
                        principalSchema: "pms",
                        principalTable: "Periods",
                        principalColumn: "iPeriodKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Investments_ProjectInfo_iProjectKey",
                        column: x => x.iProjectKey,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Opportunities",
                schema: "pms",
                columns: table => new
                {
                    iOpportunityID = table.Column<int>(nullable: false),
                    AccountManagerID = table.Column<string>(nullable: true),
                    MaintenanceContactID = table.Column<int>(nullable: true),
                    OpportunityTypeID = table.Column<int>(nullable: false),
                    OpportunityStatusID = table.Column<int>(nullable: false),
                    OpportunityKindID = table.Column<int>(nullable: true),
                    InvestmentProposalID = table.Column<int>(nullable: true),
                    EnergyConceptID = table.Column<int>(nullable: true),
                    ProjectInfoID = table.Column<int>(nullable: true),
                    TechnicalPrincipalID = table.Column<int>(nullable: true),
                    DeveloperID = table.Column<string>(nullable: true),
                    ProjectManagerID = table.Column<string>(nullable: true),
                    InstallationPartnerID = table.Column<int>(nullable: true),
                    InstallationPartnerProcessID = table.Column<int>(nullable: true),
                    Customer = table.Column<string>(maxLength: 200, nullable: true),
                    RequestDate = table.Column<DateTime>(nullable: true),
                    ResidenceAmount = table.Column<decimal>(nullable: false),
                    CommercialAmount = table.Column<decimal>(nullable: false),
                    BusinessFloorSurface = table.Column<decimal>(nullable: false),
                    WEQ = table.Column<decimal>(nullable: false),
                    Chance = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    ContractDate = table.Column<DateTime>(nullable: true),
                    StartingDate = table.Column<DateTime>(nullable: true),
                    PlanningApprovalDate = table.Column<DateTime>(nullable: true),
                    StartDateRealisation = table.Column<DateTime>(nullable: true),
                    Turnover = table.Column<decimal>(nullable: false),
                    Costs = table.Column<decimal>(nullable: false),
                    ConnectionFee = table.Column<decimal>(nullable: false),
                    InstallingPrice = table.Column<decimal>(nullable: false),
                    EBITDA = table.Column<decimal>(nullable: false),
                    PurchasePriceSPV = table.Column<decimal>(nullable: false),
                    Equity = table.Column<decimal>(nullable: false),
                    Purchase = table.Column<decimal>(nullable: false),
                    ExternalCosts = table.Column<decimal>(nullable: false),
                    ExternalRevenues = table.Column<decimal>(nullable: false),
                    DevelopmentCosts = table.Column<decimal>(nullable: false),
                    Margin = table.Column<decimal>(nullable: false),
                    CashFlow = table.Column<decimal>(nullable: false),
                    TakenTurnover = table.Column<decimal>(nullable: false),
                    Portfolio = table.Column<decimal>(nullable: false),
                    WAEBITDA = table.Column<decimal>(nullable: false),
                    WASPV = table.Column<decimal>(nullable: false),
                    ContractDuration = table.Column<int>(nullable: true),
                    EndOfConstruction = table.Column<DateTime>(nullable: true),
                    ConstructionTime = table.Column<int>(nullable: true),
                    FinancingPercentage = table.Column<decimal>(type: "decimal(18, 5)", nullable: true),
                    BuildingFinancingPercentage = table.Column<decimal>(type: "decimal(18, 5)", nullable: true),
                    EquityPercentage = table.Column<decimal>(type: "decimal(18, 5)", nullable: true),
                    TakenTurnoverPercentage = table.Column<decimal>(nullable: true),
                    IRRFifteen = table.Column<decimal>(nullable: false),
                    IRRThirty = table.Column<decimal>(nullable: false),
                    PurchaseFactor = table.Column<decimal>(nullable: false),
                    ActionDate = table.Column<DateTime>(nullable: false),
                    LostToCategoryID = table.Column<int>(nullable: true),
                    LostTo = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunities", x => x.iOpportunityID);
                    table.ForeignKey(
                        name: "FK_Opportunities_EnergyConcepts_EnergyConceptID",
                        column: x => x.EnergyConceptID,
                        principalSchema: "pms",
                        principalTable: "EnergyConcepts",
                        principalColumn: "EnergyConceptID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunities_InstallationPartners_InstallationPartnerID",
                        column: x => x.InstallationPartnerID,
                        principalSchema: "pms",
                        principalTable: "InstallationPartners",
                        principalColumn: "InstallationPartnerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunities_InstallationPartnerProcesses_InstallationPartnerProcessID",
                        column: x => x.InstallationPartnerProcessID,
                        principalSchema: "pms",
                        principalTable: "InstallationPartnerProcesses",
                        principalColumn: "InstallationPartnerProcessID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunities_InvestmentProposals_InvestmentProposalID",
                        column: x => x.InvestmentProposalID,
                        principalSchema: "pms",
                        principalTable: "InvestmentProposals",
                        principalColumn: "InvestmentProposalID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunities_LostToCategories_LostToCategoryID",
                        column: x => x.LostToCategoryID,
                        principalSchema: "pms",
                        principalTable: "LostToCategories",
                        principalColumn: "LostToCategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunities_MaintenanceContacts_MaintenanceContactID",
                        column: x => x.MaintenanceContactID,
                        principalSchema: "pms",
                        principalTable: "MaintenanceContacts",
                        principalColumn: "iMaintenanceContactKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunities_OpportunityKinds_OpportunityKindID",
                        column: x => x.OpportunityKindID,
                        principalSchema: "pms",
                        principalTable: "OpportunityKinds",
                        principalColumn: "OpportunityKindID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunities_OpportunityStatus_OpportunityStatusID",
                        column: x => x.OpportunityStatusID,
                        principalSchema: "pms",
                        principalTable: "OpportunityStatus",
                        principalColumn: "OpportunityStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Opportunities_OpportunityTypes_OpportunityTypeID",
                        column: x => x.OpportunityTypeID,
                        principalSchema: "pms",
                        principalTable: "OpportunityTypes",
                        principalColumn: "OpportunityTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Opportunities_ProjectInfo_ProjectInfoID",
                        column: x => x.ProjectInfoID,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunities_TechnicalPrincipals_TechnicalPrincipalID",
                        column: x => x.TechnicalPrincipalID,
                        principalSchema: "pms",
                        principalTable: "TechnicalPrincipals",
                        principalColumn: "iTechnicalPrincipalKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Opportunities_DimProject_iOpportunityID",
                        column: x => x.iOpportunityID,
                        principalSchema: "dbo",
                        principalTable: "DimProject",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OtherDeliveryProjectInfo",
                schema: "pms",
                columns: table => new
                {
                    OtherDeliveryId = table.Column<int>(nullable: false),
                    ProjectInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherDeliveryProjectInfo", x => new { x.OtherDeliveryId, x.ProjectInfoId });
                    table.ForeignKey(
                        name: "FK_OtherDeliveryProjectInfo_OtherDeliveries_OtherDeliveryId",
                        column: x => x.OtherDeliveryId,
                        principalSchema: "pms",
                        principalTable: "OtherDeliveries",
                        principalColumn: "iOtherDeliveryKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OtherDeliveryProjectInfo_ProjectInfo_ProjectInfoId",
                        column: x => x.ProjectInfoId,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectInfoPurchDeliveryType",
                schema: "pms",
                columns: table => new
                {
                    ProjectInfoPurchId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PurchaseDeliveryTypeId = table.Column<int>(nullable: false),
                    ProjectInfoiProjectKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectInfoPurchDeliveryType", x => new { x.PurchaseDeliveryTypeId, x.ProjectInfoPurchId });
                    table.ForeignKey(
                        name: "FK_ProjectInfoPurchDeliveryType_ProjectInfo_ProjectInfoiProjectKey",
                        column: x => x.ProjectInfoiProjectKey,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectInfoPurchDeliveryType_PurchaseDeliveryTypes_PurchaseDeliveryTypeId",
                        column: x => x.PurchaseDeliveryTypeId,
                        principalSchema: "pms",
                        principalTable: "PurchaseDeliveryTypes",
                        principalColumn: "iPurchaseDeliveryTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectInfoSalesDeliveryType",
                schema: "pms",
                columns: table => new
                {
                    ProjectInfoId = table.Column<int>(nullable: false),
                    SalesDeliveryTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectInfoSalesDeliveryType", x => new { x.SalesDeliveryTypeId, x.ProjectInfoId });
                    table.ForeignKey(
                        name: "FK_ProjectInfoSalesDeliveryType_ProjectInfo_ProjectInfoId",
                        column: x => x.ProjectInfoId,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectInfoSalesDeliveryType_SalesDeliveryTypes_SalesDeliveryTypeId",
                        column: x => x.SalesDeliveryTypeId,
                        principalSchema: "pms",
                        principalTable: "SalesDeliveryTypes",
                        principalColumn: "iSalesDeliveryTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectInfoUserGroup",
                schema: "pms",
                columns: table => new
                {
                    ProjectInfoId = table.Column<int>(nullable: false),
                    UserGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectInfoUserGroup", x => new { x.UserGroupId, x.ProjectInfoId });
                    table.ForeignKey(
                        name: "FK_ProjectInfoUserGroup_ProjectInfo_ProjectInfoId",
                        column: x => x.ProjectInfoId,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectInfoUserGroup_AspNetUserGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalSchema: "pms",
                        principalTable: "AspNetUserGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectReportValidationSetLogs",
                schema: "pms",
                columns: table => new
                {
                    ProjectReportValidationSetLogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectID = table.Column<int>(nullable: false),
                    ReportValidationSetID = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    CreateByUserID = table.Column<string>(nullable: false),
                    ChangeByUserID = table.Column<string>(nullable: true),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    ChangeDateTime = table.Column<DateTime>(nullable: true),
                    ReportValidationSetID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectReportValidationSetLogs", x => x.ProjectReportValidationSetLogID);
                    table.ForeignKey(
                        name: "FK_ProjectReportValidationSetLogs_ProjectInfo_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectReportValidationSetLogs_ReportValidationSets_ReportValidationSetID",
                        column: x => x.ReportValidationSetID,
                        principalSchema: "pms",
                        principalTable: "ReportValidationSets",
                        principalColumn: "ReportValidationSetID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectReportValidationSetLogs_ReportValidationSets_ReportValidationSetID1",
                        column: x => x.ReportValidationSetID1,
                        principalSchema: "pms",
                        principalTable: "ReportValidationSets",
                        principalColumn: "ReportValidationSetID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subsidies",
                schema: "pms",
                columns: table => new
                {
                    iSubsidyKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iProjectKey = table.Column<int>(nullable: false),
                    iSubsidyCategoryKey = table.Column<int>(nullable: false),
                    dtStartDate = table.Column<DateTime>(nullable: false),
                    dtEndDate = table.Column<DateTime>(nullable: false),
                    dAmount = table.Column<decimal>(nullable: false),
                    sSubsidyDescription = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subsidies", x => x.iSubsidyKey);
                    table.ForeignKey(
                        name: "FK_Subsidies_ProjectInfo_iProjectKey",
                        column: x => x.iProjectKey,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subsidies_SubsidyCategories_iSubsidyCategoryKey",
                        column: x => x.iSubsidyCategoryKey,
                        principalSchema: "pms",
                        principalTable: "SubsidyCategories",
                        principalColumn: "iSubsidyCategoryKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeqMutations",
                schema: "pms",
                columns: table => new
                {
                    iWeqMutationKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iWeqCategoryKey = table.Column<int>(nullable: false),
                    iProjectKey = table.Column<int>(nullable: false),
                    iDispensingUnitKey = table.Column<int>(nullable: true),
                    dDate = table.Column<DateTime>(nullable: false),
                    iDone = table.Column<int>(nullable: false),
                    iToBeRealised = table.Column<int>(nullable: false),
                    iConnections = table.Column<int>(nullable: true),
                    iConnectionsToBeRealised = table.Column<int>(nullable: true),
                    dUseSurface = table.Column<decimal>(nullable: true),
                    dBusinessFloorDone = table.Column<decimal>(nullable: true),
                    dBusinessFloorToBeRealised = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeqMutations", x => x.iWeqMutationKey);
                    table.ForeignKey(
                        name: "FK_WeqMutations_DispensingUnits_iDispensingUnitKey",
                        column: x => x.iDispensingUnitKey,
                        principalSchema: "pms",
                        principalTable: "DispensingUnits",
                        principalColumn: "iDispensingUnitKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WeqMutations_ProjectInfo_iProjectKey",
                        column: x => x.iProjectKey,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeqMutations_WeqCategories_iWeqCategoryKey",
                        column: x => x.iWeqCategoryKey,
                        principalSchema: "pms",
                        principalTable: "WeqCategories",
                        principalColumn: "iWeqCategoryKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceInvoices",
                schema: "service",
                columns: table => new
                {
                    ServiceInvoiceID = table.Column<Guid>(nullable: false),
                    StatusID = table.Column<int>(nullable: false),
                    MaintenanceContactID = table.Column<int>(nullable: false),
                    ProjectID = table.Column<int>(nullable: true),
                    ServiceInvoiceCode = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    PostedDateTime = table.Column<DateTime>(nullable: true),
                    ExternalReference = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceInvoices", x => x.ServiceInvoiceID);
                    table.ForeignKey(
                        name: "FK_ServiceInvoices_MaintenanceContacts_MaintenanceContactID",
                        column: x => x.MaintenanceContactID,
                        principalSchema: "pms",
                        principalTable: "MaintenanceContacts",
                        principalColumn: "iMaintenanceContactKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceInvoices_ProjectInfo_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceInvoices_Statuses_StatusID",
                        column: x => x.StatusID,
                        principalSchema: "pms",
                        principalTable: "Statuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportRows",
                schema: "meter",
                columns: table => new
                {
                    iReportRowKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iReportKey = table.Column<int>(nullable: false),
                    iRubricKey = table.Column<int>(nullable: false),
                    dOriginalAmount = table.Column<decimal>(nullable: false),
                    dMutationAmount = table.Column<decimal>(nullable: false),
                    dBudgetAmount = table.Column<decimal>(nullable: false),
                    dCumCalculateAmount = table.Column<decimal>(nullable: false),
                    dCumReportAmount = table.Column<decimal>(nullable: false),
                    dCumBudgetAmount = table.Column<decimal>(nullable: false),
                    CalculationTypeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportRows", x => x.iReportRowKey);
                    table.ForeignKey(
                        name: "FK_ReportRows_CalculationType_CalculationTypeID",
                        column: x => x.CalculationTypeID,
                        principalSchema: "pms",
                        principalTable: "CalculationType",
                        principalColumn: "iCalculationTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportRows_Reports_iReportKey",
                        column: x => x.iReportKey,
                        principalSchema: "meter",
                        principalTable: "Reports",
                        principalColumn: "iReportKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportRows_Rubrics_iRubricKey",
                        column: x => x.iRubricKey,
                        principalSchema: "meter",
                        principalTable: "Rubrics",
                        principalColumn: "iRubricKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportValidations",
                schema: "meter",
                columns: table => new
                {
                    iReportValidationKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iReportKey = table.Column<int>(nullable: false),
                    iStatusCode = table.Column<int>(nullable: true),
                    sValidationMessage = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportValidations", x => x.iReportValidationKey);
                    table.ForeignKey(
                        name: "FK_ReportValidations_Reports_iReportKey",
                        column: x => x.iReportKey,
                        principalSchema: "meter",
                        principalTable: "Reports",
                        principalColumn: "iReportKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressDebtors",
                schema: "invoice",
                columns: table => new
                {
                    iAddressDebtorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iAddressID = table.Column<int>(nullable: false),
                    iDebtorID = table.Column<int>(nullable: false),
                    dtStartDate = table.Column<DateTime>(nullable: false),
                    dtEndDate = table.Column<DateTime>(nullable: true),
                    bIsActive = table.Column<bool>(nullable: false),
                    bFinished = table.Column<bool>(nullable: false),
                    BillingTypeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressDebtors", x => x.iAddressDebtorID);
                    table.ForeignKey(
                        name: "FK_AddressDebtors_BillingTypes_BillingTypeID",
                        column: x => x.BillingTypeID,
                        principalSchema: "invoice",
                        principalTable: "BillingTypes",
                        principalColumn: "BillingTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AddressDebtors_Addresses_iAddressID",
                        column: x => x.iAddressID,
                        principalSchema: "pms",
                        principalTable: "Addresses",
                        principalColumn: "iAddressKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressDebtors_Debtors_iDebtorID",
                        column: x => x.iDebtorID,
                        principalSchema: "invoice",
                        principalTable: "Debtors",
                        principalColumn: "iDebtorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressOccupants",
                schema: "invoice",
                columns: table => new
                {
                    iAdressOccupantID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iAddressID = table.Column<int>(nullable: false),
                    iOccupantID = table.Column<int>(nullable: false),
                    dtStartDate = table.Column<DateTime>(nullable: false),
                    dtEndDate = table.Column<DateTime>(nullable: true),
                    bIsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressOccupants", x => x.iAdressOccupantID);
                    table.ForeignKey(
                        name: "FK_AddressOccupants_Addresses_iAddressID",
                        column: x => x.iAddressID,
                        principalSchema: "pms",
                        principalTable: "Addresses",
                        principalColumn: "iAddressKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressOccupants_Occupants_iOccupantID",
                        column: x => x.iOccupantID,
                        principalSchema: "invoice",
                        principalTable: "Occupants",
                        principalColumn: "iOccupantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                schema: "invoice",
                columns: table => new
                {
                    iInvoiceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iAddressID = table.Column<int>(nullable: false),
                    iDebtorID = table.Column<int>(nullable: false),
                    iInvoiceBatchID = table.Column<int>(nullable: false),
                    dtPostingDate = table.Column<DateTime>(nullable: false),
                    sYouReference = table.Column<string>(maxLength: 35, nullable: true),
                    dtDocumentDate = table.Column<DateTime>(nullable: false),
                    sExternalDocumentNo = table.Column<string>(maxLength: 35, nullable: true),
                    sPaymentCondition = table.Column<string>(maxLength: 20, nullable: true),
                    sBookingDescription = table.Column<string>(maxLength: 50, nullable: true),
                    sConsumptionAddress = table.Column<string>(maxLength: 80, nullable: true),
                    sSettlementCode = table.Column<string>(maxLength: 20, nullable: true),
                    dNewDepositAmount = table.Column<decimal>(nullable: false),
                    dNewFixedCosts = table.Column<decimal>(nullable: false),
                    dnewMontlyAmount = table.Column<decimal>(nullable: false),
                    iYear = table.Column<int>(nullable: false),
                    iPeriod = table.Column<int>(nullable: false),
                    iStatusID = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    StatusID = table.Column<int>(nullable: true),
                    Process = table.Column<int>(nullable: false),
                    ProcessedDateTime = table.Column<DateTime>(nullable: true),
                    ProcessedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.iInvoiceID);
                    table.ForeignKey(
                        name: "FK_Invoices_AspNetUsers_ProcessedBy",
                        column: x => x.ProcessedBy,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Statuses_StatusID",
                        column: x => x.StatusID,
                        principalSchema: "pms",
                        principalTable: "Statuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Addresses_iAddressID",
                        column: x => x.iAddressID,
                        principalSchema: "pms",
                        principalTable: "Addresses",
                        principalColumn: "iAddressKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Debtors_iDebtorID",
                        column: x => x.iDebtorID,
                        principalSchema: "invoice",
                        principalTable: "Debtors",
                        principalColumn: "iDebtorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_InvoiceBatches_iInvoiceBatchID",
                        column: x => x.iInvoiceBatchID,
                        principalSchema: "invoice",
                        principalTable: "InvoiceBatches",
                        principalColumn: "iInvoiceBatchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_InvoiceStatuses_iStatusID",
                        column: x => x.iStatusID,
                        principalSchema: "invoice",
                        principalTable: "InvoiceStatuses",
                        principalColumn: "iStatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsumptionMeters",
                schema: "meter",
                columns: table => new
                {
                    iConsumptionMeterKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sConsumptionMeterNumber = table.Column<string>(maxLength: 100, nullable: false),
                    sEANCode = table.Column<string>(maxLength: 100, nullable: true),
                    iAddressKey = table.Column<int>(nullable: true),
                    iMeterTypeKey = table.Column<int>(nullable: false),
                    iConsumptionMeterSupplierKey = table.Column<int>(nullable: false),
                    iFrequencyKey = table.Column<int>(nullable: false),
                    sConsumptionMeterComment = table.Column<string>(maxLength: 150, nullable: true),
                    iBuildYear = table.Column<int>(nullable: false),
                    iServiceRunKey = table.Column<int>(nullable: true),
                    iDaysMargin = table.Column<int>(nullable: false),
                    bMeterDeleted = table.Column<bool>(nullable: false),
                    iMeasuringOfficerID = table.Column<int>(nullable: true),
                    iOperatorID = table.Column<int>(nullable: true),
                    iEnergySupplierID = table.Column<int>(nullable: true),
                    bMeterCalibrationPool = table.Column<bool>(nullable: false),
                    sMeterCodeEsight = table.Column<string>(maxLength: 15, nullable: true),
                    iEsighMeterID = table.Column<int>(nullable: true),
                    sMeterPoolCode = table.Column<string>(maxLength: 100, nullable: true),
                    bVisibleForCustomers = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptionMeters", x => x.iConsumptionMeterKey);
                    table.ForeignKey(
                        name: "FK_ConsumptionMeters_Addresses_iAddressKey",
                        column: x => x.iAddressKey,
                        principalSchema: "pms",
                        principalTable: "Addresses",
                        principalColumn: "iAddressKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsumptionMeters_ConsumptionMeterSuppliers_iConsumptionMeterSupplierKey",
                        column: x => x.iConsumptionMeterSupplierKey,
                        principalSchema: "meter",
                        principalTable: "ConsumptionMeterSuppliers",
                        principalColumn: "iConsumptionMeterSupplierKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumptionMeters_EnergySuppliers_iEnergySupplierID",
                        column: x => x.iEnergySupplierID,
                        principalSchema: "pms",
                        principalTable: "EnergySuppliers",
                        principalColumn: "iEnergySupplierID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsumptionMeters_Frequencies_iFrequencyKey",
                        column: x => x.iFrequencyKey,
                        principalSchema: "meter",
                        principalTable: "Frequencies",
                        principalColumn: "iFrequencyKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumptionMeters_MeasuringOfficers_iMeasuringOfficerID",
                        column: x => x.iMeasuringOfficerID,
                        principalSchema: "pms",
                        principalTable: "MeasuringOfficers",
                        principalColumn: "iMeasuringOfficerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsumptionMeters_MeterTypes_iMeterTypeKey",
                        column: x => x.iMeterTypeKey,
                        principalSchema: "meter",
                        principalTable: "MeterTypes",
                        principalColumn: "iMeterTypeKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumptionMeters_Operators_iOperatorID",
                        column: x => x.iOperatorID,
                        principalSchema: "pms",
                        principalTable: "Operators",
                        principalColumn: "iOperatorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsumptionMeters_ServiceRuns_iServiceRunKey",
                        column: x => x.iServiceRunKey,
                        principalSchema: "meter",
                        principalTable: "ServiceRuns",
                        principalColumn: "iServiceRunKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CostlierValues",
                schema: "meter",
                columns: table => new
                {
                    CostlierValueID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    TotalConsumption = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    LossOfDistribution = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    PollTax = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    TotalCostlier = table.Column<decimal>(type: "decimal(18, 5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostlierValues", x => x.CostlierValueID);
                    table.ForeignKey(
                        name: "FK_CostlierValues_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalSchema: "pms",
                        principalTable: "Addresses",
                        principalColumn: "iAddressKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RateCardScaleHistories",
                schema: "meter",
                columns: table => new
                {
                    RateCardScaleHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DebtorID = table.Column<int>(nullable: false),
                    AddressID = table.Column<int>(nullable: false),
                    RateCardRowID = table.Column<int>(nullable: false),
                    Consumed = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    Period = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateCardScaleHistories", x => x.RateCardScaleHistoryID);
                    table.ForeignKey(
                        name: "FK_RateCardScaleHistories_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalSchema: "pms",
                        principalTable: "Addresses",
                        principalColumn: "iAddressKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RateCardScaleHistories_Debtors_DebtorID",
                        column: x => x.DebtorID,
                        principalSchema: "invoice",
                        principalTable: "Debtors",
                        principalColumn: "iDebtorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RateCardScaleHistories_RateCardRows_RateCardRowID",
                        column: x => x.RateCardRowID,
                        principalSchema: "meter",
                        principalTable: "RateCardRows",
                        principalColumn: "iRateCardRowKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressRateCards",
                schema: "pms",
                columns: table => new
                {
                    iAddressKey = table.Column<int>(nullable: false),
                    iRateCardKey = table.Column<int>(nullable: false),
                    dtStartDate = table.Column<DateTime>(nullable: false),
                    dtEndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressRateCards", x => new { x.iAddressKey, x.iRateCardKey });
                    table.ForeignKey(
                        name: "FK_AddressRateCards_Addresses_iAddressKey",
                        column: x => x.iAddressKey,
                        principalSchema: "pms",
                        principalTable: "Addresses",
                        principalColumn: "iAddressKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressRateCards_RateCards_iRateCardKey",
                        column: x => x.iRateCardKey,
                        principalSchema: "meter",
                        principalTable: "RateCards",
                        principalColumn: "iRateCardKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                schema: "service",
                columns: table => new
                {
                    iTicketID = table.Column<int>(nullable: false),
                    sUserID = table.Column<string>(nullable: true),
                    iOccupantID = table.Column<int>(nullable: true),
                    iProjectID = table.Column<int>(nullable: true),
                    iTicketStatusID = table.Column<int>(nullable: false),
                    iTicketTypeID = table.Column<int>(nullable: false),
                    iDebtorID = table.Column<int>(nullable: true),
                    iAddressID = table.Column<int>(nullable: true),
                    sTitle = table.Column<string>(maxLength: 500, nullable: false),
                    sMessage = table.Column<string>(nullable: false),
                    bAssigned = table.Column<bool>(nullable: false),
                    bFinished = table.Column<bool>(nullable: false),
                    dtCreateDateTime = table.Column<DateTime>(nullable: false),
                    dtFinishedDateTime = table.Column<DateTime>(nullable: true),
                    sSolution = table.Column<string>(nullable: true),
                    sEmail = table.Column<string>(maxLength: 100, nullable: true),
                    sPhoneNumber = table.Column<string>(maxLength: 30, nullable: true),
                    Suppressed = table.Column<bool>(nullable: false),
                    SuppressedUntil = table.Column<DateTime>(nullable: true),
                    MailConfigID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.iTicketID);
                    table.ForeignKey(
                        name: "FK_Tickets_MailConfigs_MailConfigID",
                        column: x => x.MailConfigID,
                        principalSchema: "pms",
                        principalTable: "MailConfigs",
                        principalColumn: "MailConfigID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Addresses_iAddressID",
                        column: x => x.iAddressID,
                        principalSchema: "pms",
                        principalTable: "Addresses",
                        principalColumn: "iAddressKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Debtors_iDebtorID",
                        column: x => x.iDebtorID,
                        principalSchema: "invoice",
                        principalTable: "Debtors",
                        principalColumn: "iDebtorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Occupants_iOccupantID",
                        column: x => x.iOccupantID,
                        principalSchema: "invoice",
                        principalTable: "Occupants",
                        principalColumn: "iOccupantID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_ProjectInfo_iProjectID",
                        column: x => x.iProjectID,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_ResponseConcepts_iTicketID",
                        column: x => x.iTicketID,
                        principalSchema: "service",
                        principalTable: "ResponseConcepts",
                        principalColumn: "iTicketID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_TicketStatuses_iTicketStatusID",
                        column: x => x.iTicketStatusID,
                        principalSchema: "service",
                        principalTable: "TicketStatuses",
                        principalColumn: "iTicketStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_TicketTypes_iTicketTypeID",
                        column: x => x.iTicketTypeID,
                        principalSchema: "service",
                        principalTable: "TicketTypes",
                        principalColumn: "iTicketTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_sUserID",
                        column: x => x.sUserID,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BudgetDimensions",
                schema: "budget",
                columns: table => new
                {
                    iBudgetDimensionKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iBudgetDimensionTypeKey = table.Column<int>(nullable: false),
                    iBudgetBaseKey = table.Column<int>(nullable: false),
                    iBudgetSettingKey = table.Column<int>(nullable: false),
                    iProjectKey = table.Column<int>(nullable: true),
                    iYearPreview = table.Column<int>(nullable: false),
                    iEndPeriodPreview = table.Column<int>(nullable: false),
                    iYearBudget = table.Column<int>(nullable: false),
                    sBudgetDimensionDescription = table.Column<string>(maxLength: 250, nullable: false),
                    BudgetReferenceId = table.Column<Guid>(nullable: true),
                    BaseOnDefaultProfile = table.Column<bool>(nullable: false),
                    StartMonth = table.Column<int>(nullable: true),
                    Note = table.Column<string>(maxLength: 1000, nullable: true),
                    dtLastModified = table.Column<DateTime>(nullable: false),
                    bDraft = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetDimensions", x => x.iBudgetDimensionKey);
                    table.ForeignKey(
                        name: "FK_BudgetDimensions_BaseModels_BudgetReferenceId",
                        column: x => x.BudgetReferenceId,
                        principalSchema: "pms",
                        principalTable: "BaseModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetDimensions_BudgetBases_iBudgetBaseKey",
                        column: x => x.iBudgetBaseKey,
                        principalSchema: "budget",
                        principalTable: "BudgetBases",
                        principalColumn: "iBudgetBaseKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetDimensions_BudgetDimensionTypes_iBudgetDimensionTypeKey",
                        column: x => x.iBudgetDimensionTypeKey,
                        principalSchema: "budget",
                        principalTable: "BudgetDimensionTypes",
                        principalColumn: "iBudgetDimensionTypeKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetDimensions_BudgetSettings_iBudgetSettingKey",
                        column: x => x.iBudgetSettingKey,
                        principalSchema: "budget",
                        principalTable: "BudgetSettings",
                        principalColumn: "iBudgetSettingKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetDimensions_DimProject_iProjectKey",
                        column: x => x.iProjectKey,
                        principalSchema: "dbo",
                        principalTable: "DimProject",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityNotes",
                schema: "pms",
                columns: table => new
                {
                    iOpportunityNoteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iOpportunityID = table.Column<int>(nullable: false),
                    sNote = table.Column<string>(nullable: false),
                    dtCreateDateTime = table.Column<DateTime>(nullable: false),
                    sUserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityNotes", x => x.iOpportunityNoteID);
                    table.ForeignKey(
                        name: "FK_OpportunityNotes_Opportunities_iOpportunityID",
                        column: x => x.iOpportunityID,
                        principalSchema: "pms",
                        principalTable: "Opportunities",
                        principalColumn: "iOpportunityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityValueLogs",
                schema: "pms",
                columns: table => new
                {
                    iOpportunityValueLogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iOpportunityID = table.Column<int>(nullable: false),
                    dtDateTime = table.Column<DateTime>(nullable: false),
                    dChance = table.Column<decimal>(nullable: false),
                    dEbitda = table.Column<decimal>(nullable: false),
                    dWAEbitda = table.Column<decimal>(nullable: false),
                    dWAEbitdaMutation = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityValueLogs", x => x.iOpportunityValueLogID);
                    table.ForeignKey(
                        name: "FK_OpportunityValueLogs_Opportunities_iOpportunityID",
                        column: x => x.iOpportunityID,
                        principalSchema: "pms",
                        principalTable: "Opportunities",
                        principalColumn: "iOpportunityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deposits",
                schema: "invoice",
                columns: table => new
                {
                    iDepositID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iAddressDebtorID = table.Column<int>(nullable: false),
                    dAmount = table.Column<decimal>(nullable: false),
                    dAmountexVAT = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    dtStartDate = table.Column<DateTime>(nullable: false),
                    dtEndDate = table.Column<DateTime>(nullable: true),
                    bIsActive = table.Column<bool>(nullable: false),
                    bDecreasedByDebtor = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposits", x => x.iDepositID);
                    table.ForeignKey(
                        name: "FK_Deposits_AddressDebtors_iAddressDebtorID",
                        column: x => x.iAddressDebtorID,
                        principalSchema: "invoice",
                        principalTable: "AddressDebtors",
                        principalColumn: "iAddressDebtorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceChecks",
                schema: "invoice",
                columns: table => new
                {
                    InvoiceCheckID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoiceID = table.Column<int>(nullable: false),
                    InvoiceCheckOptionID = table.Column<int>(nullable: false),
                    Valid = table.Column<bool>(nullable: false),
                    Message = table.Column<string>(maxLength: 250, nullable: true),
                    CheckDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceChecks", x => x.InvoiceCheckID);
                    table.ForeignKey(
                        name: "FK_InvoiceChecks_InvoiceCheckOptions_InvoiceCheckOptionID",
                        column: x => x.InvoiceCheckOptionID,
                        principalSchema: "invoice",
                        principalTable: "InvoiceCheckOptions",
                        principalColumn: "InvoiceCheckOptionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceChecks_Invoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalSchema: "invoice",
                        principalTable: "Invoices",
                        principalColumn: "iInvoiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLines",
                schema: "invoice",
                columns: table => new
                {
                    iInvoiceLineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iInvoiceID = table.Column<int>(nullable: false),
                    iRateCardRowID = table.Column<int>(nullable: true),
                    iLedgerNumber = table.Column<int>(nullable: false),
                    dQuantity = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    dAmount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    dTotalAmount = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    sDescription = table.Column<string>(maxLength: 50, nullable: false),
                    sDescription2 = table.Column<string>(maxLength: 50, nullable: true),
                    dUnitPrice = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    iRubricTypeID = table.Column<int>(nullable: false),
                    iUnitID = table.Column<int>(nullable: false),
                    dtStartDate = table.Column<DateTime>(nullable: true),
                    dtEndDate = table.Column<DateTime>(nullable: true),
                    dStartPosition = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    dEndPosition = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    dConsumption = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    bIsEndCalculation = table.Column<bool>(nullable: false),
                    sSettlementCode = table.Column<string>(maxLength: 20, nullable: true),
                    sSettlementText = table.Column<string>(maxLength: 30, nullable: true),
                    Discount = table.Column<bool>(nullable: false),
                    VatConditionCode = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLines", x => x.iInvoiceLineID);
                    table.ForeignKey(
                        name: "FK_InvoiceLines_Invoices_iInvoiceID",
                        column: x => x.iInvoiceID,
                        principalSchema: "invoice",
                        principalTable: "Invoices",
                        principalColumn: "iInvoiceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceLines_RateCardRows_iRateCardRowID",
                        column: x => x.iRateCardRowID,
                        principalSchema: "meter",
                        principalTable: "RateCardRows",
                        principalColumn: "iRateCardRowKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceLines_RubricTypes_iRubricTypeID",
                        column: x => x.iRubricTypeID,
                        principalSchema: "meter",
                        principalTable: "RubricTypes",
                        principalColumn: "iRubricTypeKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceLines_Units_iUnitID",
                        column: x => x.iUnitID,
                        principalSchema: "meter",
                        principalTable: "Units",
                        principalColumn: "iUnitKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentHistory",
                schema: "invoice",
                columns: table => new
                {
                    iPaymentHistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iDebtorID = table.Column<int>(nullable: false),
                    iAddressID = table.Column<int>(nullable: true),
                    sInvoiceNumber = table.Column<string>(maxLength: 20, nullable: true),
                    sDescription = table.Column<string>(maxLength: 50, nullable: true),
                    dtInvoiceDate = table.Column<DateTime>(nullable: false),
                    dtExpirationDate = table.Column<DateTime>(nullable: false),
                    dtPaymentDate = table.Column<DateTime>(nullable: true),
                    dAmountinVAT = table.Column<decimal>(nullable: false),
                    dOpenAmountinVAT = table.Column<decimal>(nullable: false),
                    sInvoiceLink = table.Column<string>(nullable: true),
                    iInvoiceStatus = table.Column<int>(nullable: true),
                    sInvoiceStatus = table.Column<string>(maxLength: 150, nullable: true),
                    InvoicePeriod = table.Column<string>(maxLength: 10, nullable: true),
                    InvoiceID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentHistory", x => x.iPaymentHistoryID);
                    table.ForeignKey(
                        name: "FK_PaymentHistory_Invoices_InvoiceID",
                        column: x => x.InvoiceID,
                        principalSchema: "invoice",
                        principalTable: "Invoices",
                        principalColumn: "iInvoiceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentHistory_Addresses_iAddressID",
                        column: x => x.iAddressID,
                        principalSchema: "pms",
                        principalTable: "Addresses",
                        principalColumn: "iAddressKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentHistory_Debtors_iDebtorID",
                        column: x => x.iDebtorID,
                        principalSchema: "invoice",
                        principalTable: "Debtors",
                        principalColumn: "iDebtorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Counters",
                schema: "meter",
                columns: table => new
                {
                    iCounterKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sCounterCode = table.Column<string>(maxLength: 100, nullable: false),
                    iUnitKey = table.Column<int>(nullable: false),
                    iConsumptionMeterKey = table.Column<int>(nullable: true),
                    iCounterTypeKey = table.Column<int>(nullable: false),
                    bHasTurnOverRatio = table.Column<bool>(nullable: false),
                    iTurnOverRatioFrom = table.Column<int>(nullable: false),
                    iTurnOverRatioTo = table.Column<int>(nullable: false),
                    dCorrectionMutation = table.Column<decimal>(nullable: false),
                    iServiceRunKey = table.Column<int>(nullable: true),
                    iCompletedRounds = table.Column<int>(nullable: true),
                    iMaxCounterValue = table.Column<int>(nullable: true),
                    bActive = table.Column<bool>(nullable: false),
                    DefaultPercentageDeviationFromAverage = table.Column<bool>(nullable: false),
                    PercentageDeviationFromAverage = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    StatusID = table.Column<int>(nullable: true),
                    StatusDescription = table.Column<string>(maxLength: 500, nullable: true),
                    StatusChangeDescription = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counters", x => x.iCounterKey);
                    table.ForeignKey(
                        name: "FK_Counters_Statuses_StatusID",
                        column: x => x.StatusID,
                        principalSchema: "pms",
                        principalTable: "Statuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Counters_ConsumptionMeters_iConsumptionMeterKey",
                        column: x => x.iConsumptionMeterKey,
                        principalSchema: "meter",
                        principalTable: "ConsumptionMeters",
                        principalColumn: "iConsumptionMeterKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Counters_CounterTypes_iCounterTypeKey",
                        column: x => x.iCounterTypeKey,
                        principalSchema: "meter",
                        principalTable: "CounterTypes",
                        principalColumn: "iCounterTypeKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Counters_ServiceRuns_iServiceRunKey",
                        column: x => x.iServiceRunKey,
                        principalSchema: "meter",
                        principalTable: "ServiceRuns",
                        principalColumn: "iServiceRunKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Counters_Units_iUnitKey",
                        column: x => x.iUnitKey,
                        principalSchema: "meter",
                        principalTable: "Units",
                        principalColumn: "iUnitKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeterChangeLogs",
                schema: "meter",
                columns: table => new
                {
                    iMeterChangeKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iConsumptionMeterKey = table.Column<int>(nullable: false),
                    iAddressKeyFrom = table.Column<int>(nullable: true),
                    iAddressKeyTo = table.Column<int>(nullable: true),
                    iEventKey = table.Column<int>(nullable: false),
                    iChangeReasonKey = table.Column<int>(nullable: true),
                    iExchangeFormKey = table.Column<int>(nullable: true),
                    sNotes = table.Column<string>(maxLength: 500, nullable: true),
                    dtEffectiveDateTime = table.Column<DateTime>(nullable: false),
                    dtEventCreated = table.Column<DateTime>(nullable: false),
                    sUser = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeterChangeLogs", x => x.iMeterChangeKey);
                    table.ForeignKey(
                        name: "FK_MeterChangeLogs_Addresses_iAddressKeyFrom",
                        column: x => x.iAddressKeyFrom,
                        principalSchema: "pms",
                        principalTable: "Addresses",
                        principalColumn: "iAddressKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeterChangeLogs_Addresses_iAddressKeyTo",
                        column: x => x.iAddressKeyTo,
                        principalSchema: "pms",
                        principalTable: "Addresses",
                        principalColumn: "iAddressKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeterChangeLogs_ChangeReasons_iChangeReasonKey",
                        column: x => x.iChangeReasonKey,
                        principalSchema: "meter",
                        principalTable: "ChangeReasons",
                        principalColumn: "iChangeReasonKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MeterChangeLogs_ConsumptionMeters_iConsumptionMeterKey",
                        column: x => x.iConsumptionMeterKey,
                        principalSchema: "meter",
                        principalTable: "ConsumptionMeters",
                        principalColumn: "iConsumptionMeterKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeterChangeLogs_Events_iEventKey",
                        column: x => x.iEventKey,
                        principalSchema: "meter",
                        principalTable: "Events",
                        principalColumn: "iEventKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeterChangeLogs_ExchangeForms_iExchangeFormKey",
                        column: x => x.iExchangeFormKey,
                        principalSchema: "meter",
                        principalTable: "ExchangeForms",
                        principalColumn: "iExchangeFormKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketLabel",
                schema: "pms",
                columns: table => new
                {
                    TicketId = table.Column<int>(nullable: false),
                    LabelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketLabel", x => new { x.TicketId, x.LabelId });
                    table.ForeignKey(
                        name: "FK_TicketLabel_Labels_LabelId",
                        column: x => x.LabelId,
                        principalSchema: "service",
                        principalTable: "Labels",
                        principalColumn: "iLabelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketLabel_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "service",
                        principalTable: "Tickets",
                        principalColumn: "iTicketID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Responses",
                schema: "service",
                columns: table => new
                {
                    iResponseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iTicketID = table.Column<int>(nullable: false),
                    sUserID = table.Column<string>(nullable: true),
                    iResponseTypeID = table.Column<int>(nullable: false),
                    dtCreateDateTime = table.Column<DateTime>(nullable: false),
                    sMessage = table.Column<string>(nullable: false),
                    bIncoming = table.Column<bool>(nullable: false),
                    sFromEmail = table.Column<string>(maxLength: 150, nullable: true),
                    sFromName = table.Column<string>(maxLength: 150, nullable: true),
                    sToEmail = table.Column<string>(maxLength: 150, nullable: true),
                    sToName = table.Column<string>(maxLength: 150, nullable: true),
                    BCCList = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responses", x => x.iResponseID);
                    table.ForeignKey(
                        name: "FK_Responses_ResponseTypes_iResponseTypeID",
                        column: x => x.iResponseTypeID,
                        principalSchema: "service",
                        principalTable: "ResponseTypes",
                        principalColumn: "iResponseTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Responses_Tickets_iTicketID",
                        column: x => x.iTicketID,
                        principalSchema: "service",
                        principalTable: "Tickets",
                        principalColumn: "iTicketID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTickets",
                schema: "service",
                columns: table => new
                {
                    ServiceTicketID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    StatusID = table.Column<int>(nullable: false),
                    ProjectID = table.Column<int>(nullable: false),
                    MaintenanceContactID = table.Column<int>(nullable: true),
                    AddressID = table.Column<int>(nullable: false),
                    OccupantID = table.Column<int>(nullable: true),
                    DebtorID = table.Column<int>(nullable: true),
                    ServiceTicketTypeID = table.Column<int>(nullable: true),
                    TicketCode = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    FinishedDateTime = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 500, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    Urgent = table.Column<bool>(nullable: false),
                    ExpectedAmount = table.Column<decimal>(nullable: true),
                    MaintenanceContactInformed = table.Column<bool>(nullable: false),
                    TicketiTicketID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTickets", x => x.ServiceTicketID);
                    table.ForeignKey(
                        name: "FK_ServiceTickets_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalSchema: "pms",
                        principalTable: "Addresses",
                        principalColumn: "iAddressKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceTickets_Debtors_DebtorID",
                        column: x => x.DebtorID,
                        principalSchema: "invoice",
                        principalTable: "Debtors",
                        principalColumn: "iDebtorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceTickets_MaintenanceContacts_MaintenanceContactID",
                        column: x => x.MaintenanceContactID,
                        principalSchema: "pms",
                        principalTable: "MaintenanceContacts",
                        principalColumn: "iMaintenanceContactKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceTickets_Occupants_OccupantID",
                        column: x => x.OccupantID,
                        principalSchema: "invoice",
                        principalTable: "Occupants",
                        principalColumn: "iOccupantID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceTickets_ProjectInfo_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "pms",
                        principalTable: "ProjectInfo",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceTickets_ServiceTicketTypes_ServiceTicketTypeID",
                        column: x => x.ServiceTicketTypeID,
                        principalSchema: "service",
                        principalTable: "ServiceTicketTypes",
                        principalColumn: "ServiceTicketTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceTickets_Statuses_StatusID",
                        column: x => x.StatusID,
                        principalSchema: "pms",
                        principalTable: "Statuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceTickets_Tickets_TicketiTicketID",
                        column: x => x.TicketiTicketID,
                        principalSchema: "service",
                        principalTable: "Tickets",
                        principalColumn: "iTicketID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceTickets_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketLogs",
                schema: "service",
                columns: table => new
                {
                    iTicketLogID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iTicketID = table.Column<int>(nullable: false),
                    sUserID = table.Column<string>(nullable: true),
                    sActivity = table.Column<string>(maxLength: 500, nullable: false),
                    dtTimestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketLogs", x => x.iTicketLogID);
                    table.ForeignKey(
                        name: "FK_TicketLogs_Tickets_iTicketID",
                        column: x => x.iTicketID,
                        principalSchema: "service",
                        principalTable: "Tickets",
                        principalColumn: "iTicketID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketLogs_AspNetUsers_sUserID",
                        column: x => x.sUserID,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BudgetDimensionRules",
                schema: "budget",
                columns: table => new
                {
                    iBudgetDimensionRuleKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iReportingStructureKey = table.Column<int>(nullable: false),
                    iBudgetDimensionKey = table.Column<int>(nullable: false),
                    iRecNo = table.Column<int>(nullable: false),
                    dTotal = table.Column<decimal>(nullable: false),
                    dJanuary = table.Column<decimal>(nullable: false),
                    dFebruary = table.Column<decimal>(nullable: false),
                    dMarch = table.Column<decimal>(nullable: false),
                    dApril = table.Column<decimal>(nullable: false),
                    dMay = table.Column<decimal>(nullable: false),
                    dJune = table.Column<decimal>(nullable: false),
                    dJuly = table.Column<decimal>(nullable: false),
                    dAugust = table.Column<decimal>(nullable: false),
                    dSeptember = table.Column<decimal>(nullable: false),
                    dOctober = table.Column<decimal>(nullable: false),
                    dNovember = table.Column<decimal>(nullable: false),
                    dDecember = table.Column<decimal>(nullable: false),
                    PercentJanuary = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    PercentFebruary = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    PercentMarch = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    PercentApril = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    PercentMay = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    PercentJune = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    PercentJuly = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    PercentAugust = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    PercentSeptember = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    PercentOctober = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    PercentNovember = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    PercentDecember = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    bSpatie = table.Column<bool>(nullable: false),
                    bSubtotaal = table.Column<bool>(nullable: false),
                    sComment = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetDimensionRules", x => x.iBudgetDimensionRuleKey);
                    table.ForeignKey(
                        name: "FK_BudgetDimensionRules_BudgetDimensions_iBudgetDimensionKey",
                        column: x => x.iBudgetDimensionKey,
                        principalSchema: "budget",
                        principalTable: "BudgetDimensions",
                        principalColumn: "iBudgetDimensionKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetDimensionRules_ReportingStructure_iReportingStructureKey",
                        column: x => x.iReportingStructureKey,
                        principalSchema: "donkervoort",
                        principalTable: "ReportingStructure",
                        principalColumn: "iReportingStructureKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FactBudget",
                schema: "dbo",
                columns: table => new
                {
                    iFactBudgetKey = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iBudgetDimensionKey = table.Column<int>(nullable: true),
                    iBudgetVersionKey = table.Column<int>(nullable: true),
                    iPartnerID = table.Column<int>(nullable: true),
                    iCustomerID = table.Column<int>(nullable: true),
                    iLedgerKey = table.Column<int>(nullable: true),
                    iProjectKey = table.Column<int>(nullable: true),
                    iUserKey = table.Column<int>(nullable: true),
                    iYear = table.Column<int>(nullable: true),
                    sPeriod = table.Column<string>(maxLength: 10, nullable: true),
                    sReportingCode = table.Column<int>(nullable: true),
                    sReportingCode_old = table.Column<int>(nullable: true),
                    dcAmountBudget = table.Column<decimal>(nullable: true),
                    sComment = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactBudget", x => x.iFactBudgetKey);
                    table.ForeignKey(
                        name: "FK_FactBudget_BudgetDimensions_iBudgetDimensionKey",
                        column: x => x.iBudgetDimensionKey,
                        principalSchema: "budget",
                        principalTable: "BudgetDimensions",
                        principalColumn: "iBudgetDimensionKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FactBudget_DimProject_iProjectKey",
                        column: x => x.iProjectKey,
                        principalSchema: "dbo",
                        principalTable: "DimProject",
                        principalColumn: "iProjectKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlindConsumptions",
                schema: "meter",
                columns: table => new
                {
                    iBlindConsumptionKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iCounterKey = table.Column<int>(nullable: false),
                    iAddressKey = table.Column<int>(nullable: false),
                    iServiceRunKey = table.Column<int>(nullable: false),
                    dtStartDateTime = table.Column<DateTime>(nullable: false),
                    dtEndDateTime = table.Column<DateTime>(nullable: false),
                    dBlindConsumption = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlindConsumptions", x => x.iBlindConsumptionKey);
                    table.ForeignKey(
                        name: "FK_BlindConsumptions_Addresses_iAddressKey",
                        column: x => x.iAddressKey,
                        principalSchema: "pms",
                        principalTable: "Addresses",
                        principalColumn: "iAddressKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlindConsumptions_Counters_iCounterKey",
                        column: x => x.iCounterKey,
                        principalSchema: "meter",
                        principalTable: "Counters",
                        principalColumn: "iCounterKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlindConsumptions_ServiceRuns_iServiceRunKey",
                        column: x => x.iServiceRunKey,
                        principalSchema: "meter",
                        principalTable: "ServiceRuns",
                        principalColumn: "iServiceRunKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsumptionUnvalidated",
                schema: "meter",
                columns: table => new
                {
                    iConsumptionUnvalidatedID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iAddressKey = table.Column<int>(nullable: false),
                    iCounterKey = table.Column<int>(nullable: false),
                    iServiceRunKey = table.Column<int>(nullable: false),
                    dtImportDateTime = table.Column<DateTime>(nullable: false),
                    dtReadingDate = table.Column<DateTime>(nullable: false),
                    dPosition = table.Column<decimal>(nullable: false),
                    bValidated = table.Column<bool>(nullable: false),
                    sValidatedBy = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumptionUnvalidated", x => x.iConsumptionUnvalidatedID);
                    table.ForeignKey(
                        name: "FK_ConsumptionUnvalidated_Addresses_iAddressKey",
                        column: x => x.iAddressKey,
                        principalSchema: "pms",
                        principalTable: "Addresses",
                        principalColumn: "iAddressKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumptionUnvalidated_Counters_iCounterKey",
                        column: x => x.iCounterKey,
                        principalSchema: "meter",
                        principalTable: "Counters",
                        principalColumn: "iCounterKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumptionUnvalidated_ServiceRuns_iServiceRunKey",
                        column: x => x.iServiceRunKey,
                        principalSchema: "meter",
                        principalTable: "ServiceRuns",
                        principalColumn: "iServiceRunKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CounterStatus",
                schema: "meter",
                columns: table => new
                {
                    iCounterStatusKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iCounterKey = table.Column<int>(nullable: false),
                    dtDateTime = table.Column<DateTime>(nullable: false),
                    sMessage = table.Column<string>(maxLength: 250, nullable: false),
                    bHasError = table.Column<bool>(nullable: false),
                    bHasNoConsumption = table.Column<bool>(nullable: false),
                    bHasNoRateCard = table.Column<bool>(nullable: false),
                    bLastServiceRunHasError = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CounterStatus", x => x.iCounterStatusKey);
                    table.ForeignKey(
                        name: "FK_CounterStatus_Counters_iCounterKey",
                        column: x => x.iCounterKey,
                        principalSchema: "meter",
                        principalTable: "Counters",
                        principalColumn: "iCounterKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CounterYearConsumptions",
                schema: "meter",
                columns: table => new
                {
                    CounterYearConsumptionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CounterID = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Consumption = table.Column<decimal>(type: "decimal(18, 5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CounterYearConsumptions", x => x.CounterYearConsumptionID);
                    table.ForeignKey(
                        name: "FK_CounterYearConsumptions_Counters_CounterID",
                        column: x => x.CounterID,
                        principalSchema: "meter",
                        principalTable: "Counters",
                        principalColumn: "iCounterKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaximumPowers",
                schema: "meter",
                columns: table => new
                {
                    iMaximumPowerKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iCounterKey = table.Column<int>(nullable: false),
                    iAddressKey = table.Column<int>(nullable: false),
                    iServiceRunKey = table.Column<int>(nullable: false),
                    dtStartDateTime = table.Column<DateTime>(nullable: false),
                    dtEndDateTime = table.Column<DateTime>(nullable: false),
                    dMaximumPower = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaximumPowers", x => x.iMaximumPowerKey);
                    table.ForeignKey(
                        name: "FK_MaximumPowers_Counters_iCounterKey",
                        column: x => x.iCounterKey,
                        principalSchema: "meter",
                        principalTable: "Counters",
                        principalColumn: "iCounterKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaximumPowers_ServiceRuns_iServiceRunKey",
                        column: x => x.iServiceRunKey,
                        principalSchema: "meter",
                        principalTable: "ServiceRuns",
                        principalColumn: "iServiceRunKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CounterChangeLogs",
                schema: "meter",
                columns: table => new
                {
                    iCounterChangeLogKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iCounterKey = table.Column<int>(nullable: false),
                    iMeterChangeKey = table.Column<int>(nullable: true),
                    iConsumptionMeterKeyFrom = table.Column<int>(nullable: true),
                    iConsumptionMeterKeyTo = table.Column<int>(nullable: true),
                    iEventKey = table.Column<int>(nullable: false),
                    dEndPosition = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    dtEffectiveDateTime = table.Column<DateTime>(nullable: false),
                    dtEventCreated = table.Column<DateTime>(nullable: false),
                    sUser = table.Column<string>(nullable: false),
                    iExchangeFormKey = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CounterChangeLogs", x => x.iCounterChangeLogKey);
                    table.ForeignKey(
                        name: "FK_CounterChangeLogs_ConsumptionMeters_iConsumptionMeterKeyFrom",
                        column: x => x.iConsumptionMeterKeyFrom,
                        principalSchema: "meter",
                        principalTable: "ConsumptionMeters",
                        principalColumn: "iConsumptionMeterKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CounterChangeLogs_ConsumptionMeters_iConsumptionMeterKeyTo",
                        column: x => x.iConsumptionMeterKeyTo,
                        principalSchema: "meter",
                        principalTable: "ConsumptionMeters",
                        principalColumn: "iConsumptionMeterKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CounterChangeLogs_Counters_iCounterKey",
                        column: x => x.iCounterKey,
                        principalSchema: "meter",
                        principalTable: "Counters",
                        principalColumn: "iCounterKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CounterChangeLogs_Events_iEventKey",
                        column: x => x.iEventKey,
                        principalSchema: "meter",
                        principalTable: "Events",
                        principalColumn: "iEventKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CounterChangeLogs_ExchangeForms_iExchangeFormKey",
                        column: x => x.iExchangeFormKey,
                        principalSchema: "meter",
                        principalTable: "ExchangeForms",
                        principalColumn: "iExchangeFormKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CounterChangeLogs_MeterChangeLogs_iMeterChangeKey",
                        column: x => x.iMeterChangeKey,
                        principalSchema: "meter",
                        principalTable: "MeterChangeLogs",
                        principalColumn: "iMeterChangeKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailAddresses",
                schema: "service",
                columns: table => new
                {
                    iEmailAddressID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iResponseID = table.Column<int>(nullable: false),
                    sEmailAddress = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAddresses", x => x.iEmailAddressID);
                    table.ForeignKey(
                        name: "FK_EmailAddresses_Responses_iResponseID",
                        column: x => x.iResponseID,
                        principalSchema: "service",
                        principalTable: "Responses",
                        principalColumn: "iResponseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MailAttachments",
                schema: "service",
                columns: table => new
                {
                    iMailAttachmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sFileName = table.Column<string>(nullable: true),
                    sContentType = table.Column<string>(nullable: true),
                    bByteArray = table.Column<byte[]>(nullable: true),
                    iResponseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailAttachments", x => x.iMailAttachmentID);
                    table.ForeignKey(
                        name: "FK_MailAttachments_Responses_iResponseID",
                        column: x => x.iResponseID,
                        principalSchema: "service",
                        principalTable: "Responses",
                        principalColumn: "iResponseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceMessages",
                schema: "service",
                columns: table => new
                {
                    ServiceMessageID = table.Column<Guid>(nullable: false),
                    ServiceTicketID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    FromDisplayName = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    Body = table.Column<string>(nullable: false),
                    IsInternalNote = table.Column<bool>(nullable: false),
                    OutGoing = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceMessages", x => x.ServiceMessageID);
                    table.ForeignKey(
                        name: "FK_ServiceMessages_ServiceTickets_ServiceTicketID",
                        column: x => x.ServiceTicketID,
                        principalSchema: "service",
                        principalTable: "ServiceTickets",
                        principalColumn: "ServiceTicketID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                schema: "service",
                columns: table => new
                {
                    WorkOrderID = table.Column<Guid>(nullable: false),
                    ServiceTicketID = table.Column<Guid>(nullable: false),
                    StatusID = table.Column<int>(nullable: false),
                    SolutionCategoryID = table.Column<int>(nullable: true),
                    MechanicInputID = table.Column<Guid>(nullable: true),
                    WorkOrderCode = table.Column<int>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    FinishedDateTime = table.Column<DateTime>(nullable: true),
                    Instruction = table.Column<string>(maxLength: 1000, nullable: true),
                    SolutionDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    MechanicName = table.Column<string>(maxLength: 250, nullable: true),
                    OccupantName = table.Column<string>(maxLength: 250, nullable: true),
                    MechanicSignature = table.Column<string>(nullable: true),
                    OccupantSignature = table.Column<string>(nullable: true),
                    ChargeCustomer = table.Column<bool>(nullable: false),
                    Urgent = table.Column<bool>(nullable: false),
                    FallsWithinContract = table.Column<bool>(nullable: false),
                    PlannedDateTime = table.Column<DateTime>(nullable: true),
                    RequestComment = table.Column<bool>(nullable: false),
                    ExternalReference = table.Column<string>(maxLength: 250, nullable: true),
                    InternalComment = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.WorkOrderID);
                    table.ForeignKey(
                        name: "FK_WorkOrders_ServiceInvoiceLineInputs_MechanicInputID",
                        column: x => x.MechanicInputID,
                        principalSchema: "service",
                        principalTable: "ServiceInvoiceLineInputs",
                        principalColumn: "ServiceInvoiceLineInputID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_ServiceTickets_ServiceTicketID",
                        column: x => x.ServiceTicketID,
                        principalSchema: "service",
                        principalTable: "ServiceTickets",
                        principalColumn: "ServiceTicketID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrders_SolutionCategories_SolutionCategoryID",
                        column: x => x.SolutionCategoryID,
                        principalSchema: "service",
                        principalTable: "SolutionCategories",
                        principalColumn: "SolutionCategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Statuses_StatusID",
                        column: x => x.StatusID,
                        principalSchema: "pms",
                        principalTable: "Statuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consumption",
                schema: "meter",
                columns: table => new
                {
                    iConsumptionKey = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    iAddressKey = table.Column<int>(nullable: true),
                    iCounterKey = table.Column<int>(nullable: false),
                    iServiceRunKey = table.Column<int>(nullable: false),
                    dtStartDateTime = table.Column<DateTime>(nullable: false),
                    dtEndDateTime = table.Column<DateTime>(nullable: false),
                    dConsumption = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    dEndPosition = table.Column<decimal>(type: "decimal(18, 5)", nullable: false),
                    bExcludeForReport = table.Column<bool>(nullable: false),
                    bValidated = table.Column<bool>(nullable: false),
                    iConsumptionUnvalidatedID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumption", x => x.iConsumptionKey);
                    table.ForeignKey(
                        name: "FK_Consumption_Addresses_iAddressKey",
                        column: x => x.iAddressKey,
                        principalSchema: "pms",
                        principalTable: "Addresses",
                        principalColumn: "iAddressKey",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consumption_ConsumptionUnvalidated_iConsumptionUnvalidatedID",
                        column: x => x.iConsumptionUnvalidatedID,
                        principalSchema: "meter",
                        principalTable: "ConsumptionUnvalidated",
                        principalColumn: "iConsumptionUnvalidatedID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consumption_Counters_iCounterKey",
                        column: x => x.iCounterKey,
                        principalSchema: "meter",
                        principalTable: "Counters",
                        principalColumn: "iCounterKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consumption_ServiceRuns_iServiceRunKey",
                        column: x => x.iServiceRunKey,
                        principalSchema: "meter",
                        principalTable: "ServiceRuns",
                        principalColumn: "iServiceRunKey",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                schema: "pms",
                columns: table => new
                {
                    iFileID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<Guid>(nullable: true),
                    sDisplayName = table.Column<string>(maxLength: 250, nullable: true),
                    sOriginalFileName = table.Column<string>(maxLength: 250, nullable: true),
                    sContentType = table.Column<string>(maxLength: 150, nullable: true),
                    bContent = table.Column<byte[]>(nullable: true),
                    bAllowSharing = table.Column<bool>(nullable: false),
                    sUserID = table.Column<string>(nullable: true),
                    iRateCardYearID = table.Column<int>(nullable: true),
                    OpportunityID = table.Column<int>(nullable: true),
                    ServiceTicketID = table.Column<Guid>(nullable: true),
                    WorkOrderID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.iFileID);
                    table.ForeignKey(
                        name: "FK_Files_Opportunities_OpportunityID",
                        column: x => x.OpportunityID,
                        principalSchema: "pms",
                        principalTable: "Opportunities",
                        principalColumn: "iOpportunityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_ServiceTickets_ServiceTicketID",
                        column: x => x.ServiceTicketID,
                        principalSchema: "service",
                        principalTable: "ServiceTickets",
                        principalColumn: "ServiceTicketID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_WorkOrders_WorkOrderID",
                        column: x => x.WorkOrderID,
                        principalSchema: "service",
                        principalTable: "WorkOrders",
                        principalColumn: "WorkOrderID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_RateCardYears_iRateCardYearID",
                        column: x => x.iRateCardYearID,
                        principalSchema: "meter",
                        principalTable: "RateCardYears",
                        principalColumn: "iRateCardYearKey",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceInvoiceLines",
                schema: "service",
                columns: table => new
                {
                    ServiceInvoiceLineID = table.Column<Guid>(nullable: false),
                    ServiceInvoiceID = table.Column<Guid>(nullable: true),
                    StatusID = table.Column<int>(nullable: false),
                    MaintenanceContactID = table.Column<int>(nullable: false),
                    MaintenanceContactInputID = table.Column<Guid>(nullable: true),
                    AssetManagerInputID = table.Column<Guid>(nullable: true),
                    CoordinatorInputID = table.Column<Guid>(nullable: true),
                    OwnerInputID = table.Column<Guid>(nullable: true),
                    RejectByID = table.Column<string>(nullable: true),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    FinishedDateTime = table.Column<DateTime>(nullable: true),
                    ExternalReference = table.Column<string>(maxLength: 250, nullable: true),
                    Rejected = table.Column<bool>(nullable: false),
                    RejectComment = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceInvoiceLines", x => x.ServiceInvoiceLineID);
                    table.ForeignKey(
                        name: "FK_ServiceInvoiceLines_ServiceInvoiceLineInputs_AssetManagerInputID",
                        column: x => x.AssetManagerInputID,
                        principalSchema: "service",
                        principalTable: "ServiceInvoiceLineInputs",
                        principalColumn: "ServiceInvoiceLineInputID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceInvoiceLines_ServiceInvoiceLineInputs_CoordinatorInputID",
                        column: x => x.CoordinatorInputID,
                        principalSchema: "service",
                        principalTable: "ServiceInvoiceLineInputs",
                        principalColumn: "ServiceInvoiceLineInputID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceInvoiceLines_MaintenanceContacts_MaintenanceContactID",
                        column: x => x.MaintenanceContactID,
                        principalSchema: "pms",
                        principalTable: "MaintenanceContacts",
                        principalColumn: "iMaintenanceContactKey",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceInvoiceLines_ServiceInvoiceLineInputs_MaintenanceContactInputID",
                        column: x => x.MaintenanceContactInputID,
                        principalSchema: "service",
                        principalTable: "ServiceInvoiceLineInputs",
                        principalColumn: "ServiceInvoiceLineInputID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceInvoiceLines_ServiceInvoiceLineInputs_OwnerInputID",
                        column: x => x.OwnerInputID,
                        principalSchema: "service",
                        principalTable: "ServiceInvoiceLineInputs",
                        principalColumn: "ServiceInvoiceLineInputID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceInvoiceLines_AspNetUsers_RejectByID",
                        column: x => x.RejectByID,
                        principalSchema: "pms",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceInvoiceLines_ServiceInvoices_ServiceInvoiceID",
                        column: x => x.ServiceInvoiceID,
                        principalSchema: "service",
                        principalTable: "ServiceInvoices",
                        principalColumn: "ServiceInvoiceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceInvoiceLines_WorkOrders_ServiceInvoiceLineID",
                        column: x => x.ServiceInvoiceLineID,
                        principalSchema: "service",
                        principalTable: "WorkOrders",
                        principalColumn: "WorkOrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceInvoiceLines_Statuses_StatusID",
                        column: x => x.StatusID,
                        principalSchema: "pms",
                        principalTable: "Statuses",
                        principalColumn: "StatusID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetBases_iBudgetBaseTypeKey",
                schema: "budget",
                table: "BudgetBases",
                column: "iBudgetBaseTypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetBases_iCustomerID",
                schema: "budget",
                table: "BudgetBases",
                column: "iCustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetDimensionRules_iBudgetDimensionKey",
                schema: "budget",
                table: "BudgetDimensionRules",
                column: "iBudgetDimensionKey");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetDimensionRules_iReportingStructureKey",
                schema: "budget",
                table: "BudgetDimensionRules",
                column: "iReportingStructureKey");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetDimensions_BudgetReferenceId",
                schema: "budget",
                table: "BudgetDimensions",
                column: "BudgetReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetDimensions_iBudgetBaseKey",
                schema: "budget",
                table: "BudgetDimensions",
                column: "iBudgetBaseKey");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetDimensions_iBudgetDimensionTypeKey",
                schema: "budget",
                table: "BudgetDimensions",
                column: "iBudgetDimensionTypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetDimensions_iBudgetSettingKey",
                schema: "budget",
                table: "BudgetDimensions",
                column: "iBudgetSettingKey");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetDimensions_iProjectKey",
                schema: "budget",
                table: "BudgetDimensions",
                column: "iProjectKey");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetSectionIndex_iBudgetSettingKey",
                schema: "budget",
                table: "BudgetSectionIndex",
                column: "iBudgetSettingKey");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetSectionIndex_iReportingStructureKey",
                schema: "budget",
                table: "BudgetSectionIndex",
                column: "iReportingStructureKey");

            migrationBuilder.CreateIndex(
                name: "IX_MonthDegreeDayIndex_iBudgetSettingKey",
                schema: "budget",
                table: "MonthDegreeDayIndex",
                column: "iBudgetSettingKey");

            migrationBuilder.CreateIndex(
                name: "IX_MonthDegreeDayIndex_iMonthKey",
                schema: "budget",
                table: "MonthDegreeDayIndex",
                column: "iMonthKey");

            migrationBuilder.CreateIndex(
                name: "IX_YearDegreeDayIndex_iBudgetSettingKey",
                schema: "budget",
                table: "YearDegreeDayIndex",
                column: "iBudgetSettingKey");

            migrationBuilder.CreateIndex(
                name: "IX_DimProject_iCustomerID",
                schema: "dbo",
                table: "DimProject",
                column: "iCustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_FactBudget_iBudgetDimensionKey",
                schema: "dbo",
                table: "FactBudget",
                column: "iBudgetDimensionKey");

            migrationBuilder.CreateIndex(
                name: "IX_FactBudget_iProjectKey",
                schema: "dbo",
                table: "FactBudget",
                column: "iProjectKey");

            migrationBuilder.CreateIndex(
                name: "IX_AddressDebtors_BillingTypeID",
                schema: "invoice",
                table: "AddressDebtors",
                column: "BillingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_AddressDebtors_iAddressID",
                schema: "invoice",
                table: "AddressDebtors",
                column: "iAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_AddressDebtors_iDebtorID",
                schema: "invoice",
                table: "AddressDebtors",
                column: "iDebtorID");

            migrationBuilder.CreateIndex(
                name: "IX_AddressOccupants_iAddressID",
                schema: "invoice",
                table: "AddressOccupants",
                column: "iAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_AddressOccupants_iOccupantID",
                schema: "invoice",
                table: "AddressOccupants",
                column: "iOccupantID");

            migrationBuilder.CreateIndex(
                name: "IX_DebtorFiles_iDebtorID",
                schema: "invoice",
                table: "DebtorFiles",
                column: "iDebtorID");

            migrationBuilder.CreateIndex(
                name: "IX_Debtors_InvoicePeriodID",
                schema: "invoice",
                table: "Debtors",
                column: "InvoicePeriodID");

            migrationBuilder.CreateIndex(
                name: "IX_Debtors_iDebtorTypeID",
                schema: "invoice",
                table: "Debtors",
                column: "iDebtorTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Debtors_iPaymentTermID",
                schema: "invoice",
                table: "Debtors",
                column: "iPaymentTermID");

            migrationBuilder.CreateIndex(
                name: "IX_Debtors_iShippingProfileID",
                schema: "invoice",
                table: "Debtors",
                column: "iShippingProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_Debtors_iTitleID",
                schema: "invoice",
                table: "Debtors",
                column: "iTitleID");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_iAddressDebtorID",
                schema: "invoice",
                table: "Deposits",
                column: "iAddressDebtorID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceBatches_InvoicePeriodID",
                schema: "invoice",
                table: "InvoiceBatches",
                column: "InvoicePeriodID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceBatches_StatusID",
                schema: "invoice",
                table: "InvoiceBatches",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceBatches_iInvoiceTypeID",
                schema: "invoice",
                table: "InvoiceBatches",
                column: "iInvoiceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceBatches_iProjectID",
                schema: "invoice",
                table: "InvoiceBatches",
                column: "iProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceBatches_iStatusID",
                schema: "invoice",
                table: "InvoiceBatches",
                column: "iStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceChecks_InvoiceCheckOptionID",
                schema: "invoice",
                table: "InvoiceChecks",
                column: "InvoiceCheckOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceChecks_InvoiceID",
                schema: "invoice",
                table: "InvoiceChecks",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_iInvoiceID",
                schema: "invoice",
                table: "InvoiceLines",
                column: "iInvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_iRateCardRowID",
                schema: "invoice",
                table: "InvoiceLines",
                column: "iRateCardRowID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_iRubricTypeID",
                schema: "invoice",
                table: "InvoiceLines",
                column: "iRubricTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_iUnitID",
                schema: "invoice",
                table: "InvoiceLines",
                column: "iUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ProcessedBy",
                schema: "invoice",
                table: "Invoices",
                column: "ProcessedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_StatusID",
                schema: "invoice",
                table: "Invoices",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_iAddressID",
                schema: "invoice",
                table: "Invoices",
                column: "iAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_iDebtorID",
                schema: "invoice",
                table: "Invoices",
                column: "iDebtorID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_iInvoiceBatchID",
                schema: "invoice",
                table: "Invoices",
                column: "iInvoiceBatchID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_iStatusID",
                schema: "invoice",
                table: "Invoices",
                column: "iStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Occupants_DebtoriDebtorID",
                schema: "invoice",
                table: "Occupants",
                column: "DebtoriDebtorID");

            migrationBuilder.CreateIndex(
                name: "IX_Occupants_TitleiTitleID",
                schema: "invoice",
                table: "Occupants",
                column: "TitleiTitleID");

            migrationBuilder.CreateIndex(
                name: "IX_Occupants_iDebtorID",
                schema: "invoice",
                table: "Occupants",
                column: "iDebtorID");

            migrationBuilder.CreateIndex(
                name: "IX_Occupants_iTitleID",
                schema: "invoice",
                table: "Occupants",
                column: "iTitleID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistory_InvoiceID",
                schema: "invoice",
                table: "PaymentHistory",
                column: "InvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistory_iAddressID",
                schema: "invoice",
                table: "PaymentHistory",
                column: "iAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistory_iDebtorID",
                schema: "invoice",
                table: "PaymentHistory",
                column: "iDebtorID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTermHistory_PaymentTermiPaymentTermID",
                schema: "invoice",
                table: "PaymentTermHistory",
                column: "PaymentTermiPaymentTermID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTermHistory_iDebtorID",
                schema: "invoice",
                table: "PaymentTermHistory",
                column: "iDebtorID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTermHistory_iPaymentTermID",
                schema: "invoice",
                table: "PaymentTermHistory",
                column: "iPaymentTermID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTermHistory_sUserID",
                schema: "invoice",
                table: "PaymentTermHistory",
                column: "sUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AverageMonthConsumptions_CounterTypeID",
                schema: "meter",
                table: "AverageMonthConsumptions",
                column: "CounterTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_AverageMonthConsumptions_ProjectID",
                schema: "meter",
                table: "AverageMonthConsumptions",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_AverageMonthConsumptions_UnitID",
                schema: "meter",
                table: "AverageMonthConsumptions",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_BlindConsumptions_iAddressKey",
                schema: "meter",
                table: "BlindConsumptions",
                column: "iAddressKey");

            migrationBuilder.CreateIndex(
                name: "IX_BlindConsumptions_iCounterKey",
                schema: "meter",
                table: "BlindConsumptions",
                column: "iCounterKey");

            migrationBuilder.CreateIndex(
                name: "IX_BlindConsumptions_iServiceRunKey",
                schema: "meter",
                table: "BlindConsumptions",
                column: "iServiceRunKey");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingManagementSystems_CommunicationTypeID",
                schema: "meter",
                table: "BuildingManagementSystems",
                column: "CommunicationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingManagementSystems_ProjectID",
                schema: "meter",
                table: "BuildingManagementSystems",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Consumption_iAddressKey",
                schema: "meter",
                table: "Consumption",
                column: "iAddressKey");

            migrationBuilder.CreateIndex(
                name: "IX_Consumption_iConsumptionUnvalidatedID",
                schema: "meter",
                table: "Consumption",
                column: "iConsumptionUnvalidatedID");

            migrationBuilder.CreateIndex(
                name: "IX_Consumption_iCounterKey",
                schema: "meter",
                table: "Consumption",
                column: "iCounterKey");

            migrationBuilder.CreateIndex(
                name: "IX_Consumption_iServiceRunKey",
                schema: "meter",
                table: "Consumption",
                column: "iServiceRunKey");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionMeters_iAddressKey",
                schema: "meter",
                table: "ConsumptionMeters",
                column: "iAddressKey");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionMeters_iConsumptionMeterSupplierKey",
                schema: "meter",
                table: "ConsumptionMeters",
                column: "iConsumptionMeterSupplierKey");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionMeters_iEnergySupplierID",
                schema: "meter",
                table: "ConsumptionMeters",
                column: "iEnergySupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionMeters_iFrequencyKey",
                schema: "meter",
                table: "ConsumptionMeters",
                column: "iFrequencyKey");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionMeters_iMeasuringOfficerID",
                schema: "meter",
                table: "ConsumptionMeters",
                column: "iMeasuringOfficerID");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionMeters_iMeterTypeKey",
                schema: "meter",
                table: "ConsumptionMeters",
                column: "iMeterTypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionMeters_iOperatorID",
                schema: "meter",
                table: "ConsumptionMeters",
                column: "iOperatorID");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionMeters_iServiceRunKey",
                schema: "meter",
                table: "ConsumptionMeters",
                column: "iServiceRunKey");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionUnvalidated_iAddressKey",
                schema: "meter",
                table: "ConsumptionUnvalidated",
                column: "iAddressKey");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionUnvalidated_iCounterKey",
                schema: "meter",
                table: "ConsumptionUnvalidated",
                column: "iCounterKey");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumptionUnvalidated_iServiceRunKey",
                schema: "meter",
                table: "ConsumptionUnvalidated",
                column: "iServiceRunKey");

            migrationBuilder.CreateIndex(
                name: "IX_CostlierValues_AddressID",
                schema: "meter",
                table: "CostlierValues",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_CounterChangeLogs_iConsumptionMeterKeyFrom",
                schema: "meter",
                table: "CounterChangeLogs",
                column: "iConsumptionMeterKeyFrom");

            migrationBuilder.CreateIndex(
                name: "IX_CounterChangeLogs_iConsumptionMeterKeyTo",
                schema: "meter",
                table: "CounterChangeLogs",
                column: "iConsumptionMeterKeyTo");

            migrationBuilder.CreateIndex(
                name: "IX_CounterChangeLogs_iCounterKey",
                schema: "meter",
                table: "CounterChangeLogs",
                column: "iCounterKey");

            migrationBuilder.CreateIndex(
                name: "IX_CounterChangeLogs_iEventKey",
                schema: "meter",
                table: "CounterChangeLogs",
                column: "iEventKey");

            migrationBuilder.CreateIndex(
                name: "IX_CounterChangeLogs_iExchangeFormKey",
                schema: "meter",
                table: "CounterChangeLogs",
                column: "iExchangeFormKey");

            migrationBuilder.CreateIndex(
                name: "IX_CounterChangeLogs_iMeterChangeKey",
                schema: "meter",
                table: "CounterChangeLogs",
                column: "iMeterChangeKey");

            migrationBuilder.CreateIndex(
                name: "IX_Counters_StatusID",
                schema: "meter",
                table: "Counters",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Counters_iConsumptionMeterKey",
                schema: "meter",
                table: "Counters",
                column: "iConsumptionMeterKey");

            migrationBuilder.CreateIndex(
                name: "IX_Counters_iCounterTypeKey",
                schema: "meter",
                table: "Counters",
                column: "iCounterTypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_Counters_iServiceRunKey",
                schema: "meter",
                table: "Counters",
                column: "iServiceRunKey");

            migrationBuilder.CreateIndex(
                name: "IX_Counters_iUnitKey",
                schema: "meter",
                table: "Counters",
                column: "iUnitKey");

            migrationBuilder.CreateIndex(
                name: "IX_CounterStatus_iCounterKey",
                schema: "meter",
                table: "CounterStatus",
                column: "iCounterKey");

            migrationBuilder.CreateIndex(
                name: "IX_CounterYearConsumptions_CounterID",
                schema: "meter",
                table: "CounterYearConsumptions",
                column: "CounterID");

            migrationBuilder.CreateIndex(
                name: "IX_MaximumPowers_iCounterKey",
                schema: "meter",
                table: "MaximumPowers",
                column: "iCounterKey");

            migrationBuilder.CreateIndex(
                name: "IX_MaximumPowers_iServiceRunKey",
                schema: "meter",
                table: "MaximumPowers",
                column: "iServiceRunKey");

            migrationBuilder.CreateIndex(
                name: "IX_MeterChangeLogs_iAddressKeyFrom",
                schema: "meter",
                table: "MeterChangeLogs",
                column: "iAddressKeyFrom");

            migrationBuilder.CreateIndex(
                name: "IX_MeterChangeLogs_iAddressKeyTo",
                schema: "meter",
                table: "MeterChangeLogs",
                column: "iAddressKeyTo");

            migrationBuilder.CreateIndex(
                name: "IX_MeterChangeLogs_iChangeReasonKey",
                schema: "meter",
                table: "MeterChangeLogs",
                column: "iChangeReasonKey");

            migrationBuilder.CreateIndex(
                name: "IX_MeterChangeLogs_iConsumptionMeterKey",
                schema: "meter",
                table: "MeterChangeLogs",
                column: "iConsumptionMeterKey");

            migrationBuilder.CreateIndex(
                name: "IX_MeterChangeLogs_iEventKey",
                schema: "meter",
                table: "MeterChangeLogs",
                column: "iEventKey");

            migrationBuilder.CreateIndex(
                name: "IX_MeterChangeLogs_iExchangeFormKey",
                schema: "meter",
                table: "MeterChangeLogs",
                column: "iExchangeFormKey");

            migrationBuilder.CreateIndex(
                name: "IX_RateCardRows_VatConditionID",
                schema: "meter",
                table: "RateCardRows",
                column: "VatConditionID");

            migrationBuilder.CreateIndex(
                name: "IX_RateCardRows_iCounterTypeKey",
                schema: "meter",
                table: "RateCardRows",
                column: "iCounterTypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_RateCardRows_iRateCardScaleRowKey",
                schema: "meter",
                table: "RateCardRows",
                column: "iRateCardScaleRowKey");

            migrationBuilder.CreateIndex(
                name: "IX_RateCardRows_iRateCardYearKey",
                schema: "meter",
                table: "RateCardRows",
                column: "iRateCardYearKey");

            migrationBuilder.CreateIndex(
                name: "IX_RateCardRows_iRubricKey",
                schema: "meter",
                table: "RateCardRows",
                column: "iRubricKey");

            migrationBuilder.CreateIndex(
                name: "IX_RateCardRows_iUnitKey",
                schema: "meter",
                table: "RateCardRows",
                column: "iUnitKey");

            migrationBuilder.CreateIndex(
                name: "IX_RateCards_iRateCardTypeKey",
                schema: "meter",
                table: "RateCards",
                column: "iRateCardTypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_RateCardScaleHistories_AddressID",
                schema: "meter",
                table: "RateCardScaleHistories",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_RateCardScaleHistories_DebtorID",
                schema: "meter",
                table: "RateCardScaleHistories",
                column: "DebtorID");

            migrationBuilder.CreateIndex(
                name: "IX_RateCardScaleHistories_RateCardRowID",
                schema: "meter",
                table: "RateCardScaleHistories",
                column: "RateCardRowID");

            migrationBuilder.CreateIndex(
                name: "IX_RateCardScaleRows_iRateCardScaleKey",
                schema: "meter",
                table: "RateCardScaleRows",
                column: "iRateCardScaleKey");

            migrationBuilder.CreateIndex(
                name: "IX_RateCardScales_iUnitKey",
                schema: "meter",
                table: "RateCardScales",
                column: "iUnitKey");

            migrationBuilder.CreateIndex(
                name: "IX_RateCardYears_iRateCardKey",
                schema: "meter",
                table: "RateCardYears",
                column: "iRateCardKey");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_CalculationTypeID",
                schema: "meter",
                table: "ReportRows",
                column: "CalculationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_iReportKey",
                schema: "meter",
                table: "ReportRows",
                column: "iReportKey");

            migrationBuilder.CreateIndex(
                name: "IX_ReportRows_iRubricKey",
                schema: "meter",
                table: "ReportRows",
                column: "iRubricKey");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportValidationSetID",
                schema: "meter",
                table: "Reports",
                column: "ReportValidationSetID");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_iProjectKey",
                schema: "meter",
                table: "Reports",
                column: "iProjectKey");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_iReportPeriodKey",
                schema: "meter",
                table: "Reports",
                column: "iReportPeriodKey");

            migrationBuilder.CreateIndex(
                name: "IX_ReportValidations_iReportKey",
                schema: "meter",
                table: "ReportValidations",
                column: "iReportKey");

            migrationBuilder.CreateIndex(
                name: "IX_Rubrics_iRubricGroupKey",
                schema: "meter",
                table: "Rubrics",
                column: "iRubricGroupKey");

            migrationBuilder.CreateIndex(
                name: "IX_Rubrics_iRubricTypeKey",
                schema: "meter",
                table: "Rubrics",
                column: "iRubricTypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRunErrors_iServiceRunKey",
                schema: "meter",
                table: "ServiceRunErrors",
                column: "iServiceRunKey");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRuns_iServiceKey",
                schema: "meter",
                table: "ServiceRuns",
                column: "iServiceKey");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CollectiveAddressID",
                schema: "pms",
                table: "Addresses",
                column: "CollectiveAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_iProjectKey",
                schema: "pms",
                table: "Addresses",
                column: "iProjectKey");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_iServiceRunKey",
                schema: "pms",
                table: "Addresses",
                column: "iServiceRunKey");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_sConnectionTypeKey",
                schema: "pms",
                table: "Addresses",
                column: "sConnectionTypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_AddressRateCards_iRateCardKey",
                schema: "pms",
                table: "AddressRateCards",
                column: "iRateCardKey");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGroup_ApplicationUserId",
                schema: "pms",
                table: "ApplicationUserGroup",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserGroup_UserGroupId",
                schema: "pms",
                table: "ApplicationUserGroup",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "pms",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "pms",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "pms",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "pms",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRolegroupRoles_RoleGroupId",
                schema: "pms",
                table: "AspNetUserRolegroupRoles",
                column: "RoleGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoleGroups_UserId",
                schema: "pms",
                table: "AspNetUserRoleGroups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "pms",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MaintenanceContactID",
                schema: "pms",
                table: "AspNetUsers",
                column: "MaintenanceContactID");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "pms",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "pms",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BaseModels_CounterTypeId",
                schema: "pms",
                table: "BaseModels",
                column: "CounterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseModels_ProjectYearDetailId",
                schema: "pms",
                table: "BaseModels",
                column: "ProjectYearDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseModels_SeasonalPatternId",
                schema: "pms",
                table: "BaseModels",
                column: "SeasonalPatternId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseModels_DebtorId",
                schema: "pms",
                table: "BaseModels",
                column: "DebtorId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseModels_PeriodPercentage_SeasonalPatternId",
                schema: "pms",
                table: "BaseModels",
                column: "PeriodPercentage_SeasonalPatternId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseModels_ProjectId",
                schema: "pms",
                table: "BaseModels",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseModels_MaintenanceContactID",
                schema: "pms",
                table: "BaseModels",
                column: "MaintenanceContactID");

            migrationBuilder.CreateIndex(
                name: "IX_CalcMutations_iCalcCategoryKey",
                schema: "pms",
                table: "CalcMutations",
                column: "iCalcCategoryKey");

            migrationBuilder.CreateIndex(
                name: "IX_CalcMutations_iProjectKey",
                schema: "pms",
                table: "CalcMutations",
                column: "iProjectKey");

            migrationBuilder.CreateIndex(
                name: "IX_CO2ConstantRows_iCO2ConstantKey",
                schema: "pms",
                table: "CO2ConstantRows",
                column: "iCO2ConstantKey");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_iContactTypeKey",
                schema: "pms",
                table: "Contacts",
                column: "iContactTypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_iProjectKey",
                schema: "pms",
                table: "Contacts",
                column: "iProjectKey");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccounts_iCustomerID",
                schema: "pms",
                table: "CustomerAccounts",
                column: "iCustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDocuments_iCustomerKey",
                schema: "pms",
                table: "CustomerDocuments",
                column: "iCustomerKey");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDocuments_iDocumentCategoryKey",
                schema: "pms",
                table: "CustomerDocuments",
                column: "iDocumentCategoryKey");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyConsumption_iPeriodKey",
                schema: "pms",
                table: "EnergyConsumption",
                column: "iPeriodKey");

            migrationBuilder.CreateIndex(
                name: "IX_EnergyConsumption_iProjectKey",
                schema: "pms",
                table: "EnergyConsumption",
                column: "iProjectKey");

            migrationBuilder.CreateIndex(
                name: "IX_Files_OpportunityID",
                schema: "pms",
                table: "Files",
                column: "OpportunityID");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ServiceTicketID",
                schema: "pms",
                table: "Files",
                column: "ServiceTicketID");

            migrationBuilder.CreateIndex(
                name: "IX_Files_WorkOrderID",
                schema: "pms",
                table: "Files",
                column: "WorkOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Files_iRateCardYearID",
                schema: "pms",
                table: "Files",
                column: "iRateCardYearID");

            migrationBuilder.CreateIndex(
                name: "IX_Financings_iDsraDepositKey",
                schema: "pms",
                table: "Financings",
                column: "iDsraDepositKey");

            migrationBuilder.CreateIndex(
                name: "IX_Financings_iFinancerKey",
                schema: "pms",
                table: "Financings",
                column: "iFinancerKey");

            migrationBuilder.CreateIndex(
                name: "IX_Financings_iPeriodKey",
                schema: "pms",
                table: "Financings",
                column: "iPeriodKey");

            migrationBuilder.CreateIndex(
                name: "IX_Financings_iProjectKey",
                schema: "pms",
                table: "Financings",
                column: "iProjectKey");

            migrationBuilder.CreateIndex(
                name: "IX_Financings_iSubFinancerKey",
                schema: "pms",
                table: "Financings",
                column: "iSubFinancerKey");

            migrationBuilder.CreateIndex(
                name: "IX_Hyperlinks_iProjectKey",
                schema: "pms",
                table: "Hyperlinks",
                column: "iProjectKey");

            migrationBuilder.CreateIndex(
                name: "IX_Investments_iPeriodKey",
                schema: "pms",
                table: "Investments",
                column: "iPeriodKey");

            migrationBuilder.CreateIndex(
                name: "IX_Investments_iProjectKey",
                schema: "pms",
                table: "Investments",
                column: "iProjectKey");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_EnergyConceptID",
                schema: "pms",
                table: "Opportunities",
                column: "EnergyConceptID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_InstallationPartnerID",
                schema: "pms",
                table: "Opportunities",
                column: "InstallationPartnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_InstallationPartnerProcessID",
                schema: "pms",
                table: "Opportunities",
                column: "InstallationPartnerProcessID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_InvestmentProposalID",
                schema: "pms",
                table: "Opportunities",
                column: "InvestmentProposalID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_LostToCategoryID",
                schema: "pms",
                table: "Opportunities",
                column: "LostToCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_MaintenanceContactID",
                schema: "pms",
                table: "Opportunities",
                column: "MaintenanceContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_OpportunityKindID",
                schema: "pms",
                table: "Opportunities",
                column: "OpportunityKindID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_OpportunityStatusID",
                schema: "pms",
                table: "Opportunities",
                column: "OpportunityStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_OpportunityTypeID",
                schema: "pms",
                table: "Opportunities",
                column: "OpportunityTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_ProjectInfoID",
                schema: "pms",
                table: "Opportunities",
                column: "ProjectInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_TechnicalPrincipalID",
                schema: "pms",
                table: "Opportunities",
                column: "TechnicalPrincipalID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityNotes_iOpportunityID",
                schema: "pms",
                table: "OpportunityNotes",
                column: "iOpportunityID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityValueLogs_iOpportunityID",
                schema: "pms",
                table: "OpportunityValueLogs",
                column: "iOpportunityID");

            migrationBuilder.CreateIndex(
                name: "IX_OtherDeliveryProjectInfo_ProjectInfoId",
                schema: "pms",
                table: "OtherDeliveryProjectInfo",
                column: "ProjectInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_AssetManagerID",
                schema: "pms",
                table: "ProjectInfo",
                column: "AssetManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_AssetManageriAssetManagerKey",
                schema: "pms",
                table: "ProjectInfo",
                column: "AssetManageriAssetManagerKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_DebtCollectionCustomerID",
                schema: "pms",
                table: "ProjectInfo",
                column: "DebtCollectionCustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_MailConfigID",
                schema: "pms",
                table: "ProjectInfo",
                column: "MailConfigID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_ProjectTypeID",
                schema: "pms",
                table: "ProjectInfo",
                column: "ProjectTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_ReportValidationSetID",
                schema: "pms",
                table: "ProjectInfo",
                column: "ReportValidationSetID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iCalculationTypePurchaseID",
                schema: "pms",
                table: "ProjectInfo",
                column: "iCalculationTypePurchaseID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iCalculationTypeSalesID",
                schema: "pms",
                table: "ProjectInfo",
                column: "iCalculationTypeSalesID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iColdTemperatureRangeKey",
                schema: "pms",
                table: "ProjectInfo",
                column: "iColdTemperatureRangeKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iDefaultDebtorID",
                schema: "pms",
                table: "ProjectInfo",
                column: "iDefaultDebtorID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iDemarcationKey",
                schema: "pms",
                table: "ProjectInfo",
                column: "iDemarcationKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iDistributionNetWorkKey",
                schema: "pms",
                table: "ProjectInfo",
                column: "iDistributionNetWorkKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iHomeMaintenanceContactKey",
                schema: "pms",
                table: "ProjectInfo",
                column: "iHomeMaintenanceContactKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iMaintenanceContactKey",
                schema: "pms",
                table: "ProjectInfo",
                column: "iMaintenanceContactKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iMeterKey",
                schema: "pms",
                table: "ProjectInfo",
                column: "iMeterKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iProjectStatusID",
                schema: "pms",
                table: "ProjectInfo",
                column: "iProjectStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iReferenceProjectKey",
                schema: "pms",
                table: "ProjectInfo",
                column: "iReferenceProjectKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iSupplyWaterTypeKey",
                schema: "pms",
                table: "ProjectInfo",
                column: "iSupplyWaterTypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iTechnicalPrincipalMainKey",
                schema: "pms",
                table: "ProjectInfo",
                column: "iTechnicalPrincipalMainKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iTechnicalPrincipalSub1Key",
                schema: "pms",
                table: "ProjectInfo",
                column: "iTechnicalPrincipalSub1Key");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iTechnicalPrincipalSub2Key",
                schema: "pms",
                table: "ProjectInfo",
                column: "iTechnicalPrincipalSub2Key");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iTemperatureRangeKey",
                schema: "pms",
                table: "ProjectInfo",
                column: "iTemperatureRangeKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iTransactionModeID",
                schema: "pms",
                table: "ProjectInfo",
                column: "iTransactionModeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_iWaterTypeKey",
                schema: "pms",
                table: "ProjectInfo",
                column: "iWaterTypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_sEnergyManagerID",
                schema: "pms",
                table: "ProjectInfo",
                column: "sEnergyManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfoPurchDeliveryType_ProjectInfoiProjectKey",
                schema: "pms",
                table: "ProjectInfoPurchDeliveryType",
                column: "ProjectInfoiProjectKey");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfoSalesDeliveryType_ProjectInfoId",
                schema: "pms",
                table: "ProjectInfoSalesDeliveryType",
                column: "ProjectInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfoUserGroup_ProjectInfoId",
                schema: "pms",
                table: "ProjectInfoUserGroup",
                column: "ProjectInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectReportValidationSetLogs_ProjectID",
                schema: "pms",
                table: "ProjectReportValidationSetLogs",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectReportValidationSetLogs_ReportValidationSetID",
                schema: "pms",
                table: "ProjectReportValidationSetLogs",
                column: "ReportValidationSetID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectReportValidationSetLogs_ReportValidationSetID1",
                schema: "pms",
                table: "ProjectReportValidationSetLogs",
                column: "ReportValidationSetID1");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceProjectRows_iCounterTypeKey",
                schema: "pms",
                table: "ReferenceProjectRows",
                column: "iCounterTypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceProjectRows_iReferenceProjectKey",
                schema: "pms",
                table: "ReferenceProjectRows",
                column: "iReferenceProjectKey");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceProjectRows_sConnectionTypeKey",
                schema: "pms",
                table: "ReferenceProjectRows",
                column: "sConnectionTypeKey");

            migrationBuilder.CreateIndex(
                name: "IX_ReportValidationSetLines_ReportValidationSetID",
                schema: "pms",
                table: "ReportValidationSetLines",
                column: "ReportValidationSetID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportValidationSetLines_RubricID",
                schema: "pms",
                table: "ReportValidationSetLines",
                column: "RubricID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportValidationSetLines_ValueOperatorID",
                schema: "pms",
                table: "ReportValidationSetLines",
                column: "ValueOperatorID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportValidationSets_ChangeByUserID",
                schema: "pms",
                table: "ReportValidationSets",
                column: "ChangeByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportValidationSets_CreateByUserID",
                schema: "pms",
                table: "ReportValidationSets",
                column: "CreateByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Subsidies_iProjectKey",
                schema: "pms",
                table: "Subsidies",
                column: "iProjectKey");

            migrationBuilder.CreateIndex(
                name: "IX_Subsidies_iSubsidyCategoryKey",
                schema: "pms",
                table: "Subsidies",
                column: "iSubsidyCategoryKey");

            migrationBuilder.CreateIndex(
                name: "IX_TicketLabel_LabelId",
                schema: "pms",
                table: "TicketLabel",
                column: "LabelId");

            migrationBuilder.CreateIndex(
                name: "IX_WeqMutations_iDispensingUnitKey",
                schema: "pms",
                table: "WeqMutations",
                column: "iDispensingUnitKey");

            migrationBuilder.CreateIndex(
                name: "IX_WeqMutations_iProjectKey",
                schema: "pms",
                table: "WeqMutations",
                column: "iProjectKey");

            migrationBuilder.CreateIndex(
                name: "IX_WeqMutations_iWeqCategoryKey",
                schema: "pms",
                table: "WeqMutations",
                column: "iWeqCategoryKey");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddresses_iResponseID",
                schema: "service",
                table: "EmailAddresses",
                column: "iResponseID");

            migrationBuilder.CreateIndex(
                name: "IX_MailAttachments_iResponseID",
                schema: "service",
                table: "MailAttachments",
                column: "iResponseID");

            migrationBuilder.CreateIndex(
                name: "IX_ResponseConcepts_iResponseTypeID",
                schema: "service",
                table: "ResponseConcepts",
                column: "iResponseTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ResponseConcepts_iTicketStatusID",
                schema: "service",
                table: "ResponseConcepts",
                column: "iTicketStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_iResponseTypeID",
                schema: "service",
                table: "Responses",
                column: "iResponseTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_iTicketID",
                schema: "service",
                table: "Responses",
                column: "iTicketID");

            migrationBuilder.CreateIndex(
                name: "IX_ResponseTexts_iTicketTypeID",
                schema: "service",
                table: "ResponseTexts",
                column: "iTicketTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoiceLineInputs_UserID",
                schema: "service",
                table: "ServiceInvoiceLineInputs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoiceLines_AssetManagerInputID",
                schema: "service",
                table: "ServiceInvoiceLines",
                column: "AssetManagerInputID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoiceLines_CoordinatorInputID",
                schema: "service",
                table: "ServiceInvoiceLines",
                column: "CoordinatorInputID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoiceLines_MaintenanceContactID",
                schema: "service",
                table: "ServiceInvoiceLines",
                column: "MaintenanceContactID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoiceLines_MaintenanceContactInputID",
                schema: "service",
                table: "ServiceInvoiceLines",
                column: "MaintenanceContactInputID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoiceLines_OwnerInputID",
                schema: "service",
                table: "ServiceInvoiceLines",
                column: "OwnerInputID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoiceLines_RejectByID",
                schema: "service",
                table: "ServiceInvoiceLines",
                column: "RejectByID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoiceLines_ServiceInvoiceID",
                schema: "service",
                table: "ServiceInvoiceLines",
                column: "ServiceInvoiceID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoiceLines_StatusID",
                schema: "service",
                table: "ServiceInvoiceLines",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoices_MaintenanceContactID",
                schema: "service",
                table: "ServiceInvoices",
                column: "MaintenanceContactID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoices_ProjectID",
                schema: "service",
                table: "ServiceInvoices",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInvoices_StatusID",
                schema: "service",
                table: "ServiceInvoices",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceMessages_ServiceTicketID",
                schema: "service",
                table: "ServiceMessages",
                column: "ServiceTicketID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTickets_AddressID",
                schema: "service",
                table: "ServiceTickets",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTickets_DebtorID",
                schema: "service",
                table: "ServiceTickets",
                column: "DebtorID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTickets_MaintenanceContactID",
                schema: "service",
                table: "ServiceTickets",
                column: "MaintenanceContactID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTickets_OccupantID",
                schema: "service",
                table: "ServiceTickets",
                column: "OccupantID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTickets_ProjectID",
                schema: "service",
                table: "ServiceTickets",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTickets_ServiceTicketTypeID",
                schema: "service",
                table: "ServiceTickets",
                column: "ServiceTicketTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTickets_StatusID",
                schema: "service",
                table: "ServiceTickets",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTickets_TicketiTicketID",
                schema: "service",
                table: "ServiceTickets",
                column: "TicketiTicketID",
                unique: true,
                filter: "[TicketiTicketID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTickets_UserID",
                schema: "service",
                table: "ServiceTickets",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketLogs_iTicketID",
                schema: "service",
                table: "TicketLogs",
                column: "iTicketID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketLogs_sUserID",
                schema: "service",
                table: "TicketLogs",
                column: "sUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_MailConfigID",
                schema: "service",
                table: "Tickets",
                column: "MailConfigID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_iAddressID",
                schema: "service",
                table: "Tickets",
                column: "iAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_iDebtorID",
                schema: "service",
                table: "Tickets",
                column: "iDebtorID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_iOccupantID",
                schema: "service",
                table: "Tickets",
                column: "iOccupantID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_iProjectID",
                schema: "service",
                table: "Tickets",
                column: "iProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_iTicketStatusID",
                schema: "service",
                table: "Tickets",
                column: "iTicketStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_iTicketTypeID",
                schema: "service",
                table: "Tickets",
                column: "iTicketTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_sUserID",
                schema: "service",
                table: "Tickets",
                column: "sUserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_MechanicInputID",
                schema: "service",
                table: "WorkOrders",
                column: "MechanicInputID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ServiceTicketID",
                schema: "service",
                table: "WorkOrders",
                column: "ServiceTicketID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_SolutionCategoryID",
                schema: "service",
                table: "WorkOrders",
                column: "SolutionCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_StatusID",
                schema: "service",
                table: "WorkOrders",
                column: "StatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetDimensionRules",
                schema: "budget");

            migrationBuilder.DropTable(
                name: "BudgetSectionIndex",
                schema: "budget");

            migrationBuilder.DropTable(
                name: "MonthDegreeDayIndex",
                schema: "budget");

            migrationBuilder.DropTable(
                name: "YearDegreeDayIndex",
                schema: "budget");

            migrationBuilder.DropTable(
                name: "DimLedger",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FactBudget",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FactFinancialTransactions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Resultaatoverzicht",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AddressOccupants",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "DebtorFiles",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "Deposits",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "InvoiceChecks",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "InvoiceLines",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "PaymentHistory",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "PaymentTermHistory",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "AverageMonthConsumptions",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "BlindConsumptions",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "BuildingManagementSystems",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "Consumption",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "CostlierValues",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "CounterChangeLogs",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "CounterStatus",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "CounterYearConsumptions",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "MaximumPowers",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "RateCardScaleHistories",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "ReportRows",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "ReportValidations",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "ServiceRunErrors",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "AddressRateCards",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "ApplicationUserGroup",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "AspNetUserRolegroupRoles",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "AspNetUserRoleGroups",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "CalcMutations",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "CalcRules",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "CO2ConstantRows",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "CustomerAccounts",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "CustomerDocuments",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Defaults",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "EnergyConsumption",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Files",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Financings",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Hyperlinks",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Investments",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Logs",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "OpportunityDefaults",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "OpportunityNotes",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "OpportunityValueLogs",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "OtherDeliveryProjectInfo",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "ProjectInfoPurchDeliveryType",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "ProjectInfoSalesDeliveryType",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "ProjectInfoUserGroup",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "ProjectReportValidationSetLogs",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "ReferenceProjectRows",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "ReportColumns",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "ReportValidationSetLines",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Subsidies",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "TicketLabel",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "WeqMutations",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "EmailAddresses",
                schema: "service");

            migrationBuilder.DropTable(
                name: "MailAttachments",
                schema: "service");

            migrationBuilder.DropTable(
                name: "ResponseTexts",
                schema: "service");

            migrationBuilder.DropTable(
                name: "ServiceInvoiceLines",
                schema: "service");

            migrationBuilder.DropTable(
                name: "ServiceMessages",
                schema: "service");

            migrationBuilder.DropTable(
                name: "TicketLogs",
                schema: "service");

            migrationBuilder.DropTable(
                name: "ReportingStructure",
                schema: "donkervoort");

            migrationBuilder.DropTable(
                name: "Months",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "BudgetDimensions",
                schema: "budget");

            migrationBuilder.DropTable(
                name: "AddressDebtors",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "InvoiceCheckOptions",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "Invoices",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "CommunicationTypes",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "ConsumptionUnvalidated",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "MeterChangeLogs",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "RateCardRows",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "Reports",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "AspNetRoleGroups",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "CalcCategories",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "CO2Constants",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "ContactTypes",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "CustomerInfo",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "DocumentCategories",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "DsraDeposits",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Financers",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Periods",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Opportunities",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "OtherDeliveries",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "PurchaseDeliveryTypes",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "SalesDeliveryTypes",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "AspNetUserGroups",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "ValueOperators",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "SubsidyCategories",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Labels",
                schema: "service");

            migrationBuilder.DropTable(
                name: "DispensingUnits",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "WeqCategories",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Responses",
                schema: "service");

            migrationBuilder.DropTable(
                name: "ServiceInvoices",
                schema: "service");

            migrationBuilder.DropTable(
                name: "WorkOrders",
                schema: "service");

            migrationBuilder.DropTable(
                name: "BaseModels",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "BudgetBases",
                schema: "budget");

            migrationBuilder.DropTable(
                name: "BudgetDimensionTypes",
                schema: "budget");

            migrationBuilder.DropTable(
                name: "BudgetSettings",
                schema: "budget");

            migrationBuilder.DropTable(
                name: "BillingTypes",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "InvoiceBatches",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "Counters",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "ChangeReasons",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "Events",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "ExchangeForms",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "VatConditions",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "RateCardScaleRows",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "RateCardYears",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "Rubrics",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "ReportPeriods",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "EnergyConcepts",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "InstallationPartners",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "InstallationPartnerProcesses",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "InvestmentProposals",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "LostToCategories",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "OpportunityKinds",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "OpportunityStatus",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "OpportunityTypes",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "ServiceInvoiceLineInputs",
                schema: "service");

            migrationBuilder.DropTable(
                name: "ServiceTickets",
                schema: "service");

            migrationBuilder.DropTable(
                name: "SolutionCategories",
                schema: "service");

            migrationBuilder.DropTable(
                name: "BudgetBaseTypes",
                schema: "budget");

            migrationBuilder.DropTable(
                name: "InvoiceTypes",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "InvoiceStatuses",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "ConsumptionMeters",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "CounterTypes",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "RateCardScales",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "RateCards",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "RubricGroups",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "RubricTypes",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "ServiceTicketTypes",
                schema: "service");

            migrationBuilder.DropTable(
                name: "Statuses",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Tickets",
                schema: "service");

            migrationBuilder.DropTable(
                name: "ConsumptionMeterSuppliers",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "EnergySuppliers",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Frequencies",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "MeasuringOfficers",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "MeterTypes",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "Operators",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Units",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "RateCardTypes",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Occupants",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "ResponseConcepts",
                schema: "service");

            migrationBuilder.DropTable(
                name: "TicketTypes",
                schema: "service");

            migrationBuilder.DropTable(
                name: "ProjectInfo",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "ServiceRuns",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "ConnectionTypes",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "ResponseTypes",
                schema: "service");

            migrationBuilder.DropTable(
                name: "TicketStatuses",
                schema: "service");

            migrationBuilder.DropTable(
                name: "AssetManager",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "MailConfigs",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "ProjectType",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "ReportValidationSets",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "CalculationType",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "TemperatureRanges",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Debtors",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "Demarcations",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "DistributionNetWorks",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Meters",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "DimProject",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ProjectStatuses",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "ReferenceProjects",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "SupplyWaterTypes",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "TechnicalPrincipals",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "TransactionModes",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "WaterTypes",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "meter");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "pms");

            migrationBuilder.DropTable(
                name: "InvoicePeriods",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "DebtorTypes",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "PaymentTerms",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "ShippingProfiles",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "Titles",
                schema: "invoice");

            migrationBuilder.DropTable(
                name: "DimCustomers",
                schema: "astonmartin");

            migrationBuilder.DropTable(
                name: "MaintenanceContacts",
                schema: "pms");
        }
    }
}
