using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient_.HealthPService;

namespace WebClient_.Controllers
{
    public class InstitutionController : Controller
    {
        private HealthPService.IHPService mService = new HPServiceClient();
        //
        // GET: /Institution/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string SearchInstitution(string textSearch)
        {
            string result = mService.SearchInstitution(textSearch);
            return result;
        }
	}
}