using Microsoft.AspNetCore.Mvc.Rendering;
using PmsEteck.Data.Models;
using PmsEteck.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class AccountsViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AccountListViewModel
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Voornaam")]
        public string sFirstName { get; set; }

        [Display(Name = "Achternaam")]
        public string sLastName { get; set; }

        [Display(Name = "Email bevestigd")]
        public bool EmailConfirmed { get; set; }
    }
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "{0} mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Voornaam")]
        public string sFirstName { get; set; }

        [StringLength(100, ErrorMessage = "{0} mag maximaal {1} tekens bevatten.")]
        [Display(Name = "Achternaam")]
        public string sLastName { get; set; }

        [Display(Name = "Is onderhoudspartij")]
        public bool IsMaintenanceContact { get; set; }

        [Display(Name = "Selecteer onderhoudspartij")]
        [RequiredIf("IsMaintenanceContact", true)]
        public int? MaintenanceContactID { get; set; }
        public SelectList MaintenanceContactList { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Het {0} moet minimaal {2} karakters lang zijn.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bevestig wachtwoord")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Wachtwoorden komen niet overeen.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        public string userId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Het {0} moet minimaal {2} tekens bevatten.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nieuw wachtwoord")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Wachtwoord bevestigen")]
        [Compare("NewPassword", ErrorMessage = "Wachtwoorden komen niet overeen.")]
        public string ConfirmPassword { get; set; }
    }
    public class AccountRoleGroupViewModel
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "Rolgroepen")]
        //String
        public IList<string> SelectedGroups { get; set; }

        public IList<SelectListItem> RoleGroups { get; set; }
    }
    public class AccountDetailsViewModel
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Volledige naam")]
        public string Name { get; set; }

        public bool EmailConfirmed { get; set; }

        [Display(Name = "Rolgroepen")]
        //string
        public IList<string> RoleGroups { get; set; }

        [Display(Name = "Gebruikersgroepen")]
        public string[] UserGroups { get; set; }

        [Display(Name = "Gebruiker geblokkeerd")]
        public bool UserLocked { get; set; }
    }
    public class AccountUserGroupViewModel
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "Gebruikersgroepen")]
        public string[] SelectedGroups { get; set; }

        public IList<SelectListItem> UserGroups { get; set; }
    }
}

