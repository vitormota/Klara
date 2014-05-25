using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebInstitution.HealthPService;
using WebInstitution.Models;

namespace WebInstitution.Controllers
{
    public class DashboardController : BaseController
    {
        private HealthPService.IHPService mService = new HPServiceClient();
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            SessionModel session = (SessionModel)Session["manager"];

            if (session == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }            
        }

        public ActionResult Localization() {
            return PartialView("_Localization");
        }

        public ActionResult Add() {
            return PartialView("_Add");
        }

        public ActionResult Settings()
        {
            SessionModel session = (SessionModel)Session["manager"];

            return PartialView("_Settings", session.currentInstitution);
        }

        /// <summary>
        /// Action to remove cupons
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit() {
            SessionModel session = (SessionModel)Session["manager"];

            List<AdDisplayModel> ads = JsonConvert.DeserializeObject<List<AdDisplayModel>>(mService.GetActiveAds(session.currentInstitution.id));
            
            // save all ad's ids in list
            List<int> adIds = new List<int>();

            foreach (AdDisplayModel adm in ads)
            {
                adIds.Add(adm.id);
            }

            // get each ad's guid(s)
            string guids_str = mService.GetAdPhotos(adIds.ToArray());

            Dictionary<int, List<string>> guids = JsonConvert.DeserializeObject<Dictionary<int, List<string>>>(guids_str);

            foreach (AdDisplayModel adm in ads)
            {
                List<string> ad_guids = guids[adm.id];

                if (ad_guids.Any())
                {
                    adm.guids = ad_guids;
                }
                else
                {
                    // add defalut ad image
                }
            }


            ViewData["active_ads"] = ads;

            return PartialView("_Edit");
        }

        public ActionResult Stats() {
            return PartialView("_Stats");
        }
	}
}