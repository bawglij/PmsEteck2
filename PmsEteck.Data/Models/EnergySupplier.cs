using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "EnergySuppliers")]
    public class EnergySupplier
    {
        [Key]
        public int iEnergySupplierID { get; set; }

        [Display(Name="Energieleverancier")]
        [MaxLength(100)]
        public string sName { get; set; }

        public bool bActive { get; set; } = true;

        [Display(Name="Verbruiksmeters")]
        public List<ConsumptionMeter> ConsumptionMeters { get; set; }
    }
}