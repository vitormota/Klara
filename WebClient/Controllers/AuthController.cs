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

        private HealthPService.IHPService mService = new HPServiceClient();

        //
        // GET: /Auth/
        public ActionResult Index()
        {
            
            return View();
        }

        /// <summary>
        /// Register user
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult registerUser(string access_token,int provider)
        {

            JObject response = JObject.Parse(mService.RegisterUser(access_token,provider));

            //Save session
            saveSession(response,access_token,provider);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult userLogin(string access_token,int provider)
        {
            JObject response = JObject.Parse(mService.UserLogin(access_token,provider));

            //Save session
            saveSession(response,access_token,provider);

            return RedirectToAction("Index", "Home");
        }

        private void saveSession(JObject json,string access_token,int provider_id)
        {
            Session["internal_id"] = json["id"].ToString();
            Session["name"] = json["name"].ToString();
            Session["access_token"] = access_token;
            Session["provider_id"] = provider_id;
            //If it is a returning user this value will be null
            //and must be fecthed async
            //if the user just registered picture picture_url will be availiable
            Session["profile_picture_url"] = json["picture"];
        }
    }
}
