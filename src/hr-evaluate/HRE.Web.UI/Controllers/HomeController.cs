using System;
using HRE.EntityFrameworkCore;
using HRE.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HRE.Core.Entities;
using HRE.Core.Identity;
using HRE.Core.Shared.Identity;
using Microsoft.Extensions.Logging;

namespace HRE.Web.UI.Controllers
{
    [Authorize]
    public class HomeController : HrController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HrDbContext _dbContext;
        private readonly CustomUserManager _userManager;

        public HomeController(ILogger<HomeController> logger, HrDbContext dbContext, CustomUserManager customUser)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = customUser;
        }

        [Authorize]
        public async Task<IActionResult> Switcher()
        {
            if (User.IsInRole(RoleNames.Admin))
            {
                return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
            }

            var user = await _userManager.GetUserAsync(User);
            return RedirectToAction("PromotionTime", "Home", new { id = user.BODID });
        }

        [Authorize(Roles = RoleNames.BOD)]
        [HttpGet]
        public async Task<IActionResult> Index(int id, int responseNo = 1)
        {
            //TempData["responseNo"] = responseNo;

            var UserInfo = _dbContext.Employees.SingleOrDefault(x => x.Id == id);

            var getNexPositionID = UserInfo.NextPositionId;

            var getNexLevelID = UserInfo.NextLevelId;
            ViewBag.NextPositionName = _dbContext.Positions.FirstOrDefault(x => x.Id == getNexPositionID);
            ViewBag.NextLevelName = _dbContext.Levels.FirstOrDefault(x => x.Id == getNexLevelID);
            ViewBag.ListQuestion = _dbContext.Questions.Where(x => x.LevelId == UserInfo.CurrentLevelId).OrderBy(x => x.SortOrder).ToList();
            ViewBag.Emp = _dbContext.Employees.SingleOrDefault(x => x.Id == id);
            return View();
        }

        [Authorize(Roles = RoleNames.BOD)]
        [HttpPost]
        public async Task<ActionResult> Index(int id, HrEvaluateViewModel hrviewmodel)
        {
            var loggedUser = await _userManager.GetUserAsync(User);
            try
            {
                //var checkAnswerisNull = hrviewmodel.VMlstQuestions.Any(x => x.VMAnswerName == null);
                //if (checkAnswerisNull)
                //{
                //    return JavaScript("全てコメントのところにコメントを入れてください -　The comment is not null or empty");
                //}
                var EmpID = id;

                var getEmpInfo = _dbContext.Employees.SingleOrDefault(x => x.Id == EmpID);

                int _currentLevelId = (int)getEmpInfo.CurrentLevelId;
                int _currentPositionID = (int)getEmpInfo.CurrentPositionId;
                int _NextLevelID = (int)getEmpInfo.NextLevelId;
                int _NextPositionId = (int)getEmpInfo.NextPositionId;


                foreach (var item in hrviewmodel.VMlstQuestions)
                {
                    Sumary sum = new()
                    {
                        BodID = loggedUser.BODID.Value,
                        EmpID = EmpID,
                        QuestionID = item.VMId,
                        AnswerName = item.VMAnswerName,
                        Score = 0,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        //Evaluatetimes = item.,
                        CurrentLevelID = _currentLevelId,
                        CurrentPositionId = _currentPositionID,
                        NextLevelID = _NextLevelID,
                        NextPositionId = _NextPositionId
                    };
                    _dbContext.Sumaries.Add(sum);

                    if (item.NumberScore != null)
                    {
                        sum.Score = item.NumberScore;
                    }
                }

                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Success");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("ListUser", new { id = loggedUser.BODID.Value });
        }

        [Authorize(Roles = RoleNames.BOD)]
        [HttpGet]
        public async Task<ActionResult> ListUser(int id)
        {
            var loggedUser = await _userManager.GetUserAsync(User);
            var users = await _dbContext.ManagerEmps
            .Where(x => x.BODID == loggedUser.BODID && x.EvaluateYearId == id)
            .Select(x => new ListUserViewModel
            {
                EmployeeId = x.EmployeeID,
                EmployeeCode = x.Employee.Code,
                EmployeeName = x.Employee.Name,
                ProfileImage = x.Employee.Img,
                Department = x.Employee.Department.DepartmentName,
                Position = x.Employee.Position.PositionName,
                Level = x.Employee.Level.LevelName,
                IsEnable = x.Employee.IsEnable.Value,
                HasSummary = false
            })
            .ToListAsync();

            var summaries = await _dbContext.Sumaries.Where(x => x.BodID == loggedUser.BODID).Select(x => new { x.EmpID, x.Evaluatetimes }).ToListAsync();
            foreach (var user in users)
            {
                user.HasSummary = summaries.Any(x => x.EmpID == user.EmployeeId);

            }

            return View(users);
        }

        [Authorize(Roles = RoleNames.BOD)]
        [HttpGet]
        public async Task<IActionResult> Success(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            cancellationToken.ThrowIfCancellationRequested();
            return View();
        }

        [Authorize(Roles = RoleNames.BOD)]
        [HttpGet]
        public async Task<IActionResult> Review(int id, int bodId, int responseNo = 1)
        {
            // id: empid
            try
            {
                var UserInfo = _dbContext.Employees.SingleOrDefault(x => x.Id == id);

                var getNexPositionID = UserInfo.NextPositionId;

                var getNexLevelID = UserInfo.NextLevelId;
                ViewBag.NextPositionName = _dbContext.Positions.FirstOrDefault(x => x.Id == getNexPositionID);
                ViewBag.NextLevelName = _dbContext.Levels.FirstOrDefault(x => x.Id == getNexLevelID);
                ViewBag.ListQuestion = _dbContext.Questions.Where(x => x.LevelId == UserInfo.CurrentLevelId).OrderBy(x => x.SortOrder).ToList();
                ViewBag.Emp = _dbContext.Employees.SingleOrDefault(x => x.Id == id);
                ViewBag.listAnswer = _dbContext.Sumaries.Where(x => x.EmpID == id && x.BodID == bodId && x.Evaluatetimes == 1).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Review(HrEvaluateViewModel hrviewmodel)
        {
            var loggedUser = await _userManager.GetUserAsync(User);
            try
            {
                //responseNo
                int empid = Convert.ToInt32(TempData["empid"]);
                var getSummary = _dbContext.Sumaries.Where(x => x.BodID == loggedUser.BODID && x.EmpID == empid && x.Evaluatetimes == 1).ToList();

                if (getSummary != null)
                {
                    int count = 0;
                    foreach (var item in hrviewmodel.VMlstQuestions)
                    {
                        if (item.NumberScore != null)
                        {
                            getSummary[count].AnswerName = item.VMAnswerName;
                            getSummary[count].Score = item.NumberScore;
                            getSummary[count].TotalScore = 0;
                            getSummary[count].UpdateDate = DateTime.Now;
                        }
                        else
                        {
                            getSummary[count].AnswerName = item.VMAnswerName;
                            getSummary[count].Score = 0;
                            getSummary[count].TotalScore = 0;
                            getSummary[count].UpdateDate = DateTime.Now;
                        }
                        count++;
                    }

                    var changesCount = await _dbContext.SaveChangesAsync();

                    return RedirectToAction("Success");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return RedirectToAction("ListUser", new { id = loggedUser.BODID });
        }

        [Authorize(Roles = RoleNames.BOD)]
        [HttpGet]
        public async Task<IActionResult> BodInsertMemo(int id)
        {
            var loggedUser = await _userManager.GetUserAsync(User);
            BodMemo model = new()
            {
                BodId = loggedUser.BODID.Value
            };
            return View(model);
        }

        [Authorize(Roles = RoleNames.BOD)]
        [HttpPost]
        public async Task<ActionResult> BodInsertMemo(int id, BodMemo bodMemo)
        {
            var loggedUser = await _userManager.GetUserAsync(User);
            var InsertBODMemo = new BodMemo
            {
                Pros = bodMemo.Pros,
                Cons = bodMemo.Cons,
                CreateDate = DateTime.Now,
                updatedate = DateTime.Now,
                BodId = loggedUser.BODID.Value,
                EmpId = id,
                MonthOfMemo = DateTime.Now.Month,
            };
            _dbContext.BodMemoes.Add(InsertBODMemo);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Success");
        }

        [Authorize(Roles = RoleNames.BOD)]
        [HttpGet]
        public async Task<ActionResult> PromotionTime(int id)
        {
            // id: bodid
            ViewBag.EvaluateYear = await _dbContext.EvaluateYears.OrderByDescending(x => x.NextEvaluatePeriod).ToListAsync();
            return View();
        }

        [Authorize(Roles = RoleNames.BOD)]
        [HttpGet]
        public async Task<ActionResult> ShowMemoHistory(int id, int? bodId = null)
        {
            // id: EmpId
            try
            {
                ViewBag.GetMemoOfSept = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 9 && x.BodId == bodId && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfOct = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 10 && x.BodId == bodId && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfNov = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 11 && x.BodId == bodId && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfDec = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 12 && x.BodId == bodId && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfJan = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 1 && x.BodId == bodId && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfFeb = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 2 && x.BodId == bodId && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfMar = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 3 && x.BodId == bodId && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfApr = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 4 && x.BodId == bodId && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfMay = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 5 && x.BodId == bodId && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfJun = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 6 && x.BodId == bodId && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfJul = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 7 && x.BodId == bodId && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfAug = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 8 && x.BodId == bodId && x.EmpId == id).ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}