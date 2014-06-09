using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient_.HealthPService;
using WebClient_.Models;

namespace WebClient_.Controllers
{
    public class AdController : Controller
    {
        private HealthPService.IHPService mService = new HPServiceClient();

        //
        // GET: /Ad/
        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            string json_ad = mService.GetAdById((int)id);
            KeyValuePair<Ad, InstitutionModel> adResult = JsonConvert.DeserializeObject<KeyValuePair<Ad, InstitutionModel>>(json_ad);

            Ad ad = adResult.Key;
            ad.institution_name = adResult.Value.name;
            ad.local = adResult.Value.city;

            return View(ad);
        }

        [HttpGet]
        public string SubscribeAd(int ad_id)
        {
            UserSession us = (UserSession)Session["user"];
            int client_id = us.internal_id;

            string result = mService.SubscribeAd(client_id, ad_id);
            return result;
        }

        [HttpGet]
        public string UnsubscribeAd(int ad_id)
        {
            UserSession us = (UserSession)Session["user"];
            int client_id = us.internal_id;

            string result = mService.UnsubscribeAd(client_id, ad_id);
            return result;
        }

        [HttpGet]
        public string AdsSubscribe()
        {
            UserSession us = (UserSession)Session["user"];
            int client_id = us.internal_id;

            string result = mService.AdsSubscribe(client_id);
            List<Dictionary<string, string>> resultList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result); // permite passar os anuncios que recebeu para uma lista, com um dicionario la dentro
            
            return result;
        }

        [HttpPost]
        public string SearchAd(string textSearch)
        {
            string result = mService.SearchAd(textSearch);
            List<Dictionary<string, string>> resultList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result); // permite passar os anuncios que recebeu para uma lista, com um dicionario la dentro
            
            return result;
        }

        [HttpGet]
        public string SubscribeAdUser(int ad_id)
        {
            UserSession us = (UserSession)Session["user"];
            int client_id = us.internal_id;

            string result = mService.IsSubscribeUser(client_id, ad_id);
            return result;
        }
	}
}