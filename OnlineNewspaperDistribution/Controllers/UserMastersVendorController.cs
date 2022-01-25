using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineNewspaperDistribution.Models;

namespace OnlineNewspaperDistribution.Controllers
{
    public class UserMastersVendorController : Controller
    {
        private NewspaperEntities1 db = new NewspaperEntities1();

        // GET: UserMastersVendor
        public ActionResult Index()
        {
            List<UserMaster> vendorlist = new List<UserMaster>();
            vendorlist = (from s in db.UserMasters where s.UserTypeId.Equals(2) select s).ToList();
            return View(vendorlist);
        }

        // GET: UserMastersVendor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = db.UserMasters.Find(id);
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            return View(userMaster);
        }

        // GET: UserMastersVendor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserMastersVendor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,EmailId,UserSalt,UserTypeId,Password,CreatedBy,CreatedDateTime,LastEditedBy,LastEditedDateTime")] UserMaster userMaster)
        {
            if (ModelState.IsValid)
            {
                db.UserMasters.Add(userMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userMaster);
        }

        // GET: UserMastersVendor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = db.UserMasters.Find(id);
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            return View(userMaster);
        }

        // POST: UserMastersVendor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,EmailId,UserSalt,UserTypeId,Password,CreatedBy,CreatedDateTime,LastEditedBy,LastEditedDateTime")] UserMaster userMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userMaster);
        }

        // GET: UserMastersVendor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = db.UserMasters.Find(id);
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            return View(userMaster);
        }

        // POST: UserMastersVendor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserMaster userMaster = db.UserMasters.Find(id);
            db.UserMasters.Remove(userMaster);
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
