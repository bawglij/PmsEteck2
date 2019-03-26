using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace PmsEteck.Data.Models
{
    using PmsEteck.Data.Extensions;
    using System;
    using System.Configuration;
    using System.IO;
    using Toolbelt.ComponentModel.DataAnnotations.Schema;
    //using WSCustomer;

    public enum PartnerType
    {
        Leeg = 0,
        Bedrijf = 1,
        Persoon = 2
    }
    [Table("Debtors", Schema = "invoice")]
    public class Debtor
    {
        public Debtor()
        {
            db = new PmsEteckContext();
        }
        public Debtor(PmsEteckContext context)
        {
            db = context;
        }
        #region Constructor
        private readonly PmsEteckContext db = new PmsEteckContext();
        #endregion

        #region Static Fields
        private readonly string WebserviceUser = ConfigurationManager.AppSettings["WebserviceUser"];
        private readonly string WebserviceDomain = ConfigurationManager.AppSettings["WebserviceDomain"];
        private readonly string WebservicePassword = ConfigurationManager.AppSettings["WebservicePassword"];
        private readonly string ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
        private readonly string IncassoBV = ConfigurationManager.AppSettings["IncassoBV"];
        private readonly string InvoiceBaseLocation = ConfigurationManager.AppSettings["originalLocation"];
        #endregion

        #region Properties

        [Key]
        public int iDebtorID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Debiteurnummer")]
        [Index("IX_UniqueDebtorCode", IsUnique = true)]
        public int iDebtorCode { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Verzendprofiel van document")]
        public int iShippingProfileID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Klantboekingsgroep")]
        [MaxLength(10, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sCustomerPostingGroup { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Betalingscondities")]
        public int iPaymentTermID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Aanmaningsconditiecode")]
        [MaxLength(10, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sReminderTermsCode { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Btw-bedrijfsboekingsgroep")]
        [MaxLength(10, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sVATBusPostingGroup { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Partnersoort")]
        public PartnerType iPartnerType { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Aanhef")]
        public int iTitleID { get; set; }

        [Display(Name = "Voorletter(s)")]
        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sInitials { get; set; }

        [MaxLength(250, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Voornaam")]
        public string sFirstName { get; set; }

        [MaxLength(250, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Achternaam")]
        public string sLastName { get; set; }

        [MaxLength(30, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Telefoonnummer")]
        [DataType(DataType.PhoneNumber)]
        public string sPhoneNumber { get; set; }

        [MaxLength(80, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Emailadres")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} is geen geldig emailadres")]
        public string sEmailAddress { get; set; }

        [MaxLength(250, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Opmerking")]
        public string sRemark { get; set; }

        [MaxLength(80, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Btw-nummer")]
        public string sVATNumber { get; set; }

        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "IBAN nummer")]
        public string sIBANNumber { get; set; }

        [MaxLength(20, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "SWIFT")]
        public string sSWIFTCode { get; set; }

        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Standaard boekingsomschrijving")]
        public string sInvoiceRemark { get; set; }

        [Display(Name = "Leegstand")]
        public bool bIsVacancy { get; set; }

        [Display(Name = "Geblokkeerd")]
        public bool bIsBlocked { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Naam")]
        public string sBillingName { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Straat + huisnummer")]
        public string sBillingAddress { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [MaxLength(20, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Postcode")]
        public string sBillingPostalCode { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [MaxLength(30, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Plaats")]
        public string sBillingCity { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [MaxLength(10, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Land")]
        public string sBillingCountry { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Klanttype")]
        public int iDebtorTypeID { get; set; }

        [Display(Name = "Nog importeren")]
        public bool bWaitingForImport { get; set; }

        [Display(Name = "Geboortedatum")]
        public DateTime? dtDateOfBirth { get; set; }

        [Display(Name = "Postbusadres")]
        public bool bMailboxAddress { get; set; }

        [Display(Name = "Geen verzamelfactuur ontvangen?")]
        public bool bNoCombinedInvoice { get; set; }

        [Display(Name = "Ontvangt nieuwsbrief")]
        public bool ReceiveNewsletter { get; set; }

        [Display(Name = "Geen factuur aanmaken voor periode")]
        public DateTime? dtNoInvoicePeriod { get; set; }

        [Display(Name = "Factuurperiode")]
        public int? InvoicePeriodID { get; set; } = 1;

        public string sFullName
        {
            get
            {
                return string.Join(" ", !string.IsNullOrEmpty(sInitials) ? sInitials : !string.IsNullOrEmpty(sFirstName) ? sFirstName : string.Empty, sLastName, !string.IsNullOrEmpty(sInitials) && !string.IsNullOrEmpty(sFirstName) ? "(" + sFirstName + ")" : string.Empty);
            }
        }

        #endregion

        #region Single References

        [Display(Name = "Betalingsconditie")]
        public virtual PaymentTerm PaymentTerm { get; set; }

        [Display(Name = "Verzendprofiel van document")]
        public virtual ShippingProfile ShippingProfile { get; set; }

        [Display(Name = "Klanttype")]
        public virtual DebtorType DebtorType { get; set; }

        [Display(Name = "Aanhef")]
        public virtual Title Title { get; set; }

        [Display(Name = "Factuurperiode")]
        public virtual InvoicePeriod InvoicePeriod { get; set; }

        #endregion

        #region List References

        [Display(Name = "Adresdebiteuren")]
        public ICollection<AddressDebtor> AddressDebtors { get; set; }

        [Display(Name = "Bewoners")]
        public ICollection<Occupant> Occupants { get; set; }

        [Display(Name = "Meldingen")]
        public ICollection<Ticket> Tickets { get; set; }

        [Display(Name = "Bestanden")]
        public ICollection<DebtorFile> DebtorFiles { get; set; }

        public ICollection<PaymentTermHistory> PaymentTermHistories { get; set; }

        [Display(Name = "Servicemeldingen")]
        public ICollection<ServiceTicket> ServiceTickets { get; set; }

        [Display(Name = "Facturen")]
        public List<Invoice> Invoices { get; set; }

        [Display(Name = "Betalingsgegevens")]
        public List<PaymentHistory> PaymentHistory { get; set; }

        [Display(Name = "Betaalstaffel")]
        public ICollection<RateCardScaleHistory> RateCardScaleHistories { get; set; }

        [Display(Name = "Geblokkeerde perioden")]
        public ICollection<NoInvoicePeriods> NoInvoicePeriods { get; set; }
        #endregion

        #region Methods
        /*
        public async Task<Result> AddDebtorToNav(string navisionPrefix = null)
        {
            navisionPrefix = string.IsNullOrWhiteSpace(navisionPrefix) ? IncassoBV : navisionPrefix;
            try
            {
                WSCustomer_Service _service = new WSCustomer_Service();
                _service.Credentials = new System.Net.NetworkCredential(WebserviceUser, WebservicePassword, WebserviceDomain);
                _service.Url = string.Format("{0}/WS/{1}/Page/WSCustomer", ServiceUrl, Uri.EscapeDataString(navisionPrefix));


                WSCustomer wsCustomer = new WSCustomer
                {
                    City = sBillingCity,
                    Customer_Posting_Group = sCustomerPostingGroup,
                    Document_Sending_Profile = ShippingProfile.sCode,
                    E_Mail = sEmailAddress,
                    IBAN = sIBANNumber,
                    Address = sBillingAddress,
                    Blocked = false,
                    BlockedSpecified = true,
                    Country_Region_Code = sBillingCountry,
                    Name = sBillingName,
                    No = iDebtorCode.ToString(),
                    Partner_Type = iPartnerType == PartnerType.Bedrijf ? Partner_Type.Company : iPartnerType == PartnerType.Persoon ? Partner_Type.Person : Partner_Type._blank_,
                    Partner_TypeSpecified = true,
                    Payment_Terms_Code = PaymentTerm.sCode,
                    Phone_No = sPhoneNumber,
                    Post_Code = sBillingPostalCode,
                    Reminder_Terms_Code = sReminderTermsCode,
                    SWIFT_Code = sSWIFTCode,
                    VAT_Bus_Posting_Group = sVATBusPostingGroup,
                    VAT_Registration_No = sVATNumber
                };

                _service.Create(ref wsCustomer);

                return await Task.FromResult(Result.Success);

            }
            catch (Exception e)
            {
                return await Task.FromResult(Result.Failed(e.Message));
            }
        }
        */
        private Task CheckDirectories()
        {
            string UserName = string.Format("{0}@{1}.nl", WebserviceUser, WebserviceDomain);
            ImpersonationHelper.Impersonate(null, UserName, WebservicePassword, delegate
            {

                string debtorPath = Path.Combine(InvoiceBaseLocation, iDebtorCode.ToString());
                if (!Directory.Exists(debtorPath))
                    Directory.CreateDirectory(debtorPath);

                string[] invoiceDirectories = ConfigurationManager.AppSettings["invoiceDirectories"].Split(',');
                for (int i = 0; i < invoiceDirectories.Length; i++)
                {
                    if (!Directory.Exists(Path.Combine(debtorPath, invoiceDirectories[i])))
                        Directory.CreateDirectory(Path.Combine(debtorPath, invoiceDirectories[i]));
                }
            });

            return Task.FromResult(0);
        }
        #endregion
    }
}