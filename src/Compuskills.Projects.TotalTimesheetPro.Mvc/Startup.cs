using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Compuskills.Projects.TotalTimesheetPro.Mvc.Startup))]
namespace Compuskills.Projects.TotalTimesheetPro.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
