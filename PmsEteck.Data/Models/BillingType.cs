using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("BillingTypes", Schema = "invoice")]
    public class BillingType
    {
        [Key]
        public int BillingTypeID { get; set; }

        [Display(Name = "Omschrijving")]
        [StringLength(150, ErrorMessage = "{0} mag maximaal {1} karakters bevatten.")]
        public string Description { get; set; }

        [Display(Name = "Sortering")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public int Order { get; set; }

        [Display(Name = "Actief")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public bool Active { get; set; }
    }
}
