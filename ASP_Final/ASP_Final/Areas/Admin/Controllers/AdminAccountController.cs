using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ASP_Final.Models;

namespace ASP_Final.Areas.Admin.Controllers
{
    public class AdminAccountController : Controller
    {
        private readonly ASPFinalEntities1 db;

        public AdminAccountController()
        {
            db = new ASPFinalEntities1();
        }
        // GET: Admin/AdminAccount
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(string name, string password)
        {
            Models.Admin admin = db.Admins.FirstOrDefault(a => a.Name.Trim() == name.Trim());

            if (admin != null &&
                Crypto.VerifyHashedPassword(admin.Password, password))
            {
                Session["admin"] = admin;

                return RedirectToAction("Index", "Dashboard");
            }

            ViewBag.LoginError = "Admin name or password is wrong.";
            return View();
        }
    }
}