using Facebook;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HpREST_Bridge.Auth
{
    class FacebookLoginAdapter : ILoginProvider
    {

        /// <summary>
        /// Add fields to this dictionary as provided on
        /// http://developers.facebook.com
        /// </summary>
        private readonly Dictionary<int, string> FIELDS = new Dictionary<int, string>(){
            {5 , "email" }, 
            {10 , "username"},
            {15 , "picture.type(large)" },
            {20 , "hometown"}
        };

        ArrayList info = null;
        FacebookClient client;

        public FacebookLoginAdapter(string access_token)
        {
            info = new ArrayList();
            client = new FacebookClient(access_token);
        }

        public Object makeRequest()
        {
            //these are the basics
            string asking_fields = "name,id";
            foreach (string str in info)
            {
                asking_fields += "," + str;
            }
            dynamic result = client.Get("me", new { fields = asking_fields });
            
            return result;
        }

        public void addFieldRequest(params REQUEST[] reqests)
        {
            foreach (REQUEST req in reqests)
            {
                info.Add(FIELDS[(int)req]);
            }

        }

    }
}
