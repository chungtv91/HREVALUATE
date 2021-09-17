using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRE.Core.Entities;
using HRE.Core.Shared.Identity;
using HRE.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace HRE.Web.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    public class ManagerEmpsController : HrAdminController
    {
        private readonly HrDbContext _dbContext;

        public ManagerEmpsController(HrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Admin/ManagerEmps
        public async Task<IActionResult> Index()
        {
            var managerEmps = _dbContext.ManagerEmps.Include(m => m.BOD).Include(m => m.Employee);
            return View(managerEmps.ToList());
        }

        // GET: Admin/ManagerEmps/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ManagerEmp managerEmp = _dbContext.ManagerEmps.Find(id);
            if (managerEmp == null)
            {
                return NotFound();
            }
            return View(managerEmp);
        }

        // GET: Admin/ManagerEmps/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.BODID = new SelectList(_dbContext.BODs, "Id", "Code");
            ViewBag.EmployeeID = new SelectList(_dbContext.Employees, "Id", "Code");
            return View();
        }

        // POST: Admin/ManagerEmps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManagerEmp managerEmp)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ManagerEmps.Add(managerEmp);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BODID = new SelectList(_dbContext.BODs, "Id", "Code", managerEmp.BODID);
            ViewBag.EmployeeID = new SelectList(_dbContext.Employees, "Id", "Code", managerEmp.EmployeeID);
            return View(managerEmp);
        }

        // GET: Admin/ManagerEmps/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ManagerEmp managerEmp = _dbContext.ManagerEmps.Find(id);
            if (managerEmp == null)
            {
                return NotFound();
            }
            ViewBag.BODID = new SelectList(_dbContext.BODs, "Id", "Code", managerEmp.BODID);
            ViewBag.EmployeeID = new SelectList(_dbContext.Employees, "Id", "Code", managerEmp.EmployeeID);
            return View(managerEmp);
        }

        // POST: Admin/ManagerEmps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ManagerEmp managerEmp)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(managerEmp).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BODID = new SelectList(_dbContext.BODs, "Id", "Code", managerEmp.BODID);
            ViewBag.EmployeeID = new SelectList(_dbContext.Employees, "Id", "Code", managerEmp.EmployeeID);
            return View(managerEmp);
        }

        // GET: Admin/ManagerEmps/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            ManagerEmp managerEmp = _dbContext.ManagerEmps.Find(id);
            if (managerEmp == null)
            {
                return NotFound();
            }
            return View(managerEmp);
        }

        // POST: Admin/ManagerEmps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ManagerEmp managerEmp = _dbContext.ManagerEmps.Find(id);
            _dbContext.ManagerEmps.Remove(managerEmp);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
