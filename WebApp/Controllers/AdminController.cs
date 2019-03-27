using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2.HPack;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models;
using PmsEteck.Helpers;
using WebApp.Data;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
        }
        //[Authorize(Roles="UserManager")]
        public async Task<IActionResult> Index(string searchValue, string sorting = null, int page = 1)
        {
            IQueryable<AccountListViewModel> accountList = _userManager.Users
                                                                    .Where(w =>
                                                                        string.IsNullOrEmpty(searchValue) ||
                                                                        w.Email.ToUpper().Contains(searchValue.ToUpper()) ||
                                                                        w.sFirstName.ToUpper().Contains(searchValue.ToUpper()) ||
                                                                        w.sLastName.ToUpper().Contains(searchValue.ToUpper())
                                                                    )
                                                                    .Select(s => new AccountListViewModel
                                                                    {
                                                                        Id = s.Id,
                                                                        Email = s.Email,
                                                                        sFirstName = s.sFirstName,
                                                                        sLastName = s.sLastName,
                                                                        EmailConfirmed = s.EmailConfirmed
                                                                    });
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_UserList", await accountList.ToListAsync());

            return View(await accountList.ToListAsync());
        }

        // GET: /Admin/Register
        //[Authorize(Roles = "UserManager")]
        public ActionResult Register()
        {
            var model = new RegisterViewModel()
            {
                MaintenanceContactList = new SelectList(_context.MaintenanceContacts.OrderBy(o => o.sOrganisation).Select(s => new SelectListItem { Value = s.iMaintenanceContactKey.ToString(), Text = s.sOrganisation + " (" + s.iMaintenanceContactKey + ")" }), "Value", "Text")
            };
            return View(model);
        }

        // POST: /Admin/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "UserManager")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var secretKey = Password.Generate(40, 0);
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, sFirstName = model.sFirstName, sLastName = model.sLastName, IsDuoAuthenticatorEnabled = true, DuoAuthenticatorSecretKey = secretKey, MaintenanceContactID = model.MaintenanceContactID };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.SetTwoFactorEnabledAsync(user, true);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebUtility.UrlEncode(code);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Url.ActionContext.HttpContext.Request.Scheme);
                    string body = string.Format("<div>&nbsp;</div>" +
                        "<div><p>Beste {0},</p>" +
                        "<p>Er is een account aangemaakt voor toegang tot het Project Management Systeem van Eteck.</p>" +
                        "<p>Om toegang te krijgen tot het systeem is het noodzakelijk om je emailadres te verifiëren. Dit kun je doen doormiddel van onderstaande link.</p>" +
                        "</div><div>&nbsp;</div>" +
                        "<div><p>" +
                        "<a href=\"" + callbackUrl + "\" style=\"color: #fff; text-decoration: none; background-color: rgb(28, 132, 198);text-align: center; vertical-align: middle;cursor: pointer;padding: 6px 12px; border-radius: 4px;;line-height: 1.42857143;\">Emailadres verifiëren</a>" +
                        "</p></div>" +
                        "<div>&nbsp;</div>" +
                        "<div><p>Na het verifiëren van het emailadres kan direct een wachtwoord ingesteld worden.</p><p>Groeten,<br> Het Eteck Team</p></div>", user.FullName);
                    await _emailSender.SendEmailAsync(user.Email, "Emailadres bevestigen", body);

                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            model.MaintenanceContactList = new SelectList(_context.MaintenanceContacts.OrderBy(o => o.sOrganisation).Select(s => new SelectListItem { Value = s.iMaintenanceContactKey.ToString(), Text = s.sOrganisation + " (" + s.iMaintenanceContactKey + ")" }), "Value", "Text");
            return View(model);
        }

        // GET: /Admin/ManagerRoleGroups/5
        //[Authorize(Roles = "UserManager")]
        public async Task<ActionResult> ManageRoleGroups(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var model = new AccountRoleGroupViewModel
            {
                Id = user.Id,
                SelectedGroups = await _context.ApplicationUserRoleGroup.Where(c => c.UserId == user.Id).Select(c => c.RoleGroup.Name).ToListAsync(),
                RoleGroups = _context.RoleGroups.OrderBy(o => o.Name)
                                        .Select(s => new SelectListItem
                                        {
                                            Text = s.Name,
                                            Value = s.Name
                                        })
                                        .ToList()
                                                     
            };
            
            return View(model);
        }
        
        //POST: /Admin/ManageUserGroups/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "UserManager")]
        public async Task<ActionResult> ManageRoleGroups(AccountRoleGroupViewModel model, string id)
        {
            if (string.IsNullOrEmpty(id))
                ModelState.AddModelError(string.Empty, "Geen geldige aanvraag.");

            ApplicationUser user = await _context.Users.Include(u => u.RoleGroups).ThenInclude(x => x.RoleGroup).FirstOrDefaultAsync(f => f.Id == id);
            
            if (user == null)
                ModelState.AddModelError(string.Empty, "De ingestuurde gebruiker kan niet gevonden worden");

            if (ModelState.IsValid)
            {
                IQueryable<RoleGroup> roleGroupList = _context.RoleGroups.Include(i => i.Roles).ThenInclude(a => a.ApplicationRole).AsQueryable();

                if (model.SelectedGroups != null)
                {
                    var test = model.SelectedGroups;
                    var selectedRoleGroups = roleGroupList.Where(w => model.SelectedGroups.Contains(w.Name));
                    HashSet<int> selectedRoleIds = new HashSet<int>(roleGroupList.Where(w => model.SelectedGroups.Contains(w.Name)).Select(s => s.Id));
                    var userRoleGroups = user.RoleGroups;
                    HashSet<int> userRoleGroupIds = new HashSet<int>(user.RoleGroups.Select(s => s.RoleGroup.Id));

                    HashSet<string> roleNameList = new HashSet<string>(await _context.ApplicationUserRoleGroup.Where(c => c.UserId == user.Id).Select(c => c.RoleGroup.Name).ToListAsync());
                        //user.RoleGroups.SelectMany(sm => sm.RoleGroup.Roles).Select(s => s.RoleGroup.Name));

                    // What to do with new selected rolegroups
                    foreach (var roleGroup in selectedRoleGroups.Where(w => !userRoleGroupIds.Contains(w.Id)).ToList())
                    {
                        bool roleGroupHasError = false;
                        foreach (var applicationRole in roleGroup.Roles)
                        {
                            // Check of user is in role
                            if (!await _userManager.IsInRoleAsync(user, applicationRole.ApplicationRole.Name))
                            {
                                var addUserTolRoleResult = await _userManager.AddToRoleAsync(user, applicationRole.ApplicationRole.Name);
                                if (!addUserTolRoleResult.Succeeded)
                                {
                                    roleGroupHasError = true;
                                    AddErrors(addUserTolRoleResult);
                                }
                            }

                        }
                        if (!roleGroupHasError)
                        {
                            //await _userManager.AddToRoleAsync(user, roleGroup.Name);
                            //user.RoleGroups.Add(roleGroup); --standard
                            ApplicationUserRoleGroup est = await _context.ApplicationUserRoleGroup.Where(s => s.RoleGroupId == roleGroup.Id).FirstOrDefaultAsync();
                            user.RoleGroups.Add(est);
                        }
                        // Voeg de rolgroep aan de gebruiker toe
                    }

                    // What to do with unselected rolegroups
                    foreach (var roleGroup in userRoleGroups.Where(w => !selectedRoleIds.Contains(w.RoleGroup.Id)).ToList())
                    {
                        bool roleGroupHasError = false;
                        // Removed RoleGroups
                        foreach (var role in roleGroup.RoleGroup.Users)
                        {
                            if (await _userManager.IsInRoleAsync(user, role.RoleGroup.Name))
                            {
                                // Check of de rol vanuit andere rolgroep ook gebruikt wordt
                                HashSet<string> selectedRoles = new HashSet<string>(selectedRoleGroups.SelectMany(sm => sm.Users).Select(s => s.RoleGroup.Name));

                                if (!selectedRoles.Any(u => u == role.RoleGroup.Name))
                                {
                                    var removeUserFromRoleResult = await _userManager.RemoveFromRoleAsync(user, role.RoleGroup.Name);
                                    if (!removeUserFromRoleResult.Succeeded)
                                    {
                                        roleGroupHasError = true;
                                        AddErrors(removeUserFromRoleResult);
                                    }
                                }
                            }
                        }
                        // Bij geen fouten de gebruiker verwijderen bij de rolgroep
                        if (!roleGroupHasError)
                            user.RoleGroups.Remove(roleGroup);
                    }

                }
                else
                {
                    foreach (var roleGroup in user.RoleGroups.ToList())
                    {
                        bool roleGroupHasError = false;
                        foreach (var role in roleGroup.RoleGroup.Roles)
                        {
                            if (await _userManager.IsInRoleAsync(user, role.RoleGroup.Name))
                            {
                                var removeUserFromRoleResult = await _userManager.RemoveFromRoleAsync(user, role.RoleGroup.Name);
                                if (!removeUserFromRoleResult.Succeeded)
                                {
                                    roleGroupHasError = true;
                                    AddErrors(removeUserFromRoleResult);
                                }
                            }
                        }
                        if (!roleGroupHasError)
                            user.RoleGroups.Remove(roleGroup);
                    }
                }
                
                _context.SaveChanges();

                if (user.Id == User.Identity.GetUserID())
                {
                    await _signInManager.SignInAsync(user, false, null);
                }

                return RedirectToAction("Details", new { id = user.Id });
            }
            model.RoleGroups = _context.RoleGroups.OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Name }).ToList();

            return View(model);
        }
        
        // GET: /Admin/Details
        //[Authorize(Roles = "UserViewer")]
        public async Task<ActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            AccountDetailsViewModel model = new AccountDetailsViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.FullName,
                EmailConfirmed = user.EmailConfirmed,
                //UserGroups = user.UserGroups.Select(s => s.UserGroup.Name).ToArray(),
                RoleGroups = await _context.ApplicationUserRoleGroup.Where(c => c.UserId == user.Id).Select(c => c.RoleGroup.Name).ToListAsync(),
                UserLocked = user.IsLocked.HasValue && user.IsLocked.Value
            };
            return View(model);
        }

        // GET: /Admin/ManageUserGroups/5
        //[Authorize(Roles = "UserManager")]
        public async Task<ActionResult> ManageUserGroups(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var model = new AccountUserGroupViewModel
            {
                Id = user.Id,
                SelectedGroups = user.UserGroups.Select(s => s.UserGroup.Name).ToArray(),
                UserGroups = _context.UserGroups.OrderBy(o => o.Name)
                                        .Select(s => new SelectListItem
                                        {
                                            Text = s.Name,
                                            Value = s.Name
                                        })
                                        .ToList()
            };
            return View(model);
        }

        //POST: /Admin/ManageUserGroups/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "UserManager")]
        public async Task<ActionResult> ManageUserGroups(AccountUserGroupViewModel model, string id)
        {
            if (string.IsNullOrEmpty(id))
                ModelState.AddModelError(string.Empty, "Geen geldige aanvraag.");

            ApplicationUser user = await _context.Users.Include(i => i.UserGroups).FirstOrDefaultAsync(f => f.Id == id);

            if (user == null)
                ModelState.AddModelError(string.Empty, "De ingestuurde gebruiker kan niet gevonden worden");

            if (ModelState.IsValid)
            {

                if (model.SelectedGroups != null)
                {
                    //user.UserGroups = _context.UserGroups.Where(w => model.SelectedGroups.Contains(w.Name)).ToList();
                    user.UserGroups = user.UserGroups.Where(w => model.SelectedGroups.Contains(w.UserGroup.Name)).ToList();
                }
                else
                {
                    foreach (var userGroup in user.UserGroups.ToList())
                    {
                        user.UserGroups.Remove(userGroup);
                    }
                }
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = user.Id });
            }

            model.UserGroups = _context.UserGroups.OrderBy(o => o.Name).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString(), Selected = s.Users.Any(u => u.ApplicationUser.Id == user.Id) }).ToList();

            return View(model);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion
    }
}