using System.ComponentModel.DataAnnotations;

namespace Esight.Models
{
    public class Meter
    {
        public int MeterID { get; set; }
        public int ParentID { get; set; }
        public int ParentTypeID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(15)]
        public string MeterCode { get; set; }

        public string SiteMeterCode { get; set; }

        [StringLength(250)]
        public string DataImportCode { get; set; }

        [StringLength(15)]
        public string SerialNumber { get; set; }

        public string MeterPrefix { get; set; }

        [StringLength(15)]
        public string MeterNumber { get; set; }

        public string MeterType { get; set; }

        public bool AMR { get; set; }

        public string MeterReadFrequency { get; set; }

        public string ContractNumber { get; set; }

        public int MeterTypeID { get; set; }

        public bool Enabled { get; set; }

        public int TypeID { get; set; }
    }
}
