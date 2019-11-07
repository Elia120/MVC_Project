using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Compuskills.Projects.TotalTimesheetPro.Mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
         
        }
        public ActionResult EnterTime()
        {
            return View();
        }
        public ActionResult ManageClients()
        {
            return View();
        }
        public ActionResult ManageProjects()
        {
            return View();
        }
        public ActionResult HoursReport()
        {
            return View();
        }
        public ActionResult BillingReport()
        {
            return View();
        }
    }
    

}