using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models;
using WebApp.Data;
using WebApp.ViewModels;
using X.PagedList;

namespace WebApp.Controllers
{
    public class OperatorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OperatorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Operators
        public ActionResult Index(string sorting, string searchValue = "", int page = 1)
        {
            IQueryable<OperatorIndexViewModel> model = _context.Operators
                .Where(w => string.IsNullOrEmpty(searchValue)
                || w.iOperatorID.ToString().Contains(searchValue)
                || w.sName.Contains(searchValue))
                .Select(s => new OperatorIndexViewModel
                {
                    iOperatorID = s.iOperatorID,
                    sName = s.sName,
                    bActive = s.bActive,
                    iMeterCount = _context.ConsumptionMeters
                        .Where(w => w.iOperatorID == s.iOperatorID).Count()
                });

            switch (sorting)
            {
                case "KeyAsc":
                    model = model.OrderBy(o => o.iOperatorID);
                    break;
                case "KeyDesc":
                    model = model.OrderByDescending(o => o.iOperatorID);
                    break;
                case "NameAsc":
                    model = model.OrderBy(o => o.sName);
                    break;
                case "NameDesc":
                    model = model.OrderByDescending(o => o.sName);
                    break;
                case "ActiveAsc":
                    model = model.OrderBy(o => o.bActive).ThenBy(t => t.iOperatorID);
                    break;
                case "ActiveDesc":
                    model = model.OrderByDescending(o => o.bActive).ThenBy(t => t.iOperatorID);
                    break;
                case "CountAsc":
                    model = model.OrderBy(o => o.iMeterCount);
                    break;
                case "CountDesc":
                    model = model.OrderByDescending(o => o.iMeterCount);
                    break;
                default:
                    model = model.OrderBy(o => o.iOperatorID);
                    break;
            }

            IPagedList pagedListModel = model.ToPagedList(page, 25);

            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_OperatorList", pagedListModel);

            return View(pagedListModel);
        }

        // GET: Operators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Operators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("iOperatorID,sName,bActive")] Operator @operator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@operator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@operator);
        }

        [Authorize(Roles = "OperatorsEditor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Operator Operator = await _context.Operators.FindAsync(id);

            if (Operator == null)
                return NotFound();

            OperatorEditViewModel model = new OperatorEditViewModel
            {
                iOperatorID = Operator.iOperatorID,
                sName = Operator.sName
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "OperatorsEditor")]
        public async Task<IActionResult> Edit(OperatorEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Operator Operator = await _context.Operators.FindAsync(model.iOperatorID);
                Operator.sName = model.sName;

                try
                {
                    _context.Update(Operator);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!OperatorExists(Operator.iOperatorID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        // GET: Operators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @operator = await _context.Operators
                .FirstOrDefaultAsync(m => m.iOperatorID == id);
            if (@operator == null)
            {
                return NotFound();
            }

            return View(@operator);
        }

        // POST: Operators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @operator = await _context.Operators.FindAsync(id);
            _context.Operators.Remove(@operator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperatorExists(int id)
        {
            return _context.Operators.Any(e => e.iOperatorID == id);
        }
    }
}
