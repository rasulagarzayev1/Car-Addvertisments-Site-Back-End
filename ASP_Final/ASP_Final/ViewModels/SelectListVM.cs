using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_Final.Models;

namespace ASP_Final.ViewModels
{
    public class SelectListVM
    {
        public SelectList Cities { get; set; }
        public SelectList Capasities { get; set; }
        public SelectList Colors { get; set; }
        public SelectList Fueltypes { get; set; }
        public SelectList SpeedBoxes { get; set; }
        public SelectList Models { get; set; }
        public SelectList Markas { get; set; }
        public Advertisment Advertisment { get; set; }
    }
}