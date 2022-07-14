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
    public class BODsController : Controller
    {
        private HrEvaluateDatacontext db = new HrEvaluateDatacontext();

        // GET: Admin/BODs
        public ActionResult Index()
        {
            var bODs = db.BODs.Include(b => b.Department).Include(b => b.Level).Include(b => b.Position);
            return View(bODs.ToList());
        }

        // GET: Admin/BODs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOD bOD = db.BODs.Find(id);
            if (bOD == null)
            {
                return HttpNotFound();
            }
            return View(bOD);
        }

        // GET: Admin/BODs/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "Id", "DepartmentName");
            ViewBag.LevelID = new SelectList(db.Levels, "Id", "LevelName");
            ViewBag.PositionID = new SelectList(db.Positions, "Id", "PositionName");
            return View();
        }

        // POST: Admin/BODs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,Img,DepartmentID,PositionID,LevelID")] BOD bOD)
        {
            if (ModelState.IsValid)
            {
                db.BODs.Add(bOD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "Id", "DepartmentName", bOD.DepartmentID);
            ViewBag.LevelID = new SelectList(db.Levels, "Id", "LevelName", bOD.LevelID);
            ViewBag.PositionID = new SelectList(db.Positions, "Id", "PositionName", bOD.PositionID);
            return View(bOD);
        }

        // GET: Admin/BODs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOD bOD = db.BODs.Find(id);
            if (bOD == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "Id", "DepartmentName", bOD.DepartmentID);
            ViewBag.LevelID = new SelectList(db.Levels, "Id", "LevelName", bOD.LevelID);
            ViewBag.PositionID = new SelectList(db.Positions, "Id", "PositionName", bOD.PositionID);
            return View(bOD);
        }

        // POST: Admin/BODs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,Img,DepartmentID,PositionID,LevelID")] BOD bOD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bOD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "Id", "DepartmentName", bOD.DepartmentID);
            ViewBag.LevelID = new SelectList(db.Levels, "Id", "LevelName", bOD.LevelID);
            ViewBag.PositionID = new SelectList(db.Positions, "Id", "PositionName", bOD.PositionID);
            return View(bOD);
        }

        // GET: Admin/BODs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOD bOD = db.BODs.Find(id);
            if (bOD == null)
            {
                return HttpNotFound();
            }
            return View(bOD);
        }

        // POST: Admin/BODs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BOD bOD = db.BODs.Find(id);
            db.BODs.Remove(bOD);
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
