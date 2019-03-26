using System;
using System.ComponentModel.DataAnnotations.Schema;
using Toolbelt.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("AddressRateCards")]
    public class AddressRateCard
    {
        [ForeignKey("Address")]
        [Index("IX_iAddressKey")]
        public int iAddressKey { get; set; }

        [ForeignKey("RateCard")]
        [Index("IX_iRateCardKey")]
        public int iRateCardKey { get; set; }

        public DateTime dtStartDate { get; set; }

        public DateTime? dtEndDate { get; set; }

        public virtual Address Address { get; set; }

        public virtual RateCard RateCard { get; set; }
    }
}
