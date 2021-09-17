using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using HR_Evaluate.Models;
using HR_Evaluate.Membership;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Diagnostics;


namespace HR_Evaluate.Controllers
{
    public class LoginController : Controller
    {
        HrEvaluateDatacontext hr = new HrEvaluateDatacontext();
        // GET: Login
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        #region 


        //public async Task< ActionResult> Login(Login userlogin)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //           // var user = await UserManager.FindByNameAsync(userlogin.UserName);
        //            //var user = await UserManager(Login).FindByNameAsync(userlogin.UserName);


        //            if (Request.IsAjaxRequest())
        //            {
        //                if (User.IsInRole("Admin"))
        //                {
        //                    return new HttpStatusCodeResult(HttpStatusCode.OK, "Admin");
        //                }

        //                return new HttpStatusCodeResult(HttpStatusCode.OK, "member");
        //            }
        //            if (User.IsInRole("Admin"))
        //            {

        //                return  RedirectToAction("Index", new { controller = "Home", Area = "Admin" });
        //            }

        //            return RedirectToAction("Index", new { controller = "Home", Area = "" });


        //        }
        //        var getUserinfo = hr.Logins.FirstOrDefault(x => x.UserName == userlogin.UserName);

        //        int bodid = (int)getUserinfo.BODID;

        //        if (userlogin.UserName == getUserinfo.UserName && userlogin.Password == getUserinfo.Password)
        //        {
        //            return RedirectToAction("ListUser", "Home", new { id = getUserinfo.BODID });
        //        }
        //        else
        //        {
        //            return JavaScript("Incorrect Username Or Password");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        ViewBag.Error = ex.Message;
        //    }
        //    return View();
        //}
        #endregion
        #region 
        public ActionResult Login(Login userlogin)
        {
            try
            {
                #region MyRegion


                //if (ModelState.IsValid)
                //{

                //    if (Request.IsAjaxRequest())
                //    {
                //        if (User.IsInRole("Admin"))
                //        {
                //            return new HttpStatusCodeResult(HttpStatusCode.OK, "Admin");
                //        }

                //        return new HttpStatusCodeResult(HttpStatusCode.OK, "member");
                //    }
                //    if (User.IsInRole("Admin"))
                //    {

                //        return RedirectToAction("Index", new { controller = "Home", Area = "Admin" });
                //    }

                //    return RedirectToAction("Index", new { controller = "Home", Area = "" });
                //}
                #endregion

                var getUserinfo = hr.Logins.FirstOrDefault(x => x.UserName == userlogin.UserName);

                int bodid = (int)getUserinfo.BODID;
                if (userlogin.UserName != getUserinfo.UserName && userlogin.Password != getUserinfo.Password)
                {
                    return JavaScript("Incorrect Username Or Password");
                }
                else
                if (userlogin.UserName == "admin" && userlogin.Password == "123456")
                {
                    return RedirectToAction("Index", "Admin/AdminHome");
                }

                if (userlogin.UserName == getUserinfo.UserName && userlogin.Password == getUserinfo.Password)
                {
                    if (getUserinfo.IsEnable == 1)
                    {
                        return RedirectToAction("PromotionTime", "Home", new { id = getUserinfo.BODID });
                        //return RedirectToAction("ListUser", "Home", new { id = getUserinfo.BODID });
                    }
                    else
                    {
                        return JavaScript("Your account is Disable");
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
        #endregion

        [HttpPost]
        public ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(Login id)
        {
            var bodinfo = hr.Logins.SingleOrDefault(x => x.UserName == id.UserName);
            string bodname = hr.BODs.FirstOrDefault(x => x.Id == bodinfo.BODID).Name;
            bodinfo.Password = "123456";
            hr.SaveChanges();

            SendEmail(id.Password, bodname);

            return RedirectToAction("ResetSuccess", "Login");
        }
        [AllowAnonymous]
        public ActionResult ResetSuccess()
        {
            return View();
        }
        [AllowAnonymous]
        public static void SendEmail(string email, string name)
        {
            try
            {
                MailMessage nmail = new MailMessage();
                //nmail.From = new MailAddress("truong_van_Chung@hamadenvn.com", "Cigma Notification");
                nmail.From = new MailAddress("chung.truong.van.a1y@ap.denso.com", "HR Evaluate Password");
                //nmail.To.Add("que.luu.ngoc.a9j@ap.denso.com");
                nmail.To.Add("chung.truong.van.a1y@ap.denso.com");
                nmail.Subject = "HR Evaluate Password";
                //nmail.Body = EmailContent;
                #region
                nmail.Body = @"Dear Mr " + name + ", \n\n" +
                              "You has been reset password in HR EVALUATE Application.\n\n" +
                               "This is NEW PASSWORD: 123456\n\n" +
                               "Thanks and Best Regards\n\n";

                //nmail.Attachments.Add(new Attachment("D:\\Invoice921212.exe"));
                #endregion
                SmtpClient mailer1 = new SmtpClient("10.72.220.5", 25);
                mailer1.UseDefaultCredentials = true;
                //mailer1.Credentials = new NetworkCredential("hv14529", "uHV14529");
                mailer1.EnableSsl = false;
                mailer1.Send(nmail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }




    }
}