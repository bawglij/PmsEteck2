using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("EnergyConcepts")]
    public class EnergyConcept
    {
        [Key]
        [Display(Name = "Type Energie Concept")]
        public int EnergyConceptID { get; set; }

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