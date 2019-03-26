using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PmsEteck.Data.Models;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PmsEteck
{
    /*
    public class EmailService : IIdentityMessageService
    {
        
        public static string MailAddressFrom = ConfigurationManager.AppSettings["mailAddressFrom"];
        public static string Host = ConfigurationManager.AppSettings["smtpHost"];
        public static string Port = ConfigurationManager.AppSettings["smtpPort"];
        public static bool HasCredentials = bool.Parse(ConfigurationManager.AppSettings["smtpHasCredentials"]);

        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            MailMessage msg = new MailMessage();
            msg.IsBodyHtml = true;
            msg.From = new MailAddress(MailAddressFrom, "PMS Beheerder");
            msg.To.Add(new MailAddress(message.Destination));
            string template;
            using (StreamReader reader = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Helpers", "EmailTemplate.txt"), Encoding.UTF8))
            {
                template = reader.ReadToEnd();
            }
            msg.Body = template.Replace("- subject -", message.Subject).Replace("- body -", message.Body);
            msg.Subject = message.Subject;

            SmtpClient smtpClient = new SmtpClient(Host, Convert.ToInt32(Port));
            smtpClient.UseDefaultCredentials = false;
            if (HasCredentials)
            {
                smtpClient.Credentials = new NetworkCredential("AKIAJIYIKZUD4PPARTTQ", "AmHZWh0ToaSWr2uYRs8yoVxOuZhzVPchtzYMdt4K+GTe");
                smtpClient.EnableSsl = true;
            }
            else
            {
                smtpClient.EnableSsl = false;
            }

            smtpClient.Send(msg);

            return Task.FromResult(0);
        }
    }
    */
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
    /*
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store): base(store)
        {
        }

        public static ApplicationUserManager CreateAsync(ApplicationUserManager options, HttpContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Session);
            // Configure validation logic for usernames
            manager.UserValidators = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true,
            };

            // Configure validation logic for passwords
            manager.PasswordValidators = new IPasswordValidator<manager>
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            //manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            //{
            //    MessageFormat = "Your security code is {0}"
            //});
            //manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            //{
            //    Subject = "Security Code",
            //    BodyFormat = "Your security code is {0}"
            //});
            manager.RegisterTwoFactorProvider("Duo Web", new DuoWebTokenProvider());

            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("Eteck Identity")) {  TokenLifespan = TimeSpan.FromDays(7) };
            }
            return manager;
        }
    }
    /*
    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser>
    {


        public ApplicationSignInManager(ApplicationUserManager userManager) : base(userManager)
        {
        }

        public override async Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return await user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
            
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, HttpContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

        public override async Task PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var user = UserManager.FindByEmailAsync(userName);

            if (user != null && user.IsLocked.HasValue && user.IsLocked.Value)
            {
                return Task.FromResult(SignInStatus.Failure);
            }
            return await base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
        }
    }

    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(
            RoleStore<ApplicationRole> roleStore)
            : base(roleStore)
        {
        }
        public static ApplicationRoleManager Create(
            IdentityFactoryOptions<ApplicationRoleManager> options, HttpContext  context)
        {
            return new ApplicationRoleManager(
                new RoleStore<ApplicationRole>(context.Get<PmsEteckContext>()));
        }
    }
    
        public Task SendAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }

    }
    */
}
