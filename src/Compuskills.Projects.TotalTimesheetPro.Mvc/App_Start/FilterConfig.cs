using System.Web;
using System.Web.Mvc;

namespace Compuskills.Projects.TotalTimesheetPro.Mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
