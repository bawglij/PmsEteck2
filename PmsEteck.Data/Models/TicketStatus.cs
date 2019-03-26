using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("TicketStatuses", Schema = "service")]
    public class TicketStatus
    {
        [Key]
        public int iTicketStatusID { get; set; }

        [Required]
        [MaxLength(50)]
        public string sName { get; set; }

        public ICollection<Ticket> Ticket { get; set; }

        public ICollection<ResponseConcept> ResponseConcepts { get; set; }
    }
}