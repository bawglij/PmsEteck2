using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("ResponseTexts", Schema = "service")]
    public class ResponseText
    {
        [Key]
        public int iResponseTextID { get; set; }

        public int iTicketTypeID { get; set; }

        [Required]
        [MaxLength(50)]
        public string sTitle { get; set; }

        [Required]
        public string sMessage { get; set; }

        public virtual TicketType TicketType { get; set; }
    }
}