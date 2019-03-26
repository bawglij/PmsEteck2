using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("AverageMonthConsumptions", Schema = "meter")]
    public class AverageMonthConsumption
    {
        public int AverageMonthConsumptionID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        [ForeignKey("CounterType")]
        public int CounterTypeID { get; set; }

        [ForeignKey("Unit")]
        public int UnitID { get; set; }

        [ForeignKey("ProjectInfo")]
        public int ProjectID { get; set; }

        public decimal Consumption { get; set; }

        public virtual CounterType CounterType { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ProjectInfo ProjectInfo { get; set; }

    }
}