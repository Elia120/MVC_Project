using Compuskills.Projects.TotalTimesheetPro.Domain.DataSource;
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
        private TotalTimesheetProContext db = new TotalTimesheetProContext();

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

        public ActionResult HoursReport()
        {
            return View();
        }
        public ActionResult BillingReport()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
    

}