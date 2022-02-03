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
    public class BillMastersController : Controller
    {
        private NewspaperEntities1 db = new NewspaperEntities1();


        public ActionResult Index()
        {
            return View(db.BillMasters.ToList());
        }
        public ActionResult IndexBill()
        {
            List<Subscribed> mylist = new List<Subscribed>();
            mylist = (from s in db.Subscribeds  select s).ToList();
            //var subscribeds = db.Subscribeds.Include(s => s.Subscribed1).Include(s => s.Subscribed2);
            return View(mylist);
            //return View(db.Subscribeds.ToList());
        }

        public ActionResult IndexVendor()
        {
            //List<BillDetail> mylist = new List<BillDetail>();
            //var loggedinId = (int)Session["LogginedInUserId"];
            ////mylist = (from a1 in db.BillMasters join a2 in db.BillDetails on a1.BillId equals a2.BillId where loggedinId == a2.VendorId select a1).ToList();
            //mylist = (from a in db.BillDetails where a.VendorId.Equals(loggedinId) select a).ToList();
            //return View(mylist);
            List<Subscribed> mylist = new List<Subscribed>();
            var loggedinId = (int)Session["LogginedInUserId"];
            mylist = (from a in db.Subscribeds where a.VendorId.Equals(loggedinId) select a).ToList();
            return View(mylist);
        }

        public ActionResult IndexCustomer()
        {
            List<BillMaster> mylist = new List<BillMaster>();
            var loggedinId = (int)Session["LogginedInUserId"];
            mylist = (from v in db.BillMasters where v.CustomerId==loggedinId select v).ToList();
            return View(mylist);
        }

        // GET: BillMasters
        public ActionResult Bill(int? id)
        {
            NewspaperEntities1 db1 = new NewspaperEntities1();
            Subscribed s = db.Subscribeds.Find(id);
          
            BillMaster ss = new BillMaster();

            db1.Entry(ss).State = EntityState.Deleted;
            db1.SaveChanges();

            var result = db1.Subscribeds.GroupBy(x => x.UserId)
              .Select(g => new {UserId = g.Key,TotalAmount = g.Sum(x => x.MonthlyPrice)}).ToList();

            foreach (var i in result)
            {
                ss.CustomerId = i.UserId;
                ss.TotalAmount = i.TotalAmount;

                db.SaveChanges();
                db1.BillMasters.Add(ss);
                db1.SaveChanges();
            }
            //return View();
            
            return RedirectToAction("Index", "BillMasters");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillMaster billMaster = db.BillMasters.Find(id);
            if (billMaster == null)
            {
                return HttpNotFound();
            }
            return View(billMaster);
        }

        // GET: BillMasters/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: BillMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            return View();
        }

        // GET: BillMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillMaster billMaster = db.BillMasters.Find(id);
            if (billMaster == null)
            {
                return HttpNotFound();
            }
            return View(billMaster);
        }

        // POST: BillMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BillId,CustomerId,TotalAmount,CreatedBy,CreatedDateTime,LastEditedBy,LastEditedDateTime")] BillMaster billMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(billMaster);
        }

        // GET: BillMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillMaster billMaster = db.BillMasters.Find(id);
            if (billMaster == null)
            {
                return HttpNotFound();
            }
            return View(billMaster);
        }

        // POST: BillMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BillMaster billMaster = db.BillMasters.Find(id);
            db.BillMasters.Remove(billMaster);
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
