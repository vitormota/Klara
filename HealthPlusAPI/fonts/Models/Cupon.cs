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
    
    public partial class Cupon
    {
        public int id { get; set; }
        public int client_id { get; set; }
        public int ad_id { get; set; }
        public int state { get; set; }
        public System.DateTime start_time { get; set; }
        public System.DateTime end_time { get; set; }
        public System.DateTime purchase_time { get; set; }
    }
}