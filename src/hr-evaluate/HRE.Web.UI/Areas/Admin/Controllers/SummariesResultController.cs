using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HRE.Core.Shared.Identity;
using HRE.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace HRE.Web.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    public class SummariesResultController : HrAdminController
    {
        private readonly HrDbContext _dbContext;

        public SummariesResultController(HrDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult> ListAllUser()
        {
            var listUserEvaluated = await _dbContext.ListEvaluateds.ToListAsync();
            return View(listUserEvaluated);
        }

        public async Task<IActionResult> Index(int id)
        {
            try
            {
                var UserInfo = _dbContext.Employees.SingleOrDefault(x => x.Id == id);

                var getNexPositionID = UserInfo.NextPositionId;

                var getNexLevelID = UserInfo.NextLevelId;
                ViewBag.NextPositionName = _dbContext.Positions.FirstOrDefault(x => x.Id == getNexPositionID);
                ViewBag.NextLevelName = _dbContext.Levels.FirstOrDefault(x => x.Id == getNexLevelID);
                ViewBag.ListQuestion = _dbContext.Questions.Where(x => x.LevelId == UserInfo.CurrentLevelId).OrderBy(x => x.SortOrder).ToList();
                ViewBag.Emp = _dbContext.Employees.SingleOrDefault(x => x.Id == id);
                ViewBag.listAnswer = _dbContext.Sumaries.Where(x => x.EmpID == id && x.BodID == 1).ToList();

                ViewBag.ListBOD = _dbContext.BODs.ToList();

                var getdataEvaluate1 = _dbContext.Sumaries.Where(x => x.EmpID == id && x.BodID == 1).OrderByDescending(x => x.UpdateDate).Take(13).ToList();
                ViewBag.listAnswer1 = getdataEvaluate1.OrderBy(x => x.QuestionID).ToList();

                var getdataEvaluate2 = _dbContext.Sumaries.Where(x => x.EmpID == id && x.BodID == 2).OrderByDescending(x => x.UpdateDate).Take(13).ToList();
                ViewBag.listAnswer2 = getdataEvaluate2.OrderBy(x => x.QuestionID).ToList();

                var getdataEvaluate3 = _dbContext.Sumaries.Where(x => x.EmpID == id && x.BodID == 3).OrderByDescending(x => x.UpdateDate).Take(13).ToList();
                ViewBag.listAnswer3 = getdataEvaluate3.OrderBy(x => x.QuestionID).ToList();

                var getdataEvaluate4 = _dbContext.Sumaries.Where(x => x.EmpID == id && x.BodID == 4).OrderByDescending(x => x.UpdateDate).Take(13).ToList();
                ViewBag.listAnswer4 = getdataEvaluate4.OrderBy(x => x.QuestionID).ToList();

                var getdataEvaluate5 = _dbContext.Sumaries.Where(x => x.EmpID == id && x.BodID == 5).OrderByDescending(x => x.UpdateDate).Take(13).ToList();
                ViewBag.listAnswer5 = getdataEvaluate5.OrderBy(x => x.QuestionID).ToList();

                var getdataEvaluate6 = _dbContext.Sumaries.Where(x => x.EmpID == id && x.BodID == 6).OrderByDescending(x => x.UpdateDate).Take(13).ToList();
                ViewBag.listAnswer6 = getdataEvaluate6.OrderBy(x => x.QuestionID).ToList();

                var getdataEvaluate7 = _dbContext.Sumaries.Where(x => x.EmpID == id && x.BodID == 7).OrderByDescending(x => x.UpdateDate).Take(13).ToList();
                ViewBag.listAnswer7 = getdataEvaluate7.OrderBy(x => x.QuestionID).ToList();

                //ViewBag.listAnswer1 = getUserEvaluate.Where(x => x.BodID == 1).OrderBy(x => x.QuestionID).Take(13).ToList();
                //ViewBag.listAnswer2 = _dbContext.Sumaries.Where(x => x.BodID == 2).OrderBy(x => x.QuestionID).Take(13).ToList();
                //ViewBag.listAnswer3 = _dbContext.Sumaries.Where(x => x.BodID == 3).OrderByDescending(x => x.UpdateDate).OrderBy(x => x.QuestionID).Take(13).ToList();
                //ViewBag.listAnswer4 = _dbContext.Sumaries.Where(x => x.BodID == 4).OrderByDescending(x => x.UpdateDate).OrderBy(x => x.QuestionID).Take(13).ToList();
                //ViewBag.listAnswer5 = _dbContext.Sumaries.Where(x => x.BodID == 5).OrderByDescending(x => x.UpdateDate).OrderBy(x => x.QuestionID).Take(13).ToList();
                //ViewBag.listAnswer6 = _dbContext.Sumaries.Where(x => x.BodID == 6).OrderByDescending(x => x.UpdateDate).OrderBy(x => x.QuestionID).Take(13).ToList();
                //ViewBag.listAnswer7 = _dbContext.Sumaries.Where(x => x.BodID == 7).OrderByDescending(x => x.UpdateDate).OrderBy(x => x.QuestionID).Take(13).ToList();

                //ViewBag.listAnswer = _dbContext.Sumaries.Where(x => x.EmpID == id && x.BodID == bodid).OrderBy(x => x.CreateDate).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }
    }
}