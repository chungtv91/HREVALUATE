using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HR_Evaluate.Models;

namespace HR_Evaluate.Areas.Admin.Controllers
{
    public class EmployeesController : Controller
    {
        private HrEvaluateDatacontext db = new HrEvaluateDatacontext();

        // GET: Admin/Employees
        public ActionResult Index()
        {
            try
            {
                var employees = db.Employees;

                //var GetEmployee = from emp in db.Employees
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
            //var employees = db.Employees.Include(e => e.Department).Include(e => e.Level).Include(e => e.Position).Include(e=>e.Position);
        }

        // GET: Admin/Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Admin/Employees/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName");
            ViewBag.CurrentLevelId = new SelectList(db.Levels, "Id", "LevelName");
            ViewBag.CurrentPositionId = new SelectList(db.Positions, "Id", "PositionName");

            ViewBag.NextPositionId = new SelectList(db.Positions, "Id", "PositionName");
            ViewBag.NextLevelId = new SelectList(db.Levels, "Id", "LevelName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", employee.DepartmentId);
            ViewBag.CurrentLevelId = new SelectList(db.Levels, "Id", "LevelName", employee.CurrentLevelId);
            ViewBag.CurrentPositionId = new SelectList(db.Positions, "Id", "PositionName", employee.CurrentPositionId);

            ViewBag.NextPositionId = new SelectList(db.Positions, "Id", "PositionName", employee.NextPositionId);
            ViewBag.NextLevelId = new SelectList(db.Levels, "Id", "LevelName", employee.NextLevelId);
            //ViewBag.NextPositionId = new SelectList(db.Positions, "Id", "PositionName", employee.NextPositionId);
            return View(employee);
        }

        // GET: Admin/Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", employee.DepartmentId);
            ViewBag.CurrentLevelId = new SelectList(db.Levels, "Id", "LevelName", employee.CurrentLevelId);
            ViewBag.CurrentPositionId = new SelectList(db.Positions, "Id", "PositionName", employee.CurrentPositionId);

            ViewBag.NextPositionId = new SelectList(db.Positions, "Id", "PositionName", employee.NextPositionId);
            ViewBag.NextLevelId = new SelectList(db.Levels, "Id", "LevelName", employee.NextLevelId);

            return View(employee);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,Img,DepartmentId,CurrentPositionId,CurrentLevelId,DateEvaluate,Targets,NextPositionId,NextLevelId,IsEnable")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            } 
            
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", employee.DepartmentId);
            ViewBag.CurrentLevelId = new SelectList(db.Levels, "Id", "LevelName", employee.CurrentLevelId);
            ViewBag.CurrentPositionId = new SelectList(db.Positions, "Id", "PositionName", employee.CurrentPositionId);

            ViewBag.NextPositionId = new SelectList(db.Positions, "Id", "PositionName", employee.NextPositionId);
            ViewBag.NextLevelId = new SelectList(db.Levels, "Id", "LevelName", employee.NextLevelId);

            return View(employee);
        }

        // GET: Admin/Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Admin/Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
