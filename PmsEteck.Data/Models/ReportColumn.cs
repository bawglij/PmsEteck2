using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class ReportColumn
    {
        [Key]
        public int iReportColumnKey { get; set; }

        [Display(Name = "Rapportage-onderdeel")]
        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} karakter bevatten")]
        public string sReportingColumnName { get; set; }
    }
}