using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("CalcRules")]
    public class CalcRule
    {
        [Key, ForeignKey("ProjectInfo")]
        public int iProjectKey { get; set; }

        [Display(Name = "Inkoop energie tbv warmte %")]
        public decimal dEnergyCostsHeating { get; set; }

        [Display(Name = "Inkoop energie tbv koeling %")]
        public decimal dEnergyCostsCooling { get; set; }

        [Display(Name = "Index inkoop energie")]
        public decimal dIndexEnergyCosts { get; set; }

        [Display(Name = "Index inkoop overig")]
        public decimal dIndexOtherCosts { get; set; }

        [Display(Name = "Index verkoop energie")]
        public decimal dIndexEnergySales { get; set; }

        [Display(Name = "Index vastrecht")]
        public decimal dIndexStandingCharge { get; set; }

        public ProjectInfo ProjectInfo { get; set; }

        public CalcRule defaultCalcRule(int iProjectKey)
        {
            var calcRule = new CalcRule()
            {
                iProjectKey = iProjectKey,
                dEnergyCostsCooling = 2,
                dEnergyCostsHeating = 2,
                dIndexEnergyCosts = 2,
                dIndexEnergySales = 2,
                dIndexOtherCosts = 2,
                dIndexStandingCharge = 2
            };

            return calcRule;
        }
    }  
}