using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("CalculationType")]
    public class CalculationType
    {
        [Key]
        public int iCalculationTypeID { get; set; }

        [MaxLength(50)]
        public string sName { get; set; }

        ICollection<ProjectInfo> Projects { get; set; }

        ICollection<ReportRow> ReportRows { get; set; }
    }
}
