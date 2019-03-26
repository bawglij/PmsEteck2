using System;
using System.ComponentModel.DataAnnotations;

namespace PmsEteck.Data.Models
{
    public class OpportunityValueLog
    {
        [Key]
        public int iOpportunityValueLogID { get; set; }

        [Display(Name = "Opportunity")]
        public int iOpportunityID { get; set; }

        [Display(Name = "Datum")]
        public DateTime dtDateTime { get; set; }

        [Display(Name = "Kans")]
        public decimal dChance { get; set; }

        [Display(Name = "EBITDA")]
        public decimal dEbitda { get; set; }

        [Display(Name = "WA EBITDA")]
        public decimal dWAEbitda { get; set; }

        [Display(Name = "WA EBITDA mutatie")]
        public decimal dWAEbitdaMutation { get; set; }

        [Display(Name = "Opportunity")]
        public Opportunity Opportunity { get; set; }
    }
}