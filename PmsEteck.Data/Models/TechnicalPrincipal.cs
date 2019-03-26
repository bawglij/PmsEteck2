using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("TechnicalPrincipals")]
    public class TechnicalPrincipal
    {
        [Key]
        public int iTechnicalPrincipalKey { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name="Technisch principe")]
        public string sTechnicalPrincipal { get; set; }

        [Required]
        [MaxLength(150)]
        public string EnglishDescription { get; set; }

        [Display(Name = "Betreft gaslevering")]
        public bool bIsGas { get; set; }

        public bool bActive { get; set; }

        public ICollection<Opportunity> Opportunities { get; set; }
    }
}