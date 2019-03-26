using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    public enum PatternType
    {
        EigenIndex = 0,
        GraaddagenIndex = 1,
        KoeldagenIndex = 2
    }

    [Table("SeasonalPatterns")]
    public class SeasonalPattern : BaseModel
    {
        [Required(ErrorMessage = "{0} verplicht invoeren.")]
        [StringLength(250, ErrorMessage = "{0} mag maximaal {1} tekens lang zijn.")]
        [Display(Name = "Curvenaam")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} verplicht invoeren.")]
        [Display(Name = "Type curve")]
        public PatternType PatternType { get; set; }

        [Display(Name = "% vaste voet")]
        public decimal PercentFootHold { get; set; }

        [Display(Name = "Berkekende curve?")]
        public bool Calculated { get; set; }

        [Display(Name = "Waarde per periode")]
        public ICollection<PeriodPercentage> PeriodPercentages { get; set; }
    }
}
