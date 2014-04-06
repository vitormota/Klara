using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient_.Models
{
    public class Register
    {
        public Reg_account account_model { get; set; }
        public Reg_client client_model { get; set; }
        
    }

    public class Reg_account
    {
        public string type { get; set; }
        public long fb_id { get; set; }
        public string currency { get; set; }
    }

    public class Reg_client
    {
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string phone_number { get; set; }
        public string nif { get; set; }
        //public string email { get; set; }
    }
}