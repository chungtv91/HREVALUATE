using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRE.Core.Entities;
using HRE.Core.Shared.Identity;
using HRE.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HRE.Web.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    public class EvaluateYearController : HrAdminController
    {
        private readonly HrDbContext _dbContext;

        public EvaluateYearController(HrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Admin/EvaluateYear
        public async Task<IActionResult> Index()
        {
            var getEvaluateYear = _dbContext.EvaluateYears.ToList();
            return View(getEvaluateYear);

        }


        // GET: Admin/EvaluateYear/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Admin/EvaluateYear/Create
        [HttpPost]
        public async Task<IActionResult> Create(EvaluateYear evaluateYear)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    _dbContext.EvaluateYears.Add(evaluateYear);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
            }
            return View(evaluateYear);

        }

        // GET: Admin/EvaluateYear/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            EvaluateYear evaluateYear = _dbContext.EvaluateYears.Find(id);
            if (evaluateYear == null)
            {
                return NotFound();
            }

            return View(evaluateYear);

        }

        // POST: Admin/EvaluateYear/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(EvaluateYear evaluateYear)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    _dbContext.Entry(evaluateYear).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

            }
            return View(evaluateYear);
        }

        // GET: Admin/EvaluateYear/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                EvaluateYear evaluateYear = _dbContext.EvaluateYears.Find(id);
                if (evaluateYear == null)
                {
                    return NotFound();
                }
                return View(evaluateYear);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: Admin/EvaluateYear/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            EvaluateYear evaluateYear = _dbContext.EvaluateYears.Find(id);
            _dbContext.EvaluateYears.Remove(evaluateYear);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
