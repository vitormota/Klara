using System.Web.Mvc;

namespace WebClient_.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            HealthPService.IHPService client = new HealthPService.HPServiceClient();
            ViewBag.Message = client.GetAccounts();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}