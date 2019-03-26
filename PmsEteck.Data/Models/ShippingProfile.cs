using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("ShippingProfiles", Schema = "invoice")]
    public class ShippingProfile
    {
        [Key]
        public int iShippingProfileID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Code")]
        [MaxLength(20, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sCode { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Verzendprofiel")]
        [MaxLength(20, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sDescription { get; set; }

    }
}