using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Extensions;
using PmsEteck.Data.Models;
using PmsEteck.Data.Models.Results;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PmsEteck.Data.Services
{
    public class InvoiceService
    {
        private PmsEteckContext context;
        private List<Invoice> Invoices = new List<Invoice>();
        private InvoiceBatch InvoiceBatch;
        private string UserID;

        public DateTime LastPeriodDate { get; private set; }
        public DateTime FirstPeriodDate { get; private set; }
        public CultureInfo CultureInfo { get; private set; }

        public InvoiceService()
        {
            context = new PmsEteckContext();
            CultureInfo = new CultureInfo("nl-NL");
        }

        public InvoiceService(PmsEteckContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            CultureInfo = new CultureInfo("nl-NL");
        }
        /*
        public async Task AddInvoicesToNavision(string userID)
        {
            UserID = userID;
            Invoices = context.Invoices.Include(i => i.InvoiceLines).Where(w => (w.iStatusID == 4 || w.InvoiceStatus.StatusCode == 130) && (w.InvoiceBatch.iStatusID == 4 || w.InvoiceBatch.InvoiceBatchStatus.StatusCode == 130)).Take(500).ToList();
            Console.Write("Er worden totaal " + Invoices.Count + " facturen geboekt.");
            int finishedStatus = context.InvoiceStatuses.FirstOrDefault(f => f.StatusCode == 200).StatusID;
            foreach (Invoice invoice in Invoices)
            {
                Console.WriteLine("Factuur met id " + invoice.iInvoiceID + " wordt nu gefactureerd.");
                InvoiceResult invoiceResult = await invoice.AddToNav();
                if (invoiceResult.Succeeded)
                {
                    invoice.iStatusID = 3;
                    invoice.StatusID = finishedStatus;
                    // Invoicebatch is Eindafrekening dus adresdebiteur op finished zetten
                    if (invoice.InvoiceBatch.iInvoiceTypeID == 3)
                    {
                        // Select adresdebtor
                        AddressDebtor addressDebtor = await context.AddressDebtors.FirstOrDefaultAsync(f => f.iAddressID == invoice.iAddressID && f.iDebtorID == invoice.iDebtorID && !f.bIsActive);
                        addressDebtor.bFinished = true;
                        context.Entry(addressDebtor).State = EntityState.Modified;
                    }
                }
                else
                {
                    Console.WriteLine("Er is iets fout gegaan. " + string.Join(", ", invoiceResult.Errors));
                    await context.Entry(invoice).Collection(c => c.InvoiceChecks).LoadAsync();
                    invoice.InvoiceChecks.RemoveAll(t => t.InvoiceCheckOptionID == 6);
                    invoice.InvoiceChecks.Add(new InvoiceCheck
                    {
                        CheckDateTime = DateTime.UtcNow,
                        InvoiceCheckOptionID = 6,
                        Message = string.Join(", ", invoiceResult.Errors),
                        Valid = false,
                    });
                }
                context.Entry(invoice).State = EntityState.Modified;
                await context.SaveChangesAsync(UserID);
            }
            await context.SaveChangesAsync(UserID);
        }
        */
        public async Task<InvoiceResult> CreateInvoicesForBatch(int invoiceBatchID, string userId)
        {
            UserID = userId ?? throw new NullReferenceException(nameof(userId));
            InvoiceBatch = await context.InvoiceBatches.FindAsync(invoiceBatchID);
            if (InvoiceBatch == null)
                return await Task.FromResult(InvoiceResult.Failed(string.Format("Er kon geen batch met id: {0} gevonden worden.", invoiceBatchID)));
            FirstPeriodDate = new DateTime(InvoiceBatch.iYear, InvoiceBatch.iPeriod, 1);
            LastPeriodDate = FirstPeriodDate.AddMonths(1);
            switch (InvoiceBatch.InvoicePeriodID)
            {
                case 2:
                    FirstPeriodDate = FirstPeriodDate.FirstDayOfQuarter();
                    break;
                case 3:
                    FirstPeriodDate = FirstPeriodDate.FirstDayOfYear();
                    break;
            }


            List<AddressDebtor> addressDebtors = GetAddressDebtors();
            var taskList = new List<Task<Invoice>>();
            switch (InvoiceBatch.iInvoiceTypeID)
            {
                case 1:
                    //Prepayment Invoice
                    foreach (AddressDebtor addressDebtor in addressDebtors)
                    {
                        taskList.Add(CreatePrepaymentInvoice(addressDebtor, InvoiceBatch.InvoicePeriodID ?? 1));
                    }
                    break;
                case 2:
                    //Annual_Settlement
                    break;
                case 3:
                    //Final Invoice
                    foreach (AddressDebtor addressDebtor in addressDebtors)
                    {
                        taskList.Add(CreateFinalInvoice(addressDebtor));
                    }
                    break;
                case 4:
                    //Settlement
                    //Prepayment Invoice
                    foreach (AddressDebtor addressDebtor in addressDebtors)
                    {
                        taskList.Add(CreateSettlementInvoice(addressDebtor, InvoiceBatch.InvoicePeriodID ?? 1));
                    }
                    break;
            }
            var resultList = await Task.WhenAll(taskList);
            context.Invoices.AddRange(resultList);
            await context.SaveChangesAsync(UserID);
            resultList.ToList().ForEach(f => f.Check(UserID, context, true));
            return await Task.FromResult(InvoiceResult.Success);
        }

        public async Task<Invoice> CreateSettlementInvoice(AddressDebtor addressDebtor, int invoicePeriodID)
        {
            RateCardRowService rateCardRowService = new RateCardRowService(context);
            Invoice invoice = new Invoice()
            {
                dNewDepositAmount = 0,
                dNewFixedCosts = 0,
                dnewMontlyAmount = 0,
                dtDocumentDate = DateTime.Today,
                dtPostingDate = DateTime.Today,
                iAddressID = addressDebtor.iAddressID,
                iDebtorID = addressDebtor.iDebtorID,
                iInvoiceBatchID = InvoiceBatch.iInvoiceBatchID,
                InvoiceChecks = new List<InvoiceCheck>(),
                InvoiceLines = new List<InvoiceLine>(),
                iPeriod = InvoiceBatch.iPeriod,
                iStatusID = 1,
                iYear = InvoiceBatch.iYear,
                sBookingDescription = addressDebtor.Debtor.sInvoiceRemark ?? (invoicePeriodID == 1 ? FirstPeriodDate.ToString("MMMM yyyy", CultureInfo) : invoicePeriodID == 2 ? string.Format("Q{0}-{1}", Math.Ceiling(LastPeriodDate.Month / (decimal)3), FirstPeriodDate.Year) : string.Empty),
                sConsumptionAddress = addressDebtor.Address.GetAddressString(),
                sPaymentCondition = addressDebtor.Debtor.PaymentTerm.sCode,
                sSettlementCode = string.Join("-", InvoiceBatch.iYear, addressDebtor.iDebtorID, addressDebtor.iAddressID)
            };
            Deposit deposit = context.Entry(addressDebtor).Collection(c => c.Deposits).Query().OrderByDescending(o => o.dtStartDate).FirstOrDefault(f => f.bIsActive);
            if (deposit != null || deposit.dAmount != 0)
            {
                decimal quantity = invoicePeriodID == 1 ? 1 : invoicePeriodID == 2 ? 3 : invoicePeriodID == 3 ? 12 : 1;
                invoice.InvoiceLines.Add(new InvoiceLine()
                {
                    bIsEndCalculation = false,
                    dUnitPrice = deposit.dAmount,
                    dQuantity = quantity,
                    dAmount = deposit.dAmountexVAT,
                    dTotalAmount = quantity * deposit.dAmountexVAT,
                    iLedgerNumber = new[] { "23", "25", "27" }.Contains(addressDebtor.Address.sConnectionTypeKey) ? 204000 : 202000,
                    // Set rubricType Deposit
                    iRubricTypeID = 1,
                    // Set Unit is Month
                    iUnitID = 6,
                    // Default description
                    sDescription = "Voorschot",
                    sSettlementCode = string.Join("-", invoice.iYear, invoice.iDebtorID, invoice.iAddressID),
                    sSettlementText = (invoicePeriodID == 1 ? FirstPeriodDate.ToString("MMMM yyyy", CultureInfo) : invoicePeriodID == 2 ? string.Format("Q{0}-{1}", Math.Ceiling(LastPeriodDate.Month / (decimal)3), FirstPeriodDate.Year) : string.Empty)
                });
            }

            IEnumerable<RateCardRow> rateCardRows = GetRateCardRows(addressDebtor.iAddressID);

            foreach (RateCardRow rateCardRow in rateCardRows.ToList())
            {
                InvoiceLine invoiceLine = rateCardRowService.CreateInvoiceLine(rateCardRow, addressDebtor.Address, invoice, FirstPeriodDate, LastPeriodDate, invoicePeriodID);
                if (invoiceLine.dTotalAmount != 0)
                    invoice.InvoiceLines.Add(invoiceLine);
            }
            return await Task.FromResult(invoice);
        }

        public async Task<Invoice> CreateFinalInvoice(AddressDebtor addressDebtor)
        {
            DateTime firstPeriodDate = new DateTime(Math.Max(FirstPeriodDate.FirstDayOfYear().Ticks, addressDebtor.dtStartDate.Ticks));
            DateTime lastPeriodDate = addressDebtor.dtEndDate.Value;
            RateCardRowService rateCardRowService = new RateCardRowService(context);

            Invoice invoice = new Invoice()
            {
                dNewDepositAmount = 0,
                dNewFixedCosts = 0,
                dnewMontlyAmount = 0,
                dtDocumentDate = DateTime.Today,
                dtPostingDate = DateTime.Today,
                iAddressID = addressDebtor.iAddressID,
                iDebtorID = addressDebtor.iDebtorID,
                iInvoiceBatchID = InvoiceBatch.iInvoiceBatchID,
                InvoiceChecks = new List<InvoiceCheck>(),
                InvoiceLines = new List<InvoiceLine>(),
                iPeriod = InvoiceBatch.iPeriod,
                iStatusID = 1,
                iYear = InvoiceBatch.iYear,
                sBookingDescription = addressDebtor.Debtor.sInvoiceRemark ?? string.Format("Eindnota {0}", firstPeriodDate.Year),
                sConsumptionAddress = addressDebtor.Address.GetAddressString(),
                sPaymentCondition = addressDebtor.Debtor.PaymentTerm.sCode,
                sSettlementCode = string.Join("-", InvoiceBatch.iYear, addressDebtor.iDebtorID, addressDebtor.iAddressID)
            };
            
            IEnumerable<RateCardRow> rateCardRows = GetRateCardRows(addressDebtor.iAddressID);

            foreach (RateCardRow rateCardRow in rateCardRows.ToList())
            {
                InvoiceLine invoiceLine = rateCardRowService.CreateInvoiceLine(rateCardRow, addressDebtor.Address, invoice, firstPeriodDate, lastPeriodDate, InvoiceBatch.InvoicePeriodID ?? 1);
                if (invoiceLine.dTotalAmount != 0)
                    invoice.InvoiceLines.Add(invoiceLine);
            }
            return await Task.FromResult(invoice);
        }

        public async Task<Invoice> CreatePrepaymentInvoice(AddressDebtor addressDebtor, int invoicePeriodID)
        {
            RateCardRowService rateCardRowService = new RateCardRowService(context);
            Invoice invoice = new Invoice() {
                dNewDepositAmount = 0,
                dNewFixedCosts = 0,
                dnewMontlyAmount = 0,
                dtDocumentDate = DateTime.Today,
                dtPostingDate = DateTime.Today,
                iAddressID = addressDebtor.iAddressID,
                iDebtorID = addressDebtor.iDebtorID,
                iInvoiceBatchID = InvoiceBatch.iInvoiceBatchID,
                InvoiceChecks = new List<InvoiceCheck>(),
                InvoiceLines = new List<InvoiceLine>(),
                iPeriod = InvoiceBatch.iPeriod,
                iStatusID = 1,
                iYear = InvoiceBatch.iYear,
                sBookingDescription = addressDebtor.Debtor.sInvoiceRemark ?? (invoicePeriodID == 1 ? FirstPeriodDate.ToString("MMMM yyyy", CultureInfo) : invoicePeriodID == 2 ? string.Format("Q{0}-{1}", Math.Ceiling(LastPeriodDate.Month / (decimal)3) , FirstPeriodDate.Year) : string.Empty),
                sConsumptionAddress = addressDebtor.Address.GetAddressString(),
                sPaymentCondition = addressDebtor.Debtor.PaymentTerm.sCode,
                sSettlementCode = string.Join("-", InvoiceBatch.iYear, addressDebtor.iDebtorID, addressDebtor.iAddressID)
            };
            Deposit deposit = context.Entry(addressDebtor).Collection(c => c.Deposits).Query().OrderByDescending(o => o.dtStartDate).FirstOrDefault(f => f.bIsActive);
            if (deposit != null || deposit.dAmount != 0)
            {
                invoice.InvoiceLines.Add(new InvoiceLine() {
                    bIsEndCalculation = false,
                    dUnitPrice = deposit.dAmount,
                    dQuantity = invoicePeriodID == 1 ? 1 : invoicePeriodID == 2 ? 3 : 1,
                    dAmount = deposit.dAmountexVAT,
                    dTotalAmount = decimal.Multiply(invoicePeriodID == 1 ? 1 : invoicePeriodID == 2 ? 3 : 1, deposit.dAmountexVAT),
                    iLedgerNumber = new[] { "23", "25", "27" }.Contains(addressDebtor.Address.sConnectionTypeKey) ? 204000 : 202000,
                    // Set rubricType Deposit
                    iRubricTypeID = 1,
                    // Set Unit is Month
                    iUnitID = 6,
                    // Default description
                    sDescription = "Voorschot",
                    sSettlementCode = string.Join("-", invoice.iYear, invoice.iDebtorID, invoice.iAddressID),
                    sSettlementText = invoicePeriodID == 2 ? string.Format("Q{0}-{1}", Math.Ceiling(LastPeriodDate.Month / (decimal)3), FirstPeriodDate.Year) : FirstPeriodDate.ToString("MMMM yyyy", CultureInfo)
                });
            }

            List<RateCardRow> rateCardRows = GetRateCardRows(addressDebtor.iAddressID);
            foreach (RateCardRow rateCardRow in rateCardRows)
            {
                InvoiceLine invoiceLine = rateCardRowService.CreateInvoiceLine(rateCardRow, addressDebtor.Address, invoice, FirstPeriodDate, LastPeriodDate, invoicePeriodID);
                if (invoiceLine.dTotalAmount != 0)
                    invoice.InvoiceLines.Add(invoiceLine);
            }
            return await Task.FromResult(invoice);
        }

        private List<RateCardRow> GetRateCardRows(int AddressID)
        {
            IQueryable<RateCardRow> rateCardRows = context.AddressRateCards.Where(w => w.iAddressKey == AddressID && w.dtStartDate < LastPeriodDate && (!w.dtEndDate.HasValue || w.dtEndDate.Value >= FirstPeriodDate))
                .SelectMany(sm => sm.RateCard.RateCardYears)
                .Where(w => w.iYear == FirstPeriodDate.Year)
                .SelectMany(sm => sm.RateCardRows);
            if (InvoiceBatch.iInvoiceTypeID == 1)
                rateCardRows = rateCardRows.Where(w => w.Rubric.iRubricTypeKey == 2);
            return rateCardRows.ToList();
        }

        private List<AddressDebtor> GetAddressDebtors()
        {
            //Default Selecting AddressDebtors For Invoicing
            IQueryable<AddressDebtor> addressDebtors = context.AddressDebtors
                    .Where(w => w.Address.iProjectKey == InvoiceBatch.iProjectID &&
                    !w.bFinished &&
                        w.dtStartDate < LastPeriodDate &&
                        (!w.dtEndDate.HasValue || (w.dtEndDate.HasValue && w.dtEndDate >= FirstPeriodDate)) &&
                        (!w.dtEndDate.HasValue || EF.Functions.DateDiffDay(w.dtStartDate, w.dtEndDate) > 5)
                    );

            switch (InvoiceBatch.iInvoiceTypeID)
            {
                case 1:
                    //Prepayment
                    addressDebtors = addressDebtors.Where(w => (!w.Debtor.dtNoInvoicePeriod.HasValue || w.Debtor.dtNoInvoicePeriod.Value != FirstPeriodDate) && (!w.BillingTypeID.HasValue || w.BillingTypeID == 1));
                    if (!InvoiceBatch.InvoicePeriodID.HasValue || InvoiceBatch.InvoicePeriodID.Value == 1)
                    {
                        //Monthly Prepayment
                        addressDebtors = addressDebtors.Where(w => w.Debtor.InvoicePeriodID == 1);
                    }
                    else if (InvoiceBatch.InvoicePeriodID == 2)
                    {
                        //Quarter Prepayment
                        addressDebtors = addressDebtors.Where(w => w.Debtor.InvoicePeriodID == 2);
                    }
                    break;
                case 2:
                    //Annual Invoice
                    addressDebtors = addressDebtors.Where(w => !w.bFinished && !w.dtEndDate.HasValue);
                    break;
                case 3:
                    //Final Invoice
                    addressDebtors = addressDebtors.Where(w => w.dtEndDate.HasValue && w.dtEndDate.Value >= FirstPeriodDate && w.dtEndDate.Value < LastPeriodDate);
                    break;
                case 4:
                    // Default Invoice
                    if (!InvoiceBatch.InvoicePeriodID.HasValue || InvoiceBatch.InvoicePeriodID.Value == 1)
                    {
                        //Monthly Prepayment
                        addressDebtors = addressDebtors.Where(w => w.Debtor.InvoicePeriodID == 1);
                    }
                    else if (InvoiceBatch.InvoicePeriodID == 2)
                    {
                        //Quarter Prepayment
                        addressDebtors = addressDebtors.Where(w => w.Debtor.InvoicePeriodID == 2);
                    }
                    break;
            }
            return addressDebtors.ToList();
        }

        //public async Task<InvoiceResult> CreateQuarterPrepayment(int addressDebtorID, DateTime periodStartDate, DateTime periodEndDate)
        //{
        //    //Find AddressDebtor
        //    AddressDebtor addressDebtor = await context.AddressDebtors.FindAsync(addressDebtorID);
        //    if (addressDebtor == null)
        //        return await Task.FromResult(InvoiceResult.Failed(string.Format("Er kon geen adresdebiteur gevonden worden met id: {0}", addressDebtorID)));
        //    if (addressDebtor.BillingTypeID.HasValue && addressDebtor.BillingTypeID != 1)
        //        return await Task.FromResult(InvoiceResult.Failed("Voor deze debiteur kan geen voorschotnota gemaakt worden"));
        //    if (addressDebtor.Debtor.InvoicePeriodID != 2)
        //        return await Task.FromResult(InvoiceResult.Failed("De instelling van deze debiteur staat zo dat hiervoor geen kwartaalnota opgemaakt kan worden"));

        //    // Find InvoiceBatch
        //    InvoiceBatch invoiceBatch = await context.InvoiceBatches.FirstOrDefaultAsync(f => f.iInvoiceTypeID == 1 && f.iYear == periodEndDate.Year && f.iPeriod == periodEndDate.Month && f.iProjectID == addressDebtor.Address.iProjectKey);
        //    if (invoiceBatch == null)
        //        return await Task.FromResult(InvoiceResult.Failed(string.Format("Er kon geen factuurbatch met type {0} voor periode {1} en projectid {2} gevonden worden.", 1, periodEndDate.ToString("MM yyyy"), addressDebtor.Address.iProjectKey)));

        //    // Check if there is already an invoice for this addressDebtor
        //    if (context.Invoices.Any(u => u.iAddressID == addressDebtor.iAddressID && u.iDebtorID == addressDebtor.iDebtorID && u.iInvoiceBatchID == invoiceBatch.iInvoiceBatchID))
        //        return await Task.FromResult(InvoiceResult.Failed("Er bestaat al voor deze debiteur en adres en dit type een factuur"));

        //    //Create Invoice
        //    await CreateInvoice(addressDebtor, invoiceBatch, periodStartDate, periodEndDate);

        //    return await Task.FromResult(InvoiceResult.Success);
        //}

        public async Task CheckInvoice(int invoiceID, string userID)
        {
            Invoice invoice = await context.Invoices.Include(i => i.InvoiceLines).FirstOrDefaultAsync(f => f.iInvoiceID == invoiceID);
            await invoice.Check(userID, context, true);
        }

        private async Task CreateInvoice(AddressDebtor addressDebtor, InvoiceBatch invoiceBatch, DateTime firstPeriodDate, DateTime lastPeriodDate)
        {
            RateCardRowService rateCardRowService = new RateCardRowService(context);
            CultureInfo dCI = new CultureInfo("nl-NL");
            // Check if invoiceBatchType is Final Invoice so set first and lastdatetime
            switch (invoiceBatch.iInvoiceTypeID)
            {
                case 2:
                    // Annual Invoice
                    firstPeriodDate = new DateTime(Math.Max(firstPeriodDate.FirstDayOfYear().Ticks, addressDebtor.dtStartDate.Ticks));
                    break;
                case 3:
                    // Final Invoice
                    firstPeriodDate = new DateTime(Math.Max(firstPeriodDate.FirstDayOfYear().Ticks, addressDebtor.dtStartDate.Ticks));
                    lastPeriodDate = addressDebtor.dtEndDate.Value;
                    break;
            }
            Invoice debtorInvoice = new Invoice
            {
                Debtor = addressDebtor.Debtor,
                dNewDepositAmount = 0,
                dNewFixedCosts = 0,
                dnewMontlyAmount = 0,
                dtDocumentDate = DateTime.Today,
                dtPostingDate = DateTime.Today,
                iAddressID = addressDebtor.iAddressID,
                iDebtorID = addressDebtor.iDebtorID,
                InvoiceBatch = invoiceBatch,
                InvoiceChecks = new List<InvoiceCheck>(),
                InvoiceLines = new List<InvoiceLine>(),
                iPeriod = invoiceBatch.iPeriod,
                // Set conceptstatus
                iStatusID = 1,
                iYear = invoiceBatch.iYear,
                sBookingDescription = addressDebtor.Debtor.sInvoiceRemark ?? (invoiceBatch.iInvoiceTypeID == 2 ? string.Format("Jaarnota {0}", firstPeriodDate.Year) : invoiceBatch.iInvoiceTypeID == 3 ? string.Format("Eindnota {0}", firstPeriodDate.Year) : firstPeriodDate.ToString("MMMM yyyy", dCI)),
                sConsumptionAddress = addressDebtor.Address.GetAddressString(),
                sPaymentCondition = addressDebtor.Debtor.PaymentTerm.sCode,
                sSettlementCode = string.Join("-", invoiceBatch.iYear, addressDebtor.iDebtorID, addressDebtor.iAddressID)
            };

            // Check if batchtype is monthly prepayment so add deposit row
            if (invoiceBatch.iInvoiceTypeID == 1)
            {
                context.Entry(addressDebtor).Collection(r => r.Deposits).Load();

                //Get current active deposit
                Deposit activeDeposit = addressDebtor.Deposits.OrderByDescending(o => o.dtStartDate).FirstOrDefault(f => f.bIsActive);
                if (activeDeposit != null && activeDeposit.dAmount != 0)
                {
                    InvoiceLine invoiceLine = new InvoiceLine
                    {
                        bIsEndCalculation = false,
                        dUnitPrice = activeDeposit.dAmount,
                        dQuantity = 1,
                        dAmount = activeDeposit.dAmountexVAT,
                        dTotalAmount = activeDeposit.dAmountexVAT,
                        iLedgerNumber = new[] { "23", "25", "27" }.Contains(addressDebtor.Address.sConnectionTypeKey) ? 204000 : 202000,
                        // Set rubricType Deposit
                        iRubricTypeID = 1,
                        // Set Unit is Month
                        iUnitID = 6,
                        // Default description
                        sDescription = "Voorschot",
                        sSettlementCode = string.Join("-", debtorInvoice.iYear, debtorInvoice.iDebtorID, debtorInvoice.iAddressID),
                        sSettlementText = firstPeriodDate.ToString("MMMM yyyy", dCI)
                    };
                    debtorInvoice.InvoiceLines.Add(invoiceLine);
                }
            }

            // Select all RateCardRows that will invoice
            IEnumerable<RateCardRow> rateCardRows = addressDebtor.Address.AddressRateCards
                .Where(w => w.dtStartDate < lastPeriodDate && (!w.dtEndDate.HasValue || w.dtEndDate.GetValueOrDefault() >= firstPeriodDate))
                .SelectMany(sm => sm.RateCard.RateCardYears)
                .Where(w => w.iYear == debtorInvoice.iYear)
                .SelectMany(sm => sm.RateCardRows);

            // If it is monthly prepayment select only fixed costst
            if (invoiceBatch.iInvoiceTypeID == 1)
                rateCardRows = rateCardRows.Where(w => w.Rubric.iRubricTypeKey == 2);

            foreach (RateCardRow rateCardRow in rateCardRows.ToList())
            {
                InvoiceLine invoiceLine = rateCardRowService.CreateInvoiceLine(rateCardRow, addressDebtor.Address, debtorInvoice, firstPeriodDate, lastPeriodDate, InvoiceBatch.InvoicePeriodID ?? 1);
                if (invoiceLine.dTotalAmount != 0)
                    debtorInvoice.InvoiceLines.Add(invoiceLine);
            }
            invoiceBatch.Invoices.Add(debtorInvoice);
            context.SaveChanges(UserID);

            await Task.FromResult(0);
        }
    }
}
