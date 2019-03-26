using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Resultaatoverzicht", Schema ="dbo")]
    public class Resultaatoverzicht
    {
        
        public int RecNo { get; set; }

        public string sSpatie { get; set; }

        public string sDescription { get; set; }

        public int? iSubTotal { get; set; }
        
        public decimal? SaldoRealisatie { get; set; }

        public decimal? SaldoBudget { get; set; }

        public int? VastVariabel { get; set; }

        public int? Berekenen { get; set; }

        public string sOpmaak { get; set; }

        public int? iYear { get; set; }

        public int? iPeriod { get; set; }

        [Key]
        public int? iCustomerID { get; set; }

        public int? iProjectKey { get; set; }
    }
}