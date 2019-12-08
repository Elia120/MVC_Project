using Compuskills.Projects.TotalTimesheetPro.Domain.DataSource;
using Compuskills.Projects.TotalTimesheetPro.Mvc.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Compuskills.Projects.TotalTimesheetPro.Mvc.Controllers
{
    public class HoursReportController : Controller
    {
        private TotalTimesheetProContext db = new TotalTimesheetProContext();
        // GET: HoursReport
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetHours(string StartDateString, string EndDateString)
        {
            DateTime StartDate;
            DateTime EndDate;
            if (DateTime.TryParse(StartDateString,out StartDate) && DateTime.TryParse(EndDateString, out EndDate))
            {
                var temp = User.Identity.GetUserId();
                var Tse = db.TimesheetEntries.Where(x=> x.Project.Client.TtpUserId==temp).Where(x => x.StartTime > StartDate && x.StartTime < EndDate);
                TimeSpan? HoursWorked = new TimeSpan(0, 0, 0, 0, 0);
                DateTime ForeachDate =StartDate;
                int DaysWorked = 0;
                foreach (var item in Tse)
                {
                    TimeSpan? TempTime;
                    if (item.EndTime==null)
                    {
                        TempTime = DateTime.Now - item.StartTime;
                    }
                    else
                    {
                       TempTime = item.EndTime - item.StartTime;
                    }
                    
                    HoursWorked+=TempTime;
                    if (ForeachDate.Date!=item.StartTime.Date)
                    {
                        DaysWorked++;
                        ForeachDate = item.StartTime.Date;
                    }
                }
                return PartialView(new HoursReportGetHoursViewModels
                {
                    DaysWorked=DaysWorked,
                    Hours=HoursWorked
                });
            }
            return null;
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
