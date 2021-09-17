using System;
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
    public class EmployeesController : HrAdminController
    {
        private readonly HrDbContext _dbContext;

        public EmployeesController(HrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Admin/Employees
        public async Task<IActionResult> Index()
        {
            try
            {
                var employees = _dbContext.Employees;

                //var GetEmployee = from emp in _dbContext.Employees
                //                  select new {emp.Department.DepartmentName,emp.Level.LevelName,emp.Position.PositionName,emp.Code,
                //                      emp.Name,emp.Img,emp.DateEvaluate,emp.Targets};
                //if (employees.Count() > 0)
                //{
                //    foreach (var item in employees)
                //    {
                //        if (item.Name == null ||item.Name=="")
                //        {
                //            item.Name = "test";
                //        }
                        
                //    }
                //}

                return View(employees.ToList());
            }
            catch (Exception)
            {

                throw;
            }
            //var employees = _dbContext.Employees.Include(e => e.Department).Include(e => e.Level).Include(e => e.Position).Include(e=>e.Position);
        }

        // GET: Admin/Employees/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Employee employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // GET: Admin/Employees/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.DepartmentId = new SelectList(_dbContext.Departments, "Id", "DepartmentName");
            ViewBag.CurrentLevelId = new SelectList(_dbContext.Levels, "Id", "LevelName");
            ViewBag.CurrentPositionId = new SelectList(_dbContext.Positions, "Id", "PositionName");

            ViewBag.NextPositionId = new SelectList(_dbContext.Positions, "Id", "PositionName");
            ViewBag.NextLevelId = new SelectList(_dbContext.Levels, "Id", "LevelName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Employees.Add(employee);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(_dbContext.Departments, "Id", "DepartmentName", employee.DepartmentId);
            ViewBag.CurrentLevelId = new SelectList(_dbContext.Levels, "Id", "LevelName", employee.CurrentLevelId);
            ViewBag.CurrentPositionId = new SelectList(_dbContext.Positions, "Id", "PositionName", employee.CurrentPositionId);

            ViewBag.NextPositionId = new SelectList(_dbContext.Positions, "Id", "PositionName", employee.NextPositionId);
            ViewBag.NextLevelId = new SelectList(_dbContext.Levels, "Id", "LevelName", employee.NextLevelId);
            //ViewBag.NextPositionId = new SelectList(_dbContext.Positions, "Id", "PositionName", employee.NextPositionId);
            return View(employee);
        }

        // GET: Admin/Employees/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Employee employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewBag.DepartmentId = new SelectList(_dbContext.Departments, "Id", "DepartmentName", employee.DepartmentId);
            ViewBag.CurrentLevelId = new SelectList(_dbContext.Levels, "Id", "LevelName", employee.CurrentLevelId);
            ViewBag.CurrentPositionId = new SelectList(_dbContext.Positions, "Id", "PositionName", employee.CurrentPositionId);

            ViewBag.NextPositionId = new SelectList(_dbContext.Positions, "Id", "PositionName", employee.NextPositionId);
            ViewBag.NextLevelId = new SelectList(_dbContext.Levels, "Id", "LevelName", employee.NextLevelId);

            return View(employee);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(employee).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            } 
            
            ViewBag.DepartmentId = new SelectList(_dbContext.Departments, "Id", "DepartmentName", employee.DepartmentId);
            ViewBag.CurrentLevelId = new SelectList(_dbContext.Levels, "Id", "LevelName", employee.CurrentLevelId);
            ViewBag.CurrentPositionId = new SelectList(_dbContext.Positions, "Id", "PositionName", employee.CurrentPositionId);

            ViewBag.NextPositionId = new SelectList(_dbContext.Positions, "Id", "PositionName", employee.NextPositionId);
            ViewBag.NextLevelId = new SelectList(_dbContext.Levels, "Id", "LevelName", employee.NextLevelId);

            return View(employee);
        }

        // GET: Admin/Employees/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Employee employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Admin/Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Employee employee = _dbContext.Employees.Find(id);
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
