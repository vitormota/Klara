//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HealthPlusAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ad
    {
        public int id { get; set; }
        public int institution_id { get; set; }
        public string service { get; set; }
        public string specialty { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public Nullable<float> discount { get; set; }
        public System.DateTime start_time { get; set; }
        public System.DateTime end_time { get; set; }
        public int remaining_cupons { get; set; }
        public int buyed_cupons { get; set; }
        public string state { get; set; }
    }
}
