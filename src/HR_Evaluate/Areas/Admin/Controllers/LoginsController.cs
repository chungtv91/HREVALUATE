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
    public class LoginsController : Controller
    {
        private HrEvaluateDatacontext db = new HrEvaluateDatacontext();

        // GET: Admin/Logins
        public ActionResult Index()
        {
            var logins = db.Logins.Include(l => l.BOD).Include(l => l.Role);
            return View(logins.ToList());
        }

        // GET: Admin/Logins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // GET: Admin/Logins/Create
        public ActionResult Create()
        {
            ViewBag.BODID = new SelectList(db.BODs, "Id", "Code");
            ViewBag.RoleID = new SelectList(db.Roles, "Id", "RoleName");
            return View();
        }

        // POST: Admin/Logins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Password,IsEnable,RoleID,BODID")] Login login)
        {
            if (ModelState.IsValid)
            {
                db.Logins.Add(login);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BODID = new SelectList(db.BODs, "Id", "Code", login.BODID);
            ViewBag.RoleID = new SelectList(db.Roles, "Id", "RoleName", login.RoleID);
            return View(login);
        }

        // GET: Admin/Logins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            ViewBag.BODID = new SelectList(db.BODs, "Id", "Code", login.BODID);
            ViewBag.RoleID = new SelectList(db.Roles, "Id", "RoleName", login.RoleID);
            return View(login);
        }

        // POST: Admin/Logins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Password,IsEnable,RoleID,BODID")] Login login)
        {
            if (ModelState.IsValid)
            {
                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BODID = new SelectList(db.BODs, "Id", "Code", login.BODID);
            ViewBag.RoleID = new SelectList(db.Roles, "Id", "RoleName", login.RoleID);
            return View(login);
        }

        // GET: Admin/Logins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Admin/Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Login login = db.Logins.Find(id);
            db.Logins.Remove(login);
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
