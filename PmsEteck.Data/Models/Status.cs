using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Statuses")]
    public class Status
    {
        #region Keys
        [Key]
        [Display(Name = "Status")]
        public int StatusID { get; set; }
        #endregion

        #region Properties
        [Display(Name = "Beschrijving")]
        [MaxLength(128)]
        public string Description { get; set; }

        [Display(Name = "Status code")]
        public int StatusCode { get; set; }

        [Display(Name = "Status kleur")]
        [MaxLength(50)]
        public string StatusColor { get; set; }
        #endregion
    }
}