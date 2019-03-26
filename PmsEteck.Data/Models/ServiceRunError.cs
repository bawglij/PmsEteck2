using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "ServiceRunErrors", Schema = "meter")]
    public class ServiceRunError
    {
        [Key]
        public int iServiceRunErrorKey { get; set; }

        [Required]
        public int iServiceRunKey { get; set; }
        
        [MaxLength(100)]
        public string sConsumptionMeterNumber { get; set; }

        [MaxLength(100)]
        public string sProjectNumber { get; set; }

        public int iStatusCode { get; set; }

        [Required]
        [MaxLength(1000)]
        public string sErrorMessage { get; set; }
        
        public DateTime? dtEffectiveDateTime { get; set; }

        public ServiceRun ServiceRun { get; set; }

    }
}