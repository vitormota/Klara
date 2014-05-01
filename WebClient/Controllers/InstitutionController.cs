using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Device.Location;
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

            List<Dictionary<string, string>> resultList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result); // permite passar as instituicoes que recebeu para uma lista, com um dicionario la dentro
            return result;
        }

        [HttpPost]
        public string InstitutionsSubscribe()
        {
            int client_id = 36; // Serve para teste
            string result = mService.InstitutionsSubscribe(client_id);

            List<Dictionary<string, string>> resultList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result); // permite passar as instituicoes que recebeu para uma lista, com um dicionario la dentro
            return result;
        }

        [HttpPost]
        public string SubscribeInstitution()
        {
            int institution_id = 2;
            int client_id = 36; // Posteriormente, vai-se buscar o valor da sessao

            string result = mService.SubscribeInstitution(institution_id, client_id);
            return result;
        }

        [HttpPost]
        public string UnsubscribeInstitution()
        {
            int institution_id = 2;
            int client_id = 36; // Posteriormente, vai-se buscar o valor da sessao

            string result = mService.UnsubscribeInstitution(institution_id, client_id);
            return result;
        }

        [HttpPost]
        public string NearestInstitutions()
        {
            double latitude = 41.228229;
            double longitude = -8.307141399999999;
            double distance = 5000;

            string result = mService.NearestInstitutions(latitude, longitude, distance);
            List<Dictionary<string, string>> resultList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result); // permite passar as instituicoes que recebeu para uma lista, com um dicionario la dentro
           
            return result;
        }
    }
}