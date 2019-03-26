//VLD
namespace PmsEteck.Data.Models
{
    public class TicketLabel
    {
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public int LabelId { get; set; }
        public Label Label { get; set; }

    }
}
