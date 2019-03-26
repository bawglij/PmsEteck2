using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("VatConditions", Schema = "meter")]
    public class VatCondition
    {
        #region Keys
        [Key]
        public int VatConditionID { get; set; }
        #endregion

        [Required]
        [MaxLength(50)]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Beschrijving")]
        public string Description { get; set; }

        #region List References
        [Display(Name = "Tariefkaartregels")]
        IEnumerable<RateCardRow> RateCardRows { get; set; }
        #endregion
    }
}