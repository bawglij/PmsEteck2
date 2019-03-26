using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ViewModels
{
    public class AccountViewModels
    {
        public class ExternalLoginConfirmationViewModel
        {
            [Required]
            [Display(Name = "Email")]
            public string Email { get; set; }
        }

        public class ExternalLoginListViewModel
        {
            public string ReturnUrl { get; set; }
        }

        public class SendCodeViewModel
        {
            public string SelectedProvider { get; set; }
            public ICollection<SelectListItem> Providers { get; set; }
            public string ReturnUrl { get; set; }
            public bool RememberMe { get; set; }
        }

        public class DuoWebViewModel
        {
            public string Host { get; set; }
            public string Request { get; set; }
            public string ReturnUrl { get; set; }
            public string Response { get; set; }
        }

        public class VerifyCodeViewModel
        {
            [Required]
            public string Provider { get; set; }

            [Required]
            [Display(Name = "Code")]
            public string Code { get; set; }
            public string ReturnUrl { get; set; }

            [Display(Name = "Deze browser onthouden?")]
            public bool RememberBrowser { get; set; }

            public bool RememberMe { get; set; }
        }

        public class ForgotViewModel
        {
            [Required]
            [Display(Name = "Email")]
            public string Email { get; set; }
        }

        public class LoginViewModel
        {
            [Required]
            [Display(Name = "Email")]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Wachtwoord")]
            public string Password { get; set; }

            [Display(Name = "Onthoudt mij?")]
            public bool RememberMe { get; set; }
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
            //[RequiredIf("IsMaintenanceContact", true)]
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

        public class ForgotPasswordViewModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
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
            public string[] RoleGroups { get; set; }

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

        public class AccountRoleGroupViewModel
        {
            [Key]
            public string Id { get; set; }

            [Display(Name = "Rolgroepen")]
            public string[] SelectedGroups { get; set; }

            public IList<SelectListItem> RoleGroups { get; set; }
        }
    }
}
