using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class OtherDelivery
    {
        [Key]
        public int iOtherDeliveryKey { get; set; }

        [Display(Name = "Overige levering")]
        [MaxLength(50)]
        public string sOtherDelivery { get; set; }

        public bool bActive { get; set; }

        //public ICollection<ProjectInfo> Projects { get; set; }
        public ICollection<OtherDeliveryProjectInfo> Projects { get; set; }
    }
}