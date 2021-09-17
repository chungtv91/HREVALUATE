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
    public class LevelsController : HrAdminController
    {
        private readonly HrDbContext _dbContext;

        public LevelsController(HrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Admin/Levels
        public async Task<IActionResult> Index()
        {
            return View(_dbContext.Levels.ToList());
        }

        // GET: Admin/Levels/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Level level = _dbContext.Levels.Find(id);
            if (level == null)
            {
                return NotFound();
            }
            return View(level);
        }

        // GET: Admin/Levels/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Admin/Levels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Level level)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Levels.Add(level);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(level);
        }

        // GET: Admin/Levels/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Level level = _dbContext.Levels.Find(id);
            if (level == null)
            {
                return NotFound();
            }
            return View(level);
        }

        // POST: Admin/Levels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Level level)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(level).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(level);
        }

        // GET: Admin/Levels/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Level level = _dbContext.Levels.Find(id);
            if (level == null)
            {
                return NotFound();
            }
            return View(level);
        }

        // POST: Admin/Levels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Level level = _dbContext.Levels.Find(id);
            _dbContext.Levels.Remove(level);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
