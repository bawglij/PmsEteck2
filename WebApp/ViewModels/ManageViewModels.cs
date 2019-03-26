using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PmsEteck.ViewModels
{
    public class ManageViewModels
    {
        public class IndexViewModel
        {
            public bool HasPassword { get; set; }
            public IList<UserLoginInfo> Logins { get; set; }
            public string PhoneNumber { get; set; }
            public bool TwoFactor { get; set; }
            public bool BrowserRemembered { get; set; }
        }
        public class AddPhoneNumberViewModel
        {
            [Required]
            [Phone]
            [Display(Name = "Telefoonnummer")]
            public string Number { get; set; }
        }
    }
}
