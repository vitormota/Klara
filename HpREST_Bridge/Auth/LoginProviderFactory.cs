using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace HpREST_Bridge.Auth
{
    class LoginProviderFactory
    {

        /// <summary>
        /// For extensions on Authentication providers read the release notes.
        /// </summary>
        private const string NAMESPACE = "HpREST_Bridge.Auth";
        private const string CLASS_SUFIX = "LoginAdapter";

        /// <summary>
        /// Availiable providers list
        /// </summary>
        public const string FACEBOOK = "Facebook";

        /// <summary>
        /// 
        /// </summary>
        protected LoginProviderFactory() { }


        public static Exception exception = null;
        /// <summary>
        /// Get the error thrown by the login provider
        /// </summary>
        /// <returns>A string representation of the error, or null if no error ocurred</returns>
        public static string getError()
        {
            if (exception != null)
            {
                return exception.ToString();
            }
            return null;
        }

        /// <summary>
        /// Returns the apropriate adapter for the specified provider argument
        /// 
        /// </summary>
        /// <returns>The apropiate provider object</returns>
        public static ILoginProvider getLoginProvider(string provider){
            ILoginProvider ilp = null;
            try
            {
                ObjectHandle handle = Activator.CreateInstance(NAMESPACE, provider + CLASS_SUFIX);
                ilp = (ILoginProvider)handle.Unwrap();
                Console.WriteLine(ilp.getProviderName());
                exception = null;
            }
            catch (MissingMethodException ex)
            {
                // No matching public constructor found
                exception = ex;
            }catch (TypeLoadException ex){
                // Invalid class name, make sure it has the syntax "ProviderName"+"LoginAdapter"
                exception = ex;
            }
            return ilp;
        }

    }
}
