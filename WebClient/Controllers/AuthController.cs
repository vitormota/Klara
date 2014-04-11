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
            Register registration_info = new Register();
            return View(registration_info);
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

            string response = mService.RegisterUser(access_token,provider);

            return RedirectToAction("index", "home", new { id = 1 });
        }

        [HttpGet]
        public ActionResult userLogin(string access_token,int provider)
        {
            //string response = mService.UserLogin(access_token,provider);

            return RedirectToAction("index", "home", new { id = 1 });
        }
    }
}
