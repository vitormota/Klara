using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebInstitution.Models
{
    public class AdFormModel
    {
        public string service { get; set; }
        public string specialty { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public float previous_price { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public int total_cupons { get; set; }
    }
}