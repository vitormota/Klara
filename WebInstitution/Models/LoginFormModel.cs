using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebInstitution.Models
{
    public class LoginFormModel
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        public bool remember { get; set; }
    }
}