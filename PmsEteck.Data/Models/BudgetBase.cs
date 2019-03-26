using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("BudgetBases", Schema = "budget")]
    public class BudgetBase
    {
        [Key]
        public int iBudgetBaseKey { get; set; }

        [Display(Name ="Entiteit")]
        public int iCustomerID { get; set; }

        [Display(Name ="Budget type")]
        public int iBudgetBaseTypeKey { get; set; }

        [Display(Name ="Boekjaar")]
        public int iYear { get; set; }

        [Display(Name ="Omschrijving")]
        [MaxLength(250)]
        public string sDescription { get; set; }

        [Display(Name = "Actief")]
        public bool bActive { get; set; }

        public Customer Customer { get; set; }

        public BudgetBaseType BudgetType { get; set; }

  

        public ICollection<BudgetDimension> BudgetDimensions { get; set; }

    }
}