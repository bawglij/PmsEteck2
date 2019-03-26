using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace PmsEteck.Data.Models
{

    public class ApplicationUser : IdentityUser
    {
        [MaxLength(150, ErrorMessage = "{0} mag maximaal {1} tekens bevatten.")]
        public string sFirstName { get; set; }

        [MaxLength(150, ErrorMessage = "{0} mag maximaal {1} tekens bevatten.")]
        public string sLastName { get; set; }

        public bool? IsLocked { get; set; }

        public bool IsDuoAuthenticatorEnabled { get; set; }

        public string DuoAuthenticatorSecretKey { get; set; }

        [Display(Name = "Onderhoudspartij")]
        [ForeignKey("MaintenanceContact")]
        public int? MaintenanceContactID { get; set; }

        [Display(Name = "Onderhoudspartij")]
        public virtual MaintenanceContact MaintenanceContact { get; set; }

        [Display(Name = "Rolegroups")]
       // public virtual List<RoleGroup> RoleGroups { get; set; }
        public virtual List<ApplicationUserRoleGroup> RoleGroups { get; set; }


        [Display(Name = "Usergroups")]
        //public virtual List<UserGroup> UserGroups { get; set; }
        public virtual List<ApplicationUserGroup> UserGroups { get; set; }

        public List<Invoice> Invoices { get; set; }

        public string FullName { get { return string.IsNullOrWhiteSpace(string.Join(" ", sFirstName, sLastName)) ? UserName : string.Join(" ", sFirstName, sLastName); } }

        public ICollection<Ticket> Tickets { get; set; }

        public ICollection<TicketLog> TicketLogs { get; set; }

        public ICollection<PaymentTermHistory> PaymentTermHistories { get; set; }

        public ICollection<ServiceTicket> ServiceTickets { get; set; }

        public ICollection<ServiceInvoiceLineInput> ServiceInvoiceLineInputs { get; set; }

        /*
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            //if (string.IsNullOrEmpty(authenticationType))
            {
              //  authenticationType = DefaultAuthenticationTypes.ApplicationCookie;
           // }
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.Creat(this, authenticationType);

            userIdentity.AddClaim(new Claim("UserID", Id));
            userIdentity.AddClaim(new Claim("FullName", FullName));
            userIdentity.AddClaim(new Claim("MaintenanceContactID", MaintenanceContactID.ToString()));
            // Add custom user claims here
            return userIdentity;
        }
        */
    }

    public static class SIIdentity
    {
        public static string GetUserID(this IIdentity identity)
        {
            return ((ClaimsIdentity)identity).Claims.FirstOrDefault(c => c.Type == "UserID").Value;
        }
        
        //Not needed anymore
        /*
        public static string GetName(this IIdentity identity)
        {
            ClaimsIdentity claimIdentity = new ClaimsIdentity(identity);
            return claimIdentity.FindFirst("FullName").ToString();
        }
        */
        public static int? MaintenanceContactID(this IIdentity identity)
        {
            if (int.TryParse(((ClaimsIdentity)identity).FindFirst("MaintenanceContactID").ToString(), out int maintenanceContactID)) return maintenanceContactID;
            return null;
        }
        
    }

}