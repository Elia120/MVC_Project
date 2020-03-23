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
    public class ManageProjectsController : Controller
    {
        private TotalTimesheetProContext db = new TotalTimesheetProContext();

        // GET: ManageProjects
        public ActionResult Index()
        {
            var temp = User.Identity.GetUserId();
            if (db.Clients.Where(x => x.TtpUserId == temp).SingleOrDefault() == null)
            {
                return RedirectToAction("Client", "Redirect");
            }
            var projects = db.Projects.Include(p => p.Client).Where(x => x.Client.TtpUserId==temp && x.IsActive==true);
            return View(projects.ToList());
        }

        // GET: ManageProjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            if (project.Client.TtpUserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(project);
        }

        // GET: ManageProjects/Create
        public ActionResult Create()
        {
            var temp = User.Identity.GetUserId();
            ViewBag.ClientID = new SelectList(db.Clients.Where(x=> x.TtpUserId==temp), "ClientID", "Name");
            return View();
        }

        // POST: ManageProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,ClientID,BillRate,IsActive,ProjectName")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var temp = User.Identity.GetUserId();
            ViewBag.ClientID = new SelectList(db.Clients.Where(x => x.TtpUserId == temp), "ClientID", "Name", project.ClientID);
            return View(project);
        }

        // GET: ManageProjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            if (project.Client.TtpUserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "Name", project.ClientID);
            return View(project);
        }

        // POST: ManageProjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectID,ClientID,BillRate,IsActive,ProjectName")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "Name", project.ClientID);
            return View(project);
        }

        // GET: ManageProjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            if (project.Client.TtpUserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(project);
        }

        // POST: ManageProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            if (project.Client.TtpUserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            db.Projects.Remove(project);
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
