using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Tickets", Schema = "service")]
    public class Ticket
    {
        //int
        [Key]
        public int iTicketID { get; set; }

        [ForeignKey("ApplicationUser")]
        public string sUserID { get; set; }

        public int? iOccupantID { get; set; }

        [ForeignKey("Project")]
        public int? iProjectID { get; set; }

        public int iTicketStatusID { get; set; }

        public int iTicketTypeID { get; set; }

        public int? iDebtorID { get; set; }

        [ForeignKey("Address")]
        public int? iAddressID { get; set; }

        [Required]
        [MaxLength(500)]
        public string sTitle { get; set; }

        [Required]
        public string sMessage { get; set; }

        public bool bAssigned { get; set; }

        public bool bFinished { get; set; }

        public DateTime dtCreateDateTime { get; set; }

        public DateTime? dtFinishedDateTime { get; set; }

        public string sSolution { get; set; }

        [MaxLength(100)]
        public string sEmail { get; set; }

        [MaxLength(30, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Telefoonnummer")]
        [DataType(DataType.PhoneNumber)]
        public string sPhoneNumber { get; set; }

        public bool Suppressed { get; set; }

        [Display(Name = "Onderdrukt tot")]
        public DateTime? SuppressedUntil { get; set; }

        private int? mailConfigID;
        [Display(Name = "Mail Configuratie")]
        [ForeignKey("MailConfig")]
        public int? MailConfigID
        {
            get => mailConfigID ?? 1;
            set { mailConfigID = value; }
        }

        public ICollection<TicketLog> TicketLogs { get; set; }

        public ICollection<Response> Responses { get; set; }

        [Display(Name = "Labels")]
        //public virtual List<Label> Labels { get; set; }
        public virtual List<TicketLabel> Labels { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ServiceTicket ServiceTicket { get; set; }

        public virtual Occupant Occupant { get; set; }

        public virtual ProjectInfo Project { get; set; }

        public virtual TicketStatus TicketStatus { get; set; }

        public virtual TicketType TicketType { get; set; }

        public virtual Debtor Debtor { get; set; }

        public virtual Address Address { get; set; }

        public virtual ResponseConcept ResponseConcept { get; set; }

        public virtual MailConfig MailConfig { get; set; }
    }
}