using System.Data;
using System.Linq;
using System.Net;
using PmsEteck.ViewModels;
using PmsEteck.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;
using System.Threading.Tasks;

namespace PmsEteck.Controllers
{
    //TM
    [Authorize]
    [RequireHttps]
    public class ContactsController : Controller
    {
        //private PmsEteckContext db = new PmsEteckContext();
        private readonly ApplicationDbContext _context;

        public ContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Contacts/Create
        //[Authorize(Roles = "ContactsCreator")]
        public ActionResult Create(int? iProjectKey)
        {
            if (iProjectKey == null)
            {
                return BadRequest();
            }

            ProjectInfo projectInfo = _context.ProjectInfo.Find(iProjectKey);

            if (projectInfo == null)
            {
                return NotFound();
            }

            var viewmodel = new ContactCreateViewModel()
            {
                iProjectKey = (int)iProjectKey,
                ContactTypes = new SelectList(_context.ContactTypes.OrderBy(o => o.sContactTypeName), "iContactTypeKey", "sContactTypeName")
            };

            return View(viewmodel);

        }

        // POST: Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "ContactsCreator")]
        public async Task<IActionResult> Create(ContactCreateViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                var contact = new Contact()
                {
                    iContactTypeKey = viewmodel.iContactTypeKey,
                    iProjectKey = viewmodel.iProjectKey,
                    sOrganisation = viewmodel.sOrganisation,
                    sTitle = viewmodel.sTitle,
                    sContactName = viewmodel.sContactName,
                    sDescription = viewmodel.sDescription,
                    sEmail = viewmodel.sEmail,
                    sTelephone = viewmodel.sTelephone
                };

                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();

                return new RedirectResult(Url.Action("Details", "Projects", new { id = viewmodel.iProjectKey }) + "#overigeContacten");
            }

            viewmodel.ContactTypes = new SelectList(_context.ContactTypes.OrderBy(o => o.sContactTypeName), "iContactTypeKey", "sContactTypeName", viewmodel.iContactTypeKey);

            return View(viewmodel);
        }

        // GET: Contacts/Edit/5
        //[Authorize(Roles = "ContactsEditor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Contact contact = _context.Contacts.Find(id);

            if (contact == null)
            {
                return NotFound();
            }

            var viewmodel = new ContactEditViewModel()
            {
                iContactKey = contact.iContactKey,
                iContactTypeKey = contact.iContactTypeKey,
                iProjectKey = contact.iProjectKey,
                sOrganisation = contact.sOrganisation,
                sTitle = contact.sTitle,
                sContactName = contact.sContactName,
                sDescription = contact.sDescription,
                sEmail = contact.sEmail,
                sTelephone = contact.sTelephone,
                ContactTypes = new SelectList(_context.ContactTypes.OrderBy(o => o.sContactTypeName), "iContactTypeKey", "sContactTypeName", contact.iContactTypeKey)
            };

            return View(viewmodel);
        }

        // POST: Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "ContactsEditor")]
        public async Task<IActionResult> Edit(ContactEditViewModel viewmodel)
        {
            if (ModelState.IsValid)
            {
                var contact = _context.Contacts.Find(viewmodel.iContactKey);

                contact.sContactName = viewmodel.sContactName;
                contact.sOrganisation = viewmodel.sOrganisation;
                contact.sTitle = viewmodel.sTitle;
                contact.sDescription = viewmodel.sDescription;
                contact.sEmail = viewmodel.sEmail;
                contact.sTelephone = viewmodel.sTelephone;
                contact.iContactTypeKey = viewmodel.iContactTypeKey;

                _context.Entry(contact).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new RedirectResult(Url.Action("Details", "Projects", new { id = contact.iProjectKey }) + "#overigeContacten");
            }

            viewmodel.ContactTypes = new SelectList(_context.ContactTypes.OrderBy(o => o.sContactTypeName), "iContactTypeKey", "sContactTypeName", viewmodel.iContactTypeKey);

            return View(viewmodel);
        }

        // GET: Contacts/Delete/5
        //[Authorize(Roles = "ContactsDeleter")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Contact contact = _context.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "ContactsDeleter")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Contact contact = _context.Contacts.Find(id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return new RedirectResult(Url.Action("Details", "Projects", new { id = contact.iProjectKey }) + "#overigeContacten");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
