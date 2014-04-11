using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HpREST_Bridge.Auth
{
    /// <summary>
    /// Simple utility class
    /// 
    /// cannot be instanciated
    /// </summary>
    abstract class Utility
    {
        /// <summary>
        /// Enumeration of implemented login providers
        /// </summary>
        public enum LoginProvider
        {
            Facebook
        }

        /// <summary>
        /// Retrieve a List of availiable Login providers
        /// </summary>
        /// <returns></returns>
        public static LinkedList<string> getLoginProviders()
        {
            LinkedList<string> list = new LinkedList<string>();
            foreach(var lp in Enum.GetValues(typeof(LoginProvider))){
                list.AddLast(((LoginProvider)lp).ToString());
            }
            return list;
        }
    }
}
