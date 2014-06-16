using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WebClient_.HealthPService;
using WebClient_.Models;
using WebInstitution.Controllers;
using WebInstitution.Helpers;

namespace WebClient_.Controllers
{
    public class HomeController : BaseController
    {
        private HealthPService.IHPService mService = new HPServiceClient();

        public ActionResult Index()
        {
            string json_top_ads = mService.GetAdsByRule(0, 20, "buyed_cupons-DESC");
            List<SearchAdModel> ads = JsonConvert.DeserializeObject<List<SearchAdModel>>(json_top_ads);

            string json_ad = mService.GetAdsByRule(0, 8, "id-DESC");
            List<SearchAdModel> ad = JsonConvert.DeserializeObject<List<SearchAdModel>>(json_ad);

            List<List<SearchAdModel>> total_ads = new List<List<SearchAdModel>>();
            total_ads.Add(ads);
            total_ads.Add(ad);

            return View(total_ads);
        }

        public ActionResult Ad(int lowerLimit) {
            string json_ad = mService.GetAdsByRule(lowerLimit, 8, "id-DESC");
            List<SearchAdModel> ad = JsonConvert.DeserializeObject<List<SearchAdModel>>(json_ad);

            return PartialView("_AdResult", ad);
        }

        public ActionResult SetCulture(string culture)
        {
            // Validate input
            culture = CultureHelper.GetImplementedCulture(culture);
            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index");
        }   
    }
}