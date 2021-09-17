using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRE.Core.Entities;
using HRE.Core.Shared.Identity;
using HRE.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HRE.Web.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    public class BODsController : HrAdminController
    {
        private readonly HrDbContext _dbContext;

        public BODsController(HrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Admin/BODs
        public async Task<IActionResult> Index()
        {
            var bODs = _dbContext.BODs.ToList();
            return View(bODs);
        }

        // GET: Admin/BODs/Details/5
        public async Task<IActionResult> Details(int id)
        {
            BOD bOD = _dbContext.BODs.Find(id);
            if (bOD == null)
            {
                return NotFound();
            }
            return View(bOD);
        }

        // GET: Admin/BODs/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.DepartmentID = new SelectList(_dbContext.Departments, "Id", "DepartmentName");
            ViewBag.LevelID = new SelectList(_dbContext.Levels, "Id", "LevelName");
            ViewBag.PositionID = new SelectList(_dbContext.Positions, "Id", "PositionName");
            return View();
        }

        // POST: Admin/BODs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BOD bOD)
        {
            if (ModelState.IsValid)
            {
                _dbContext.BODs.Add(bOD);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(_dbContext.Departments, "Id", "DepartmentName", bOD.DepartmentID);
            ViewBag.LevelID = new SelectList(_dbContext.Levels, "Id", "LevelName", bOD.LevelID);
            ViewBag.PositionID = new SelectList(_dbContext.Positions, "Id", "PositionName", bOD.PositionID);
            return View(bOD);
        }

        // GET: Admin/BODs/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            BOD bOD = _dbContext.BODs.Find(id);
            if (bOD == null)
            {
                return NotFound();
            }
            ViewBag.DepartmentID = new SelectList(_dbContext.Departments, "Id", "DepartmentName", bOD.DepartmentID);
            ViewBag.LevelID = new SelectList(_dbContext.Levels, "Id", "LevelName", bOD.LevelID);
            ViewBag.PositionID = new SelectList(_dbContext.Positions, "Id", "PositionName", bOD.PositionID);
            return View(bOD);
        }

        // POST: Admin/BODs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BOD bOD)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(bOD).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(_dbContext.Departments, "Id", "DepartmentName", bOD.DepartmentID);
            ViewBag.LevelID = new SelectList(_dbContext.Levels, "Id", "LevelName", bOD.LevelID);
            ViewBag.PositionID = new SelectList(_dbContext.Positions, "Id", "PositionName", bOD.PositionID);
            return View(bOD);
        }

        // GET: Admin/BODs/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            BOD bOD = _dbContext.BODs.Find(id);
            if (bOD == null)
            {
                return NotFound();
            }
            return View(bOD);
        }

        // POST: Admin/BODs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            BOD bOD = _dbContext.BODs.Find(id);
            _dbContext.BODs.Remove(bOD);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
