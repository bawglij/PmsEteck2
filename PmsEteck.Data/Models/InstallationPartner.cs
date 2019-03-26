using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("InstallationPartners")]
    public class InstallationPartner
    {
        [Key]
        [Display(Name = "Installatiepartner")]
        public int InstallationPartnerID { get; set; }

        #region Properties
        [Display(Name = "Naam")]
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }
        #endregion

        #region List References
        public ICollection<Opportunity> Opportunities { get; set; }
        #endregion
    }
}