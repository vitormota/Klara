using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient_.HealthPService;
using WebInstitution.Controllers;

namespace WebClient_.Controllers
{
    public class SearchController : BaseController
    {
        private HealthPService.IHPService mService = new HPServiceClient();

        [HttpGet]
        public ActionResult Search(string textSearch) 
        {
            if (textSearch != "") {
                List<Dictionary<string, string>> ads = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(mService.SearchAd(textSearch, 0));
                ViewBag.ListAds = ads;
                List<Dictionary<string, string>> insts = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(mService.SearchInstitution(textSearch, 0));
                ViewBag.ListInstitution = insts;

                if (ads.Count > 0)
                    ViewBag.last_ad_id = ads.Aggregate((i1, i2) => Int32.Parse(i1["id"]) > Int32.Parse(i2["id"]) ? i1 : i2)["id"];
                else
                    ViewBag.last_ad_id = 0;

                if (insts.Count > 0)
                    ViewBag.last_inst_id = insts.Aggregate((i1, i2) => Int32.Parse(i1["id"]) > Int32.Parse(i2["id"]) ? i1 : i2)["id"];
                else
                    ViewBag.last_inst_id = 0;

                ViewBag.ListSpecialty = new List<string>();

                for (int i = 0; i < ViewBag.ListAds.Count; i++) {
                    if (!ViewBag.ListSpecialty.Contains(ViewBag.ListAds[i]["specialty"])) {
                        ViewBag.ListSpecialty.Add(ViewBag.ListAds[i]["specialty"]);
                    }
                }

                return View();
            } else
                return null;
        }

        [HttpGet]
        public ActionResult SearchAd(string textSearch, string last_ad_id) 
        {
            List<Dictionary<string, string>> ads = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(mService.SearchAd(textSearch, Int32.Parse(last_ad_id)));
            ViewBag.ListAds = ads;

            if(ads.Count() > 0) {
            string new_last_ad_id = ads.Aggregate((i1, i2) => Int32.Parse(i1["id"]) > Int32.Parse(i2["id"]) ? i1 : i2)["id"];

            return PartialView("_AdResult");
            }

            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult SearchInstitution(string textSearch, string last_inst_id) {
            List<Dictionary<string, string>> insts = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(mService.SearchInstitution(textSearch, Int32.Parse(last_inst_id)));
            ViewBag.ListInstitution = insts;

            if (insts.Count() > 0) {
                string new_last_ad_id = insts.Aggregate((i1, i2) => Int32.Parse(i1["id"]) > Int32.Parse(i2["id"]) ? i1 : i2)["id"];

                return PartialView("_InstitutionResult");
            }

            return new EmptyResult();
        }
	}
}