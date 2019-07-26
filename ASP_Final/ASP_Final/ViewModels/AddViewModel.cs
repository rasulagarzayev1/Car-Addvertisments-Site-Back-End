using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP_Final.Models;

namespace ASP_Final.ViewModels
{
    public class AddViewModel
    {
        public List<Marka> Markas { get; set; }
        public List<Advertisment> Advertisments { get; set; }
        public List<Advertisment> PremiumAdvertisments { get; set; }
        public ResentNewsVM ResentNewsVM { get; set; }
    }
}


