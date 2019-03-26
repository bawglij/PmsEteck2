using Microsoft.AspNetCore.Identity;
using PmsEteck.Data.Models;
using System;
using System.Threading.Tasks;

namespace PmsEteck.Data.Services
{
    
    public class DuoWebTokenProvider : IUserTwoFactorTokenProvider<ApplicationUser>
    //IdentityUserToken<ApplicationUser, string>
    {
        private readonly string IKEY = "DIIP7SJCPX45I2DZOF31";
        private readonly string SKEY = "ZKPwKhBPftdbvgG1Z7mkjAp6hjbKLaNk0Q4VP4PP";

        public Task<string> GenerateAsync(string purpose, UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            string token = Web.SignRequest(IKEY, SKEY, user.DuoAuthenticatorSecretKey, user.UserName, DateTime.UtcNow);
            return Task.FromResult(token);
        }

        public Task<bool> ValidateAsync(string purpose, string token, UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            bool valid = user.UserName == Web.VerifyResponse(IKEY, SKEY, user.DuoAuthenticatorSecretKey, token, DateTime.UtcNow);
            return Task.FromResult(valid);
        }

        public Task NotifyAsync(string token, UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            return Task.FromResult(true);
        }

        public Task<bool> IsValidProviderForUserAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            return Task.FromResult(user.IsDuoAuthenticatorEnabled);
        }

        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
        {
            //vld
            return Task.FromResult(user.IsDuoAuthenticatorEnabled);
        }
    }
    
}