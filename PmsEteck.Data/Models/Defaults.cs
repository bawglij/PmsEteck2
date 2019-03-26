using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "Defaults")]
    public class Default
    {
        [Key]
        public int iDefaultID { get; set;  }

        [MaxLength(150)]
        public string sName { get; set; }

        public decimal dValue { get; set; }

        public int? Year { get; set; }
    }
}