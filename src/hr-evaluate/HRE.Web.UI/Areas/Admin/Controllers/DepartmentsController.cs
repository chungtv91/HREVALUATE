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
    public class DepartmentsController : HrAdminController
    {
        private readonly HrDbContext _dbContext;

        public DepartmentsController(HrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Admin/Departments
        public async Task<IActionResult> Index()
        {
            return View(_dbContext.Departments.ToList());
        }

        // GET: Admin/Departments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Department department = _dbContext.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // GET: Admin/Departments/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Admin/Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Departments.Add(department);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Admin/Departments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Department department = _dbContext.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Admin/Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(department).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Admin/Departments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Department department = _dbContext.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Admin/Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Department department = _dbContext.Departments.Find(id);
            _dbContext.Departments.Remove(department);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
