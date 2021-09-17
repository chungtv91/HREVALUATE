using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace HR_Evaluate.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {

        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "admin/{controller}/{action}/{id}",
                new { controller = "AdminHome", action = "Index", id = UrlParameter.Optional },
                constraints: new { },
                namespaces: new[] { "HR_Evaluate.Areas.Admin.Controllers" }
                );
        }
    }
}