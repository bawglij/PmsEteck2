using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table(name: "Files", Schema = "pms")]
    public class File
    {
        [Key]
        public int iFileID { get; set; }

        public Guid? Key { get; set; }

        [StringLength(250)]
        [Display(Name = "Naam")]
        public string sDisplayName { get; set; }

        [StringLength(250)]
        [Display(Name = "Orignele Bestandsnaam")]
        public string sOriginalFileName { get; set; }

        [StringLength(150)]
        public string sContentType { get; set; }

        public byte[] bContent { get; set; }

        [Display(Name = "Extern toegankelijk?")]
        public bool bAllowSharing { get; set; }

        [ForeignKey("ApplicationUser")]
        public string sUserID { get; set; }

        [ForeignKey("RateCardYear")]
        public int? iRateCardYearID { get; set; }

        [ForeignKey("Opportunity")]
        public int? OpportunityID { get; set; }

        public Guid? ServiceTicketID { get; set; }

        public Guid? WorkOrderID { get; set; }

        public RateCardYear RateCardYear { get; set; }
        [NotMapped]
        public ApplicationUser ApplicationUser { get; set; }

        public ServiceTicket ServiceTicket { get; set; }

        public WorkOrder WorkOrder { get; set; }

        public Opportunity Opportunity { get; set; }
    }
}