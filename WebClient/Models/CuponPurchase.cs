using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient_.Models
{
    public class CuponPurchase
    {

        public string service { get; set; }
        public string name { get; set; }
        public string speciality { get; set; }
        public int state { get; set; }
        public System.DateTime end_time { get; set; }
        public System.DateTime purchase_time { get; set; }
    }
}