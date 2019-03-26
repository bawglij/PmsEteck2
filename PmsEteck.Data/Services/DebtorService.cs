using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models;
using PmsEteck.Data.Models.Results;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace PmsEteck.Data.Services
{
    public class DebtorService : BaseService<Debtor>
    {

        #region Static Fields
        private readonly string WebserviceUser = ConfigurationManager.AppSettings["WebserviceUser"];
        private readonly string WebserviceDomain = ConfigurationManager.AppSettings["WebserviceDomain"];
        private readonly string WebservicePassword = ConfigurationManager.AppSettings["WebservicePassword"];
        private readonly string ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        private readonly string IncassoBV = ConfigurationManager.AppSettings["IncassoBV"];
        private readonly string InvoiceBaseLocation = ConfigurationManager.AppSettings["originalLocation"];
        #endregion

        public Result NoInvoiceForPeriod(int debtorId, DateTime period)
        {
            Debtor debtor = dbSet.Include(i => i.NoInvoicePeriods)
                .FirstOrDefault(f => f.iDebtorID == debtorId);
            if (debtor == null)
                return Result.Failed("Debtor not found");
            if (debtor.NoInvoicePeriods.Any(u => u.BlockedPeriod == period))
                return Result.Success;

            debtor.NoInvoicePeriods.Add(new NoInvoicePeriods(debtorId, period));
            Update(debtor);
            return Result.Success;
        }

        public async Task<Result> NoInvoicePeriodAsync(int debtorId, DateTime period)
        {
            Debtor debtor = await dbSet.Include(i => i.NoInvoicePeriods)
                .FirstOrDefaultAsync(f => f.iDebtorID == debtorId);
            if (debtor == null)
                return Result.Failed("Debtor not found");
            if (debtor.NoInvoicePeriods.Any(u => u.BlockedPeriod == period))
                return Result.Success;

            debtor.NoInvoicePeriods.Add(new NoInvoicePeriods(debtorId, period));
            Update(debtor);
            return Result.Success;
        }

        public Result RemoveBlockedInvoicePeriod(int debtorId, DateTime period)
        {
            Debtor debtor = dbSet.Include(i => i.NoInvoicePeriods).FirstOrDefault(f => f.iDebtorID == debtorId);
            if (debtor == null)
                return Result.Failed("Debtor not found");

            if (debtor.NoInvoicePeriods.Any(u => u.BlockedPeriod == period))
            {
                NoInvoicePeriods noInvoicePeriod = debtor.NoInvoicePeriods.FirstOrDefault(f => f.BlockedPeriod == period);
                debtor.NoInvoicePeriods.Remove(noInvoicePeriod);
                Update(debtor);
            }
            return Result.Success;
        }

        public async Task<Result> RemoveBlockedInvoicePeriodAsync(int debtorId, DateTime period)
        {
            Debtor debtor = await dbSet.Include(i => i.NoInvoicePeriods).FirstOrDefaultAsync(f => f.iDebtorID == debtorId);
            if (debtor == null)
                return Result.Failed("Debtor not found");

            if (debtor.NoInvoicePeriods.Any(u => u.BlockedPeriod == period))
            {
                NoInvoicePeriods noInvoicePeriod = debtor.NoInvoicePeriods.FirstOrDefault(f => f.BlockedPeriod == period);
                debtor.NoInvoicePeriods.Remove(noInvoicePeriod);
                Update(debtor);
            }
            return Result.Success;
        }

        //public async Task<Result> AddDebtorToNav(int debtorId)
        //{
        //    try
        //    {
        //        Debtor debtor = Include(i => i.AddressDebtors).FindById(debtorId);
        //        if (debtor == null)
        //        {
        //            throw new Exception(string.Format("Debiteur met id {0} kan niet gevonden worden", debtorId));
        //        }

        //        WSCustomer.WSCustomer_Service _service = new WSCustomer.WSCustomer_Service();
        //        _service.Credentials = new System.Net.NetworkCredential(WebserviceUser, WebservicePassword, WebserviceDomain);
        //        _service.Url = string.Format("{0}/WS/{1}/Page/WSCustomer", ServiceUrl, Uri.EscapeDataString(IncassoBV));
        //        if (debtor.AddressDebtors.Any(u => u.Address.Project.InvoiceViaOwnCollection))
        //        {
        //            ProjectInfo project = debtor.AddressDebtors.FirstOrDefault(f => f.Address.Project.InvoiceViaOwnCollection).Address.Project;
        //            _service.Url = string.Format("{0}/WS/{1}/Page/WSCustomer", ServiceUrl, Uri.EscapeDataString(project.DebtCollectionCustomer.NavisionPrefix));
        //        }

        //        WSCustomer.WSCustomer wsCustomer = new WSCustomer.WSCustomer
        //        {
        //            City = debtor.sBillingCity,
        //            Customer_Posting_Group = debtor.sCustomerPostingGroup,
        //            Document_Sending_Profile = debtor.ShippingProfile.sCode,
        //            E_Mail = debtor.sEmailAddress,
        //            IBAN = debtor.sIBANNumber,
        //            Address = debtor.sBillingAddress,
        //            Blocked = false,
        //            BlockedSpecified = true,
        //            Country_Region_Code = debtor.sBillingCountry,
        //            Name = debtor.sBillingName,
        //            No = debtor.iDebtorCode.ToString(),
        //            Partner_Type = debtor.iPartnerType == PartnerType.Bedrijf ? WSCustomer.Partner_Type.Company : debtor.iPartnerType == PartnerType.Persoon ? WSCustomer.Partner_Type.Person : WSCustomer.Partner_Type._blank_,
        //            Partner_TypeSpecified = true,
        //            Payment_Terms_Code = debtor.PaymentTerm.sCode,
        //            Phone_No = debtor.sPhoneNumber,
        //            Post_Code = debtor.sBillingPostalCode,
        //            Reminder_Terms_Code = debtor.sReminderTermsCode,
        //            SWIFT_Code = debtor.sSWIFTCode,
        //            VAT_Bus_Posting_Group = debtor.sVATBusPostingGroup,
        //            VAT_Registration_No = debtor.sVATNumber
        //        };

        //        _service.Create(ref wsCustomer);

        //        return await Task.FromResult(Result.Success);

        //    }
        //    catch (Exception e)
        //    {
        //        return await Task.FromResult(Result.Failed(e.Message));
        //    }
        //}
    }
}
