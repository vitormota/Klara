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
    
    public partial class Acccount
    {
        public Acccount()
        {
            this.type = "client";
            this.receive_notification = true;
            this.show_ads = true;
            this.currency = "EUR";
        }
    
        public int id { get; set; }
        public string type { get; set; }
        public long fb_id { get; set; }
        public bool receive_notification { get; set; }
        public bool show_ads { get; set; }
        public string currency { get; set; }
    }
}