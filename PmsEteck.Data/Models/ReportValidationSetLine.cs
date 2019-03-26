using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    public class ReportValidationSetLine
    {
        [Key]
        public int ReportValidationSetLineID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [ForeignKey("ReportValidationSet")]
        [Display(Name = "Validatieprofiel")]
        public int ReportValidationSetID { get; set; }

        [ForeignKey("Rubric")]
        [Display(Name = "Rubriek")]
        public int? RubricID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Omschrijving")]
        [StringLength(250, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string ReportValidationSetLineName { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Operator")]
        public int ValueOperatorID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Waarde")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Formaat")]
        [StringLength(100, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string FormatString { get; set; }

        #region Single References

        [Display(Name = "Validatieprofiel")]
        public virtual ReportValidationSet ReportValidationSet { get; set; }

        [Display(Name = "Rubriek")]
        public virtual Rubric Rubric { get; set; }

        [Display(Name = "Operator")]
        public virtual ValueOperator ValueOperator { get; set; }
        #endregion
    }
}