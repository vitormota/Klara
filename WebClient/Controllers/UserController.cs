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

            ui.ads_subscriptions = JsonConvert.DeserializeObject<List<Ad>>(mService.GetAdSubscriptions(id));
            ui.insts_subscriptions = JsonConvert.DeserializeObject<List<InstitutionModel>>(mService.GetInstitutionSubscriptions(id));
            ui.cupons = JsonConvert.DeserializeObject<List<Ad>>(mService.GetClientPurchases(id));

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