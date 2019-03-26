using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("SolutionCategories", Schema = "service")]
    public class SolutionCategory
    {
        #region Keys
        [Key]
        [Display(Name = "Oplossing categorie")]
        public int SolutionCategoryID { get; set; }
        #endregion

        #region Properties
        [Display(Name = "Beschrijving")]
        [MaxLength(100)]
        public string Description { get; set; }
        #endregion

        #region List References
        [Display(Name = "Werkbonnen")]
        public ICollection<WorkOrder> WorkOrders { get; set; }
        #endregion
    }
}