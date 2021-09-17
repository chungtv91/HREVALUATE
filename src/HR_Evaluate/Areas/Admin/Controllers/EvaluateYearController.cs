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
    public class EvaluateYearController : Controller
    {
        private readonly HrEvaluateDatacontext _dbContext = new HrEvaluateDatacontext();

        // GET: Admin/EvaluateYear
        public ActionResult Index()
        {
            var getEvaluateYear = _dbContext.EvaluateYears.ToList();
            return View(getEvaluateYear);

        }


        // GET: Admin/EvaluateYear/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/EvaluateYear/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,FromYear,ToYear")] EvaluateYear evaluateYear)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    _dbContext.EvaluateYears.Add(evaluateYear);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
            }
            return View(evaluateYear);

        }

        // GET: Admin/EvaluateYear/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvaluateYear evaluateYear = _dbContext.EvaluateYears.Find(id);
            if (evaluateYear == null)
            {
                return HttpNotFound();
            }

            return View(evaluateYear);

        }

        // POST: Admin/EvaluateYear/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,FromYear,ToYear")] EvaluateYear evaluateYear)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    _dbContext.Entry(evaluateYear).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

            }
            return View(evaluateYear);
        }

        // GET: Admin/EvaluateYear/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EvaluateYear evaluateYear = _dbContext.EvaluateYears.Find(id);
                if (evaluateYear == null)
                {
                    return HttpNotFound();
                }
                return View(evaluateYear);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // POST: Admin/EvaluateYear/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvaluateYear evaluateYear = _dbContext.EvaluateYears.Find(id);
            _dbContext.EvaluateYears.Remove(evaluateYear);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
