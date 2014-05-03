using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient_.HealthPService;

namespace WebClient_.Controllers
{
    public class AdController : Controller
    {
        private HealthPService.IHPService mService = new HPServiceClient();

        //
        // GET: /Ad/
        public ActionResult Index()
        {
            return View();
        }

        public string SubscribeAd()
        {
            int client_id = 44;
            int ad_id = 5;

            string result = mService.SubscribeAd(client_id, ad_id);
            return result;
        }

        public string UnsubscribeAd()
        {
            int client_id = 44;
            int ad_id = 5;

            string result = mService.UnsubscribeAd(client_id, ad_id);
            return result;
        }

        public string AdsSubscribe()
        {
            int client_id = 44;

            string result = mService.AdsSubscribe(client_id);
            return result;
        }

        public string SearchAd(string textSearch)
        {
            string result = mService.SearchAd(textSearch);
            return result;
        }
	}
}