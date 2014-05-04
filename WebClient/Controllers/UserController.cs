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
    public class UserController : Controller
    {

        private HealthPService.IHPService mService = new HPServiceClient();

        //
        // GET: /User/
        public ActionResult Index()
        {
            UserSession us = (UserSession)Session["user"];

            if (us == null) return View(); /*return RedirectToAction("Index","Auth");*/

            int id = us.internal_id;

            JObject userjson = JObject.Parse(mService.GetClientDetails(id));

            UserInfo ui = UserInfo.jsonToModel(userjson);

            ui.ads_subscriptions = JsonConvert.DeserializeObject<List<AdSubscripted>>(mService.GetAdSubscriptions(id));

            return View(ui);
        }
	}
}