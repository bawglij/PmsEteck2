using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Months")]
    public class Month
    {
        [Key]
        public int iMonthKey { get; set; }
        
        [Required]
        [MaxLength(50)]
        [Display(Name ="Maand")]
        public string sMonthName { get; set; }
    }
}