using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_Final.Models;
using ASP_Final.ViewModels;

namespace ASP_Final.Controllers
{
    public class PersonalController : Controller
    {
        private readonly ASPFinalEntities1 db;
        public PersonalController()
        {
            db = new ASPFinalEntities1();
        }
        // GET: Personal
        [AuthorizeUserFilter]
        public ActionResult Index()
        {
            int userID = (Session["user"] as User).ID;
            PersonalVM vm = new PersonalVM
            {
                Advertisments = db.Advertisments.Where(a => a.UserID == userID).OrderByDescending(a => a.UpdatedAt).ToList(),
                user = Session["user"] as User
            };
            return View(vm);
        }
        [AuthorizeUserFilter]
        public ActionResult PersonalDetails(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Advertisment advertisment = db.Advertisments.Find(id);

            if (advertisment == null)
            {
                return HttpNotFound("This advertisments not found");
            }

            DetailsVM vm = new DetailsVM
            {
                Advertisment = advertisment,
                RelatedAds = db.Advertisments.Where(a => a.Model.ID == advertisment.Model.ID && a.ID != advertisment.ID).Take(5).ToList(),
                ResentNewsVM = new ResentNewsVM
                {
                    News = db.News.Take(5).ToList()
                },
            };
            ViewBag.UserID = (Session["user"] as User).ID;
            return View(vm);
        }
    }
}