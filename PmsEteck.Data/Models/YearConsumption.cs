using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("CounterYearConsumptions", Schema = "meter")]
    public class CounterYearConsumption
    {
        [Key]
        public int CounterYearConsumptionID { get; set; }

        [ForeignKey("Counter")]
        public int CounterID { get; set; }

        public int Year { get; set; }

        public decimal Consumption { get; set; }

        public Counter Counter { get; set; }
    }
}