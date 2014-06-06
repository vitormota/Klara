using System.Web.Mvc;
using WebClient_.HealthPService;
using WebClient_.Models;

namespace WebClient_.Controllers
{
    public class HomeController : Controller
    {
        private HealthPService.IHPService mService = new HPServiceClient();

        public ActionResult Index()
        {
            

            
            return View();
        }

        public ActionResult About()
        {
            int client_id = 36; // Id de teste

            ViewBag.Message = "Your application description page.";
            HealthPService.IHPService client = new HealthPService.HPServiceClient();
            ViewBag.Message = client.GetClientDetails(client_id);
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}