using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("DebtorTypes", Schema = "invoice")]
    public class DebtorType
    {
        [Key]
        public int iDebtorTypeID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Omschrijving")]
        [MaxLength(20, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sName { get; set; }
    }
}