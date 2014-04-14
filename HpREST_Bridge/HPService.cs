using System;
using HpREST_Bridge.Auth;
using Newtonsoft.Json.Linq;
using System.Net;using System.Collections.Generic;
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
            JObject face_response = JObject.Parse(ilp.makeRequest().ToString());

            JObject account = new JObject(
                //new JProperty("fb_id", face_response["id"])
                new JProperty("fb_id", face_response["id"].ToString())
                );

            JObject api_response = JObject.Parse(RestUtility.HttpPostJSON(base_url + accounts_controller, account));

            ///Please follow HealthPlusAPI Client model to define this JSON
            JObject client = new JObject(
                new JProperty("id", api_response["id"]),
                new JProperty("name", face_response["name"]),
                new JProperty("city", face_response["hometown"]["name"]),
                new JProperty("email", face_response["email"])
                );

            api_response = JObject.Parse(RestUtility.HttpPostJSON(base_url + clients_controller,client));
            api_response.Add(new JProperty("picture", face_response["picture"]["data"]["url"]));

            return api_response.ToString();
        }


        public string UserLogin(string access_token, int provider)
        {
            Auth.ILoginProvider ilp = LoginProviderFactory.createInstance(access_token, (Utility.LoginProvider)provider);
            
            //Grab the user id
            JObject json = JObject.Parse(ilp.makeRequest().ToString());
            //Find it on DB
            string get_result = null;

            //If user is not present on DB, API will respond with a 404,
            //causing the api_response parsing to throw an exception...
            //until proper solution is found, this is the implemented workaround

            get_result = RestUtility.HttpGet(base_url + accounts_controller + "(" + json["id"] + "L)");
            if(get_result==null && ((HttpWebResponse)RestUtility.exception.Response).StatusCode==HttpStatusCode.NotFound){
                //If API responded with a 404 the exception was caught and stored
                //
                //TODO: Fix API, so a proper api_response is made, and the service won't crash...
                return RegisterUser(access_token, provider);
            }
            JObject response = JObject.Parse(get_result);
            response.Add(new JProperty("name", json["name"].ToString()));
            JToken provider_id = response["fb_id"];
            if (provider_id == null)
            {
                //not registered
                return RegisterUser(access_token,provider);
            }

            return response.ToString();
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

        public string InstitutionsSubscribe(int client_id)
        {
            return "error";
        }
    }
}
