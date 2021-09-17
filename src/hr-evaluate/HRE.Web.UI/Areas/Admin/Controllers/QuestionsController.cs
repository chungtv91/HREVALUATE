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
    public class QuestionsController : HrAdminController
    {
        private readonly HrDbContext _dbContext;

        public QuestionsController(HrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Admin/Questions
        public async Task<IActionResult> Index()
        {
            var questions = _dbContext.Questions.Include(q => q.Level);
            return View(questions.ToList());
        }

        // GET: Admin/Questions/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Question question = _dbContext.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // GET: Admin/Questions/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.LevelId = new SelectList(_dbContext.Levels, "Id", "LevelName");
            return View();
        }

        // POST: Admin/Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Question question)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Questions.Add(question);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LevelId = new SelectList(_dbContext.Levels, "Id", "LevelName", question.LevelId);
            return View(question);
        }

        // GET: Admin/Questions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Question question = _dbContext.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewBag.LevelId = new SelectList(_dbContext.Levels, "Id", "LevelName", question.LevelId);
            return View(question);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Question question)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Entry(question).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
                }
                ViewBag.LevelId = new SelectList(_dbContext.Levels, "Id", "LevelName", question.LevelId);
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(question);
        }

        // GET: Admin/Questions/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Question question = _dbContext.Questions.Find(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: Admin/Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Question question = _dbContext.Questions.Find(id);
            _dbContext.Questions.Remove(question);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
