using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "RateCards", Schema = "meter")]
    public class RateCard
    {
        [Key]
        public int iRateCardKey { get; set; }

        [Display(Name = "Type tariefkaart")]
        public int? iRateCardTypeKey { get; set; }

        [Required(ErrorMessage = "{0} is verplicht.")]
        [MaxLength(150, ErrorMessage = "Dit veld mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Omschrijving")]
        public string sDescription { get; set; }

        public virtual RateCardType RateCardType { get; set; }

        public List<AddressRateCard> AddressRateCards { get; set; }

        public List<RateCardYear> RateCardYears { get; set; }

    }
}