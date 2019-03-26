using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("CustomerAccounts")]
    public class CustomerAccount
    {
        [Key]
        public int iCustomerAccountID { get; set; }

        [Display(Name = "Bank")]
        [MaxLength(200)]
        public string sBankName { get; set; }

        [Display(Name = "IBAN nummer")]
        [MaxLength(50)]
        public string sIBANNumber { get; set; }

        [Display(Name = "SWIFT")]
        [MaxLength(20)]
        public string sSWIFTCode { get; set; }

        [Display(Name = "Account type")]
        [MaxLength(100)]
        public string sAccountType { get; set; }

        [ForeignKey("CustomerInfo")]
        public int iCustomerID { get; set; }

        public CustomerInfo CustomerInfo { get; set; }
    }
}