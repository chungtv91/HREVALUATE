using HR_Evaluate.Models;
using Microsoft.Owin;
using Owin;
using System;

[assembly:OwinStartupAttribute(typeof(HR_Evaluate.Startup))]
namespace HR_Evaluate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            
        }
    }
}