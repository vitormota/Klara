using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient_.Models {
    public class SearchAdModel {
        public string name { get; set; }
        public string inst_name { get; set; }
        public decimal price { get; set; }
        public Nullable<float> discount { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string specialty { get; set; }
        public string service { get; set; }
        public int id { get; set; }
        public string img_url { get; set; }
        public string city { get; set; }
    }
}