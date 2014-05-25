using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebInstitution.Models
{
    public class AdFormModel
    {
        [Display(Name = "AdServiceName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "AdServiceNameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "AdServiceNameLength")]
        public string service { get; set; }

        [Display(Name = "AdSpecialtyName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "AdSpecialtyNameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "AdSpecialtyNameLength")]
        public string specialty { get; set; }

        [Display(Name = "AdName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "AdNameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "AdNameLength")]
        public string name { get; set; }

        [Display(Name = "AdDescriptionName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "AdDescriptionNameRequired")]
        [StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "AdDescriptionNameLength")]
        public string description { get; set; }

        [Display(Name = "AdPriceName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "AdPriceNameRequired")]
        [DataType(DataType.Currency)]
        public float price { get; set; }

        [Display(Name = "AdPreviousPriceName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "AdPreviousPriceNameRequired")]
        [DataType(DataType.Currency)]
        public float previous_price { get; set; }

        [Display(Name = "AdStartDateName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "AdStartDateNameRequired")]
        [DataType(DataType.DateTime)]
        public DateTime start_time { get; set; }

        [Display(Name = "AdEndDateName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "AdEndDateNameRequired")]
        [DataType(DataType.DateTime)]
        public DateTime end_time { get; set; }

        [Display(Name = "AdTotalCuponsName", ResourceType = typeof(Resources.Resources))]
        [Required(ErrorMessageResourceType = typeof(Resources.Resources), ErrorMessageResourceName = "AdTotalCuponsNameRequired")]
        [Range(1, Int32.MaxValue)]
        public int total_cupons { get; set; }
    }
}