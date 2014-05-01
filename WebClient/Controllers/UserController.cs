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
            int id = Convert.ToInt32(Session["internal_id"]);

            JObject userjson = JObject.Parse(mService.GetClientDetails(id));

            UserInfo ui = UserInfo.jsonToModel(userjson);


            return View(ui);
        }
	}
}