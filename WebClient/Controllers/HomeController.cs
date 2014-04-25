using System.Web.Mvc;
using WebClient_.Models;

namespace WebClient_.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //TEST
            UserInfo user = new UserInfo();
            user.name = (string)Session["name"];
            if (Session["picture"] != null)
            {
                user.picture_url = (string)Session["picture"].ToString();
            }

            return View(user);
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