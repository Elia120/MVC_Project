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
    public class ManageClientsController : Controller
    {
        private TotalTimesheetProContext db = new TotalTimesheetProContext();

        // GET: ManageClients
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            var clients = db.Clients.Where(x => x.TtpUserId == user.Id);
            return View(clients.ToList());
        }

        // GET: ManageClients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            
            if (client == null)
            {
                return HttpNotFound();
            }
            if (client.TtpUserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            return View(client);
            
        }

        // GET: ManageClients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageClients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientID,Name")] Client client)
        {
            client.TtpUserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: ManageClients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            if (client.TtpUserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(client);
        }

        // POST: ManageClients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientID,Name,TtpUserId")] Client client)
        {
            if (client.TtpUserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: ManageClients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            if (client.TtpUserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            return View(client);
        }

        // POST: ManageClients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            if (client.TtpUserId != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            db.Clients.Remove(client);
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
