using PmsEteck.Data.Models.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PmsEteck.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.Configuration;
    //using WSInvoice;

    public enum Process
    {
        Manual,
        Automatic
    }

    [Table("Invoices", Schema = "invoice")]
    public class Invoice
    {
        #region Constructor
        private PmsEteckContext db = new PmsEteckContext();
        //WSInvoice_Service _service;
        #endregion

        #region Static Fields
        private string WebserviceUser = ConfigurationManager.AppSettings["WebserviceUser"];
        private string WebserviceDomain = ConfigurationManager.AppSettings["WebserviceDomain"];
        private string WebservicePassword = ConfigurationManager.AppSettings["WebservicePassword"];
        private string ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        private string IncassoBV = ConfigurationManager.AppSettings["IncassoBV"];
        #endregion

        #region Properties

        [Key]
        public int iInvoiceID { get; set; }

        [Display(Name = "Aansluitadres")]
        [ForeignKey("Address")]
        public int iAddressID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Debiteur")]
        public int iDebtorID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Factuurbatch")]
        public int iInvoiceBatchID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Boekingsdatum")]
        [DataType(DataType.Date)]
        public DateTime dtPostingDate { get; set; }

        [Display(Name = "Referentie")]
        [MaxLength(35, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sYouReference { get; set; }

        [Display(Name = "Factuurdatum")]
        [DataType(DataType.Date)]
        public DateTime dtDocumentDate { get; set; }

        [Display(Name = "Extern documentnummer")]
        [MaxLength(35, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sExternalDocumentNo { get; set; }

        [Display(Name = "Betalingsconditie")]
        [MaxLength(20, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sPaymentCondition { get; set; }

        [Display(Name = "Boekingsomschrijving")]
        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sBookingDescription { get; set; }

        [Display(Name = "Aansluitadres")]
        [MaxLength(80, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sConsumptionAddress { get; set; }

        [Display(Name = "Settlement Code")]
        [MaxLength(20, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sSettlementCode { get; set; }

        [Display(Name = "Nieuw voorschotbedrag")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal dNewDepositAmount { get; set; }

        [Display(Name = "Nieuw bedrag vaste kosten")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal dNewFixedCosts { get; set; }

        [Display(Name = "Nieuw maandelijks bedrag")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal dnewMontlyAmount { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Jaar")]
        public int iYear { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Periode")]
        [Range(1, 12, ErrorMessage = "{0} moet een waarde bevatten tussen {1} en {2}")]
        public int iPeriod { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Status")]
        [ForeignKey("OldStatus")]
        public int iStatusID { get; set; }

        [Display(Name = "Notitie")]
        public string Note { get; set; }

        [Display(Name = "Status")]
        [ForeignKey("InvoiceStatus")]
        public int? StatusID { get; set; }

        [Display(Name = "Validatieproces")]
        public Process Process { get; set; }

        [Display(Name = "Validatiemoment")]
        public DateTime? ProcessedDateTime { get; set; }

        [Display(Name = "Validator")]
        [ForeignKey("Validator")]
        public string ProcessedBy { get; set; }

        [Display(Name = "Status")]
        public virtual InvoiceStatus InvoiceStatus { get; set; }

        [Display(Name = "Aansluitadres")]
        public virtual Address Address { get; set; }

        [Display(Name = "Status")]
        public virtual OldInvoiceStatus OldStatus { get; set; }

        [Display(Name = "Debiteur")]
        public virtual Debtor Debtor { get; set; }

        [Display(Name = "Factuurbatch")]
        public virtual InvoiceBatch InvoiceBatch { get; set; }

        public virtual ApplicationUser Validator { get; set; }

        [Display(Name = "Factuurregels")]
        public virtual List<InvoiceLine> InvoiceLines { get; set; }

        [Display(Name = "Factuurchecks")]
        public virtual List<InvoiceCheck> InvoiceChecks { get; set; }
        #endregion

        #region Methods

        public async Task<InvoiceResult> CreateNewInvoice()
        {
            switch (InvoiceBatch.iInvoiceTypeID)
            {
                case 1:
                case 4:
                    //Maandnota
                    return await CreateMontlyInvoice();
                case 2:
                    // Jaarnota
                    return await CreateAnnualInvoice();
                case 3:
                    // Eindafrekening
                    return await CreateFinalInvoice();
            }

            return InvoiceResult.Failed("De controle welk type factuur gemaakt moet worden is niet goed uitgevoerd.");
        }

        public async Task<InvoiceResult> CreateMontlyInvoice()
        {
            DateTime firstPeriodDate = new DateTime(iYear, iPeriod, 1);
            DateTime lastPeriodDate = firstPeriodDate.AddMonths(1).AddDays(-1);
            sBookingDescription = Debtor.sInvoiceRemark ?? firstPeriodDate.ToString("MMMM yyyy", System.Globalization.CultureInfo.CurrentCulture);

            if (InvoiceBatch.iInvoiceTypeID == 1)
            {
                Deposit deposit = db.Deposits.FirstOrDefault(f => f.bIsActive && f.AddressDebtor.iAddressID == iAddressID && f.AddressDebtor.iDebtorID == iDebtorID);
                if (deposit != null && deposit.dAmountexVAT != 0)
                {
                    // Create an invoiceLine for deposit
                    InvoiceLine invoiceLine = new InvoiceLine
                    {
                        bIsEndCalculation = false,
                        dUnitPrice = deposit.dAmount,
                        dQuantity = 1,
                        dAmount = deposit.dAmountexVAT,
                        dTotalAmount = deposit.dAmountexVAT,
                        iLedgerNumber = new[] { "23", "25", "27" }.Contains(Address.sConnectionTypeKey) ? 204000 : 202000,
                        // Set rubricType Deposit
                        iRubricTypeID = 1,
                        // Set Unit is Month
                        iUnitID = 6,
                        // Default description
                        sDescription = "Voorschot",
                        sSettlementCode = string.Join("-", iYear, iDebtorID, iAddressID),
                        sSettlementText = firstPeriodDate.ToString("MMMM yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    };
                    InvoiceLines.Add(invoiceLine);
                }
            }

            // Pak alle tariefkaartregels voor vaste kosten
            IQueryable<RateCardRow> rateCardRowQuery = db.RateCardRows.Include(i => i.RateCardScaleHistories).Where(w => new int?[] { 2, 3 }.Contains(w.Rubric.iRubricTypeKey) && w.RateCardYear.iYear == iYear && w.RateCardYear.RateCard.AddressRateCards.Any(u => u.iAddressKey == iAddressID));
            if (InvoiceBatch.iInvoiceTypeID == 1)
                rateCardRowQuery = rateCardRowQuery.Where(w => w.Rubric.iRubricTypeKey == 2);
            List<RateCardRow> rateCardRows = await rateCardRowQuery.ToListAsync();
            //List<RateCardRow> rateCardRows = await db.RateCardRows.Where(w => w.Rubric.iRubricTypeKey == 2 && w.RateCardYear.iYear == iYear && w.RateCardYear.RateCard.AddressRateCards.Any(u => u.iAddressKey == iAddressID)).ToListAsync();
            List<Task<InvoiceLine>> taskList = new List<Task<InvoiceLine>>();

            foreach (RateCardRow rateCardRow in rateCardRows)
            {
                taskList.Add(rateCardRow.CreateInvoiceLineAsync(iAddressID, this, firstPeriodDate, lastPeriodDate));
            }
            try
            {
                await Task.WhenAll(taskList);
                foreach (Task<InvoiceLine> item in taskList)
                {
                    if (item.Result.dTotalAmount != 0)
                    {
                        InvoiceLines.Add(item.Result);
                    }
                }
                return await Task.FromResult(InvoiceResult.Success);
            }
            catch (Exception e)
            {
                return await Task.FromResult(InvoiceResult.Failed(e.Message));
            }
        }

        public async Task<InvoiceResult> CreateAnnualInvoice()
        {
            // Default settings for creating this invoice
            // InvoiceBatch period = 01-12-2017

            // Get AddressDebtor ad InvoiceBatch
            AddressDebtor addressDebtor = await db.AddressDebtors.FirstOrDefaultAsync(f => f.iDebtorID == iDebtorID && f.iAddressID == iAddressID);

            DateTime firstPeriodDate = new DateTime(Math.Max(new DateTime(InvoiceBatch.iYear, 1, 1).Ticks, addressDebtor.dtStartDate.Ticks)); // Getting max date between 1-1-2017 and addressDebtorStartDate
            DateTime lastPeriodDate = new DateTime(firstPeriodDate.Year, 1, 1).AddYears(1); // Setting lastPeriodDate to 1-1-2018

            // Get Active deposit for this addressDebtor
            var deposit = await db.Deposits.FirstOrDefaultAsync(f => f.iAddressDebtorID == addressDebtor.iAddressDebtorID && f.bIsActive);
            sBookingDescription = Debtor.sInvoiceRemark ?? string.Format("Jaarnota {0}", firstPeriodDate.Year);

            // Get all rateCardRows from address
            List<RateCardRow> rateCardRows = await db.RateCardRows.Where(w => w.RateCardYear.iYear == iYear && w.RateCardYear.RateCard.AddressRateCards.Any(u => u.iAddressKey == iAddressID)).ToListAsync();
            var taskList = new List<Task<InvoiceLine>>();

            foreach (RateCardRow rateCardRow in rateCardRows)
            {
                taskList.Add(rateCardRow.CreateInvoiceLineAsync(iAddressID, this, firstPeriodDate, lastPeriodDate));
            }
            try
            {
                await Task.WhenAll(taskList);
                foreach (var item in taskList)
                {
                    if (item.Result.dTotalAmount != 0)
                    {
                        InvoiceLines.Add(item.Result);
                    }
                }
                return await Task.FromResult(InvoiceResult.Success);
            }
            catch (Exception e)
            {
                return await Task.FromResult(InvoiceResult.Failed(e.Message));
            }
        }

        public async Task<InvoiceResult> CreateFinalInvoice()
        {
            // Get AddressDebtor
            AddressDebtor addressDebtor = await db.AddressDebtors.FirstOrDefaultAsync(f => f.iDebtorID == iDebtorID && f.iAddressID == iAddressID);

            DateTime firstPeriodDate = new DateTime(Math.Max(addressDebtor.dtStartDate.Ticks, new DateTime(addressDebtor.dtEndDate.Value.Year, 1, 1).Ticks));
            DateTime lastPeriodDate = addressDebtor.dtEndDate.Value;
            sBookingDescription = Debtor.sInvoiceRemark ?? string.Format("Eindnota {0}", firstPeriodDate.Year);

            List<RateCardRow> rateCardRows = await db.RateCardRows.Where(w => w.RateCardYear.iYear == iYear && w.RateCardYear.RateCard.AddressRateCards.Any(u => u.iAddressKey == iAddressID)).ToListAsync();
            var taskList = new List<Task<InvoiceLine>>();

            foreach (RateCardRow rateCardRow in rateCardRows)
            {
                taskList.Add(rateCardRow.CreateInvoiceLineAsync(iAddressID, this, firstPeriodDate, lastPeriodDate));
            }

            try
            {
                await Task.WhenAll(taskList);
                foreach (var item in taskList)
                {
                    if (item.Result.dTotalAmount != 0)
                    {
                        InvoiceLines.Add(item.Result);
                    }
                }
                return await Task.FromResult(InvoiceResult.Success);
            }
            catch (Exception e)
            {
                return await Task.FromResult(InvoiceResult.Failed(e.Message));
            }
        }

        public async Task<InvoiceResult> CreateForDebtor(int addressDebtorID, int invoiceBatchID)
        {
            // Get AddressDebtor ad InvoiceBatch
            AddressDebtor addressDebtor = await db.AddressDebtors.FindAsync(addressDebtorID);
            InvoiceBatch invoiceBatch = await db.InvoiceBatches.FindAsync(invoiceBatchID);

            DateTime firstPeriodDate = new DateTime(invoiceBatch.iYear, invoiceBatch.iPeriod, 1); // 1-1-2017
            DateTime lastPeriodDate = firstPeriodDate;
            switch (invoiceBatch.iInvoiceTypeID)
            {
                case 1:
                    // Maandnota
                    firstPeriodDate = new DateTime(invoiceBatch.iYear, invoiceBatch.iPeriod, 1);
                    lastPeriodDate = firstPeriodDate.AddMonths(1);
                    break;
                case 2:
                    // Jaarafrekening
                    firstPeriodDate = new DateTime(invoiceBatch.iYear, 1, 1);
                    lastPeriodDate = firstPeriodDate.AddYears(1);
                    break;
                case 3:
                    // Eindafrekening
                    firstPeriodDate = new DateTime(Math.Max(addressDebtor.dtStartDate.Ticks, new DateTime(addressDebtor.dtEndDate.Value.Year, 1, 1).Ticks));
                    lastPeriodDate = addressDebtor.dtEndDate.Value;
                    break;
                default:
                    break;
            }
            // Setting first and last day of period
            // Check if invoice is Jaarafrekening of Maandnota

            // Create invoice with default settings
            Invoice invoice = new Invoice
            {
                Debtor = addressDebtor.Debtor,
                dNewDepositAmount = 0,
                dNewFixedCosts = 0,
                dnewMontlyAmount = 0,
                iAddressID = addressDebtor.iAddressID,
                InvoiceBatch = invoiceBatch,
                InvoiceLines = new List<InvoiceLine>(),
                iYear = invoiceBatch.iYear,
                iPeriod = invoiceBatch.iPeriod,
                iStatusID = 1,
                sConsumptionAddress = addressDebtor.Address.GetAddressString(),
                sBookingDescription = addressDebtor.Debtor.sInvoiceRemark ?? firstPeriodDate.ToString("MMMM yyyy", System.Globalization.CultureInfo.CurrentCulture),
                sPaymentCondition = addressDebtor.Debtor.PaymentTerm.sCode,
                sSettlementCode = string.Join("-", invoiceBatch.iYear, addressDebtor.iDebtorID, addressDebtor.iAddressID)
            };
            db.Entry(invoice).State = EntityState.Added;

            switch (invoiceBatch.iInvoiceTypeID)
            {
                case 1:
                    // Is Monthly Invoice
                    invoice.dtDocumentDate = invoiceBatch.dtInvoiceDateTime;
                    invoice.dtPostingDate = invoiceBatch.dtInvoiceDateTime;
                    // Get active deposit for this addressdebtor
                    Deposit deposit = db.Deposits.FirstOrDefault(f => f.bIsActive && f.iAddressDebtorID == addressDebtor.iAddressDebtorID);
                    if (deposit != null && deposit.dAmountexVAT != 0)
                    {
                        // Create an invoiceLine for deposit
                        InvoiceLine invoiceLine = new InvoiceLine
                        {
                            bIsEndCalculation = false,
                            dUnitPrice = deposit.dAmount,
                            dQuantity = 1,
                            dAmount = deposit.dAmountexVAT,
                            dTotalAmount = deposit.dAmountexVAT,
                            Invoice = invoice,
                            iLedgerNumber = new[] { "23", "25", "27" }.Contains(addressDebtor.Address.sConnectionTypeKey) ? 204000 : 202000,
                            // Set rubricType Deposit
                            iRubricTypeID = 1,
                            // Set Unit is Month
                            iUnitID = 6,
                            // Default description
                            sDescription = "Voorschot",
                            sSettlementCode = string.Join("-", invoiceBatch.iYear, addressDebtor.iDebtorID, addressDebtor.iAddressID),
                            sSettlementText = firstPeriodDate.ToString("MMMM yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        };
                        invoice.InvoiceLines.Add(invoiceLine);
                    }
                    break;
                case 2:
                    invoice.dtDocumentDate = invoiceBatch.dtInvoiceDateTime;
                    invoice.dtPostingDate = invoiceBatch.dtInvoiceDateTime;
                    break;
                case 3:
                    // Is Final Settlement
                    invoice.dtDocumentDate = addressDebtor.dtEndDate.Value;
                    invoice.dtPostingDate = addressDebtor.dtEndDate.Value;
                    invoice.iPeriod = addressDebtor.dtEndDate.Value.Month;
                    //firstPeriodDate = new DateTime(addressDebtor.dtEndDate.Value.Year, 1, 1); // 1-1-2017 00:00:00
                    //if (addressDebtor.dtStartDate > firstPeriodDate)
                    //    firstPeriodDate = addressDebtor.dtStartDate;
                    //lastPeriodDate = addressDebtor.dtEndDate.Value; // 15-6-2017 12:00:00
                    break;
                default:
                    break;
            }

            // Get all rateCardRows from address
            List<RateCardRow> rateCardRows = await db.RateCardRows.Where(w => w.RateCardYear.iYear == invoice.iYear && w.RateCardYear.RateCard.AddressRateCards.Any(u => u.iAddressKey == addressDebtor.iAddressID)).ToListAsync();
            var taskList = new List<Task<InvoiceLine>>();
            foreach (RateCardRow rateCardRow in rateCardRows)
            {
                if (invoiceBatch.iInvoiceTypeID != 1 || rateCardRow.Rubric.iRubricTypeKey == 2)
                    //await rateCardRow.CreateInvoiceLineAsync(rateCardRow.iRateCardRowKey, addressDebtor.iAddressID, invoice, firstPeriodDate, lastPeriodDate);
                    taskList.Add(rateCardRow.CreateInvoiceLineAsync(addressDebtor.iAddressID, invoice, firstPeriodDate, lastPeriodDate));
            }
            try
            {

                await Task.WhenAll(taskList);
                foreach (var item in taskList)
                {
                    if (item.Result.dTotalAmount != 0)
                        invoice.InvoiceLines.Add(item.Result);
                }
                try
                {
                    await db.SaveChangesAsync();
                    return await Task.FromResult(InvoiceResult.Success);
                }
                catch (Exception e)
                {
                    return await Task.FromResult(InvoiceResult.Failed(e.Message));
                }
            }
            catch (Exception e)
            {
                return await Task.FromResult(InvoiceResult.Failed(e.Message));

            }
        }
        /*
        public async Task<InvoiceResult> ValidateAsync()
        {
            Invoice invoice = await db.Invoices.Include(i => i.InvoiceLines).FirstOrDefaultAsync(f => f.iInvoiceID == iInvoiceID);
            if (invoice != null)
            {
                /*
                InvoiceResult result = await invoice.AddToNav();
                if (result.Succeeded)
                {
                    try
                    {
                        invoice.iStatusID = 3;
                        db.Entry(invoice).State = EntityState.Modified;
                        if (invoice.InvoiceBatch.iInvoiceTypeID == 3)
                        {
                            // Factuur is eindafrekening dus set state of addressdebtor to finished

                            AddressDebtor addressDebtor = db.AddressDebtors.FirstOrDefault(f => f.iAddressID == invoice.iAddressID && f.iDebtorID == invoice.iDebtorID && !f.bIsActive);
                            if (addressDebtor != null)
                            {
                                addressDebtor.bFinished = true;
                                db.Entry(addressDebtor).State = EntityState.Modified;
                            }
                        }
                        db.SaveChanges();
                        return InvoiceResult.Success;
                    }
                    catch (Exception e)
                    {
                        return InvoiceResult.Failed(e.Message);
                    }

                }
                return result;
                
            }
            else
            {
                return InvoiceResult.Failed("Factuur kan niet gevonden worden.");
            }
        }
        
        public async Task<InvoiceResult> AddToNav()
        {
            // Get financial Project
            ProjectBase finProject = await db.ProjectBases.FirstOrDefaultAsync(f => f.iProjectKey == InvoiceBatch.Project.iFinProjectKey);
            try
            {
                _service = new WSInvoice_Service();
                _service.Credentials = new System.Net.NetworkCredential(WebserviceUser, WebservicePassword, WebserviceDomain);
                if(InvoiceBatch.Project.InvoiceViaOwnCollection)
                {
                    _service.Url = string.Format("{0}/WS/{1}/Page/WSInvoice", ServiceUrl, Uri.EscapeDataString(InvoiceBatch.Project.DebtCollectionCustomer.NavisionPrefix));
                }
                else
                {
                    _service.Url = string.Format("{0}/WS/{1}/Page/WSInvoice", ServiceUrl, Uri.EscapeDataString(IncassoBV));
                }


                WSInvoice wsInvoice = new WSInvoice
                {
                    Combined_Invoice_Code = Debtor.bNoCombinedInvoice ? string.Join("-", iYear, iPeriod, iDebtorID, iInvoiceID) : string.Join("-", iYear, iPeriod, iDebtorID),
                    Connection_Address = sConsumptionAddress,
                    Description_Dimension_1_Code = finProject.sProjectDescription,
                    Description_Dimension_2_Code = finProject.Customer.sName,
                    Document_Date = DateTime.Today,
                    Document_DateSpecified = true,
                    External_Document_No = iInvoiceID.ToString(),
                    Invoice_Period = iYear.ToString() + iPeriod.ToString("00"),
                    Invoice_Type = InvoiceBatch.iInvoiceTypeID == 1 ? Invoice_Type.Prepayment : InvoiceBatch.iInvoiceTypeID == 2 ? Invoice_Type.Annual_Settlement : InvoiceBatch.iInvoiceTypeID == 3 ? Invoice_Type.Final : InvoiceBatch.iInvoiceTypeID == 4 ? Invoice_Type.Monthly_Settlement : Invoice_Type._blank_,
                    Invoice_TypeSpecified = true,
                    New_Advance = dNewDepositAmount,
                    New_AdvanceSpecified = true,
                    New_Monthly_Amount = dnewMontlyAmount,
                    New_Monthly_AmountSpecified = true,
                    New_Standing_Charge = dNewFixedCosts,
                    New_Standing_ChargeSpecified = true,
                    Payment_Terms_Code = sPaymentCondition,
                    Shortcut_Dimension_1_Code = finProject.sProjectCode,
                    Shortcut_Dimension_2_Code = finProject.Customer.sClientcode,
                    Posting_Date = DateTime.Today,
                    Posting_DateSpecified = true,
                    Posting_Description = sBookingDescription,
                    Sell_to_Customer_No = Debtor.iDebtorCode.ToString(),
                    Settlement_Code = sSettlementCode,
                    Settlement_Text = InvoiceLines.First().sSettlementText,
                    Transaction_Mode_Code = Debtor.iPaymentTermID == 8 ? Debtor.iPartnerType == PartnerType.Bedrijf ? Address.Project.TransactionMode.sCodeBusiness : Address.Project.TransactionMode.sCodePerson : Address.Project.TransactionMode.sCodeNonIncasso,
                    Your_Reference = sYouReference                 
                };

                List<WS_Sales_Invoice_Line_Import> wsLines = new List<WS_Sales_Invoice_Line_Import>();
                foreach (var invoiceLine in InvoiceLines)
                {
                    WS_Sales_Invoice_Line_Import wsLine = new WS_Sales_Invoice_Line_Import
                    {
                        Category_Code = invoiceLine.iRubricTypeID.ToString(),
                        Category_Description = invoiceLine.RubricType.sRubricTypeDescription,
                        Description = invoiceLine.sDescription,
                        Description_2 = invoiceLine.sDescription2,
                        External_Document_No = iInvoiceID.ToString(),
                        Final_Settlement = invoiceLine.bIsEndCalculation,
                        Final_SettlementSpecified = true,
                        Line_No = invoiceLine.iInvoiceLineID,
                        Line_NoSpecified = true,
                        No = invoiceLine.iLedgerNumber.ToString(),
                        Period_End_Date = invoiceLine.dtEndDate.GetValueOrDefault(),
                        Period_End_DateSpecified = true,
                        Period_End_Reading = invoiceLine.dEndPosition,
                        Period_End_ReadingSpecified = true,
                        Period_Start_Reading = invoiceLine.dStartPosition,
                        Period_Start_ReadingSpecified = true,
                        Period_Start_Date = invoiceLine.dtStartDate.GetValueOrDefault(),
                        Period_Start_DateSpecified = true,
                        Quantity = invoiceLine.dQuantity,
                        QuantitySpecified = true,
                        Settlement_Code = invoiceLine.sSettlementCode,
                        Settlement_Text = invoiceLine.sSettlementText,
                        Shortcut_Dimension_1_Code = finProject.sProjectCode,
                        Unit_of_Measure = invoiceLine.Unit.sUnit,
                        Unit_Price = invoiceLine.dAmount,
                        Unit_PriceSpecified = true, 
                        Discount_Line = invoiceLine.Discount,
                        VAT_Prod_Posting_Group = invoiceLine.VatConditionCode
                    };
                    wsLines.Add(wsLine);
                }

                wsInvoice.Sales_Invoice_Line_Import = wsLines.ToArray();

                _service.Create(ref wsInvoice);

                dtDocumentDate = DateTime.Today;
                dtPostingDate = DateTime.Today;
                ProcessedDateTime = DateTime.UtcNow;
                return await Task.FromResult(InvoiceResult.Success);

            }
            catch (Exception e)
            {
                return await Task.FromResult(InvoiceResult.Failed(e.Message));
            }
            finally
            {
                _service.Dispose();
                _service = null;
            }
        }
        
        public async Task<InvoiceResult> CreditInvoice()
        {
            // Get financial Project
            ProjectBase finProject = await db.ProjectBases.FirstOrDefaultAsync(f => f.iProjectKey == InvoiceBatch.Project.iFinProjectKey);
            try
            {
                _service = new WSInvoice_Service();
                _service.Credentials = new System.Net.NetworkCredential(WebserviceUser, WebservicePassword, WebserviceDomain);
                if (InvoiceBatch.Project.InvoiceViaOwnCollection)
                {
                    _service.Url = string.Format("{0}/WS/{1}/Page/WSInvoice", ServiceUrl, Uri.EscapeDataString(InvoiceBatch.Project.DebtCollectionCustomer.NavisionPrefix));
                }
                else
                {
                    _service.Url = string.Format("{0}/WS/{1}/Page/WSInvoice", ServiceUrl, Uri.EscapeDataString(IncassoBV));
                }

                WSInvoice wsInvoice = new WSInvoice
                {
                    Combined_Invoice_Code = Debtor.bNoCombinedInvoice ? string.Join("-", iYear, iPeriod, iDebtorID, iInvoiceID) : string.Join("-", iYear, iPeriod, iDebtorID),
                    Connection_Address = sConsumptionAddress,
                    Description_Dimension_1_Code = finProject.sProjectDescription,
                    Description_Dimension_2_Code = finProject.Customer.sName,
                    Document_Date = DateTime.Today,
                    Document_DateSpecified = true,
                    External_Document_No = string.Format("C-{0}", iInvoiceID.ToString()),
                    Invoice_Period = iYear.ToString() + iPeriod.ToString("00"),
                    Invoice_Type = InvoiceBatch.iInvoiceTypeID == 1 ? Invoice_Type.Prepayment : InvoiceBatch.iInvoiceTypeID == 2 ? Invoice_Type.Annual_Settlement : InvoiceBatch.iInvoiceTypeID == 3 ? Invoice_Type.Final : InvoiceBatch.iInvoiceTypeID == 4 ? Invoice_Type.Monthly_Settlement : Invoice_Type._blank_,
                    Invoice_TypeSpecified = true,
                    New_Advance = dNewDepositAmount,
                    New_AdvanceSpecified = true,
                    New_Monthly_Amount = dnewMontlyAmount,
                    New_Monthly_AmountSpecified = true,
                    New_Standing_Charge = dNewFixedCosts,
                    New_Standing_ChargeSpecified = true,
                    Payment_Terms_Code = sPaymentCondition,
                    Shortcut_Dimension_1_Code = finProject.sProjectCode,
                    Shortcut_Dimension_2_Code = finProject.Customer.sClientcode,
                    Posting_Date = DateTime.Today,
                    Posting_DateSpecified = true,
                    Posting_Description = sBookingDescription,
                    Sell_to_Customer_No = Debtor.iDebtorCode.ToString(),
                    Settlement_Code = sSettlementCode,
                    Settlement_Text = InvoiceLines.First().sSettlementText,
                    Transaction_Mode_Code = Debtor.iPaymentTermID == 8 ? Debtor.iPartnerType == PartnerType.Bedrijf ? Address.Project.TransactionMode.sCodeBusiness : Address.Project.TransactionMode.sCodePerson : Address.Project.TransactionMode.sCodeNonIncasso,
                    Your_Reference = sYouReference
                };

                List<WS_Sales_Invoice_Line_Import> wsLines = new List<WS_Sales_Invoice_Line_Import>();
                foreach (var invoiceLine in InvoiceLines)
                {
                    WS_Sales_Invoice_Line_Import wsLine = new WS_Sales_Invoice_Line_Import
                    {
                        Category_Code = invoiceLine.iRubricTypeID.ToString(),
                        Category_Description = invoiceLine.RubricType.sRubricTypeDescription,
                        Description = invoiceLine.sDescription,
                        Description_2 = invoiceLine.sDescription2,
                        External_Document_No = string.Format("C-{0}", iInvoiceID.ToString()),
                        Final_Settlement = invoiceLine.bIsEndCalculation,
                        Final_SettlementSpecified = true,
                        Line_No = invoiceLine.iInvoiceLineID,
                        Line_NoSpecified = true,
                        No = invoiceLine.iLedgerNumber.ToString(),
                        Period_End_Date = invoiceLine.dtEndDate.GetValueOrDefault(),
                        Period_End_DateSpecified = true,
                        Period_End_Reading = invoiceLine.dEndPosition,
                        Period_End_ReadingSpecified = true,
                        Period_Start_Reading = invoiceLine.dStartPosition,
                        Period_Start_ReadingSpecified = true,
                        Period_Start_Date = invoiceLine.dtStartDate.GetValueOrDefault(),
                        Period_Start_DateSpecified = true,
                        Quantity = invoiceLine.dQuantity * -1,
                        QuantitySpecified = true,
                        Settlement_Code = invoiceLine.sSettlementCode,
                        Settlement_Text = invoiceLine.sSettlementText,
                        Shortcut_Dimension_1_Code = finProject.sProjectCode,
                        Unit_of_Measure = invoiceLine.Unit.sUnit,
                        Unit_Price = invoiceLine.dAmount,
                        Unit_PriceSpecified = true,
                        Discount_Line = invoiceLine.Discount,
                        VAT_Prod_Posting_Group = invoiceLine.VatConditionCode
                    };
                    wsLines.Add(wsLine);
                }

                wsInvoice.Sales_Invoice_Line_Import = wsLines.ToArray();

                _service.Create(ref wsInvoice);
                return await Task.FromResult(InvoiceResult.Success);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(InvoiceResult.Failed(ex.Message, ex.InnerException?.Message));
            }
        }
        */
        public Task Check(string userID, PmsEteckContext context, bool save)
        {
            if ((InvoiceStatus != null && InvoiceStatus.StatusCode == 100) || iStatusID == 1)
            {
                bool checkExists;
                bool valid;
                int count;
                DateTime invoicePeriod = new DateTime(iYear, iPeriod, 1);
                for (int i = 1; i < context.InvoiceCheckOptions.Count() + 1; i++)
                {
                    if (i != 6)
                    {
                        if (((!Address.ConnectionType.IsStandingRight && InvoiceBatch.iInvoiceTypeID != 3) || i != 2) // (Geen vastrecht en geen eindafrekening) of niet check 2
                        && (Address.ConnectionType.IsStandingRight || i != 3)
                        && ((InvoiceBatch.iInvoiceTypeID == 1) || i != 4)
                        && ((InvoiceLines?.Any() ?? false) || i != 5))
                        {
                            context.Entry(this).Collection(c => c.InvoiceChecks).Load();
                            checkExists = InvoiceChecks != null && InvoiceChecks.Count > 0 && InvoiceChecks.Any(a => a.InvoiceCheckOptionID == i);
                            InvoiceCheck check = checkExists ? InvoiceChecks.First(f => f.InvoiceCheckOptionID == i) : new InvoiceCheck();
                            if (!checkExists)
                                check.InvoiceCheckOption = context.InvoiceCheckOptions.Find(i);
                            context.Entry(this).Collection(c => c.InvoiceLines).Load();
                            switch (i)
                            {
                                case 1:
                                    count = InvoiceLines.Count(a => a.iRubricTypeID == 2);
                                    check.Valid = count > 0;
                                    check.Message = count > 0 ? string.Format("Deze aansluiting bevat {0} vastrechtregel{1}", count, count > 1 ? "s" : "") : "Deze aansluiting bevat geen vastrechtregels";
                                    break;
                                case 2:
                                    count = InvoiceLines.Count(c => c.iRubricTypeID == 1);
                                    valid = count > 0;
                                    check.Valid = valid;
                                    check.Message = valid ? string.Format("Deze factuur bevat voorschot") : "Deze factuur bevat geen voorschot";
                                    break;
                                case 3:
                                    count = InvoiceLines.Count(c => c.iRubricTypeID == 1);
                                    valid = count == 0;
                                    check.Valid = valid;
                                    check.Message = valid ? "Deze factuur bevat geen voorschot" : "Deze factuur bevat voorschot";
                                    break;
                                case 4:
                                    List<Invoice> invoiceList = context.Invoices.Include(il => il.InvoiceLines).Where(w => w.iDebtorID == iDebtorID && w.iAddressID == iAddressID).ToList();
                                    if (invoiceList.Count == 0)
                                    {
                                        check.Valid = true;
                                        check.Message = string.Format("Er konden geen facturen gevonden worden bij deze debiteur.");
                                    }
                                    else if (invoiceList.Count(c => new DateTime(c.iYear, c.iPeriod, 1) < invoicePeriod) < 1)
                                    {
                                        check.Valid = true;
                                        check.Message = string.Format("Deze debiteur heeft geen facturen voor " + invoicePeriod.ToShortDateString() + " ontvangen.");
                                    }
                                    else
                                    {
                                        decimal currentAmount = InvoiceLines.Where(w => w.iRubricTypeID == 2).Sum(s => s.dTotalAmount);
                                        decimal previousAmount = invoiceList.OrderByDescending(o => new DateTime(o.iYear, o.iPeriod, 1)).First(f => new DateTime(f.iYear, f.iPeriod, 1) < invoicePeriod).InvoiceLines.Where(w => w.iRubricTypeID == 2).Sum(s => s.dTotalAmount);
                                        decimal difference = Math.Abs(currentAmount - previousAmount);
                                        decimal percentage = 0;
                                        if (previousAmount == currentAmount)
                                            percentage = 0;
                                        else if (currentAmount == 0 || previousAmount == 0)
                                            percentage = 100;
                                        else
                                            percentage = decimal.Multiply(decimal.Divide(difference, previousAmount), 100);
                                        check.Valid = percentage <= 3;
                                        check.Message = string.Format("Het vastrechtbedrag van deze factuur is {0:0.##}% {1} dan de vorige factuur.", percentage, currentAmount < previousAmount ? "lager" : "hoger");
                                    }
                                    break;
                                case 5:
                                    DateTime previousMonth = DateTime.UtcNow.AddMonths(-1);
                                    List<string> RateCardRowsIDs = InvoiceLines.Where(w => w.iRateCardRowID.HasValue).Select(s => s.iRateCardRowID.Value.ToString()).ToList();
                                    count = context.Logs.Count(c => c.CreateDateTime >= previousMonth && c.TableName.StartsWith("RateCardRow") && RateCardRowsIDs.Where(w => string.Equals(c.RecordID, w)).Any());
                                    valid = count == 0;
                                    check.Valid = valid;
                                    check.Message = valid ? "Er zijn geen tariefkaartwijzingen geweest in de afgelopen maand" : string.Format("Er {0} {1} tariefkaartwijziging{2} geweest in de afgelopen maand", count == 1 ? "is" : "zijn", count, count == 1 ? "" : "en");
                                    break;
                            }
                            check.CheckDateTime = DateTime.UtcNow;
                            if (checkExists)
                            {
                                context.Entry(check).State = EntityState.Modified;
                            }
                            else
                            {
                                check.InvoiceID = iInvoiceID;
                                context.InvoiceChecks.Add(check);
                            }
                        }
                        else
                        {
                            InvoiceCheck check = InvoiceChecks.FirstOrDefault(f => f.InvoiceCheckOptionID == i);
                            if (check != null)
                                context.InvoiceChecks.Remove(check);
                        }
                    }
                }

                if (InvoiceChecks.All(a => a.Valid))
                    StatusID = db.InvoiceStatuses.FirstOrDefault(f => f.StatusCode == 130).StatusID;
                    iStatusID = 4;

                if (save)
                    context.SaveChanges(userID);

            }
            return Task.FromResult(0);
        }
        #endregion
        
    }

}