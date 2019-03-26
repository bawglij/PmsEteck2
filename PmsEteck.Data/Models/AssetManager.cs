using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("AssetManager")]
    public class AssetManager
    {
        [Key]
        public int iAssetManagerKey { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name="Voornaam")]
        public string sFirstName { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name="Achternaam")]
        public string sLastName { get; set; }

        public bool bActive { get; set; }

        [Display(Name="Naam")]
        public string Fullname
        {
            get { return sFirstName + " " + sLastName; }
        }

    }
}