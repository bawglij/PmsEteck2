using System.Collections.ObjectModel;

namespace Brunata.Models
{
    public class Meter
    {
        public Meter()
        {
        }

        public int ID { get; set; }
        public string SerialNumber { get; set; }
        public int? MeterTypeNumber { get; set; }
        //public string MeterType { get; set; }
        //public string UnitTypeNmber { get; set; }
        //public string UnitType { get; set; }
        //public string CountingMethodCode { get; set; }
        //public DateTime? MountingDate { get; set; }
        //public DateTime? DismountedDate { get; set; }
        public string UsageType { get; set; }
        //public string Usage { get; set; }
        public int? UnitReduction { get; set; }
        //public int? SequenceNumber { get; set; }
        public string Category { get; set; }
        public Collection<Reading> Readings { get; set; }
    }
}