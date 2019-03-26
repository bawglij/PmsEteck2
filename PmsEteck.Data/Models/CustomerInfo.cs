using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("CustomerInfo")]
    public class CustomerInfo
    {
        [Key, ForeignKey("CustomerBase")]
        public int iCustomerKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "KvK-nummer")]
        [StringLength(8, ErrorMessage = "{0} mag maximaal {1} tekens bevatten.")]
        public string sCocNumber { get; set; }

        [Display(Name = "Vestigingsnummer")]
        [StringLength(12, ErrorMessage = "{0} mag maximaal {1} tekens bevatten.")]
        public string sEstablishmentNumber { get; set; }

        [Display(Name = "SBI-code")]
        [StringLength(250, ErrorMessage = "{0} mag maximaal {1} tekens bevatten.")]
        public string sSbiCode { get; set; }

        [Display(Name = "BTW-nummer")]
        [StringLength(14, ErrorMessage = "{0} mag maximaal {1} tekens bevatten.")]
        public string sBtwNumber { get; set; }

        [Display(Name = "Oprichtingsdatum")]
        [DataType(DataType.Date)]
        public DateTime dtFoundationDate{ get; set; }

        [Display(Name = "Startdatum in beheer")]
        [DataType(DataType.Date)]
        public DateTime dtInControlDate { get; set; }

        public bool bExcludeForProjectBase { get; set; }

        #region Single References
        public virtual Customer CustomerBase { get; set; }
        #endregion

        #region List References
        public ICollection<CustomerDocument> Documents { get; set; }

        public ICollection<CustomerAccount> CustomerAccounts { get; set; }
        #endregion
    }
}