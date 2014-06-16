using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient_.HealthPService;
using WebClient_.Models;
using WebInstitution.Controllers;

namespace WebClient_.Controllers
{
    public class UserController : BaseController
    {

        private HealthPService.IHPService mService = new HPServiceClient();

        //
        // GET: /User/
        public ActionResult Index()
        {
            UserSession us = (UserSession)Session["user"];

            if (us == null) return RedirectToAction("Index","Auth");

            int id = us.internal_id;

            JObject userjson = JObject.Parse(mService.GetClientDetails(id));

            UserInfo ui = UserInfo.jsonToModel(userjson);

            string ads_subs = mService.GetAdSubscriptions(id);
            string inst_subs = mService.GetInstitutionSubscriptions(id);
            string ads_buys = mService.GetClientPurchases(id);

            if (ads_subs != null)
                ui.ads_subscriptions = JsonConvert.DeserializeObject<List<Ad>>(ads_subs);
            else ui.ads_subscriptions = new List<Ad>();

            if(inst_subs != null)
                ui.insts_subscriptions = JsonConvert.DeserializeObject<List<InstitutionModel>>(inst_subs);
            else ui.insts_subscriptions = new List<InstitutionModel>();

            if(ads_buys != null)
                ui.cupons = JsonConvert.DeserializeObject<List<Ad>>(ads_buys);
            else ui.cupons = new List<Ad>();

            return View(ui);
        }

        public JsonResult EditDetails(UserEditModel m) {
            UserSession us = (UserSession)Session["user"];
            var json_string = JsonConvert.SerializeObject(m);
            var lelos = mService.UpdateClientDetails(us.internal_id, json_string);
            return Json(new { Message = "success" });
        }

	}
}