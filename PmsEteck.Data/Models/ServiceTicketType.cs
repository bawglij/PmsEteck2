using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("ServiceTicketTypes", Schema = "service")]
    public class ServiceTicketType
    {
        [Key]
        public int ServiceTicketTypeID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<ServiceTicket> ServiceTickets { get; set; }
    }
}