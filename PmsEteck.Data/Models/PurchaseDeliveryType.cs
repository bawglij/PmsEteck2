using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("PurchaseDeliveryTypes")]
    public class PurchaseDeliveryType
    {
        [Key]
        public int iPurchaseDeliveryTypeID { get; set; }

        [Display(Name = "Beschrijving")]
        [MaxLength(50)]
        public string sDescription { get; set; }

        //public ICollection<ProjectInfo> Projects { get; set; }
        public ICollection<ProjectInfoPurchDeliveryType> Projects { get; set; }
    }
}