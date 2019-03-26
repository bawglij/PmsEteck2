using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("SalesDeliveryTypes")]
    public class SalesDeliveryType
    {
        [Key]
        public int iSalesDeliveryTypeID { get; set; }

        [Display(Name = "Beschrijving")]
        [MaxLength(50)]
        public string sDescription { get; set; }

        //public ICollection<ProjectInfo> Projects { get; set; }
        public ICollection<ProjectInfoSalesDeliveryType> Projects { get; set; }
    }
}