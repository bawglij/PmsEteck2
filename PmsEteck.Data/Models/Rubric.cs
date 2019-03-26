using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "Rubrics", Schema = "meter")]
    public class Rubric
    {
        [Key]
        public int iRubricKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(150, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Rubriek")]
        public string sRubricDescription { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Regelnummer")]
        public int iRowNo { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Start regelnummer")]
        public int iRowNoStart { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Einde regelnummer")]
        public int iRowNoEnd { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Totaal")]
        public bool bTotal { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Reportingcode")]
        public int iReportingCode { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Rubriekgroep")]
        public int iRubricGroupKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Rekeningnummer")]
        [DisplayFormat(DataFormatString = "{0:D6}", ApplyFormatInEditMode = true)]
        [Range(0, 999999, ErrorMessage = "{0} moet een waarde bevatten tussen {1} en {2}")]
        public int? iAccountNumber { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [Display(Name = "Tegenrekeningnummer")]
        [DisplayFormat(DataFormatString = "{0:D6}", ApplyFormatInEditMode = true)]
        [Range(0, 999999, ErrorMessage = "{0} moet een waarde bevatten tussen {1} en {2}")]
        public int? iCounterAccountNumber { get; set; }

        [Display(Name = "Rubriektype")]
        public int? iRubricTypeKey { get; set; }

        [Display(Name = "Rubriekgroep")]
        public virtual RubricGroup RubricGroup { get; set; }

        [Display(Name = "Rubriektype")]
        public virtual RubricType RubricType { get; set; }

        [Display(Name = "Tariefkaartregels")]
        public List<RateCardRow> RateCardRows { get; set; }
    }
}