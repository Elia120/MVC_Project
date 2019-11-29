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
            var temp= User.Identity.GetUserId();
            var timesheetEntries = db.TimesheetEntries.Where(x=> x.TtpUserId==temp).Include(t => t.Project).Include(t => t.TtpUser);
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "Name");
            return View(timesheetEntries.ToList());
        }

        

        // GET: EnterTime/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName");
            ViewBag.TtpUserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: EnterTime/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimesheetEntryID,TtpUserId,ProjectID,StartTime,EndTime")] TimesheetEntry timesheetEntry)
        {
            if (ModelState.IsValid)
            {
                db.TimesheetEntries.Add(timesheetEntry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectID = new SelectList(db.Projects, "ProjectID", "ProjectName", timesheetEntry.ProjectID);
            ViewBag.TtpUserId = new SelectList(db.Users, "Id", "Email", timesheetEntry.TtpUserId);
            return View(timesheetEntry);
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
            ViewBag.TtpUserId = new SelectList(db.Users, "Id", "Email", timesheetEntry.TtpUserId);
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
