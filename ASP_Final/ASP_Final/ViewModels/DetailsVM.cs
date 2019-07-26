using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP_Final.Models;

namespace ASP_Final.ViewModels
{
    public class DetailsVM
    {
        public Advertisment Advertisment { get; set; }
        public List<Advertisment> RelatedAds { get; set; }
        public ResentNewsVM ResentNewsVM { get; set; }
    }
}



