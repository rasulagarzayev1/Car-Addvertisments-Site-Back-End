using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_Final.Controllers;
using ASP_Final.Models;

namespace ASP_Final.Areas.Admin.Controllers
{
    public class AdvertismentsController : Controller
    {
        private readonly ASPFinalEntities1 db;

        public AdvertismentsController()
        {
            db = new ASPFinalEntities1();
        }
        // GET: Admin/Advertisments

        [AuthorizeAdminFilter]
        public ActionResult Index()
        {
            return View(db.Advertisments.ToList());
        }

        [AuthorizeAdminFilter]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Advertisment advertisment = db.Advertisments.Find(id);

            if (advertisment == null)
                return HttpNotFound("ID was not found");

            return View(advertisment);
        }

    }
}