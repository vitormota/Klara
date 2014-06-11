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
using WebInstitution.Controllers;

namespace WebClient_.Controllers
{
    public class AuthController : BaseController
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

            UserSession us = new UserSession();
            us.internal_id = Convert.ToInt32(json["id"].ToString());
            us.name = json["name"].ToString();
            us.access_token = access_token;
            us.provider_id = provider_id;
            us.profile_picture_url = json["picture"].ToString();

            Session["user"] = us;
        }
    }
}
