using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("EmailAddresses", Schema = "service")]
    public class EmailAddress
    {
        [Key]
        public int iEmailAddressID { get; set; }

        public int iResponseID { get; set; }

        [Required]
        [MaxLength(100)]
        public string sEmailAddress { get; set; }

        public virtual Response Response { get; set; }
    }
}