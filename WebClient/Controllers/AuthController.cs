using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebClient_.Models;
using WebClient_.HealthPService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebClient_.Controllers
{
    public class AuthController : Controller
    {
        //
        // GET: /Auth/
        public ActionResult Index()
        {
            Register registration_info = new Register();
            return View(registration_info);
        }

        [HttpPost]
        public ActionResult registerUser(Register model)
        {
            model.client_model.city = "test";
            model.client_model.phone_number = "910000000";
            model.client_model.nif = "250250250";

            model.account_model.type = "client";
            model.account_model.currency = "USD";

            JObject json_client = (JObject)JToken.FromObject(model.client_model);
            JObject json_account = (JObject)JToken.FromObject(model.account_model);
            HealthPService.IHPService ser = new HPServiceClient();
            string res_reg = ser.RegisterUser(json_client.ToString(),json_account.ToString());

            return RedirectToAction("index", "home", new { id = model.account_model.fb_id });
        }

    }
}
