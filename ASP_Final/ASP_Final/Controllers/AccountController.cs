using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ASP_Final.Models;
using ASP_Final.ViewModels;

namespace ASP_Final.Controllers
{
    public class AccountController : Controller
    {
        private readonly ASPFinalEntities1 db;
        public AccountController()
        {
            db = new ASPFinalEntities1();
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            User user = db.Users.FirstOrDefault(u => u.Email == email);

            if (user != null &&
                Crypto.VerifyHashedPassword(user.password, password))
            {
                Session["user"] = user;

                return RedirectToAction("Index", "Home");
            }

            ViewBag.LoginError = "Username or password is wrong.";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Register(UserMetadata user)
        {
            //if (String.IsNullOrEmpty(firstname))
            //{
            //    ViewBag.RegisterError="Firstname must be filled";
            //    return View();
            //}
            //if (String.IsNullOrEmpty(lastname))
            //{
            //    ViewBag.RegisterError="Lastname must be filled";
            //    return View();

            //}
            //if (String.IsNullOrEmpty(email)&&!email.Contains("@"))
            //{
            //    ViewBag.RegisterError="Email must be filled truely";
            //    return View();

            //}
            //if (String.IsNullOrEmpty(phone)&&phone.Length>6)
            //{
            //    ViewBag.RegisterError= "Phone number must be filled truely";
            //    return View();

            //}
            //if (String.IsNullOrEmpty(password))
            //{
            //    ViewBag.RegisterError="Password must be filled";
            //    return View();

            //}
            if (ModelState.IsValid)
            {
               
                db.Users.Add(new User
                {
                   Email = user.Email,
                   Firstname = user.Firstname,
                   Lastname = user.Lastname,
                   Phone = user.Phone,
                   password = user.password
                });
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View();
        }

    }
}
