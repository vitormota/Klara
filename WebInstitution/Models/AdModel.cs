using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebInstitution.Models
{
    public class AdModel
    {

        public int id { get; set; }
        public int institution_id { get; set; }
        public string service { get; set; }
        public string specialty { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public int discount { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public int remaining_cupons { get; set; }
        public int buyed_cupons { get; set; }

        public static JObject modelToJSON(AdModel model)
        {
            //Please follow api convention, if institution model changes on API
            //this will no longer work
            return new JObject(
                new JProperty("id", model.id),
                 new JProperty("institution_id", model.institution_id),
                 new JProperty("service", model.service),
                 new JProperty("specialty", model.specialty),
                 new JProperty("name", model.name),
                 new JProperty("description", model.description),
                 new JProperty("price", model.price),
                 new JProperty("start_time", model.start_time.ToString()),
                 new JProperty("end_time", model.end_time.ToString()),
                 new JProperty("remaining_cupons", model.remaining_cupons),
                 new JProperty("buyed_cupons", model.buyed_cupons)
                 );
        }
    }
}