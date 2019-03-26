using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PmsEteck.Data.Models;
using PmsEteck.ViewModels;
using WebApp.Data;
using X.PagedList;

namespace PmsEteck.Controllers
{
    //TM
    [Authorize]
    public class EnergySuppliersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnergySuppliersController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[Authorize(Roles = "EnergySuppliersViewer")]
        public ActionResult Index(string sorting, string searchValue = "", int page = 1)
        {
            IQueryable<EnergySupplierIndexViewModel> model = _context.EnergySuppliers
                .Where(w => string.IsNullOrEmpty(searchValue)
                || w.iEnergySupplierID.ToString().Contains(searchValue)
                || w.sName.Contains(searchValue))
                .Select(s => new EnergySupplierIndexViewModel
                {
                    iEnergySupplierID = s.iEnergySupplierID,
                    sName = s.sName,
                    bActive = s.bActive,
                    iMeterCount = _context.ConsumptionMeters
                        .Where(w => w.iEnergySupplierID == s.iEnergySupplierID).Count()
                });

            switch (sorting)
            {
                case "KeyAsc":
                    model = model.OrderBy(o => o.iEnergySupplierID);
                    break;
                case "KeyDesc":
                    model = model.OrderByDescending(o => o.iEnergySupplierID);
                    break;
                case "NameAsc":
                    model = model.OrderBy(o => o.sName);
                    break;
                case "NameDesc":
                    model = model.OrderByDescending(o => o.sName);
                    break;
                case "ActiveAsc":
                    model = model.OrderBy(o => o.bActive).ThenBy(t => t.iEnergySupplierID);
                    break;
                case "ActiveDesc":
                    model = model.OrderByDescending(o => o.bActive).ThenBy(t => t.iEnergySupplierID);
                    break;
                case "CountAsc":
                    model = model.OrderBy(o => o.iMeterCount);
                    break;
                case "CountDesc":
                    model = model.OrderByDescending(o => o.iMeterCount);
                    break;
                default:
                    model = model.OrderBy(o => o.iEnergySupplierID);
                    break;
            }

            IPagedList pagedListModel = model.ToPagedList(page, 25);

            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_EnergySupplierList", pagedListModel);

            return View(pagedListModel);
        }

        //[Authorize(Roles = "EnergySuppliersCreator")]
        public ViewResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "EnergySuppliersCreator")]
        public async Task<IActionResult> Create(EnergySupplierCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.EnergySuppliers.Add(new EnergySupplier
                {
                    sName = model.sName
                });

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        //[Authorize(Roles = "EnergySuppliersEditor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return BadRequest();

            EnergySupplier energySupplier = _context.EnergySuppliers.Find(id);

            if (energySupplier == null)
                return NotFound();

            EnergySupplierEditViewModel model = new EnergySupplierEditViewModel
            {
                iEnergySupplierID = energySupplier.iEnergySupplierID,
                sName = energySupplier.sName
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "EnergySuppliersEditor")]
        public async Task<IActionResult> Edit(EnergySupplierEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                EnergySupplier energySupplier = _context.EnergySuppliers.Find(model.iEnergySupplierID);
                energySupplier.sName = model.sName;

                _context.Entry(energySupplier).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
                //_context = null;
            }

            base.Dispose(disposing);
        }
    }
}