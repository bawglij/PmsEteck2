using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class LostToCategory
    {
        [Key]
        public int LostToCategoryID { get; set; }

        [MaxLength(128)]
        public string Description { get; set; }

        [MaxLength(128)]
        public string EnglishDescription { get; set; }

        public bool Active { get; set; }
        
        public int Order { get; set; }

    }
}
