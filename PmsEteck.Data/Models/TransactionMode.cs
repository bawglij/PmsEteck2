using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("TransactionModes")]
    public class TransactionMode
    {
        [Key]
        public int iTransactionModeID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [StringLength(20, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sCodePerson { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [StringLength(20, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sCodeBusiness { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [StringLength(20, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sCodeNonIncasso { get; set; }

        [StringLength(80, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sDescription { get; set; }
    }
}