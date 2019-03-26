using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("ContactTypes")]
    public class ContactType
    {
        [Key]
        public int iContactTypeKey { get; set; }

        [Required]
        [MaxLength(100)]
        public string sContactTypeName { get; set; }

    }
}