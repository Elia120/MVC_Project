using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Compuskills.Projects.TotalTimesheetPro.Mvc.Controllers
{
    [Authorize]
    public class RedirectController : Controller
    {
        
        public ActionResult Client()
        {
            return View();
        }
        
        public ActionResult Project()
        {
            return View();
        }
    }
}