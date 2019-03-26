using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("ResponseConcepts", Schema = "service")]
    public class ResponseConcept
    {
        [Key]
        public int iTicketID { get; set; }
        
        public DateTime dtDateTimeLastEdited { get; set; }

        [ForeignKey("User")]
        public string sUserID { get; set; }

        public int iResponseTypeID { get; set; }

        public int iTicketStatusID { get; set; }

        [MaxLength(150)]
        public string sToEmail { get; set; }

        public string CCList { get; set; }

        public string BCCList { get; set; }

        public string sMessage { get; set; }

        public string sSolution { get; set; }

        public bool MailHistory { get; set; }

        public bool PhoneHistory { get; set; }

        public bool NoteHistory { get; set; }

        public bool SolutionHistory { get; set; }

        public bool PortalHistory { get; set; }

        public Ticket Ticket { get; set; }
        [NotMapped]
        public ApplicationUser User { get; set; }

        public ResponseType ResponseType { get; set; }

        public TicketStatus TicketStatus { get; set; }

    }
}