using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models.Results;
using PmsEteck.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PmsEteck.Data.Models
{
    [Table("InvoiceBatches", Schema = "invoice")]
    public class InvoiceBatch
    {
        #region Constructor
        private PmsEteckContext db;
        private readonly InvoiceBatchService _invoiceBatchService;

        public InvoiceBatch()
        {
            db = new PmsEteckContext();
            _invoiceBatchService = new InvoiceBatchService();
        }
        #endregion

        #region Properties
        [Key]
        public int iInvoiceBatchID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Factuurtype")]
        [ForeignKey("InvoiceType")]
        public int iInvoiceTypeID { get; set; }

        [Display(Name = "Facturatieperiode")]
        [ForeignKey("InvoicePeriod")]
        public int? InvoicePeriodID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Project")]
        [ForeignKey("Project")]
        public int iProjectID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Jaar")]
        public int iYear { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Periode")]
        [Range(1, 12, ErrorMessage = "{0} moet een waarde bevatten tussen {1} en {2}")]
        public int iPeriod { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Aanmaakdatum")]
        public DateTime dtDateTime { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Aangemaakt door")]
        [ForeignKey("ApplicationUser")]
        public string userID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "# verkoopadressen")]
        public int iNumberOfAdresses { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Status")]
        [ForeignKey("Status")]
        public int iStatusID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Standaard factuurdatum")]
        public DateTime dtInvoiceDateTime { get; set; }

        [Display(Name = "Gepland voor")]
        public DateTime? ScheduledDate { get; set; }

        [Display(Name = "Status")]
        [ForeignKey("InvoiceBatchStatus")]
        public int? StatusID { get; set; }

        [Display(Name = "Status")]
        public virtual InvoiceBatchStatus InvoiceBatchStatus { get; set; }

        [Display(Name = "Status")]
        public virtual OldInvoiceStatus Status { get; set; }

        [Display(Name = "Factuurtype")]
        public virtual InvoiceType InvoiceType { get; set; }

        [Display(Name = "Project")]
        public virtual ProjectInfo Project { get; set; }

        [Display(Name = "Facturen")]
        public List<Invoice> Invoices { get; set; }

        [Display(Name = "Aangemaakt door")]
        [NotMapped]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Facturatieperiode")]
        public virtual InvoicePeriod InvoicePeriod { get; set; }
        #endregion

        #region Methods
        public async Task CreateAsync(string userID)
        {
            if (await db.InvoiceBatches.FirstOrDefaultAsync(f => f.iInvoiceTypeID == iInvoiceTypeID && f.iProjectID == iProjectID && f.iYear == iYear && f.iPeriod == iPeriod) == null)
            {
                DateTime firstPeriodDay = iInvoiceTypeID == 2 ? new DateTime(iYear, 1, 1) : new DateTime(iYear, iPeriod, 1);
                DateTime lastPeriodDay = iInvoiceTypeID == 2 ? firstPeriodDay.AddYears(1) : firstPeriodDay.AddMonths(1);
                switch (iInvoiceTypeID)
                {
                    case 2:
                        iNumberOfAdresses = await db.AddressDebtors.CountAsync(c => c.Address.iProjectKey == iProjectID && (!c.dtEndDate.HasValue || EF.Functions.DateDiffDay(c.dtEndDate, c.dtStartDate) > 5) && c.dtStartDate < lastPeriodDay && (!c.dtEndDate.HasValue || c.dtEndDate >= firstPeriodDay));
                        break;
                    case 3:
                        iNumberOfAdresses = await db.AddressDebtors.CountAsync(c => c.Address.iProjectKey == iProjectID && (!c.dtEndDate.HasValue || EF.Functions.DateDiffDay(c.dtEndDate, c.dtStartDate) > 5) && c.dtStartDate < lastPeriodDay && (c.dtEndDate.HasValue && c.dtEndDate >= firstPeriodDay && c.dtEndDate < lastPeriodDay));
                        break;
                    case 1:
                    default:
                        iNumberOfAdresses = await db.Addresses.CountAsync(c => c.iProjectKey == iProjectID && c.ConnectionType.sConnectionTypeDescription.StartsWith("Verkoop"));
                        break;
                }
                db.InvoiceBatches.Add(this);
                await db.SaveChangesAsync(userID);
                await db.Entry(this).GetDatabaseValuesAsync();
                IQueryable<AddressDebtor> addressDebtors = db.AddressDebtors.Where(w => w.Address.iProjectKey == iProjectID && (!w.dtEndDate.HasValue || EF.Functions.DateDiffDay(w.dtEndDate, w.dtStartDate) > 5) && !w.bFinished && w.dtStartDate < lastPeriodDay && (!w.dtEndDate.HasValue || (w.dtEndDate.HasValue && w.dtEndDate.Value >= firstPeriodDay)));
                if (iInvoiceTypeID == 3)
                    addressDebtors = addressDebtors.Where(w => w.dtEndDate.HasValue && w.dtEndDate.Value >= firstPeriodDay && w.dtEndDate.Value < lastPeriodDay);

                if (iInvoiceTypeID == 1)
                    addressDebtors = addressDebtors.Where(w => w.Debtor.InvoicePeriodID == 1 && (!w.Debtor.dtNoInvoicePeriod.HasValue || w.Debtor.dtNoInvoicePeriod.Value != firstPeriodDay) && (!w.BillingTypeID.HasValue || w.BillingTypeID == 1));
                if (iInvoiceTypeID == 4)
                    addressDebtors = addressDebtors.Where(w => w.Debtor.InvoicePeriodID == 1 && (!w.Debtor.dtNoInvoicePeriod.HasValue || w.Debtor.dtNoInvoicePeriod.Value != firstPeriodDay) && (w.BillingTypeID.HasValue && w.BillingTypeID.Value == 2));
                List<AddressDebtor> addressDebtorList = await addressDebtors.ToListAsync();
                List<Task<InvoiceResult>> taskList = new List<Task<InvoiceResult>>();
                foreach (AddressDebtor addressDebtor in addressDebtorList)
                {
                    Invoice invoice = new Invoice
                    {
                        Address = addressDebtor.Address,
                        Debtor = addressDebtor.Debtor,
                        dNewDepositAmount = 0,
                        dNewFixedCosts = 0,
                        dnewMontlyAmount = 0,
                        dtDocumentDate = dtInvoiceDateTime,
                        dtPostingDate = dtInvoiceDateTime,
                        iAddressID = addressDebtor.iAddressID,
                        iDebtorID = addressDebtor.iDebtorID,
                        iInvoiceBatchID = iInvoiceBatchID,
                        InvoiceBatch = this,
                        InvoiceLines = new List<InvoiceLine>(),
                        iPeriod = iPeriod,
                        iStatusID = 1,
                        iYear = iYear,
                        sBookingDescription = addressDebtor.Debtor.sInvoiceRemark ?? firstPeriodDay.ToString("MMMM yyyy", CultureInfo.CurrentCulture),
                        sConsumptionAddress = addressDebtor.Address.GetAddressString(),
                        sPaymentCondition = addressDebtor.Debtor.PaymentTerm.sCode,
                        sSettlementCode = string.Join("-", iYear, addressDebtor.iDebtorID, addressDebtor.iAddressID)
                    };
                    taskList.Add(invoice.CreateNewInvoice());
                    Invoices.Add(invoice);
                }
                await Task.WhenAll(taskList);
                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync()
        {
            InvoiceBatch invoiceBatch = await db.InvoiceBatches.FindAsync(iInvoiceBatchID);
            DateTime firstPeriodDay = invoiceBatch.iInvoiceTypeID == 2 ? new DateTime(invoiceBatch.iYear, 1, 1) : new DateTime(invoiceBatch.iYear, invoiceBatch.iPeriod, 1);
            DateTime lastPeriodDay = invoiceBatch.iInvoiceTypeID == 2 ? firstPeriodDay.AddYears(1) : firstPeriodDay.AddMonths(1);
            var invoiceList = db.Invoices.Where(w => w.iInvoiceBatchID == invoiceBatch.iInvoiceBatchID).Select(s => new { s.iAddressID, s.iDebtorID }).ToList();
            List<Task<InvoiceResult>> taskList = new List<Task<InvoiceResult>>();
            if (iStatusID != 3)
            {
                invoiceBatch.Invoices = new List<Invoice>();
                IQueryable<AddressDebtor> addressDebtors = db.AddressDebtors.Where(w => w.Address.iProjectKey == iProjectID && (!w.dtEndDate.HasValue || EF.Functions.DateDiffDay(w.dtEndDate, w.dtStartDate) > 5) && !w.bFinished && w.dtStartDate < lastPeriodDay && (!w.dtEndDate.HasValue || (w.dtEndDate.HasValue && w.dtEndDate.Value >= firstPeriodDay)));
                if (iInvoiceTypeID == 3)
                {
                    addressDebtors = addressDebtors.Where(w => w.dtEndDate.HasValue && w.dtEndDate.Value >= firstPeriodDay && w.dtEndDate.Value < lastPeriodDay);
                    invoiceBatch.iNumberOfAdresses = await db.AddressDebtors.Where(w => w.Address.iProjectKey == iProjectID && (!w.dtEndDate.HasValue || EF.Functions.DateDiffDay(w.dtEndDate, w.dtStartDate) > 5) && w.dtStartDate < lastPeriodDay && w.dtEndDate.HasValue && w.dtEndDate.Value >= firstPeriodDay && w.dtEndDate.Value < lastPeriodDay).CountAsync();
                }

                if (iInvoiceTypeID == 1)
                    addressDebtors = addressDebtors.Where(w => w.Debtor.InvoicePeriodID == 1 && (!w.Debtor.dtNoInvoicePeriod.HasValue || w.Debtor.dtNoInvoicePeriod.Value != firstPeriodDay) && (!w.BillingTypeID.HasValue || w.BillingTypeID == 1));
                if (iInvoiceTypeID == 4)
                    addressDebtors = addressDebtors.Where(w => w.Debtor.InvoicePeriodID == 1 && (!w.Debtor.dtNoInvoicePeriod.HasValue || w.Debtor.dtNoInvoicePeriod.Value != firstPeriodDay) && (w.BillingTypeID.HasValue && w.BillingTypeID.Value == 2));

                List<AddressDebtor> addressDebtorList = await addressDebtors.ToListAsync();

                addressDebtorList = addressDebtorList.Where(w => !invoiceList.Contains(new { w.iAddressID, w.iDebtorID })).ToList();

                foreach (AddressDebtor addressDebtor in addressDebtorList)
                {
                    Invoice invoice = new Invoice
                    {
                        Address = addressDebtor.Address,
                        Debtor = addressDebtor.Debtor,
                        dNewDepositAmount = 0,
                        dNewFixedCosts = 0,
                        dnewMontlyAmount = 0,
                        dtDocumentDate = invoiceBatch.dtInvoiceDateTime < DateTime.Today ? DateTime.Today : invoiceBatch.dtInvoiceDateTime,
                        dtPostingDate = invoiceBatch.dtInvoiceDateTime < DateTime.Today ? DateTime.Today : invoiceBatch.dtInvoiceDateTime,
                        iAddressID = addressDebtor.iAddressID,
                        iDebtorID = addressDebtor.iDebtorID,
                        iInvoiceBatchID = invoiceBatch.iInvoiceBatchID,
                        InvoiceBatch = invoiceBatch,
                        InvoiceLines = new List<InvoiceLine>(),
                        iPeriod = iPeriod,
                        iStatusID = 1,
                        iYear = iYear,
                        sBookingDescription = addressDebtor.Debtor.sInvoiceRemark ?? firstPeriodDay.ToString("MMMM yyyy", CultureInfo.CurrentCulture),
                        sConsumptionAddress = addressDebtor.Address.GetAddressString(),
                        sPaymentCondition = addressDebtor.Debtor.PaymentTerm.sCode,
                        sSettlementCode = string.Join("-", iYear, addressDebtor.iDebtorID, addressDebtor.iAddressID)
                    };
                    taskList.Add(invoice.CreateNewInvoice());
                    invoiceBatch.Invoices.Add(invoice);
                }
                await Task.WhenAll(taskList);
                await db.SaveChangesAsync();
            }
        }

        public async Task<InvoiceResult> ValidateAsync(int invoiceBatchID)
        {
            // Get InvoiceBatch
            InvoiceBatch invoiceBatch = await db.InvoiceBatches.Include(i => i.Invoices).FirstOrDefaultAsync(f => f.iInvoiceBatchID == invoiceBatchID);
            if (invoiceBatch != null)
            {
                iStatusID = invoiceBatch.Invoices.Any(u => u.iStatusID == 3) ? 2 : 1;
                try
                {
                    return await Task.FromResult(InvoiceResult.Success);
                }
                catch (Exception e)
                {
                    return await Task.FromResult(InvoiceResult.Failed(e.Message));
                }
            }
            else
            {
                return await Task.FromResult(InvoiceResult.Failed("Factuurbatch kan niet gevonden worden."));
            }
        }
        #endregion
    }
}
