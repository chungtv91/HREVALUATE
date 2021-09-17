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
    public class ManagerEmpsController : Controller
    {
        private HrEvaluateDatacontext db = new HrEvaluateDatacontext();

        // GET: Admin/ManagerEmps
        public ActionResult Index()
        {
            var managerEmps = db.ManagerEmps.Include(m => m.BOD).Include(m => m.Employee);
            return View(managerEmps.ToList());
        }

        // GET: Admin/ManagerEmps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagerEmp managerEmp = db.ManagerEmps.Find(id);
            if (managerEmp == null)
            {
                return HttpNotFound();
            }
            return View(managerEmp);
        }

        // GET: Admin/ManagerEmps/Create
        public ActionResult Create()
        {
            ViewBag.BODID = new SelectList(db.BODs, "Id", "Code");
            ViewBag.EmployeeID = new SelectList(db.Employees, "Id", "Code");
            return View();
        }

        // POST: Admin/ManagerEmps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EmployeeID,BODID")] ManagerEmp managerEmp)
        {
            if (ModelState.IsValid)
            {
                db.ManagerEmps.Add(managerEmp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BODID = new SelectList(db.BODs, "Id", "Code", managerEmp.BODID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "Id", "Code", managerEmp.EmployeeID);
            return View(managerEmp);
        }

        // GET: Admin/ManagerEmps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagerEmp managerEmp = db.ManagerEmps.Find(id);
            if (managerEmp == null)
            {
                return HttpNotFound();
            }
            ViewBag.BODID = new SelectList(db.BODs, "Id", "Code", managerEmp.BODID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "Id", "Code", managerEmp.EmployeeID);
            return View(managerEmp);
        }

        // POST: Admin/ManagerEmps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EmployeeID,BODID")] ManagerEmp managerEmp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(managerEmp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BODID = new SelectList(db.BODs, "Id", "Code", managerEmp.BODID);
            ViewBag.EmployeeID = new SelectList(db.Employees, "Id", "Code", managerEmp.EmployeeID);
            return View(managerEmp);
        }

        // GET: Admin/ManagerEmps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManagerEmp managerEmp = db.ManagerEmps.Find(id);
            if (managerEmp == null)
            {
                return HttpNotFound();
            }
            return View(managerEmp);
        }

        // POST: Admin/ManagerEmps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ManagerEmp managerEmp = db.ManagerEmps.Find(id);
            db.ManagerEmps.Remove(managerEmp);
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
