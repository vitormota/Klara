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
            Ad ad = JsonConvert.DeserializeObject<Ad>(json_ad);

            return View(ad);
        }

        [HttpPost]
        public string SubscribeAd()
        {
            int client_id = 44;
            int ad_id = 5;

            string result = mService.SubscribeAd(client_id, ad_id);
            return result;
        }

        [HttpPost]
        public string UnsubscribeAd()
        {
            int client_id = 44;
            int ad_id = 5;

            string result = mService.UnsubscribeAd(client_id, ad_id);
            return result;
        }

        [HttpPost]
        public string AdsSubscribe()
        {
            int client_id = 44;

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

        [HttpPost]
        public string SubscribeAdUser()
        {
            int ad_id = 5;
            int client_id = 44;

            string result = mService.IsSubscribeUser(client_id, ad_id);
            return result;
        }
	}
}