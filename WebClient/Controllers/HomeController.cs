using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;
using WebClient_.HealthPService;
using WebClient_.Models;

namespace WebClient_.Controllers
{
    public class HomeController : Controller
    {
        private HealthPService.IHPService mService = new HPServiceClient();

        public ActionResult Index()
        {
            string json_top_ads = mService.GetAdsByRule(0, 20, "buyed_cupons-DESC");
            List<SearchAdModel> ads = JsonConvert.DeserializeObject<List<SearchAdModel>>(json_top_ads);

            string json_ad = mService.GetAdsByRule(0, 10, "id-DESC");
            List<SearchAdModel> ad = JsonConvert.DeserializeObject<List<SearchAdModel>>(json_ad);

            List<List<SearchAdModel>> total_ads = new List<List<SearchAdModel>>();
            total_ads.Add(ads);
            total_ads.Add(ad);

            return View(total_ads);
        }

        public ActionResult Ad(int lowerLimit) {
            string json_ad = mService.GetAdsByRule(lowerLimit, 10, "id-DESC");
            List<SearchAdModel> ad = JsonConvert.DeserializeObject<List<SearchAdModel>>(json_ad);

            return PartialView("_AdResult", ad);
        }
    }
}