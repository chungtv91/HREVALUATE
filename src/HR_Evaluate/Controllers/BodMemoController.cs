using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HR_Evaluate.Models;

namespace HR_Evaluate.Controllers
{
    public class BodMemoController : Controller
    {

        HrEvaluateDatacontext _dbcontext = new HrEvaluateDatacontext();

        // GET: BodMemo
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InsertBodMemo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertBodMemo(int id)
        {
            Session["bodid"] = id;
            TempData["ID"] = id;

            return View();
        }

        [HttpGet]
        public ActionResult UpdateBodMemo()
        {
       
            return View();
        }

        [HttpPost]
        public ActionResult UpdateBodMemo(int id)
        {
            Session["bodid"] = id;
            TempData["ID"] = id;
            return View();
        }
    }
}