using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Subsidies")]
    public class Subsidy
    {
        [Key]
        public int iSubsidyKey { get; set; }

        public int iProjectKey { get; set; }

        public int iSubsidyCategoryKey { get; set; }

        [Display(Name = "Datum afgifte")]
        public DateTime dtStartDate { get; set; }

        [Display(Name = "Einddatum")]
        public DateTime dtEndDate { get; set; }

        [Display(Name="Bedrag")]
        public decimal dAmount { get; set; }

        [MaxLength(250)]
        [Display(Name ="Omschrijving")]
        public string sSubsidyDescription { get; set; }


        public virtual ProjectInfo ProjectInfo { get; set; }

        public virtual SubsidyCategory SubsidyCategory { get; set; }

    }
}