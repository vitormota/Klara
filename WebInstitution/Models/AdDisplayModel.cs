using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace WebInstitution.Models
{
    public class AdDisplayModel
    {
        public int id { get; set; }
        public string service { get; set; }
        public string specialty { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public float discount { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public int remaining_cupons { get; set; }
        public int buyed_cupons { get; set; }
        public List<string> guids { get; set; }
    }
}