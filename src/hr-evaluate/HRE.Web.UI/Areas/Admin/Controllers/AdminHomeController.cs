using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HRE.Core.Shared.Identity;

namespace HRE.Web.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    public class AdminHomeController : HrAdminController
    {
        // GET: Admin/AdminHome
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}