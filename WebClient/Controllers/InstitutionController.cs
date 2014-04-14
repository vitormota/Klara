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
            string return_str = null;
            string result = mService.SearchInstitution(textSearch);

            if(result.Equals("error"))
            {
                return_str = "error";
            }
            else
            {
                List<Dictionary<string, string>> resultList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result); // permite passar as instituicoes que recebeu para uma lista, com um dicionario la dentro
                return_str = result; // Para testar se esta a funcionar bem
            }

            return return_str;
        }

        [HttpPost]
        public string InstitutionsSubscribe()
        {
            int client_id = 36; // Serve para teste
            string return_str = null;
            string result = mService.InstitutionsSubscribe(client_id);

            if(result.Equals("error"))
            {
                return_str = "error";
            }
            else if(result.Equals("no subscriptions"))
            {
                return_str = "no subscriptions";
            }
            else
            {
                List<Dictionary<string, string>> resultList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result); // permite passar as instituicoes que recebeu para uma lista, com um dicionario la dentro
                return_str = result; // Para testar se esta a funcionar bem
            }

            return return_str;
        }

        [HttpPost]
        public string SubscribeInstitution()
        {
            int institution_id = 3;
            long client_id_by_session = 100001147170248; // Posteriormente, vai-se buscar o valor da sessao

            string result = mService.SubscribeInstitution(institution_id, client_id_by_session);
            return result;
        }
	}
}