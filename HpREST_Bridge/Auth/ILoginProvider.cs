using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HpREST_Bridge.Auth
{
    interface ILoginProvider
    {
        /// <summary>
        /// Get user data , all providers must return the
        /// the required user info
        /// 
        /// Void until proper container is defined
        /// </summary>
        void getUserData();

        string getProviderName();
    }
}
