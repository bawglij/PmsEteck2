using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("DebtorFiles", Schema = "invoice")]
    public class DebtorFile
    {
        [Key]
        public int iDebtorFileID { get; set; }

        [Required]
        [Display(Name = "Naam")]
        [MaxLength(150, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sDisplayName { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sFileName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sContentType { get; set; }

        [Required]
        public byte[] bByteArray { get; set; }

        public int iDebtorID { get; set; }

        public Debtor Debtor { get; set; }
    }
}