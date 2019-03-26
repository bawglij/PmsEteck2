using System;
using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class Log
    {
        [Key]
        public Guid LogID { get; set; }

        [Required]
        public string EventType { get; set; }

        [Required]
        public string TableName { get; set; }

        [Required]
        public string RecordID { get; set; }

        public string ColumnName { get; set; }

        public string OriginalValue { get; set; }

        public string NewValue { get; set; }

        [Required]
        public string CreatedByUserID { get; set; }

        [Required]
        public DateTime CreateDateTime { get; set; }
        
    }
}
