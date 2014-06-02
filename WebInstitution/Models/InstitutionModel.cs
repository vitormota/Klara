using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebInstitution.Models
{
    public class InstitutionModel
    {
        public int id { get; set; }

        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public int group_id { get; set; }

        [Display(Name = "InstitutionLabelName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "InstitutionLabelNameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "InstitutionLabelNameLength")]
        public string name { get; set; }

        [Display(Name = "InstitutionAddressName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "InstitutionAddressNameRequired")]
        [StringLength(250, ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "InstitutionAddressNameLength")]
        public string address { get; set; }

        [Display(Name = "InstitutionCityName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "InstitutionCityNameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "InstitutionCityNameLength")]
        public string city { get; set; }

        [Display(Name = "InstitutionLatitudeName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "InstitutionLatitudeNameRequired")]
        public decimal latitude { get; set; }

        [Display(Name = "InstitutionLongitudeName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "InstitutionLongitudeNameRequired")]
        public decimal longitude { get; set; }

        [Display(Name = "InstitutionEmailName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "InstitutionEmailNameRequired")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Display(Name = "InstitutionWebsiteName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "InstitutionWebsiteNameRequired")]
        [DataType(DataType.Url)]
        public string website { get; set; }

        [Display(Name = "InstitutionPhoneName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "InstitutionLabelPhoneRequired")]
        [DataType(DataType.PhoneNumber)]
        public string phone_number { get; set; }

        [Display(Name = "InstitutionFaxName", ResourceType = typeof(Resources.Resources))]
        [DataType(DataType.PhoneNumber)]
        public string fax { get; set; }

        [Display(Name = "InstitutionAdvertiseName", ResourceType = typeof(Resources.Resources))]
        public bool advertise { get; set; }

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
                 new JProperty("phone_number", model.phone_number),
                 new JProperty("advertise", model.advertise.ToString())
                 );
        }
    }
}