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
    public class SubscribedsController : Controller
    {
        private NewspaperEntities1 db = new NewspaperEntities1();

        // GET: Subscribeds
        public ActionResult Index()
        {
            var loggiedinId = (int)Session["LogginedInUserId"];
            //List<Subscribed> mylist = new List<Subscribed>();
            //var mylist = (from s in db.Subscribeds join a in db.UserMasters on s.UserId equals a.UserId where s.VendorId.Equals(loggiedinId) 
            //              select new { a.UserName, s.NewspaperName, s.MonthlyPrice, a.StreetName, s.DeleveryBoyId }).ToList();
            //var subscribeds = db.Subscribeds.Include(s => s.Subscribed1).Include(s => s.Subscribed2);
            var mylist = (from s in db.Subscribeds
                          where s.VendorId.Equals(loggiedinId)
                          select s).ToList();
            return View(mylist);
            //return View(db.Subscribeds.ToList());
        }

        // GET: Subscribeds/Details/5
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

        // GET: Subscribeds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subscribeds/Create
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

            return View(subscribed);
        }

        // GET: Subscribeds/Edit/5
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
            return View(subscribed);
        }

        // POST: Subscribeds/Edit/5
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
            return View(subscribed);
        }

        // GET: Subscribeds/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Subscribeds/Delete/5
        [HttpPost, ActionName("Delete")]
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

        //public ActionResult Subscribe(int? id)
        //{
        //    NewspaperEntities1 db1 = new NewspaperEntities1();
        //    NewspaperMaster s = db.NewspaperMasters.Find(id);
        //    Subscribed ss = new Subscribed();
        //    //UserMaster L = new UserMaster();

        //    ss.UserId = Convert.ToInt32(Session["UserId"]);
        //    ss.NewspaperId = s.NewspaperId;
        //    ss.NewspaperName = s.NewspaperName;
        //    ss.Price = s.Price;
        //    ss.VendorId = Convert.ToInt32(s.VendorId);
        //    ss.MonthlyPrice = Convert.ToDecimal(s.MonthlyPrice);
        //    //IssueDetails.IssuedON = DateTime.Now;
        //    //IssueDetails.ReturnON = IssueDetails.IssuedON.AddDays(15);

        //    //book.Quantity = (book.Quantity - 1);
        //    db.SaveChanges();

        //    db1.Subscribeds.Add(ss);

        //    db1.SaveChanges();
        //    return RedirectToAction("Index", "Subscribedes");
        //}
    }
}
