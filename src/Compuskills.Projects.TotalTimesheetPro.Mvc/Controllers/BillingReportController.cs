using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Compuskills.Projects.TotalTimesheetPro.Domain.DataSource;
using Compuskills.Projects.TotalTimesheetPro.Domain.Models;
using Compuskills.Projects.TotalTimesheetPro.Mvc.Models;
using Microsoft.AspNet.Identity;

namespace Compuskills.Projects.TotalTimesheetPro.Mvc.Controllers
{
    [Authorize]

    public class BillingReportController : Controller
    {
        private TotalTimesheetProContext db = new TotalTimesheetProContext();
        // GET: BillingReport
        public ActionResult Index()
        {
            return View();
        }

        // GET: BillingReport/GetReport
        [HttpGet]
        public ActionResult GetReport(string StartDateString, string EndDateString)
        {
            DateTime StartDate;
            DateTime EndDate;
            List<BillingReportViewModel> Brl = new List<BillingReportViewModel>();
            if (DateTime.TryParse(StartDateString, out StartDate) && DateTime.TryParse(EndDateString, out EndDate))
            {
                var temp = User.Identity.GetUserId();
                var Tse = db.TimesheetEntries.Where(x => x.Project.Client.TtpUserId == temp).Where(x => x.StartTime > StartDate && x.StartTime < EndDate);
                foreach (var item in Tse)
                {
                    if (item.EndTime==null)
                    {
                        item.EndTime = DateTime.Now;
                    }
                    var TempEntree = Brl.Where(x => x.Project.ProjectID == item.ProjectID).FirstOrDefault();
                    if (TempEntree==null)
                    {
                        Brl.Add(new BillingReportViewModel
                        {
                            Project=item.Project,
                            Client=item.Project.Client,
                            BillingRate=item.Project.BillRate,
                            TotalHours=(item.EndTime-item.StartTime)
                        });
                    }
                    else
                    {
                        TempEntree.TotalHours += (item.EndTime - item.StartTime);
                    }
                }
                foreach (var item in Brl)
                {
                    item.Total = (decimal)item.TotalHours.Value.TotalHours * item.BillingRate;
                }
            }
            return PartialView(Brl.AsEnumerable());
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
