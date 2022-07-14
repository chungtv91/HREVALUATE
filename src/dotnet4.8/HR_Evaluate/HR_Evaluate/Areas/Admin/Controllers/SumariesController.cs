using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HR_Evaluate.Models;
using PagedList;
using PagedList.Mvc;

namespace HR_Evaluate.Areas.Admin.Controllers
{
    public class SumariesController : Controller
    {
        private HrEvaluateDatacontext db = new HrEvaluateDatacontext();

        // GET: Admin/Sumaries
        public ActionResult Index(int? page)
        {
            try
            {
                var sumResult = db.Sumaries.ToList();
                if (sumResult != null)
                {
                    int pagesize = 26;
                    int pagenumber = (page ?? 1);
                    return View(sumResult.ToPagedList(pagenumber, pagesize));
                }
                else
                    return View();
            }
            catch (Exception ex)
            {
                ViewBag.mes = ex.Message;
                return ViewBag.mes;
            }

            //var sumaries = db.Sumaries.Include(s => s.BOD).Include(s => s.Employee).Include(s => s.Question);
            //return View(sumaries.ToList());
        }

        // GET: Admin/Sumaries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sumary sumary = db.Sumaries.Find(id);
            if (sumary == null)
            {
                return HttpNotFound();
            }
            return View(sumary);
        }

        // GET: Admin/Sumaries/Create
        public ActionResult Create()
        {
            ViewBag.BodID = new SelectList(db.BODs, "Id", "Code");
            ViewBag.EmpID = new SelectList(db.Employees, "Id", "Code");
            ViewBag.QuestionID = new SelectList(db.Questions, "Id", "QuestionName");
            return View();
        }

        // POST: Admin/Sumaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,BodID,EmpID,QuestionID,AnswerName,Score,TotalScore,CreateDate,UpdateDate")] Sumary sumary)
        {
            if (ModelState.IsValid)
            {
                db.Sumaries.Add(sumary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BodID = new SelectList(db.BODs, "Id", "Code", sumary.BodID);
            ViewBag.EmpID = new SelectList(db.Employees, "Id", "Code", sumary.EmpID);
            ViewBag.QuestionID = new SelectList(db.Questions, "Id", "QuestionName", sumary.QuestionID);
            return View(sumary);
        }

        // GET: Admin/Sumaries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sumary sumary = db.Sumaries.Find(id);
            if (sumary == null)
            {
                return HttpNotFound();
            }
            ViewBag.BodID = new SelectList(db.BODs, "Id", "Code", sumary.BodID);
            ViewBag.EmpID = new SelectList(db.Employees, "Id", "Code", sumary.EmpID);
            ViewBag.QuestionID = new SelectList(db.Questions, "Id", "QuestionName", sumary.QuestionID);
            return View(sumary);
        }

        // POST: Admin/Sumaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BodID,EmpID,QuestionID,AnswerName,Score,TotalScore,CreateDate,UpdateDate")] Sumary sumary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sumary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BodID = new SelectList(db.BODs, "Id", "Code", sumary.BodID);
            ViewBag.EmpID = new SelectList(db.Employees, "Id", "Code", sumary.EmpID);
            ViewBag.QuestionID = new SelectList(db.Questions, "Id", "QuestionName", sumary.QuestionID);
            return View(sumary);
        }

        // GET: Admin/Sumaries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sumary sumary = db.Sumaries.Find(id);
            if (sumary == null)
            {
                return HttpNotFound();
            }
            return View(sumary);
        }

        // POST: Admin/Sumaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sumary sumary = db.Sumaries.Find(id);
            db.Sumaries.Remove(sumary);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ExportData()
        {
            System.Web.UI.WebControls.GridView gv = new System.Web.UI.WebControls.GridView();
            gv.DataSource = db.Sumaries.ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=SurveyResult.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            htw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Results");
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
