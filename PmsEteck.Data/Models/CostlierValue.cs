using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("CostlierValues", Schema = "meter")]
    public class CostlierValue
    {
        [Key]
        public int CostlierValueID { get; set; }

        [ForeignKey("Address")]
        public int AddressID { get; set; }

        public DateTime DateTime { get; set; }

        [Display(Name = "Totaal verbruik")]
        public decimal TotalConsumption { get; set; }

        [Display(Name = "Distributieverlies")]
        public decimal LossOfDistribution { get; set; }

        [Display(Name = "Hoofdelijke omslag")]
        public decimal PollTax { get; set; }

        [Display(Name = "Totaal kostendelers")]
        public decimal TotalCostlier { get; set; }

        public Address Address { get; set; }
    }
}
