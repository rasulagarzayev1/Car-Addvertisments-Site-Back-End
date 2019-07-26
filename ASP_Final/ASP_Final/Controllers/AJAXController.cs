using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_Final.Models;

namespace ASP_Final.Controllers
{
    public class AJAXController : Controller
    {
        private readonly ASPFinalEntities1 db;
        // GET: AJAX

        public AJAXController()
        {
            db = new ASPFinalEntities1();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadModelsPartial(int markaID)
        {
            return PartialView("_PartialModels", db.Models.Where(m => m.MarkaID == markaID));
        }
    }
}