using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "Frequencies", Schema = "meter")]
    public class Frequency
    {
        [Key]
        public int iFrequencyKey { get; set; }

        [MaxLength(25)]
        [Display(Name = "Frequentie")]
        public string sFrequencyDescription { get; set; }

    }
}