using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("InvestmentProposals")]
    public class InvestmentProposal
    {
        [Key]
        public int InvestmentProposalID { get; set; }

        public string Description { get; set; }

        [MaxLength(100)]
        public string EnglishDescription { get; set; }

        public int Order { get; set; }

        public ICollection<Opportunity> Opportunities { get; set; }
    }
}
