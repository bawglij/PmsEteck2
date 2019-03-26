using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using PmsEteck.Data.Models;
using WebApp.ViewModels;
using static WebApp.ViewModels.AccountViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    [RequireHttps]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Require the user to have an confirmed email before they can log on.
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError(string.Empty, "Ongeldige login.");
                    return View(model);
                }
            }
            
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

            if(result.Succeeded)
            {
               // IList<Claim>identity = await _userManager.GetClaimsAsync(user);
                //if (identity.Main().HasValue)
                 return RedirectToAction("Index", "Home");
            }
            if(result.IsLockedOut)
            {
                return View("Lockout");
            }
            if(result.RequiresTwoFactor)
            {
                _logger.LogInformation("User logged in.");
                return RedirectToAction("Duo", new { ReturnUrl = returnUrl });
            }
            else
            { 
                ModelState.AddModelError("", "Ongeldige login.");
                return View(model);
            }
        }

        //DUO // GET: /Account/Duo
        [HttpGet]
        [Authorize]
        [AllowAnonymous]
        public async Task<ActionResult> Duo(string returnUrl)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            //var userid = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userId = User.Identity;
            if (user == null)
            {
                return View("Error");
            }
            var provider = new PmsEteck.Data.Services.DuoWebTokenProvider();

            string token = await provider.GenerateAsync("Duo Web", _userManager, user);

            return View(new DuoWebViewModel { Host = "api-5845d973.duosecurity.com", Request = token, ReturnUrl = returnUrl });
        }

        //POST: /Account/Duo
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Duo(string sig_response, string returnUrl)
        {
            var result = await _signInManager.TwoFactorSignInAsync("Duo Web", sig_response, isPersistent: false, rememberClient: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");

            }
            if(result.IsLockedOut)
            {
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError("", "Invalid code.");
                return RedirectToAction("Duo", new { returnUrl });
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (!ModelState.IsValid)
            {
                return View();
            }
            // Generate the token and send it
            var token  = await _userManager.GenerateTwoFactorTokenAsync(user, model.SelectedProvider);
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public ActionResult VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            //if (!await _signInManager())
            //{
            //  return View("Error");
            //}
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberClient:false);
            if(result.Succeeded)
            {
                return Redirect(model.ReturnUrl);
            }
            if(result.IsLockedOut)
            {
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError("", "Invalid code.");
                return View(model);
            }
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        // GET: /Account/ConfirmEmail
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            code = WebUtility.UrlDecode(code);
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return RedirectToAction("SetPassword", "Manage", new { userId });
            }
            return View("Error");
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
            => code == null ? View("Error") : View();

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ViewModels.ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.ToString());
        }

        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ViewModels.ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Url.ActionContext.HttpContext.Request.Scheme);
                string body = string.Format("<div>&nbsp;</div>" +
                    "<div><p>Beste {0},</p>" +
                    "<p>Wij hebben het verzoek ontvangen om het wachtwoord opnieuw in te stellen voor het account wat gekoppeld is aan dit emailadres.</p>" +
                    "<p>Als jij dit verzoek niet gedaan hebt, kun je veilig deze email negeren.</p>" +
                    "<p>Klik op onderstaande link om je wachtwoord te opnieuw in te stellen</p></div>" +
                    "<div>&nbsp;</div>" +
                    "<div><p><a href=\"" + callbackUrl + "\" style=\"color: #fff; text-decoration: none; background-color: rgb(28, 132, 198);text-align: center; vertical-align: middle;cursor: pointer;padding: 6px 12px; border-radius: 4px;;line-height: 1.42857143;\">Wachtwoord herstellen</a></p></div>" +
                    "<div>&nbsp;</div>" +
                    "<div><p>&nbsp;</p>" +
                    "<p>Groeten,<br> Het Eteck Team</p></div>", user.FullName);
                await _emailSender.SendEmailAsync(model.Email, "Wachtwoord herstellen", body);
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

    }
}