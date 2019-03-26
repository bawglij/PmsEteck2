using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "Events", Schema = "meter")]
    public class Event
    {
        [Key]
        public int iEventKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(50, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Omschrijving")]
        public string sEventDescription { get; set; }
    }
}