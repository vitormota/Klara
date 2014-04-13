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