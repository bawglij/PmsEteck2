using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("TicketTypes", Schema = "service")]
    public class TicketType
    {
        [Key]
        public int iTicketTypeID { get; set; }

        [Required]
        [MaxLength(50)]
        public string sName { get; set; }

        public ICollection<Ticket> Ticket { get; set; }

        public ICollection<ResponseText> ResponseTexts { get; set; }
    }
}