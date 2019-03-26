using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "MailConfigs", Schema = "pms")]
    public class MailConfig
    {
        public int MailConfigID { get; set; }

        [Display(Name = "Beschrijving")]
        public string Description { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Mailbox { get; set; }

        public bool ReadMailbox { get; set; }

        public string ReadFolder { get; set; }

        public string ArchiveFolder { get; set; }

        public bool ArchiveMail { get; set; }

        [MaxLength(250)]
        public string ServiceUrl { get; set; }
        
        public ICollection<ProjectInfo> Projects { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}