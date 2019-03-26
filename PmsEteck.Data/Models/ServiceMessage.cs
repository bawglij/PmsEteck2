using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("ServiceMessages", Schema = "service")]
    public class ServiceMessage
    {
        #region Keys
        [Key]
        //Guid
        public Guid ServiceMessageID { get; set; }

        [Display(Name = "Servicemelding")]
        [ForeignKey("ServiceTicket")]
        //guid
        public Guid ServiceTicketID { get; set; }

        [Display(Name = "Gebruiker")]
        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        #endregion

        #region Properties

        [Display(Name = "Ontvangen")]
        [MaxLength(250, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string FromDisplayName { get; set; }

        [Display(Name = "Aangemaakt")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public DateTime CreatedDateTime { get; set; }

        [Display(Name = "Bericht")]
        [Required(ErrorMessage = "{0} is verplicht")]
        public string Body { get; set; }

        [Display(Name = "Interne notitie")]
        public bool IsInternalNote { get; set; }

        [Display(Name = "Uitgaand")]
        public bool OutGoing { get; set; }
        #endregion

        #region Single References
        public ServiceTicket ServiceTicket { get; set; }
        [NotMapped]
        public ApplicationUser ApplicationUser { get; set; }
        #endregion
    }
}