using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "ReportRows", Schema = "meter")]
    public class ReportRow
    {
        #region Constructor
        private PmsEteckContext db = new PmsEteckContext();
        #endregion

        #region Properties

        [Key]
        public int iReportRowKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Rapportage")]
        public int iReportKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Rubriek")]
        public int iRubricKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Origineel bedrag")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dOriginalAmount { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Mutatiebedrag")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dMutationAmount { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Budgetbedrag")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dBudgetAmount { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Cumulatief berekend bedrag")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dCumCalculateAmount { get; set; }
        
        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Cumulatief gerapporteerd bedrag")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dCumReportAmount { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Cumulatief budgetbedrag")]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public decimal dCumBudgetAmount { get; set; }

        [ForeignKey("CalculationType")]
        public int? CalculationTypeID { get; set; }

        [Display(Name = "Rapportage")]
        public virtual Report Report { get; set; }

        [Display(Name = "Rubriek")]
        public virtual Rubric Rubric { get; set; }

        public virtual CalculationType CalculationType { get; set; }

        #endregion
    }
}