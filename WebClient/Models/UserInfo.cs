using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient_.Models
{
    public class UserInfo
    {

        public string name { get; set; }
        public string picture_url { get; set; }

        public bool receive_notifications { get; set; }

        public bool show_ads { get; set; }

        public string address { get; set; }

        public string phone_number { get; set; }

        public string nif { get; set; }

        public string email { get; set; }
        public string city { get; set; }

        public List<Ad> ads_subscriptions { get; set; }

        public List<Ad> cupons { get; set; }

        public static UserInfo jsonToModel(JObject json)
        {
            UserInfo ui = new UserInfo();

            ui.name = json["name"].ToString();
            ui.nif = json["nif"].ToString();
            ui.email = json["email"].ToString();
            ui.city = json["city"].ToString();
            ui.address = json["address"].ToString();
            ui.phone_number = json["phone_number"].ToString();

            return ui;
        }


        
    }
}