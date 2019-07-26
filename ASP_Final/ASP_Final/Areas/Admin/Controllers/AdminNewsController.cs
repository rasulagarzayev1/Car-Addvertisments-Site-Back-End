using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Schema;
using ASP_Final.Models;
using ASP_Final.Extensions;
using System.Data.Entity;
using ASP_Final.Controllers;

namespace ASP_Final.Areas.Admin.Controllers
{
    public class AdminNewsController : Controller
    {
        private readonly ASPFinalEntities1 db;
        // GET: Admin/News
        public AdminNewsController()
        {
            db = new ASPFinalEntities1();
        }
        [AuthorizeAdminFilter]
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        [AuthorizeAdminFilter]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeAdminFilter]
        public ActionResult Create([Bind(Exclude = "Image")] News news, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {

                if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        news.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Images/news"), Image);

                        db.News.Add(news);
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "The type of image is incorrect or the size of image is greater than 10 Mb.");
                    }
                
               

            }

            return View();
        }

        [AuthorizeAdminFilter]
        public ActionResult Details(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            News news = db.News.Find(id);

            if (news == null)
                return HttpNotFound("ID was not found");

            return View(news);
        }

        [AuthorizeAdminFilter]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            News news = db.News.Find(id);

            if (news == null)
                return HttpNotFound("ID was not found");

            return View(news);
        }

        [HttpPost]
        [AuthorizeAdminFilter]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            News news = db.News.Find(id);

            if (!Extensions.Extensions.DeleteImage(Server.MapPath("~/Images/news"), news.Image))
            {
                ViewBag.DeleteError = "File doesn't exist";
                return View();
            }

            db.News.Remove(news);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        [AuthorizeAdminFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            News news = db.News.Find(id);
           
            if (news == null)
                return HttpNotFound("ID was not found");



            return View(news);
        }

        [HttpPost]
        [AuthorizeAdminFilter]
        public ActionResult Edit([Bind(Exclude = "Image")] News news, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image!=null)
                {
                    if (Extensions.Extensions.CheckImageType(Image) && Extensions.Extensions.CheckImageSize(Image, 10))
                    {
                        news.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Images/news"), Image);


                        db.Entry(news).State = EntityState.Modified;
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "Please choose image");
                    }
                }
                else
                {
                    ModelState.AddModelError("Image", "The type of image is incorrect or the size of image is greater than 10 Mb.");

                }

            }
            return View();
        }
    }
}