using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient_.HealthPService;
using WebClient_.Models.RequestDatabase;


namespace WebClient_.Controllers
{
    public class SearchController : Controller
    {
        private HealthPService.IHPService mService = new HPServiceClient();

        [HttpGet]
        public ActionResult Search(string textSearch) 
        {
            List<Dictionary<string, string>> list_ads = AdModel.SearchAd(textSearch);
            ViewBag.ListAds = list_ads;
            ViewBag.ListInstitution = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(mService.SearchInstitution(textSearch));
            ViewBag.ListSpecialty = new List<string>();

            for (int i = 0; i < ViewBag.ListAds.Count; i++)
            {
                if (!ViewBag.ListSpecialty.Contains(ViewBag.ListAds[i]["specialty"]))
                {
                    ViewBag.ListSpecialty.Add(ViewBag.ListAds[i]["specialty"]);
                }
            }
            
            return View();
        }
	}
}