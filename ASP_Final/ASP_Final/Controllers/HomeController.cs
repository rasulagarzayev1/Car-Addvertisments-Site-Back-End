using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_Final.Models;
using ASP_Final.ViewModels;

namespace ASP_Final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ASPFinalEntities1 db;
        public HomeController()
        {
            db = new ASPFinalEntities1();
        }
        public ActionResult Index()
        {
            AddViewModel vm = new AddViewModel
            {
                Advertisments = db.Advertisments.OrderByDescending(a => a.UpdatedAt).ToList(),
                PremiumAdvertisments = db.Advertisments.Where(a => a.IsPremium == true).OrderByDescending(a => a.UpdatedAt).ToList(),
                Markas = db.Markas.ToList(),
                ResentNewsVM = new ResentNewsVM
                {
                    News = db.News.Take(5).ToList()
                }
            };
            return View(vm);
        }

        
    }
}