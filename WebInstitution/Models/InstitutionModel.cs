using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebInstitution.Models
{
    public class InstitutionModel
    {
        public int id { get; set; }

        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public int group_id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string phone_number { get; set; }
        public string fax { get; set; }

        public static JObject modelToJSON(InstitutionModel model)
        {
            //Please follow api convention, if institution model changes on API
            //this will no longer work
            return new JObject(
                new JProperty("id", model.id),
                 new JProperty("name", model.name),
                 new JProperty("address", model.address),
                 new JProperty("city", model.city),
                 new JProperty("latitude", model.latitude.ToString()),
                 new JProperty("longitude", model.longitude.ToString()),
                 new JProperty("fax", model.fax),
                 new JProperty("email", model.email),
                 new JProperty("website", model.website),
                 new JProperty("phone_number", model.phone_number)
                 );
        }
    }
}