using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    public class ReportValidationSet
    {
        [Key]
        public int ReportValidationSetID { get; set; }

        [Display(Name = "Masterprofiel")]
        public bool MasterSet { get; set; }

        [Display(Name = "Profielnaam")]
        [StringLength(150, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string ReportValidationSetName { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Aangemaakt door")]
        [ForeignKey("CreateByUser")]
        public string CreateByUserID { get; set; }

        [Display(Name = "Gewijzigd door")]
        [ForeignKey("ChangeByUser")]
        public string ChangeByUserID { get; set; }

        [Display(Name = "Aangemaakt op")]
        public DateTime CreateDateTime { get; set; }

        [Display(Name = "Gewijzigd op")]
        public DateTime? ChangeDateTime { get; set; }

        [Display(Name = "Actief")]
        public bool Active { get; set; }

        #region Single References

        [Display(Name = "Aangemaakt door")]
        public virtual ApplicationUser CreateByUser { get; set; }

        [Display(Name = "Gewijzigd door")]
        public virtual ApplicationUser ChangeByUser { get; set; }

        #endregion

        #region List References
        
        [Display(Name = "Regels")]
        public List<ReportValidationSetLine> ReportValidationSetLines { get; set; }

        [Display(Name = "Gekoppelde projecten")]
        public List<ProjectInfo> Projects { get; set; }

        [Display(Name = "Historie gekoppelde projecten")]
        public List<ProjectReportValidationSetLog> ProjectLog { get; set; }

        [Display(Name = "Rapportages")]
        public List<Report> Reports { get; set; }

        #endregion
    }
}