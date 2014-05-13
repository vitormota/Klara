using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient_.Models
{
    public class Ad
    {
        public int id { get; set; }
        public int institution_id { get; set; }
        public string service { get; set; }
        public string specialty { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string local { get; set; }
        public string state { get; set; }
        public float discount { get; set; }
        public System.DateTime start_time { get; set; }
        public System.DateTime end_time { get; set; }
        public int remaining_cupons { get; set; }


        public int client_id { get; set; }
    }
}