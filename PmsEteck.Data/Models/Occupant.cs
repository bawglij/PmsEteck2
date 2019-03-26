using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PmsEteck.Data.Models
{
    [Table("Occupants", Schema = "invoice")]
    public class Occupant
    {
        [Key]
        public int iOccupantID { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Debiteur")]
        public int iDebtorID { get; set; }

        [Display(Name = "Bedrijfsnaam")]
        [MaxLength(150, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        public string sCompanyName { get; set; }

        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Aanhef")]
        public int iTitleID { get; set; }

        [MaxLength(50, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Voorletters")]
        public string sInitials { get; set; }

        [MaxLength(250, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Voornaam")]
        public string sFirstName { get; set; }

        [MaxLength(250, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Required(ErrorMessage = "{0} is verplicht")]
        [Display(Name = "Achternaam")]
        public string sLastName { get; set; }

        [MaxLength(30, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Telefoonnummer")]
        [DataType(DataType.PhoneNumber)]
        public string sPhoneNumber { get; set; }

        [MaxLength(80, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Emailadres")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} is geen geldig emailadres")]
        public string sEmailAddress { get; set; }

        [MaxLength(250, ErrorMessage = "{0} mag maximaal {1} tekens bevatten")]
        [Display(Name = "Opmerking")]
        public string sRemark { get; set; }

        [Display(Name = "Leegstand")]
        public bool bIsVacancy { get; set; }

        [Display(Name = "Token")]
        public Guid? OccupantToken { get; set; }

        [Display(Name = "Ontvangt nieuwsbrief")]
        public bool ReceiveNewsletter { get; set; }

        [Display(Name = "Klanttevredenheidsonderzoek")]
        public bool CustomerSatisfactionResearch { get; set; }

        [Display(Name = "Debiteur")]
        public virtual Debtor Debtor { get; set; }

        [Display(Name = "Aanhef")]
        public virtual Title Title { get; set; }

        [Display(Name = "Adresbewoners")]
        public ICollection<AddressOccupant> AddressOccupants { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public ICollection<ServiceTicket> ServiceTickets { get; set; }
    }
}