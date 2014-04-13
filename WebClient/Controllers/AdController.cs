using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient_.HealthPService;

namespace WebClient_.Controllers
{
    public class AdController : Controller
    {
        private HealthPService.IHPService mService = new HPServiceClient();
        //
        // GET: /Ad/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string SearchAd(string textSearch)
        {
            string result = mService.SearchAd(textSearch);

            return result; // Para testar se esta a funcionar bem
        }
	}
}