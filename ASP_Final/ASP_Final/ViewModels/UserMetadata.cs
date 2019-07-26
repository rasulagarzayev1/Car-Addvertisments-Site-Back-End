using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP_Final.ViewModels
{
    public class UserMetadata
    {
        [Required]
        [MinLength(3)]
        public string Firstname { get; set; }
        [Required]
        [MinLength(5)]
        public string Lastname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(7)]
        public string Phone { get; set; }
        [Required]
        [MinLength(6)]
        public string password { get; set; }
    }
}