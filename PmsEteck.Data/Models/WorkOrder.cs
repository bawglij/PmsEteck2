using PmsEteck.Data.Models.Results;
using PmsEteck.Data.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;

namespace PmsEteck.Data.Models
{
    [Table("WorkOrders", Schema = "service")]
    public class WorkOrder
    {
        #region Keys
        [Key]
        [Display(Name = "Werkbon")]
        public Guid WorkOrderID { get; set; }

        [Display(Name = "Servicemelding")]
        [ForeignKey("ServiceTicket")]
        [Required(ErrorMessage = "{0} is verplicht")]
        //Guid
        public Guid ServiceTicketID { get; set; }

        [Display(Name = "Status")]
        [ForeignKey("Status")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public int StatusID { get; set; }

        [Display(Name = "Oplossing categorie")]
        [ForeignKey("SolutionCategory")]
        public int? SolutionCategoryID { get; set; }

        [Display(Name = "Invoer monteur")]
        [ForeignKey("MechanicInput")]
        public Guid? MechanicInputID { get; set; }
        #endregion

        #region Properties
        [Display(Name = "Werkbon nummer")]
        public int WorkOrderCode { get; set; }
        public string WorkOrderCodeString => "W" + WorkOrderCode.ToString("00000000");

        [Display(Name = "Aanmaakdatum")]
        [DisplayFormat(DataFormatString = "{0:d-M-yyyy}")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Afgerond")]
        [DisplayFormat(DataFormatString = "{0:d-M-yyyy}")]
        public DateTime? FinishedDateTime { get; set; }

        [Display(Name = "Instructie")]
        [MaxLength(1000, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string Instruction { get; set; }

        [Display(Name = "Oplossing beschrijving")]
        [MaxLength(1000, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string SolutionDescription { get; set; }

        [Display(Name = "Naam monteur")]
        [MaxLength(250, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string MechanicName { get; set; }

        [Display(Name = "Naam bewoner")]
        [MaxLength(250, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string OccupantName { get; set; }

        [Display(Name = "Handtekening monteur")]
        public string MechanicSignature { get; set; }

        [Display(Name = "Handtekening bewoner")]
        public string OccupantSignature { get; set; }

        [Display(Name = "Factureren naar klant")]
        public bool ChargeCustomer { get; set; }

        [Display(Name = "Urgent")]
        public bool Urgent { get; set; }

        [Display(Name = "Valt binnen contract")]
        public bool FallsWithinContract { get; set; }

        [Display(Name = "Staat ingepland voor")]
        public DateTime? PlannedDateTime { get; set; }

        [Display(Name = "Vraag reactie van Eteck")]
        public bool RequestComment { get; set; }

        [StringLength(250)]
        public string ExternalReference { get; set; }

        [Display(Name = "Interne opmerking")]
        [MaxLength(1000, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string InternalComment { get; set; }
        #endregion

        #region Single References
        public virtual ServiceTicket ServiceTicket { get; set; }

        public virtual ServiceInvoiceLine ServiceInvoiceLine { get; set; }

        public virtual WorkOrderStatus Status { get; set; }

        public virtual ServiceInvoiceLineInput MechanicInput { get; set; }

        public virtual SolutionCategory SolutionCategory { get; set; }
        #endregion

        #region List References
        [Display(Name = "Bestanden")]
        public ICollection<File> Files { get; set; }
        #endregion

        #region Methods
        private Services.EmailService es = new Services.EmailService();
        private static readonly string hostServer = ConfigurationManager.AppSettings["Hostserver"];
        public async System.Threading.Tasks.Task<Result> SendMailAsync(PmsEteckContext db)
        {
            ServiceTicket serviceTicket = db.ServiceTickets.Find(ServiceTicketID);
            Address address = db.Addresses.Find(serviceTicket.AddressID);
            ProjectBase project = db.ProjectBases.Find(serviceTicket.ProjectID);
            string subject = string.Format("{0} - {1} - {2} - {3}", 
                serviceTicket.TicketCodeString,
                address.sStreetName + " " + address.iNumber + (!string.IsNullOrWhiteSpace(address.sNumberAddition) ? "-" + address.sNumberAddition : string.Empty),
                address.sCity,
                project.sProjectDescription);
            if (serviceTicket.Urgent)
                subject = string.Format("URGENT - {0}", subject);
            TicketEmail email = new TicketEmail
            {
                Receiver = new List<string>(),
                Subject = subject,
                Body = "<b><i><span style='color:#249346;'>Dit is een automatisch gegenereerd bericht. Uw antwoord op dit bericht wordt </span></b> <b style='color: rgb(36, 14, 70); font-size:150%'>niet</b><span style='color:#249346;'> <b>gelezen. Mailt u daarvoor naar</span> <a href='mailto:support@eteck.nl'>support@eteck.nl</a></b></i><br><br>" +
                "Beste heer/ mevrouw,<br/><br/>" +
                "Er is een" + (serviceTicket.Urgent ? " urgente " : " ") + "melding geplaatst namens Eteck Energie Techniek B.V voor project <b>" + project.sProjectDescription + "</b>. via <a href='" + hostServer + "/WorkOrders/Edit/" + WorkOrderID + "'>deze link</a> vindt u de werkbon. Deze werkbon geldt als officiële opdrachtbon.<br/>" +
                "Klik <a href='" + hostServer + "/WorkOrders/Schedule/" + WorkOrderID + "'>hier</a> als u de werkzaamheden heeft ingepland.<br/><br/>" +
                "Het werkbonnummer is: " + WorkOrderCodeString + ".<br/>" +
                "Het bijbehorende storingsnummer is: " + serviceTicket.TicketCodeString + ".<br/><br/>" +
                "<span style='color:#249346;'><i><b>Digitaal afmelden van de werkbon graag eveneens d.m.v. <a style='color: rgb(36, 14, 70)' href='" + hostServer+"/WorkOrders/Edit/" + WorkOrderID + "'>deze link</a></b></i></span>.</br>" +
                "<i><b>Heeft u vragen of hulp nodig?</b></i> Of wilt u deze werkbon en melding niet digitaal <i><b>afmelden, belt</b></i> u dan naar <a href='tel:085-0218 099'>085-0218 099</a>. Of <i><b>mailt</b></i> u naar <a href='mailto:support@eteck.nl'>support@eteck.nl</a>.</br></br>" +
                "Met vriendelijke groet,",
                Urgent = Urgent,
                From = "WorkOrder",
                BCC = new List<string> { "serviceenonderhoud@eteck.nl" },
                MailConfigID = db.MailConfigs.FirstOrDefault(f => f.Description == "Storingen").MailConfigID
            };

            email.Receiver.Add(db.MaintenanceContacts.Find(serviceTicket.MaintenanceContactID).sEmail);
            Result result = await es.SendMailAsync(email);
            if(result.Succeeded)
                Status = db.WorkOrderStatuses.First(f => f.StatusCode == 201);
            return result;
        }
        #endregion
    }
}