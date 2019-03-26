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
    [Table("ServiceTickets", Schema = "service")]
    public class ServiceTicket
    {
        #region Keys
        [Key]
        [Display(Name = "Servicemelding")]
        //Guid
        public Guid ServiceTicketID { get; set; }

        [Display(Name = "Gebruiker")]
        [ForeignKey("User")]
        public string UserID { get; set; }

        [Display(Name = "Status")]
        [ForeignKey("Status")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public int StatusID { get; set; }

        [Display(Name = "Project")]
        [ForeignKey("Project")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public int ProjectID { get; set; }

        [Display(Name = "Onderhoudspartij")]
        [ForeignKey("MaintenanceContact")]
        public int? MaintenanceContactID { get; set; }

        [Display(Name = "Adres")]
        [ForeignKey("Address")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public int AddressID { get; set; }

        [Display(Name = "Bewoner")]
        [ForeignKey("Occupant")]
        public int? OccupantID { get; set; }

        [Display(Name = "Debiteur")]
        [ForeignKey("Debtor")]
        public int? DebtorID { get; set; }

        [Display(Name = "Melding type")]
        public int? ServiceTicketTypeID { get; set; }
        #endregion

        #region Properties
        [Display(Name = "Melding nummer")]
        public int TicketCode { get; set; }
        public string TicketCodeString => "S" + TicketCode.ToString("000000000");

        [Display(Name = "Aangemaakt")]
        [DisplayFormat(DataFormatString = "{0:d-M-yyyy}")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Afgerond")]
        [DisplayFormat(DataFormatString = "{0:d-M-yyyy}")]
        public DateTime? FinishedDateTime { get; set; }

        [Display(Name = "Titel")]
        [Required(ErrorMessage = "{0} is verplicht")]
        [MaxLength(500, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string Title { get; set; }

        [Display(Name = "Beschrijving")]
        [Required(ErrorMessage = "{0} is verplicht")]
        [MaxLength(1000, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string Description { get; set; }

        [Display(Name = "Dringend")]
        public bool Urgent { get; set; }

        [Display(Name = "Verwachte bedrag")]
        public decimal? ExpectedAmount { get; set; }

        [Display(Name = "Onderhoudspartij ingelicht")]
        public bool MaintenanceContactInformed { get; set; }
        #endregion

        #region Single References
        public virtual ServiceTicketType Type { get; set; }

        public virtual Ticket Ticket { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ServiceTicketStatus Status { get; set; }

        public virtual ProjectInfo Project { get; set; }

        public virtual MaintenanceContact MaintenanceContact { get; set; }

        public virtual Address Address { get; set; }

        public virtual Occupant Occupant { get; set; }

        public virtual Debtor Debtor { get; set; }
        #endregion

        #region List References
        [Display(Name = "Werkbonnen")]
        public ICollection<WorkOrder> WorkOrders { get; set; }

        [Display(Name = "Bestanden")]
        public ICollection<File> Documents { get; set; }

        [Display(Name = "Contacthistorie")]
        public List<ServiceMessage> Messages { get; set; }
        #endregion

        #region Methods
        private Services.EmailService es = new Services.EmailService();
        private static string hostServer =ConfigurationManager.AppSettings["Hostserver"];
        public async System.Threading.Tasks.Task<Result> SendMessageNotificationAsync(PmsEteckContext db, string message)
        {
            string subject = string.Format("Interne notitie voor {0}", TicketCodeString);
            TicketEmail email = new TicketEmail
            {
                Receiver = new List<string>(),
                Subject = subject,
                Body = string.Format("Er is een 'interne notitie' toegevoegd aan servicemelding <a href='" + hostServer + "/ServiceTickets/Details/" + ServiceTicketID + "'>{0}</a> <hr /> {1}", TicketCodeString, message),
                Urgent = Urgent,
                From = "ServiceTicket",
                MailConfigID = db.MailConfigs.FirstOrDefault(f => f.Description == "Storingen").MailConfigID
            };

            email.Receiver.Add(ConfigurationManager.AppSettings["SNOemail"]);
            return await es.SendMailAsync(email);
        }

        public void SetStatus(PmsEteckContext db)
        {
            int newStatusCode = int.MinValue;
            switch (WorkOrders.Min(m => m.Status.StatusCode))
            {
                case 100:
                    newStatusCode = Status.StatusCode == 400
                        ? 101
                        : 100;
                    break;
                case 201:
                    newStatusCode = 201;
                    break;
                case 202:
                    newStatusCode = 202;
                    break;
                case 300:
                    newStatusCode = 300;
                    break;
                case 400:
                    newStatusCode = 400;
                    FinishedDateTime = DateTime.UtcNow;
                    break;
            }

            if (Status.StatusCode != newStatusCode)
                Status = db.ServiceTicketStatuses.FirstOrDefault(f => f.StatusCode == newStatusCode);
        }
        #endregion
    }
}