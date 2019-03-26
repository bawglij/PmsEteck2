using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("DimCustomers", Schema = "astonmartin")]
    public class Customer
    {
        [Key]
        public int iCustomerID { get; set; }

        public int iPartnerID { get; set; }

        public int iGroupID { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Entiteit")]
        public string sName { get; set; }

        [MaxLength(100)]
        public string sClientcode { get; set; }

        [MaxLength(100)]
        public string sCocnumber { get; set; }

        [MaxLength(100)]
        public string sSbicode { get; set; }

        [MaxLength(100)]
        public string sAddress { get; set; }

        [MaxLength(20)]
        public string sPostalcode { get; set; }

        [MaxLength(100)]
        public string sCity { get; set; }

        [MaxLength(50)]
        public string sCountry { get; set; }

        public string sDescription { get; set; }

        public bool bParent { get; set; }

        public bool bActive { get; set; }

        [MaxLength(30)]
        public string NavisionPrefix { get; set; }

        public bool CustomerMetacom { get; set; }

        public bool CustomerNavision { get; set; }

        public virtual CustomerInfo CustomerInfo { get; set; }
    }
}