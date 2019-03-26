using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class CommunicationType
    {
        [Key]
        public int ComunicationTypeID { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public bool Active { get; set; }
    }
}