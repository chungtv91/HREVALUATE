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
    public class PositionsController : HrAdminController
    {
        private readonly HrDbContext _dbContext;

        public PositionsController(HrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Admin/Positions
        public async Task<IActionResult> Index()
        {
            return View(_dbContext.Positions.ToList());
        }

        // GET: Admin/Positions/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Position position = _dbContext.Positions.Find(id);
            if (position == null)
            {
                return NotFound();
            }
            return View(position);
        }

        // GET: Admin/Positions/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Admin/Positions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Position position)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Positions.Add(position);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(position);
        }

        // GET: Admin/Positions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Position position = _dbContext.Positions.Find(id);
            if (position == null)
            {
                return NotFound();
            }
            return View(position);
        }

        // POST: Admin/Positions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Position position)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(position).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(position);
        }

        // GET: Admin/Positions/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Position position = _dbContext.Positions.Find(id);
            if (position == null)
            {
                return NotFound();
            }
            return View(position);
        }

        // POST: Admin/Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Position position = _dbContext.Positions.Find(id);
            _dbContext.Positions.Remove(position);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
