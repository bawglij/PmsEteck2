using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("InstallationPartnerProcesses")]
    public class InstallationPartnerProcess
    {
        [Key]
        [Display(Name = "Installatiepartnerproces")]
        public int InstallationPartnerProcessID { get; set; }

        #region Properties
        [Display(Name = "Beschrijving")]
        [MaxLength(250)]
        [Required]
        public string Description { get; set; }

        [MaxLength(250)]
        [Required]
        public string EnglishDescription { get; set; }


        public int Order { get; set; }
        #endregion

        #region List References
        public ICollection<Opportunity> Opportunities { get; set; }
        #endregion
    }
}