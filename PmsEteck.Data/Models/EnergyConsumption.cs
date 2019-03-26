using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("EnergyConsumption")]
    public class EnergyConsumption
    {
        [Key]
        public int iEnergyConsumptionKey { get; set; }

        public int iProjectKey { get; set; }

        [Required]
        public Guid gGroupKey { get; set; }

        [Display(Name ="Periodiciteit")]
        public int iPeriodKey { get; set; }

        [Display(Name ="Periode")]
        public int iPeriod { get; set; }

        [Display(Name ="Kwartaal")]
        public int iQuarter { get; set; }

        [Display(Name ="Jaar")]
        public int iYear { get; set; }

        [Display(Name ="Warmte (ruimte verwarming)")]
        public decimal dHeatingRoomsGJ { get; set; }

        [Display(Name = "Warmte (tapwaterbereiding)")]
        public decimal  dHeatingHotWaterGJ { get; set; }

        [Display(Name = "Koude")]
        public decimal dCoolingGJ { get; set; }

        [Display(Name ="Tapwater")]
        public decimal dWaterM3 { get; set; }

        [Display(Name ="Electra")]
        public decimal dElectricityKwh { get; set; }

        [Display(Name ="Gas")]
        public decimal dGasM3 { get; set; }

        [Display(Name ="Warmte")]
        public decimal dHeatingGJ { get; set; }

        [Display(Name ="Hout/overig")]
        public decimal dOthersT { get; set; }

        public Period Period { get; set; }


    }
}