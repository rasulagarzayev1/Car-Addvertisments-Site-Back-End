using ASP_Final.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_Final.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        [AuthorizeAdminFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}