using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP_Final.Models;

namespace ASP_Final.ViewModels
{
    public class PersonalVM
    {
        public List<Advertisment> Advertisments { get; set; }
        public User user { get; set; }
    }
}
