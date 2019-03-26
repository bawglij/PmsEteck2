using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Contacts")]
    public class Contact
    {
        [Key]
        public int iContactKey { get; set; }

        public int iProjectKey { get; set; }

        public int iContactTypeKey { get; set; }

        [Display(Name ="Organisatie")]
        [MaxLength(50)]
        public string sOrganisation { get; set; }

        [Display(Name ="Functie")]
        [MaxLength(50)]
        public string sTitle { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name ="Naam")]
        public string sContactName { get; set; }

        [Display(Name ="Email")]
        [EmailAddress]
        [MaxLength(100)]
        public string sEmail { get; set; }

        [Display(Name ="Telefoon")]
        [MaxLength(50)]
        public string sTelephone { get; set; }

        [Display(Name ="Omschrijving")]
        [MaxLength(250)]
        public string sDescription { get; set; }
 

        public virtual ProjectInfo ProjectInfo { get; set; }

        public virtual ContactType ContactType { get; set; }

    }
}