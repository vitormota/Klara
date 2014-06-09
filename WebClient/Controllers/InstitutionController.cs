using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Device.Location;
using WebClient_.HealthPService;
using System.Net.Mail;
using WebClient_.Models;

namespace WebClient_.Controllers
{
    public class InstitutionController : Controller
    {
        private HealthPService.IHPService mService = new HPServiceClient();
        //
        // GET: /Institution/
        public ActionResult Index(int id)
        {
            string json_inst = mService.GetInstitution(id);
            InstitutionModel institution = JsonConvert.DeserializeObject<InstitutionModel>(json_inst);

            return View(institution);
        }

        [HttpPost]
        public string SearchInstitution(string textSearch)
        {
            string result = mService.SearchInstitution(textSearch, 0);

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

        [HttpPost]
        public string SubscribeInstitutionUser()
        {
            int institution_id = 1;
            int client_id = 36;

            string result = mService.IsSubscribeUser(client_id, institution_id);
            return result;
        }

        [HttpPost]
        public void SendEmailAboutInstitutions()
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.sapo.pt");

            mail.From = new MailAddress("healthplus_notifications@sapo.pt");
            mail.To.Add("antonio_ribeiro01@hotmail.com"); //antonio_ribeiro01@hotmail.com
            mail.Subject = "Atualizações de subscrições";
            mail.Body = "Aqui irão estar as ultimas atualizações das instituições subscritas!! :)";

            SmtpServer.Port = 25;
            SmtpServer.Credentials = new System.Net.NetworkCredential("healthplus_notifications@sapo.pt", "healthplus");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}