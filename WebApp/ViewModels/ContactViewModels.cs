using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PmsEteck.ViewModels
{
    // TM
    public class ContactIndexViewModel
    {
        public int iContactKey { get; set; }

        [Display(Name = "Organisatie")]
        [MaxLength(50)]
        public string sOrganisation { get; set; }

        [Display(Name = "Functie")]
        [MaxLength(50)]
        public string sTitle { get; set; }

        [Display(Name = "Contact type")]
        public string sContactTypeName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Naam")]
        public string sContactName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [MaxLength(100)]
        public string sEmail { get; set; }

        [Display(Name = "Telefoon")]
        [Phone]
        [MaxLength(50)]
        public string sTelephone { get; set; }

        [Display(Name = "Omschrijving")]
        [MaxLength(250)]
        public string sDescription { get; set; }
    }

    public class ContactCreateViewModel
    {
        public int iProjectKey { get; set; }

        [Display(Name = "Organisatie")]
        [MaxLength(50)]
        public string sOrganisation { get; set; }

        [Display(Name = "Functie")]
        [MaxLength(50)]
        public string sTitle { get; set; }

        [Display(Name = "Type")]
        public int iContactTypeKey { get; set; }
        public IEnumerable<SelectListItem> ContactTypes { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Naam")]
        public string sContactName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [MaxLength(100)]
        public string sEmail { get; set; }

        [Display(Name = "Telefoon")]
        [MaxLength(50)]
        public string sTelephone { get; set; }

        [Display(Name = "Omschrijving")]
        [MaxLength(250)]
        public string sDescription { get; set; }

    }
    public class ContactEditViewModel
    {
        [Key]
        public int iContactKey { get; set; }

        public int iProjectKey { get; set; }

        [Display(Name = "Organisatie")]
        [MaxLength(50)]
        public string sOrganisation { get; set; }

        [Display(Name = "Functie")]
        [MaxLength(50)]
        public string sTitle { get; set; }

        [Display(Name = "Type")]
        public int iContactTypeKey { get; set; }
        public IEnumerable<SelectListItem> ContactTypes { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Naam")]
        public string sContactName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [MaxLength(100)]
        public string sEmail { get; set; }

        [Display(Name = "Telefoon")]
        [MaxLength(50)]
        public string sTelephone { get; set; }

        [Display(Name = "Omschrijving")]
        [MaxLength(250)]
        public string sDescription { get; set; }

    }
}