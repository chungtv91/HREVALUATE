using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRE.Core.Identity;
using HRE.Core.Shared.Identity;
using HRE.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using HRE.Web.UI.Models;

namespace HRE.Web.UI.Controllers
{
    public class AccountController : HrController
    {
        private readonly HrDbContext _dbContext;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly CustomUserManager _userManager;

        public AccountController(HrDbContext dbContext, SignInManager<CustomUser> signInManager, CustomUserManager userManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: Login
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var roleName = user.Roles.Select(r => r.Name).FirstOrDefault();
                if (roleName == RoleNames.Admin)
                {
                    return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
                }

                if (!user.IsDisabled)
                {
                    return RedirectToAction("PromotionTime", "Home", new { id = user.BODID });
                }

                await _signInManager.SignOutAsync();
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Your account is disabled.");
            }
            else
            {
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Incorrect Username Or Password.");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, model.Password);

            SendEmail(user.Email, user.BOD.Name);

            return RedirectToAction("ResetSuccess", "Account");
        }

        [AllowAnonymous]
        public async Task<IActionResult> ResetSuccess()
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