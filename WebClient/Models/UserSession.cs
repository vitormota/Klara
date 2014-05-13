using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebClient_.Models
{
    /// <summary>
    /// Stores information about a user Session
    /// </summary>
    public class UserSession
    {

        public int internal_id { get; set; }
        public string name { get; set; }
        public string access_token { get; set; }
        public int provider_id { get; set; }
        public string profile_picture_url { get; set; }
    }
}