using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    public class ProjectReportValidationSetLog
    {
        [Key]
        public int ProjectReportValidationSetLogID { get; set; }

        [Display(Name = "Project")]
        [Required(ErrorMessage = "{0} is verplicht")]
        [ForeignKey("Project")]
        public int ProjectID { get; set; }

        [Display(Name = "Validatieprofiel")]
        [Required(ErrorMessage = "{0} is verplicht")]
        [ForeignKey("ReportValidationSet")]
        public int ReportValidationSetID { get; set; }

        [Display(Name = "Startdatum")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Einddatum")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Aangemaakt door")]
        [Required(ErrorMessage = "{0} is verplicht")]
        [ForeignKey("CreateByUser")]
        public string CreateByUserID { get; set; }

        [Display(Name = "Gewijzigd door")]
        [ForeignKey("ChangeByUser")]
        public string ChangeByUserID { get; set; }

        [Display(Name = "Aangemaakt op")]
        public DateTime CreateDateTime { get; set; }

        [Display(Name = "Gewijzigd op")]
        public DateTime? ChangeDateTime { get; set; }

        #region Single References

        [Display(Name = "Project")]
        public virtual ProjectInfo Project { get; set; }

        [Display(Name = "Validatieprofiel")]
        public virtual ReportValidationSet ReportValidationSet { get; set; }

        [Display(Name = "Aangemaakt door")]
        [NotMapped]
        public virtual ApplicationUser CreateByUser { get; set; }

        [Display(Name = "Gewijzigd door")]
        [NotMapped]
        public virtual ApplicationUser ChangeByUser { get; set; }
        #endregion
    }
}