using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebClient_.Models {
    public class UserEditModel {

        [DataType(DataType.Password)]
        [Display(Name = "Cellphone", ResourceType = typeof(Resources.Resources))]
        public string phone_number { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "NIF", ResourceType = typeof(Resources.Resources))]
        public string nif { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Address", ResourceType = typeof(Resources.Resources))]
        public string address { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Town", ResourceType = typeof(Resources.Resources))]
        public string city { get; set; }
    }
}