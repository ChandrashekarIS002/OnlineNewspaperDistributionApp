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
                    ModelState.AddModelError("Error", objUserLoginViewModel.EmailId + "is does not Exists.");
                    return View();

                }
                else
                {
                    var objuserMaster = objNewspaperEntities1.UserMasters.Single(model => model.EmailId == objUserLoginViewModel.EmailId);
                if(objuserMaster.Password==
                        objPbkdf2.Compute(objUserLoginViewModel.Password,objuserMaster.UserSalt))
                    {
                        string UserTypeName = objNewspaperEntities1.UserTypeMasters.Single(UserType => UserType.UserTypeId == objuserMaster.UserTypeId).UserTypeName;
                        Session["LoggedUserTypeId"] = objuserMaster.UserTypeId;
                        Session["LoggedUserTypeName"] =UserTypeName;
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

        // //Registration for Vendor

        [HttpGet]
        // [ValidateAntiForgeryToken]
        public ActionResult RegisterAsVendor()
        {
            //write linq to find usertypeid using "Vendor" and assign the value to tempdata
            TempData["UserTypeId"] = 2;
            return View();
        }
        [HttpPost]
        public ActionResult RegisterAsVendor(RegisterViewModel objRegisterViewModel)
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
                    objuserMaster.UserTypeId = (int)TempData["UserTypeId"];
                    TempData.Keep("UserTypeId");

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

        // //Registration for Customer

        [HttpGet]
        // [ValidateAntiForgeryToken]
        public ActionResult RegisterAsCustomer()
        {
            //write linq to find usertypeid using "Customer" and assign the value to tempdata
            TempData["UserTypeId"] = 4;
            return View();
        }

        [HttpPost]
        public ActionResult RegisterAsCustomer(RegisterViewModel objRegisterViewModel)
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
                    objuserMaster.UserTypeId = (int)TempData["UserTypeId"];
                    TempData.Keep("UserTypeId");

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

        //Admins menus

        public ActionResult AdminAddNewspaper()
        {
            return View();
        }

        public ActionResult AdminManageVendor()
        {
            return View();
        }

        public ActionResult AdminBillGeneration()
        {
            return View();
        }

        //Customers menus
        public ActionResult CustomerNewspaper()
        {
            return View();
        }

        public ActionResult CustomerSubscription()
        {
            return View();
        }

        public ActionResult CustomerFeedback()
        {
            return View();
        }

        //vendor menus

        public ActionResult VendorDeliveryBoy()
        {
            return View();
        }

        public ActionResult VendorCustomerSubscription()
        {
            return View();
        }

        public ActionResult VendorViewBill()
        {
            return View();
        }
    }
    }
