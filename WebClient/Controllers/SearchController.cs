using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient_.HealthPService;


namespace WebClient_.Controllers
{
    public class SearchController : Controller
    {
        private HealthPService.IHPService mService = new HPServiceClient();
        //
        // GET: /Search/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string Search(FormCollection form) 
        {
            return form["textbox"]; // Para testar se esta a funcionar bem
        }
	}
}