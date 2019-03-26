using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("ResponseTypes", Schema = "service")]
    public class ResponseType
    {
        [Key]
        public int iResponseTypeID { get; set; }

        [Required]
        [MaxLength(50)]
        public string sName { get; set; }

        [MaxLength(50)]
        public string sDisplayName { get; set; }

        public ICollection<Response> Responses { get; set; }
    }
}