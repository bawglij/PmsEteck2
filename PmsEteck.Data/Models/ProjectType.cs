using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class ProjectType
    {
        [Key]
        public int ProjectTypeID { get; set; }

        [StringLength(150)]
        public string Name { get; set; }

        public bool Active { get; set; }
    }
}