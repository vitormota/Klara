using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HpREST_Bridge.Auth
{
    class LoginProviderFactory
    {
        public static ILoginProvider createInstance(string access_token,Utility.LoginProvider provider_code)
        {
            ILoginProvider lp = null;
            switch (provider_code)
            {
                case Utility.LoginProvider.Facebook:
                    lp = new FacebookLoginAdapter(access_token);
                    break;
                default:
                    //Invalid
                    break;
            }
            return lp;
        }

    }
}
