using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Responses", Schema = "service")]
    public class Response
    {
        [Key]
        public int iResponseID { get; set; }

        public int iTicketID { get; set; }

        [ForeignKey("ApplicationUser")]
        public string sUserID { get; set; }

        public int iResponseTypeID { get; set; }

        public DateTime dtCreateDateTime { get; set; }

        [Required]
        public string sMessage { get; set; }

        public bool bIncoming { get; set; }

        [MaxLength(150)]
        public string sFromEmail { get; set; }

        [MaxLength(150)]
        public string sFromName { get; set; }

        [MaxLength(150)]
        public string sToEmail { get; set; }

        [MaxLength(150)]
        public string sToName { get; set; }

        public string BCCList { get; set; }

        public virtual Ticket Ticket { get; set; }
        [NotMapped]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ResponseType ResponseType { get; set; }

        public ICollection<EmailAddress> EmailAddresses { get; set; }

        public ICollection<MailAttachment> Attachments { get; set; }
    }
}