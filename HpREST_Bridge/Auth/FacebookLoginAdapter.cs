using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HpREST_Bridge.Auth
{
    public class FacebookLoginAdapter : ILoginProvider
    {

        public void getUserData()
        {
            throw new NotImplementedException();
        }


        public string getProviderName()
        {
            return LoginProviderFactory.FACEBOOK;
        }
    }
}