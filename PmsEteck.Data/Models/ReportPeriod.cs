using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "ReportPeriods", Schema = "meter")]
    public class ReportPeriod
    {
        [Key]
        public int iReportPeriodKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Jaar")]
        public int iYear { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Periode")]
        public int iPeriod { get; set; }

        public DateTime dtPeriodDate {
            get
            {
                return new DateTime(iYear, iPeriod, 1);
            }
            set
            {
                new DateTime(iYear, iPeriod, 1);
            }
        }

        [Display(Name = "Geblokkeerd?")]

        public bool bBlocked { get; set; }

        [Display(Name = "Rapportages")]
        public ICollection<Report> Reports { get; set; }

    }
}