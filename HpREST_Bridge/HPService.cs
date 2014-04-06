using System;

namespace HpREST_Bridge
{


    /// <summary>
    /// Class for API comunication, all POST & GET shall be requested throught this service
    /// </summary>
    public class HPService : IHPService
    {

        /// <summary>
        /// The base url were the API is hosted
        /// </summary>
        private const string base_url = "http://localhost:52144/";
        /// <summary>
        /// accounts path
        /// Please notice that odata url's are case sensitive and parameters are passed with () syntax
        /// i.e.
        ///     odata/Accounts(1)
        /// </summary>
        private const string accounts_controller = "odata/Accounts";
        private const string clients_controller = "odata/Client";

        //---------------------------------------------------------------------
        // Members - GET
        //---------------------------------------------------------------------

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        /// <summary>
        /// TESTING, please ignore
        /// </summary>
        /// <returns></returns>
        public string GetAccounts()
        {
            return RestUtility.HttpGet(base_url+accounts_controller);
        }

        //---------------------------------------------------------------------
        // Members - POST
        //---------------------------------------------------------------------

        public string RegisterUser(Object json_client, Object json_account)
        {
            return RestUtility.HttpPostJSON(base_url + clients_controller,json_client);
        }
    }
}
