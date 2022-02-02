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
    public class NewspaperMastersController : Controller
    {
        private NewspaperEntities1 db = new NewspaperEntities1();

        // GET: NewspaperMasters for customer
        public ActionResult IndexCustomer()
        {
            return View(db.NewspaperMasters.ToList());
        }

        // GET: NewspaperMasters
        public ActionResult Index()
        {
            return View(db.NewspaperMasters.ToList());
        }

        //public ActionResult IndexCusSub()
        //{
        //    return View(db.Subscribeds.ToList());
        //}
        // GET: NewspaperMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewspaperMaster newspaperMaster = db.NewspaperMasters.Find(id);
            if (newspaperMaster == null)
            {
                return HttpNotFound();
            }
            return View(newspaperMaster);
        }

        // GET: NewspaperMasters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewspaperMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewspaperId,NewspaperName,IsActive,Price,CreatedBy,CreatedDateTime,LastEditedBy,LastEditedDateTime")] NewspaperMaster newspaperMaster)
        {
            if (ModelState.IsValid)
            {
                db.NewspaperMasters.Add(newspaperMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newspaperMaster);
        }

        // GET: NewspaperMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewspaperMaster newspaperMaster = db.NewspaperMasters.Find(id);
            if (newspaperMaster == null)
            {
                return HttpNotFound();
            }
            return View(newspaperMaster);
        }

        // POST: NewspaperMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewspaperId,NewspaperName,IsActive,Price,CreatedBy,CreatedDateTime,LastEditedBy,LastEditedDateTime")] NewspaperMaster newspaperMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newspaperMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newspaperMaster);
        }

        // GET: NewspaperMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewspaperMaster newspaperMaster = db.NewspaperMasters.Find(id);
            if (newspaperMaster == null)
            {
                return HttpNotFound();
            }
            return View(newspaperMaster);
        }

        // POST: NewspaperMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewspaperMaster newspaperMaster = db.NewspaperMasters.Find(id);
            db.NewspaperMasters.Remove(newspaperMaster);
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

        public ActionResult Subscribe(int? id)
        {
            NewspaperEntities1 db1 = new NewspaperEntities1();
            NewspaperMaster s = db.NewspaperMasters.Find(id);
            Subscribed ss = new Subscribed();
            //UserMaster L = new UserMaster();

            //var loggedinId = (int)Session["LogginedInUserId"];
            ss.UserId = (int)Session["LogginedInUserId"];
            ss.NewspaperId = s.NewspaperId;
            ss.NewspaperName = s.NewspaperName;
            ss.IsActive = (bool)s.IsActive;
            ss.Price = s.Price;
            ss.VendorId = Convert.ToInt32(s.VendorId);
            ss.MonthlyPrice = Convert.ToDecimal(s.MonthlyPrice);

            ss.StreetId = (from b in db1.StreetMasters join c in db1.UserMasters on b.StreetName equals c.StreetName where c.UserId.Equals(ss.UserId) select b.StreetId).FirstOrDefault();
        
            ss.DeleveryBoyId = (from a in db1.UseDetails where a.UserTypeId==3 && a.StreetId==ss.StreetId select a.UserId).Single();

            db.SaveChanges();

            db1.Subscribeds.Add(ss);

            db1.SaveChanges();
            return RedirectToAction("IndexCustomer", "NewspaperMasters");
        }
    }
}
