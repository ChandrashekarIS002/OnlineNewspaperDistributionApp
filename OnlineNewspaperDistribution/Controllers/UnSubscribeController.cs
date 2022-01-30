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
    public class UnSubscribeController : Controller
    {
        private NewspaperEntities1 db = new NewspaperEntities1();

        // GET: UnSubscribe
        public ActionResult Index()
        {
            var loggiedinId = (int)Session["LogginedInUserId"];
            List<Subscribed> mylist = new List<Subscribed>();
            mylist = (from s in db.Subscribeds where s.UserId.Equals(loggiedinId) select s).ToList();
            //var subscribeds = db.Subscribeds.Include(s => s.Subscribed1).Include(s => s.Subscribed2);
           return View(mylist);
        }

        // GET: UnSubscribe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscribed subscribed = db.Subscribeds.Find(id);
            if (subscribed == null)
            {
                return HttpNotFound();
            }
            return View(subscribed);
        }

        // GET: UnSubscribe/Create
        public ActionResult Create()
        {
            ViewBag.SubscriptionId = new SelectList(db.Subscribeds, "SubscriptionId", "NewspaperName");
            ViewBag.SubscriptionId = new SelectList(db.Subscribeds, "SubscriptionId", "NewspaperName");
            return View();
        }

        // POST: UnSubscribe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubscriptionId,UserId,NewspaperId,NewspaperName,IsActive,Price,VendorId,MonthlyPrice")] Subscribed subscribed)
        {
            if (ModelState.IsValid)
            {
                db.Subscribeds.Add(subscribed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubscriptionId = new SelectList(db.Subscribeds, "SubscriptionId", "NewspaperName", subscribed.SubscriptionId);
            ViewBag.SubscriptionId = new SelectList(db.Subscribeds, "SubscriptionId", "NewspaperName", subscribed.SubscriptionId);
            return View(subscribed);
        }

        // GET: UnSubscribe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscribed subscribed = db.Subscribeds.Find(id);
            if (subscribed == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubscriptionId = new SelectList(db.Subscribeds, "SubscriptionId", "NewspaperName", subscribed.SubscriptionId);
            ViewBag.SubscriptionId = new SelectList(db.Subscribeds, "SubscriptionId", "NewspaperName", subscribed.SubscriptionId);
            return View(subscribed);
        }

        // POST: UnSubscribe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubscriptionId,UserId,NewspaperId,NewspaperName,IsActive,Price,VendorId,MonthlyPrice")] Subscribed subscribed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscribed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubscriptionId = new SelectList(db.Subscribeds, "SubscriptionId", "NewspaperName", subscribed.SubscriptionId);
            ViewBag.SubscriptionId = new SelectList(db.Subscribeds, "SubscriptionId", "NewspaperName", subscribed.SubscriptionId);
            return View(subscribed);
        }

        // GET: UnSubscribe/Delete/5
        public ActionResult UnSubscribe(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscribed subscribed = db.Subscribeds.Find(id);
            if (subscribed == null)
            {
                return HttpNotFound();
            }
            return View(subscribed);
        }

        // POST: UnSubscribe/Delete/5
        [HttpPost, ActionName("UnSubscribe")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subscribed subscribed = db.Subscribeds.Find(id);
            db.Subscribeds.Remove(subscribed);
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
