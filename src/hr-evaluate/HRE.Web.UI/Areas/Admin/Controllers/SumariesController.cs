using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRE.Core.Entities;
using HRE.Core.Shared.Identity;
using HRE.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;

namespace HRE.Web.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    public class SumariesController : HrAdminController
    {
        private readonly HrDbContext _dbContext;

        public SumariesController(HrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Admin/Sumaries
        public async Task<IActionResult> Index(int? page)
        {
            try
            {
                var sumResult = _dbContext.Sumaries.ToList();
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

            //var sumaries = _dbContext.Sumaries.Include(s => s.BOD).Include(s => s.Employee).Include(s => s.Question);
            //return View(sumaries.ToList());
        }

        // GET: Admin/Sumaries/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Sumary sumary = _dbContext.Sumaries.Find(id);
            if (sumary == null)
            {
                return NotFound();
            }

            return View(sumary);
        }

        // GET: Admin/Sumaries/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.BodID = new SelectList(_dbContext.BODs, "Id", "Code");
            ViewBag.EmpID = new SelectList(_dbContext.Employees, "Id", "Code");
            ViewBag.QuestionID = new SelectList(_dbContext.Questions, "Id", "QuestionName");
            return View();
        }

        // POST: Admin/Sumaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sumary sumary)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Sumaries.Add(sumary);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BodID = new SelectList(_dbContext.BODs, "Id", "Code", sumary.BodID);
            ViewBag.EmpID = new SelectList(_dbContext.Employees, "Id", "Code", sumary.EmpID);
            ViewBag.QuestionID = new SelectList(_dbContext.Questions, "Id", "QuestionName", sumary.QuestionID);
            return View(sumary);
        }

        // GET: Admin/Sumaries/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Sumary sumary = _dbContext.Sumaries.Find(id);
            if (sumary == null)
            {
                return NotFound();
            }

            ViewBag.BodID = new SelectList(_dbContext.BODs, "Id", "Code", sumary.BodID);
            ViewBag.EmpID = new SelectList(_dbContext.Employees, "Id", "Code", sumary.EmpID);
            ViewBag.QuestionID = new SelectList(_dbContext.Questions, "Id", "QuestionName", sumary.QuestionID);
            return View(sumary);
        }

        // POST: Admin/Sumaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Sumary sumary)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(sumary).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BodID = new SelectList(_dbContext.BODs, "Id", "Code", sumary.BodID);
            ViewBag.EmpID = new SelectList(_dbContext.Employees, "Id", "Code", sumary.EmpID);
            ViewBag.QuestionID = new SelectList(_dbContext.Questions, "Id", "QuestionName", sumary.QuestionID);
            return View(sumary);
        }

        // GET: Admin/Sumaries/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Sumary sumary = _dbContext.Sumaries.Find(id);
            if (sumary == null)
            {
                return NotFound();
            }

            return View(sumary);
        }

        // POST: Admin/Sumaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Sumary sumary = _dbContext.Sumaries.Find(id);
            _dbContext.Sumaries.Remove(sumary);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ExportData()
        {
            //System.Web.UI.WebControls.GridView gv = new System.Web.UI.WebControls.GridView();
            //gv.DataSource = _dbContext.Sumaries.ToList();
            //gv.DataBind();
            //Response.ClearContent();
            //Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment; filename=SurveyResult.xls");
            //Response.ContentType = "application/ms-excel";
            //Response.Charset = "";
            //Response.ContentEncoding = System.Text.Encoding.UTF8;
            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            //htw.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            //gv.RenderControl(htw);
            //Response.Output.Write(sw.ToString());
            //Response.Flush();
            //Response.End();

            // return RedirectToAction("Results");
            return Content("");
        }
    }
}