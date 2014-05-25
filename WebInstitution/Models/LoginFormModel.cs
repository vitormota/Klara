using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebInstitution.Models
{
    public class LoginFormModel
    {
        [Display(Name = "ManagerLoginUserName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "ManagerLoginUserNameRequired")]
        public string username { get; set; }

        [Display(Name = "ManagerLoginPasswordName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "ManagerLoginPasswordNameRequired")]
        public string password { get; set; }
        public bool remember { get; set; }
    }
}