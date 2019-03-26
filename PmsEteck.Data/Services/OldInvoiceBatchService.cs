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
    public class OldInvoiceBatchService
    {
        private string UserID;
        private PmsEteckContext context;
        private List<InvoiceBatch> InvoiceBatches;
        public OldInvoiceBatchService()
        {
            context = new PmsEteckContext() ?? throw new ArgumentNullException(nameof(context));
            InvoiceBatches = new List<InvoiceBatch>();
        }

        public async void CreateNewInvoiceBatches(int[] projectIDs, int invoiceType, int? invoicePeriodID, int year, int period, DateTime invoiceDate, string userId)
        {
            UserID = userId;
            foreach (int projectID in projectIDs)
            {
                if (!context.InvoiceBatches.Any(a => a.iProjectID == projectID && a.iInvoiceTypeID == invoiceType && a.iYear == year && a.iPeriod == period && a.InvoicePeriodID == invoicePeriodID))
                {
                    if (new int[] { 1, 4 }.Contains(invoiceType))
                    {
                        // Check if selected periode is oke
                    }
                    //Create new invoicebatch
                    InvoiceBatch newInvoiceBatch = new InvoiceBatch
                    {
                        dtDateTime = DateTime.UtcNow,
                        dtInvoiceDateTime = invoiceDate,
                        iInvoiceTypeID = invoiceType,
                        InvoicePeriodID = invoicePeriodID,
                        iNumberOfAdresses = context.Addresses.Count(c => c.iProjectKey == projectID && !c.sConnectionTypeKey.StartsWith("1")),
                        Invoices = new List<Invoice>(),
                        iPeriod = period,
                        iProjectID = projectID,
                        //Set to status in process
                        iStatusID = 5,
                        iYear = year,
                        userID = userId
                    };
                    InvoiceBatches.Add(newInvoiceBatch);
                    context.InvoiceBatches.Add(newInvoiceBatch);

                    await context.SaveChangesAsync(userId);
                    context.Entry(newInvoiceBatch).Reference(c => c.Project).Load();
                }
            }

            CreateInvoicesForBatch();
        }

        public async void UpdateInvoiceBatch(int invoiceBatchID, string userID)
        {
            UserID = userID;
            InvoiceBatch invoiceBatch = await context.InvoiceBatches
                .Include(i => i.Invoices)
                .FirstOrDefaultAsync(f => f.iInvoiceBatchID == invoiceBatchID);
            InvoiceBatches.Add(invoiceBatch);
            // Set first periodday to firstday of month or firstday of year
            DateTime firstPeriodDate = invoiceBatch.iInvoiceTypeID == 2 ? new DateTime(invoiceBatch.iYear, 1, 1) : new DateTime(invoiceBatch.iYear, invoiceBatch.iPeriod, 1);
            // Set last periodday to firstday of next month or firstday of next year
            DateTime lastPeriodDate = invoiceBatch.iInvoiceTypeID == 2 ? firstPeriodDate.AddYears(1) : firstPeriodDate.AddMonths(1);
            IQueryable<AddressDebtor> addressDebtors = context.AddressDebtors
                .Include(i => i.Address.AddressRateCards.Select(s => s.RateCard.RateCardYears.Select(se => se.RateCardRows)))
                .Include(i => i.Address.AddressRateCards.Select(s => s.RateCard.RateCardYears.Select(se => se.RateCardRows)))
                .Include(i => i.Deposits)
                .Where(w => w.Address.iProjectKey == invoiceBatch.iProjectID &&
                    !w.bFinished &&
                    w.dtStartDate < lastPeriodDate &&
                    (!w.dtEndDate.HasValue || (w.dtEndDate.HasValue && w.dtEndDate >= firstPeriodDate)) &&
                        (!w.dtEndDate.HasValue || EF.Functions.DateDiffDay(w.dtStartDate, w.dtEndDate) > 5)
                );
            switch (invoiceBatch.iInvoiceTypeID)
            {
                case 1:
                    // Prepayment Invoice
                    addressDebtors = addressDebtors.Where(w => w.Debtor.InvoicePeriodID == 1 && (!w.Debtor.dtNoInvoicePeriod.HasValue || w.Debtor.dtNoInvoicePeriod.Value != firstPeriodDate) && (!w.BillingTypeID.HasValue || w.BillingTypeID == 1));
                    break;
                case 2:
                    // Annual Invoice
                    break;
                case 3:
                    // Final Invoice
                    addressDebtors = addressDebtors.Where(w => w.dtEndDate.HasValue && w.dtEndDate.Value >= firstPeriodDate && w.dtEndDate.Value < lastPeriodDate);
                    break;
                case 4:
                    // Monthly Invoice
                    addressDebtors = addressDebtors.Where(w => w.Debtor.InvoicePeriodID == 1 && (!w.Debtor.dtNoInvoicePeriod.HasValue || w.Debtor.dtNoInvoicePeriod.Value != firstPeriodDate) && (w.BillingTypeID.HasValue && w.BillingTypeID == 2));
                    break;
            }
            List<AddressDebtor> addressDebtorList = addressDebtors.ToList();

            // Get addressDebtors which has more than 5 days lived on address
            //addressDebtorList = addressDebtorList.Where(w => !w.dtEndDate.HasValue || (w.dtEndDate.HasValue && (w.dtEndDate.Value - w.dtStartDate).TotalDays > 5)).ToList();

            //Select only adressdebtors without invoice
            addressDebtorList = addressDebtorList.Where(w => !invoiceBatch.Invoices.Any(u => u.iAddressID == w.iAddressID && u.iDebtorID == w.iDebtorID)).ToList();

            if (addressDebtorList.Count > 0)
            {
                // Create all invoices
                List<Task> taskList = new List<Task>();
                addressDebtorList.ForEach(f => taskList.Add(CreateInvoice(f, invoiceBatch, firstPeriodDate, lastPeriodDate)));
                // Check all created invoices
                await Task.WhenAll(taskList);
                taskList.Clear();
                // Check all created invoies
            }
            invoiceBatch.Invoices.Where(w => w.iStatusID == 1 && w.InvoiceChecks.Count == 0).ToList().ForEach(i => i.Check(UserID, context, true));

            // Save changes
            if (invoiceBatch.iStatusID == 5)
                invoiceBatch.iStatusID = 1;
            await context.SaveChangesAsync(UserID);

        }

        private async void CreateInvoicesForBatch()
        {
            foreach (InvoiceBatch invoiceBatch in InvoiceBatches)
            {
                // Nieuwe CoDE
                InvoiceService invoiceService = new InvoiceService(context);
                InvoiceResult result = await invoiceService.CreateInvoicesForBatch(invoiceBatch.iInvoiceBatchID, UserID);

                if (result.Succeeded)
                {
                    invoiceBatch.iStatusID = 1;
                    await context.SaveChangesAsync(UserID);
                    return;
                }
                // Set first periodday to firstday of month or firstday of year
                //DateTime firstPeriodDate = invoiceBatch.iInvoiceTypeID == 2 ? new DateTime(invoiceBatch.iYear, 1, 1) : new DateTime(invoiceBatch.iYear, invoiceBatch.iPeriod, 1);
                //// Set last periodday to firstday of next month or firstday of next year
                //DateTime lastPeriodDate = invoiceBatch.iInvoiceTypeID == 2 ? firstPeriodDate.AddYears(1) : firstPeriodDate.AddMonths(1);
                //IQueryable<AddressDebtor> addressDebtors = context.AddressDebtors
                //    .Include(i => i.Address.AddressRateCards.Select(s => s.RateCard.RateCardYears.Select(se => se.RateCardRows)))
                //    .Include(i => i.Address.AddressRateCards.Select(s => s.RateCard.RateCardYears.Select(se => se.RateCardRows)))
                //    .Include(i => i.Deposits)
                //    .Where(w => w.Address.iProjectKey == invoiceBatch.iProjectID &&
                //        //!w.bFinished &&
                //        w.dtStartDate < lastPeriodDate &&
                //        (!w.dtEndDate.HasValue || (w.dtEndDate.HasValue && w.dtEndDate >= firstPeriodDate)) &&
                //        (!w.dtEndDate.HasValue || DbFunctions.DiffDays(w.dtStartDate, w.dtEndDate) > 5)
                //    );
                //switch (invoiceBatch.iInvoiceTypeID)
                //{
                //    case 1:
                //        // Prepayment Invoice
                //        addressDebtors = addressDebtors.Where(w => w.Debtor.InvoicePeriodID == 1 && (!w.Debtor.dtNoInvoicePeriod.HasValue || w.Debtor.dtNoInvoicePeriod.Value != firstPeriodDate) && (!w.BillingTypeID.HasValue || w.BillingTypeID == 1));
                //        break;
                //    case 2:
                //        // Annual Invoice
                //        break;
                //    case 3:
                //        // Final Invoice
                //        addressDebtors = addressDebtors.Where(w => w.dtEndDate.HasValue && w.dtEndDate.Value >= firstPeriodDate && w.dtEndDate.Value < lastPeriodDate);
                //        break;
                //    case 4:
                //        // Monthly Invoice
                //        addressDebtors = addressDebtors.Where(w => w.Debtor.InvoicePeriodID == 1 && (!w.Debtor.dtNoInvoicePeriod.HasValue || w.Debtor.dtNoInvoicePeriod.Value != firstPeriodDate) && (w.BillingTypeID.HasValue && w.BillingTypeID == 2));
                //        break;
                //}

                //List<AddressDebtor> addressDebtorList = addressDebtors.ToList();
                //// Get addressDebtors which has more than 5 days lived on address
                ////addressDebtorList = addressDebtorList.Where(w => !w.dtEndDate.HasValue || (w.dtEndDate.HasValue && (w.dtEndDate.Value - w.dtStartDate).TotalDays > 5)).ToList();

                //if (addressDebtorList.Count > 0)
                //{
                //    //Create all invoices
                //    List<Task> taskList = new List<Task>();
                //    addressDebtorList.ForEach(f => taskList.Add(CreateInvoice(f, invoiceBatch, firstPeriodDate, lastPeriodDate)));
                //    // Check all created invoices
                //    await Task.WhenAll(taskList);
                //    taskList.Clear();
                //    invoiceBatch.Invoices.ForEach(i => taskList.Add(i.Check(UserID, context, true)));
                //    await Task.WhenAll(taskList);
                //}
                ////Set status for batch to concept
                //invoiceBatch.iStatusID = 1;
                //await context.SaveChangesAsync(UserID);
            }
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
                InvoiceLine invoiceLine = rateCardRowService.CreateInvoiceLine(rateCardRow, addressDebtor.Address, debtorInvoice, firstPeriodDate, lastPeriodDate, invoiceBatch.InvoicePeriodID.GetValueOrDefault());
                if (invoiceLine.dTotalAmount != 0)
                    debtorInvoice.InvoiceLines.Add(invoiceLine);
            }
            invoiceBatch.Invoices.Add(debtorInvoice);
            context.SaveChanges(UserID);

            await Task.FromResult(0);
        }
    }
}
