using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("TicketLogs", Schema = "service")]
    public class TicketLog
    {
        [Key]
        public int iTicketLogID { get; set; }

        public int iTicketID { get; set; }

        [ForeignKey("ApplicationUser")]
        public string sUserID { get; set; }

        [Required]
        [MaxLength(500)]
        public string sActivity { get; set; }

        public DateTime dtTimestamp { get; set; }

        public virtual Ticket Ticket { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}