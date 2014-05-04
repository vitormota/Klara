using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient_.Models
{
    public class AdSubscripted
    {

        public int ad_id { get; set; }
        public int inst { get; set; }
        public string service { get; set; }
        public string specialty { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public decimal price { get; set; }
        public System.DateTime end { get; set; }
        public int remaining { get; set; }

    }
}