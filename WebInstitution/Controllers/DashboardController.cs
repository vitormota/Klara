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
            SessionModel session = (SessionModel)Session["manager"];

            string prevAdsStr = mService.GetInactiveBestAds(session.currentInstitution.id);

            List<AdDisplayModel> prevAds = JsonConvert.DeserializeObject<List<AdDisplayModel>>(prevAdsStr);

            ViewData["prev_ads"] = prevAds;

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

            ViewData["active_ads"] = ads;

            return PartialView("_Edit");
        }

        public ActionResult Stats() {
            return PartialView("_Stats");
        }
	}
}