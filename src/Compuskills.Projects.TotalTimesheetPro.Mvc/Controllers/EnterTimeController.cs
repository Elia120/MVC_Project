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
using Microsoft.AspNet.Identity;

namespace Compuskills.Projects.TotalTimesheetPro.Mvc.Controllers
{
    [Authorize]
    public class EnterTimeController : Controller
    {
        private TotalTimesheetProContext db = new TotalTimesheetProContext();

        // GET: EnterTime
        public ActionResult Index()
        {
            var temp = User.Identity.GetUserId();
            var timesheetEntries = db.TimesheetEntries.Where(x => x.TtpUserId == temp).Include(t => t.Project).Include(t => t.TtpUser);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "Name");
            return View(timesheetEntries.ToList());
        }


        public ActionResult _Index(string datestring)
        {
            DateTime? date = DateTime.Today;
            if (datestring != null)
            {
                date = DateTime.Parse(datestring);
            }
            date = date.Value.Date;
            var enddate = date.Value.AddDays(1.0);
            var temp = User.Identity.GetUserId();
            var timesheetEntries = db.TimesheetEntries.Where(x => x.TtpUserId == temp).Where(x => x.StartTime >= date && x.StartTime < enddate).Include(x => x.Project).Include(x => x.Project.Client).AsEnumerable();
            return PartialView(timesheetEntries);

        }
        // GET: EnterTime/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "Name");

            var temp = User.Identity.GetUserId();
            var LastEntree = db.TimesheetEntries.Where(x => x.TtpUserId == temp).OrderByDescending(x => x.StartTime).FirstOrDefault();
            if (LastEntree == null)
            {
                ViewBag.Btn = "Start";
                return PartialView();
            }
            if (LastEntree.EndTime == null)
            {
                ViewBag.Btn = "Stop";
            }
            else
            {
                ViewBag.Btn = "Start";
            }
            return PartialView();
        }

        // POST: EnterTime/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(int? Client, int? Project)
        {
            if (!Client.HasValue || !Project.HasValue)
            {
                return PartialView();
            }
            var temp = User.Identity.GetUserId();
            var LastEntree = db.TimesheetEntries.Where(x => x.TtpUserId == temp).OrderByDescending(x => x.StartTime).FirstOrDefault();
            if (LastEntree == null)
            {
                CreateNewTse(Project.Value, Client.Value, temp);
            }
            else
            {
                if (LastEntree.EndTime == null)
                {
                    LastEntree.EndTime = DateTime.Now;
                    db.Entry(LastEntree).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    CreateNewTse(Project.Value, Client.Value, temp);
                }
            }

            LastEntree = db.TimesheetEntries.Where(x => x.TtpUserId == temp).OrderByDescending(x => x.StartTime).FirstOrDefault();
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "Name");
            if (LastEntree.EndTime == null)
            {
                ViewBag.Btn = "Stop";
            }
            else
            {
                ViewBag.Btn = "Start";
            }
            return PartialView(LastEntree);
        }

        // GET: EnterTime/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimesheetEntry timesheetEntry = db.TimesheetEntries.Find(id);
            if (timesheetEntry == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", timesheetEntry.ProjectID);
            ViewBag.TtpUserId = new SelectList(db.Users, "Id", "Email", timesheetEntry.TtpUserId);
            return View(timesheetEntry);
        }

        // POST: EnterTime/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimesheetEntryID,TtpUserId,ProjectID,StartTime,EndTime")] TimesheetEntry timesheetEntry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timesheetEntry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", timesheetEntry.ProjectID);
            ViewBag.TtpUserID = new SelectList(db.Users, "Id", "Email", timesheetEntry.TtpUserId);
            return View(timesheetEntry);
        }

        // GET: EnterTime/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimesheetEntry timesheetEntry = db.TimesheetEntries.Find(id);
            if (timesheetEntry == null)
            {
                return HttpNotFound();
            }
            return View(timesheetEntry);
        }

        // POST: EnterTime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimesheetEntry timesheetEntry = db.TimesheetEntries.Find(id);
            db.TimesheetEntries.Remove(timesheetEntry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public void CreateNewTse(int ProjectID, int ClientID, string UserID)
        {
            var Project = db.Projects.Find(ProjectID);
            if (Project.ClientID != ClientID)
            {
                return;
            }
            var Tse = new TimesheetEntry
            {
                StartTime = DateTime.Now,
                EndTime = null,
                ProjectID = ProjectID,
                TtpUserId = UserID
            };

            db.TimesheetEntries.Add(Tse);
            db.SaveChanges();
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
