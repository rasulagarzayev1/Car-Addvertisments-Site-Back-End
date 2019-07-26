using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ASP_Final.Models;
using ASP_Final.ViewModels;
using ASP_Final.Extensions;
using System.Web.Helpers;
using System.Data.Entity;

namespace ASP_Final.Controllers
{
    public class AddvertismentsController : Controller
    {
        private readonly ASPFinalEntities1 db;
        public AddvertismentsController()
        {
            db = new ASPFinalEntities1();
        }
        // GET: Cars
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id==null)
            {
                return HttpNotFound();
            }

            Advertisment advertisment = db.Advertisments.Find(id);

            if (advertisment==null)
            {
                return HttpNotFound("This advertisments not found");
            }
            
            DetailsVM vm = new DetailsVM
            {
                Advertisment = advertisment,
                RelatedAds = db.Advertisments.Where(a => a.Model.ID == advertisment.Model.ID && a.ID!=advertisment.ID).Take(5).ToList(),
                ResentNewsVM = new ResentNewsVM
                {
                    News = db.News.Take(5).ToList()
                },
            };
            return View(vm);
        }

        [AuthorizeUserFilter]
        public ActionResult CreateAds()
        {
           
            ViewBag.Markas = new SelectList(db.Markas, "ID", "Markaname");
            ViewBag.Models = new SelectList(db.Models, "ID", "ModelName");
            ViewBag.Cities = new SelectList(db.Cities, "ID", "Name");
            ViewBag.EngineCapacities = new SelectList(db.EngineCapacities, "ID", "Capacity");
            ViewBag.Colors = new SelectList(db.Colors, "ID", "Colorname");
            ViewBag.SpeedBoxes = new SelectList(db.SpeedBoxes, "ID", "BoxType");
            ViewBag.Fueltypes = new SelectList(db.Fueltypes, "ID", "Fueltype1");

            return View();
        }


        [AuthorizeUserFilter]
        [HttpPost]
        public ActionResult CreateAds([Bind(Exclude = "Image")] Advertisment advertisment,HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
               
                if (Image!=null)
                {
                    if (Extensions.Extensions.CheckImageType(Image))
                    {
                        advertisment.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Images/Ads"), Image);
                        advertisment.CreatedAt = advertisment.UpdatedAt = DateTime.Now;
                        advertisment.UserID = (Session["user"] as User).ID;

                        db.Advertisments.Add(advertisment);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "The type of image is incorrect or the size of image is greater than 10 Mb.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Image", "Please choose image.");
                }


            }
            return View();
        }


        [AuthorizeUserFilter]
        public ActionResult EditAds(int? id)
        {
            if (id == null)
                return HttpNotFound("ID is missing");

            Advertisment advertisment = db.Advertisments.Find(id);

            if (advertisment == null)
                return HttpNotFound("ID was not found");

            ViewBag.Markas = new SelectList(db.Markas, "ID", "Markaname");
            ViewBag.Models = new SelectList(db.Models, "ID", "ModelName");
            ViewBag.Cities = new SelectList(db.Cities, "ID", "Name");
            ViewBag.EngineCapacities = new SelectList(db.EngineCapacities, "ID", "Capacity");
            ViewBag.Colors = new SelectList(db.Colors, "ID", "Colorname");
            ViewBag.SpeedBoxes = new SelectList(db.SpeedBoxes, "ID", "BoxType");
            ViewBag.Fueltypes = new SelectList(db.Fueltypes, "ID", "Fueltype1");


            return View(advertisment);
        }


        [AuthorizeUserFilter]
        public ActionResult EditAds([Bind(Exclude = "Image")] Advertisment advertisment, HttpPostedFileBase Image)
        {
            if (Image != null)
            {
                if (Extensions.Extensions.CheckImageType(Image))
                {
                    advertisment.Image = Extensions.Extensions.SaveImage(Server.MapPath("~/Images/Ads"), Image);
                    advertisment.CreatedAt = advertisment.UpdatedAt = DateTime.Now;
                    advertisment.UserID = (Session["user"] as User).ID;

                    db.Entry(advertisment).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Image", "The type of image is incorrect or the size of image is greater than 10 Mb.");
                }
            }
            else
            {
                ModelState.AddModelError("Image", "Please choose image.");
            }

            return View(advertisment);
        }


        [AuthorizeUserFilter]
        public ActionResult Delete(int? id)
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
            return View(advertisment);
        }


        [HttpPost]
        [AuthorizeUserFilter]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            Advertisment advertisment = db.Advertisments.Find(id);

            if (!Extensions.Extensions.DeleteImage(Server.MapPath("~/Images/Ads"), advertisment.Image))
            {
                ViewBag.DeleteError = "File doesn't exist";
                return View();
            }

            db.Advertisments.Remove(advertisment);
            db.SaveChanges();

            return RedirectToAction("Index" ,"Personal");
        }
    }
}