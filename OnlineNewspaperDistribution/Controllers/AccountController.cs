using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using OnlineNewspaperDistribution.Models;
using OnlineNewspaperDistribution.ViewModels;

namespace OnlineNewspaperDistribution.Controllers
{
    public class AccountController : Controller
    {
        //private NewspaperEntities1 db = new NewspaperEntities1();
        private NewspaperEntities1 objNewspaperEntities1;
        private SimpleCrypto.PBKDF2 objPbkdf2;

        public AccountController()
        {
            objNewspaperEntities1 = new NewspaperEntities1();
            objPbkdf2 = new SimpleCrypto.PBKDF2();
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
            //return View(db.UserMasters.ToList());
        }
        [HttpPost]
        public ActionResult Login(UserLoginViewModel objUserLoginViewModel)
        {
            if(ModelState.IsValid)
            {
                if (!objNewspaperEntities1.UserMasters.Any(model => model.EmailId == objUserLoginViewModel.EmailId))
                {
                    ModelState.AddModelError("Error", objUserLoginViewModel.EmailId + "is Already Exists.");
                    return View();

                }
                else
                {
                    var objuserMaster = objNewspaperEntities1.UserMasters.Single(model => model.EmailId == objUserLoginViewModel.EmailId);
                if(objuserMaster.Password==
                        objPbkdf2.Compute(objUserLoginViewModel.Password,objuserMaster.UserSalt))
                    {
                        AddFormAuthentication(objUserLoginViewModel.EmailId);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "EmailId and Password not valid.");
                        return View("Index");
                    }
                }

            }
            return View();
        }

       [HttpGet]
      // [ValidateAntiForgeryToken]
       public ActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Register(RegisterViewModel objRegisterViewModel)
        {
            if (ModelState.IsValid)
            {
                if (objNewspaperEntities1.UserMasters.Any(model => model.UserName == objRegisterViewModel.UserName))
                {
                    ModelState.AddModelError("Error", objRegisterViewModel.UserName + "is Already Exists.");
                    return View();

                }
                else
                {
                    string UserSalt = objPbkdf2.GenerateSalt();
                    UserMaster objuserMaster = new UserMaster();
                    //objuserMaster.UserId = Guid.NewGuid();
                    objuserMaster.UserName = objRegisterViewModel.UserName;
                    objuserMaster.EmailId = objRegisterViewModel.EmailId;

                    objuserMaster.UserSalt = UserSalt;
                    objuserMaster.Password = objPbkdf2.Compute(objRegisterViewModel.Password, UserSalt);
                    objNewspaperEntities1.UserMasters.Add(objuserMaster);
                    objNewspaperEntities1.SaveChanges();
                    AddFormAuthentication(objRegisterViewModel.UserName);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        private void AddFormAuthentication(string UserName)
        {
            FormsAuthentication.SetAuthCookie(UserName, false);
            var authTicket = new FormsAuthenticationTicket(1, UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, string.Empty, FormsAuthentication.FormsCookiePath);
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Response.Cookies.Add(authCookie);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

            /* // GET: Account/Details/5
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

             // GET: Account/Create
             public ActionResult Create()
             {
                 return View();
             }

             // POST: Account/Create
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

             // GET: Account/Edit/5
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

             // POST: Account/Edit/5
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

             // GET: Account/Delete/5
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

             // POST: Account/Delete/5
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
             }*/
        }
    }
