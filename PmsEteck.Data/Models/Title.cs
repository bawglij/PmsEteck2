using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Titles", Schema = "invoice")]
    public class Title
    {
        [Key]
        public int iTitleID { get; set; }

        [Display(Name = "Aanhef")]
        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sTitleName { get; set; }

        [Display(Name = "Bewoners")]
        public ICollection<Occupant> Occupants { get; set; }
    }
}