using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HR_Evaluate.Models;

namespace HR_Evaluate.Controllers
{
    //[Authorize]
    // [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly HrEvaluateDatacontext _dbContext = new HrEvaluateDatacontext();

        [HttpGet]
        public ActionResult Index(int id/*,int responseNo=1*/)
        {
            try
            {
                Session["empid"] = id;
                //TempData["responseNo"] = responseNo;

                var UserInfo = _dbContext.Employees.SingleOrDefault(x => x.Id == id);

                var getNexPositionID = UserInfo.NextPositionId;

                var getNexLevelID = UserInfo.NextLevelId;
                ViewBag.NextPositionName = _dbContext.Positions.FirstOrDefault(x => x.Id == getNexPositionID);
                ViewBag.NextLevelName = _dbContext.Levels.FirstOrDefault(x => x.Id == getNexLevelID);
                ViewBag.ListQuestion = _dbContext.Questions.Where(x => x.LevelId == UserInfo.CurrentLevelId).OrderBy(x => x.SortOrder).ToList();
                ViewBag.Emp = _dbContext.Employees.SingleOrDefault(x => x.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(ViewModel.HrEvaluateViewModel hrviewmodel)
        {
            try
            {
                //var checkAnswerisNull = hrviewmodel.VMlstQuestions.Any(x => x.VMAnswerName == null);
                //if (checkAnswerisNull)
                //{
                //    return JavaScript("全てコメントのところにコメントを入れてください -　The comment is not null or empty");
                //}
                var EmpID = (int)Session["empid"];

                var getEmpInfo = _dbContext.Employees.SingleOrDefault(x => x.Id == EmpID);

                int _currentLevelId = (int)getEmpInfo.CurrentLevelId;
                int _currentPositionID = (int)getEmpInfo.CurrentPositionId;
                int _NextLevelID = (int)getEmpInfo.NextLevelId;
                int _NextPositionId = (int)getEmpInfo.NextPositionId;


                foreach (var item in hrviewmodel.VMlstQuestions)
                {
                    var sum = new Sumary
                    {
                        BodID = (int)Session["bodid"],
                        EmpID = EmpID,
                        QuestionID = item.VMId,
                        AnswerName = item.VMAnswerName,
                        Score = 0,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Evaluatetimes = 1,
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
            return RedirectToAction("ListUser", new { id = (int)Session["bodid"] });
        }

        [HttpGet]
        public async Task<ActionResult> ListUser(int id)
        {
            //Session["bodid"] = id;
            //TempData["ID"] = id;
            var bodid = (int)Session["bodid"];
            TempData["EvaluateYearId"] = id;
            Session["EvaluateYearId"] = id;
            //var empid = (int)TempData["ID"];
            //TempData["EvaluateYearId"] = id;

            var users = await _dbContext.ManagerEmps
            .Where(x => x.BODID == bodid && x.EvaluateYearId == id)
            .Select(x => new ListUserViewModel
            {
                EmployeeId = x.EmployeeID.Value,
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

            var summaries = await _dbContext.Sumaries.Where(x => x.BodID == bodid).Select(x => new { x.EmpID, x.Evaluatetimes }).ToListAsync();
            foreach (var user in users)
            {
                user.HasSummary = summaries.Any(x => x.EmpID == user.EmployeeId);

            }

            return View(users);
        }

        [HttpGet]
        public ActionResult Success()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Review(int id/*, int responseNo=1*/)
        {
            try
            {
                var bodid = (int)Session["bodid"];

                TempData["empid"] = id;


                var UserInfo = _dbContext.Employees.SingleOrDefault(x => x.Id == id);

                var getNexPositionID = UserInfo.NextPositionId;

                var getNexLevelID = UserInfo.NextLevelId;
                ViewBag.NextPositionName = _dbContext.Positions.FirstOrDefault(x => x.Id == getNexPositionID);
                ViewBag.NextLevelName = _dbContext.Levels.FirstOrDefault(x => x.Id == getNexLevelID);
                ViewBag.ListQuestion = _dbContext.Questions.Where(x => x.LevelId == UserInfo.CurrentLevelId).OrderBy(x => x.SortOrder).ToList();
                ViewBag.Emp = _dbContext.Employees.SingleOrDefault(x => x.Id == id);
                ViewBag.listAnswer = _dbContext.Sumaries.Where(x => x.EmpID == id && x.BodID == bodid && x.Evaluatetimes == 1).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Review(ViewModel.HrEvaluateViewModel hrviewmodel)
        {
            try
            {
                //responseNo
                int bodid = (int)Session["bodid"];
                int empid = Convert.ToInt32(TempData["empid"]);
                var getSummary = _dbContext.Sumaries.Where(x => x.BodID == bodid && x.EmpID == empid && x.Evaluatetimes == 1).ToList();

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
            return RedirectToAction("ListUser", new { id = (int)Session["bodid"] });
        }

        [HttpGet]
        public ActionResult BodInsertMemo(int id)
        {
            TempData["empid"] = id;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> BodInsertMemo(BodMemo bodMemo)
        {
            try
            {
                var InsertBODMemo = new BodMemo
                {
                    Pros = bodMemo.Pros,
                    Cons = bodMemo.Cons,
                    CreateDate = DateTime.Now,
                    updatedate = DateTime.Now,
                    BodId = (int)Session["bodid"],
                    EmpId = (int)TempData["empid"],
                    MonthOfMemo = DateTime.Now.Month
                };
                _dbContext.BodMemoes.Add(InsertBODMemo);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return RedirectToAction("Success");
            //return RedirectToAction("ListUser", new { id = TempData["EvaluateYearId"] });
        }

        [HttpGet]
        public async Task<ActionResult> PromotionTime(int id)
        {
            Session["bodid"] = id;

            ViewBag.EvaluateYear = await _dbContext.EvaluateYears.OrderByDescending(x=>x.NextEvaluatePeriod).ToListAsync();
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ShowMemoHistory(int id)
        {
            try
            {
                var bodid = (int)Session["bodid"];

                ViewBag.GetMemoOfSept = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 9 && x.BodId == bodid && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfOct = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 10 && x.BodId == bodid && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfNov = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 11 && x.BodId == bodid && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfDec = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 12 && x.BodId == bodid && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfJan = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 1 && x.BodId == bodid && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfFeb = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 2 && x.BodId == bodid && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfMar = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 3 && x.BodId == bodid && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfApr = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 4 && x.BodId == bodid && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfMay = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 5 && x.BodId == bodid && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfJun = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 6 && x.BodId == bodid && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfJul = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 7 && x.BodId == bodid && x.EmpId == id).ToListAsync();
                ViewBag.GetMemoOfAug = await _dbContext.BodMemoes.Where(x => x.MonthOfMemo == 8 && x.BodId == bodid && x.EmpId == id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return View();
        }
    }
}