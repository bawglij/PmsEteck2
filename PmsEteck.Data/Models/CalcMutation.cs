using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("CalcMutations")]
    public class CalcMutation
    {
        [Key]
        public int iCalcMutationKey { get; set; }

        public int iProjectKey { get; set; }

        [Display(Name = "Categorie")]
        public int iCalcCategoryKey { get; set; }

        [Display(Name = "Jaar")]
        public int iYear { get; set; }

        [Display(Name = "Periode")]
        public int iPeriod { get; set; }

        [Display(Name = "Bedrag")]
        public decimal dAmount { get; set; }

        [Display(Name = "Datum")]
        public DateTime dtDate { get; set; }

        [MaxLength(250)]
        [Display(Name ="Omschrijving")]
        public string sDescription { get; set; }

        public ProjectInfo ProjectInfo { get; set; }

        public CalcCategory CalcCategory { get; set; }
    }
}