using System;
using HpREST_Bridge.Auth;
using Newtonsoft.Json.Linq;
using System.Net;
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
        private const string subscriptions_controller = "odata/Subscriptions";
        private const string managers_controller = "odata/Managers";
        private const string cupons_controller = "odata/Cupon";
        private const string photos_controller = "odata/Photos";
        /// <summary>
        /// Controllers internal actions definitions
        /// </summary> 
        private const string action_get_purchases = "Purchases";
        private const string action_get_accounts_details = "OptionsFlags";
        private const string action_get_ad_subs = "Cupons";

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

        //---------------------------------------------------------------------
        // Members - User
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
            ilp.addFieldRequest(REQUEST.PROF_PIC);
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
            response.Add(new JProperty("name", json["name"]));
            response.Add(new JProperty("picture", json["picture"]["data"]["url"]));
            JToken provider_id = response["fb_id"];
            if (provider_id == null)
            {
                //not registered
                return RegisterUser(access_token,provider);
            }

            return response.ToString();
        }

        public string SearchAd(string textSearch)
        {
            string return_str = null;

            Dictionary<string, string> json_dict = new Dictionary<string, string>();
            json_dict.Add("textSearch", textSearch);

            string postJSON = RestUtility.HttpPostJSON(base_url + ads_controller + "/SearchAd", JsonConvert.SerializeObject(json_dict));
            Dictionary<string, Object> resultDict = JsonConvert.DeserializeObject<Dictionary<string, Object>>(postJSON);

            return_str = resultDict["value"].ToString();
            return return_str;
        }

        //---------------------------------------------------------------------
        // Members - Client
        //---------------------------------------------------------------------

        /// <summary>
        ///  Retrieve purchased cupoes from API with specified client id (internal)
        /// </summary>
        /// <param name="id">client id</param>
        /// <returns></returns>
        public string GetClientPurchases(int id)
        {
            return RestUtility.HttpGet(base_url+ cupons_controller+"("+id+")/"+action_get_purchases);
        }

        public string GetClientDetails(int id)
        {
            dynamic account = RestUtility.HttpGet(base_url + accounts_controller + "(" + id + ")/"+action_get_accounts_details);
            account = account.Substring(0, account.Length - 1);
            string client = RestUtility.HttpGet(base_url+clients_controller + "(" + id + ")");
            client = client.Substring(3);
            string res = account + "," + client;
            return res;
        }

        public string UpdateClientDetails(int id, string client_jobj)
        {
            return RestUtility.HttpPutJSON(base_url + clients_controller + "(" + id + ")", client_jobj);
        }

        public string GetAdSubscriptions(int client_id)
        {
            dynamic subscriptions = RestUtility.HttpGet(base_url+subscriptions_controller+"("+client_id+")/"+action_get_ad_subs);
            return subscriptions;
        }

        public string BuyCupon(string cupon_str)
        {
            JObject json = JObject.Parse(cupon_str);

            ///Please follow HealthPlusAPI Cupon model to define this JSON
            JObject cupon = new JObject(
                new JProperty("state", 0),
                new JProperty("ad_id", json["id"]),
                new JProperty("start_time", json["start_time"]),
                new JProperty("end_time", json["end_time"])
                );

            if ((int)json["client_id"] != 0)
            {
                cupon.Add("client_id", json["client_id"]);
            }

            return RestUtility.HttpPostJSON(base_url + cupons_controller, cupon);
        }

        public string BuyMultipleCupons(string cupon_list)
        {
            JArray json = JArray.Parse(cupon_list);
            JArray cupons = new JArray();
            foreach (JObject obj in json)
            {
                cupons.Add(
                    new JObject(
                        new JProperty("state", 0),
                        new JProperty("ad_id", obj["id"]),
                        new JProperty("start_time", obj["start_time"]),
                        new JProperty("end_time", obj["end_time"])
                    )
                );
            }

            return RestUtility.HttpPostJSON(base_url+cupons_controller,cupons);
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------

        public string EditInstitutionDetails(string model_data,int id)
        {
            JObject data = JObject.Parse(model_data);
            string response = RestUtility.HttpPutJSON(base_url + institutions_controller+"("+id+")", data);
            //Strip response from sensitive information?
            //
            return response;
        }

        //---------------------------------------------------------------------
        // Members - Ad
        //---------------------------------------------------------------------

        public string GetAdById(int id)
        {
            return RestUtility.HttpGet(base_url + ads_controller + "(" + id + ")");
        }

        public string CreateAd(string model_data)
        {
            JObject data = JObject.Parse(model_data);
            string response = RestUtility.HttpPostJSON(base_url + ads_controller, data);
            //Strip response from sensitive information?
            //
            return response;
        }

        /// <summary>
        /// Get institution by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetInstitution(int id)
        {
            return RestUtility.HttpGet(base_url + institutions_controller + "("+id+")");
        }

        public string SearchInstitution(string textSearch)
        {
            Dictionary<string, string> json_str = new Dictionary<string, string>();
            json_str.Add("textSearch", textSearch);

            string postJSON = RestUtility.HttpPostJSON(base_url + institutions_controller + "/SearchInstitution", JsonConvert.SerializeObject(json_str));
            Dictionary<string, Object> resultDict = JsonConvert.DeserializeObject<Dictionary<string, Object>>(postJSON);

            string returnJSON = resultDict["value"].ToString(); // serve para ajudar na obtencao da lista de instituicoes/erros
            return returnJSON;
        }

        public string FetchInstitutions(string manager_id)
        {
            Dictionary<string, string> json_str = new Dictionary<string, string>();
            json_str.Add("manager_id", manager_id);

            string postJSON = RestUtility.HttpPostJSON(base_url + institutions_controller + "(0)/FetchInstitutions", JsonConvert.SerializeObject(json_str));
            Dictionary<string, Object> resultDict = JsonConvert.DeserializeObject<Dictionary<string, Object>>(postJSON);

            string returnJSON = resultDict["value"].ToString(); // serve para ajudar na obtencao da lista de instituicoes/erros
            return returnJSON;
        }

        public string ManagerLogin(string username, string password)
        {
            Dictionary<string, string> json_str = new Dictionary<string, string>();
            json_str.Add("username", username);
            json_str.Add("password", password);

            string postJSON = RestUtility.HttpPostJSON(base_url + managers_controller + "(0)/ManagerLogin", JsonConvert.SerializeObject(json_str));
            Dictionary<string, Object> resultDict = JsonConvert.DeserializeObject<Dictionary<string, Object>>(postJSON);

            string returnJSON = resultDict["value"].ToString();
            return returnJSON;
        }

        public string InstitutionsSubscribe(int client_id)
        {
            Dictionary<string, string> json_str_int = new Dictionary<string, string>();
            string client_id_str = client_id.ToString();
            json_str_int.Add("client_id", client_id_str);

            string postJSON = RestUtility.HttpPostJSON(base_url + subscriptions_controller + "/InstitutionsSubscribe", JsonConvert.SerializeObject(json_str_int));
            Dictionary<string, Object> resultDict = JsonConvert.DeserializeObject<Dictionary<string, Object>>(postJSON);

            string returnJSON = resultDict["value"].ToString(); // serve para ajudar na obtencao da lista de instituicoes/erros
            return returnJSON;
        }

        public string SubscribeInstitution(int institution_id, int client_id)
        {
            string return_str = null;

            JObject subscription = new JObject(
                new JProperty("subscribable_id", institution_id),
                new JProperty("client_id", client_id));

            string postJSON = RestUtility.HttpPostJSON(base_url + subscriptions_controller, subscription);
            Dictionary<string, Object> resultDictSubs = JsonConvert.DeserializeObject<Dictionary<string, Object>>(postJSON);

            return_str = resultDictSubs["value"].ToString();
            return return_str;
        }

        public string UnsubscribeInstitution(int institution_id, int client_id)
        {
            string return_str = null;

            Dictionary<string, string> json_dict = new Dictionary<string, string>();
            json_dict.Add("subscribable_id", institution_id.ToString());
            json_dict.Add("client_id", client_id.ToString());
            
            string postJSON = RestUtility.HttpPostJSON(base_url + subscriptions_controller + "/DeleteSubscription", JsonConvert.SerializeObject(json_dict));
                
            Dictionary<string, string> resultDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(postJSON);
            return_str = resultDict["value"];
            
            return return_str;
        }

        public string NearestInstitutions(double latitude, double longitude, double distance)
        {
            string return_str = null;

            Dictionary<string, string> json_str_double = new Dictionary<string, string>();
            json_str_double.Add("latitude", latitude.ToString());
            json_str_double.Add("longitude", longitude.ToString());
            json_str_double.Add("distance", distance.ToString());

            string postJSON = RestUtility.HttpPostJSON(base_url + institutions_controller + "/NearestInstitutions", JsonConvert.SerializeObject(json_str_double));
            Dictionary<string, Object> resultDict = JsonConvert.DeserializeObject<Dictionary<string, Object>>(postJSON);

            return_str = resultDict["value"].ToString();
            return return_str;
        }

        public string SeeCuponsActive(int client_id)
        {
            string return_str = null;

            string postJSON = RestUtility.HttpPostJSON(base_url + cupons_controller + "(" + client_id + ")/SeeCuponsActive", null);

            Dictionary<string, Object> resultDict = JsonConvert.DeserializeObject<Dictionary<string, Object>>(postJSON);
            return_str = resultDict["value"].ToString();

            return return_str;
        }

        public string InsertAdPhoto(int ad_id, string photo_guid)
        {
            string return_str = null;

            Dictionary<string, string> json_str_double = new Dictionary<string, string>();
            json_str_double.Add("ad_id", ad_id.ToString());
            json_str_double.Add("photo_guid", photo_guid);

            string postJSON = RestUtility.HttpPostJSON(base_url + photos_controller + "(" + 0 + ")/InsertAdPhoto", JsonConvert.SerializeObject(json_str_double));
            Dictionary<string, Object> resultDict = JsonConvert.DeserializeObject<Dictionary<string, Object>>(postJSON);
            return_str = resultDict["value"].ToString();

            return return_str;
        }

        public string GetActiveAds(int institution_id)
        {
            string return_str = null;

            string postJSON = RestUtility.HttpGet(base_url + ads_controller + "?$filter=institution_id eq " + institution_id);
            Dictionary<string, Object> resultDict = JsonConvert.DeserializeObject<Dictionary<string, Object>>(postJSON);
            return_str = resultDict["value"].ToString();

            return return_str;
        }

        public string GetAdPhotos(List<int> ad_ids)
        {
            string return_str = null;

            Dictionary<string, string> json_str_double = new Dictionary<string, string>();
            json_str_double.Add("ad_ids", JsonConvert.SerializeObject(ad_ids).ToString());

            string postJSON = RestUtility.HttpPostJSON(base_url + photos_controller + "/GetAdPhotos", JsonConvert.SerializeObject(json_str_double));
            Dictionary<string, Object> resultDict = JsonConvert.DeserializeObject<Dictionary<string, Object>>(postJSON);
            return_str = resultDict["value"].ToString();
            
            return return_str;
        }

        public string DeleteAd(int ad_id)
        {
            string result = RestUtility.HttpDelete(base_url + ads_controller + "(" + ad_id + ")");
            return result;
        }

        public string SubscribeAd(int client_id, int ad_id)
        {
            string return_str = null;
        
            JObject subscription = new JObject(
                new JProperty("subscribable_id", ad_id),
                new JProperty("client_id", client_id));

            string postJSON = RestUtility.HttpPostJSON(base_url + subscriptions_controller, subscription);
            Dictionary<string, Object> resultDictSubs = JsonConvert.DeserializeObject<Dictionary<string, Object>>(postJSON);

            return_str = resultDictSubs["value"].ToString();
            return return_str;
        }

        public string UnsubscribeAd(int client_id, int ad_id)
        {
            string return_str = null;

            Dictionary<string, string> dictJSON = new Dictionary<string, string>();
            dictJSON.Add("subscribable_id", ad_id.ToString());
            dictJSON.Add("client_id", client_id.ToString());

            string postJSON = RestUtility.HttpPostJSON(base_url + subscriptions_controller + "/DeleteSubscription", JsonConvert.SerializeObject(dictJSON));

            Dictionary<string, string> resultDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(postJSON);
            return_str = resultDict["value"];

            return return_str;
        }

        public string AdsSubscribe(int client_id)
        {
            Dictionary<string, string> json_str_int = new Dictionary<string, string>();
            string client_id_str = client_id.ToString();
            json_str_int.Add("client_id", client_id_str);

            string postJSON = RestUtility.HttpPostJSON(base_url + subscriptions_controller + "/AdsSubscribe", JsonConvert.SerializeObject(json_str_int));
            Dictionary<string, Object> resultDict = JsonConvert.DeserializeObject<Dictionary<string, Object>>(postJSON);

            string returnJSON = resultDict["value"].ToString(); // serve para ajudar na obtencao da lista de instituicoes/erros
            return returnJSON;
        }

        public string IsSubscribeUser(int client_id, int subscribable_id)
        {
            Dictionary<string, string> json_dict = new Dictionary<string, string>();
            json_dict.Add("client_id", client_id.ToString());
            json_dict.Add("subscribable_id", subscribable_id.ToString());

            string postJSON = RestUtility.HttpPostJSON(base_url + subscriptions_controller + "/IsSubscribeUser", JsonConvert.SerializeObject(json_dict));
            Dictionary<string, Object> resultDict = JsonConvert.DeserializeObject<Dictionary<string, Object>>(postJSON);

            string returnJSON = resultDict["value"].ToString();
            return returnJSON;
        }


        
    }
}
