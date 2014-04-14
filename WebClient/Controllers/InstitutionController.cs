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
            string return_str = null;

            if(result.Equals("error"))
            {
                return_str = "error";
            }
            else
            {
                List<Dictionary<string, string>> resultList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result); // permite passar as instituicoes que recebeu para uma lista, com um dicionario la dentro
                return_str = JsonConvert.SerializeObject(resultList); // Para testar se esta a funcionar bem
            }

            return return_str;
        }
	}
}