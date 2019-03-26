using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "ExchangeForms", Schema = "meter")]
    public class ExchangeForm
    {
        [Key]
        public int iExchangeFormKey { get; set; }

        [StringLength(255)]
        public string sFileName { get; set; }

        [StringLength(100)]
        public string sContentType { get; set; }

        public byte[] bContent { get; set; }

        public FileType FileType { get; set; }

    }
}