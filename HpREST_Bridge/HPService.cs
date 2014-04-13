using System;
using HpREST_Bridge.Auth;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        private const string clients_controller = "odata/Clients";
        private const string institutions_controller = "odata/Institutions";
        private const string ads_controller = "odata/Ads";

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

        public string RegisterUser(string access_token, int provider)
        {

            Auth.ILoginProvider ilp = LoginProviderFactory.createInstance(access_token,(Utility.LoginProvider) provider);

            ilp.addFieldRequest(REQUEST.EMAIL, REQUEST.HOMETWON, REQUEST.PROF_PIC, REQUEST.USERNAME);
            JObject json = JObject.Parse(ilp.makeRequest().ToString());

            string s = (string)json["id"];

            JObject account = new JObject(
                new JProperty("fb_id", json["id"])
                );

            JObject response = JObject.Parse(RestUtility.HttpPostJSON(base_url + accounts_controller, account));

            ///Please follow HealthPlusAPI Client model to define this JSON
            JObject client = new JObject(
                new JProperty("id", response["id"]),
                new JProperty("name", json["name"]),
                new JProperty("city", json["hometown"]["name"]),
                new JProperty("email", json["email"])
                );

            response = JObject.Parse(RestUtility.HttpPostJSON(base_url + clients_controller,client));

            return response.ToString();
        }


        public string UserLogin(string access_token, int provider)
        {
            Auth.ILoginProvider ilp = LoginProviderFactory.createInstance(access_token, (Utility.LoginProvider)provider);
            JObject json = JObject.Parse(ilp.makeRequest().ToString());
            return "";
        }

        public string SearchInstitution(string textSearch)
        {
            Dictionary<string, string> json_str = new Dictionary<string, string>();
            json_str.Add("textSearch", textSearch);

            string postJSON = RestUtility.HttpPostJSON(base_url + institutions_controller + "(0)/SearchInstitution", JsonConvert.SerializeObject(json_str));
            Dictionary<string, Object> resultDict = JsonConvert.DeserializeObject<Dictionary<string, Object>>(postJSON);

            string auxJSON = resultDict["value"].ToString(); // serve para ajudar na obtencao da lista de instituicoes
            return auxJSON;
        }

        public string SearchAd(string textSearch)
        {
            
            string responce = RestUtility.HttpGet(base_url + ads_controller + "('" + textSearch + "')");
            
            return responce;
        }
    }
}
