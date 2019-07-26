using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_Final.Models;
using ASP_Final.ViewModels;

namespace ASP_Final.Controllers
{
    public class NewsController : Controller
    {
        private readonly ASPFinalEntities1 db;
        public NewsController()
        {
            db = new ASPFinalEntities1();
        }
        // GET: News
        public ActionResult Index()
        {
            List<News> news = db.News.OrderByDescending(n => n.CreatedAt).ToList();
            return View(news);
        }
        public ActionResult Single(int? id)
        {
            if (id == null)
                return HttpNotFound();

            News news = db.News.Find(id);

            if (news == null)
                return HttpNotFound("This news not exists");
            return View(news);

        }
    }
}