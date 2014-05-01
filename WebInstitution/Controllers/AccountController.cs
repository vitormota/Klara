using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebInstitution.HealthPService;

namespace WebInstitution.Controllers
{
    public class AccountController : Controller
    {

        private HealthPService.IHPService mService = new HPServiceClient();

        //
        // GET: /Account/
        public ActionResult Index()
        {
            if (Session["inst_id"] != null)
            {
                //Logged in
                return Details(Convert.ToInt32(Session["inst_id"]));
            }
            //Not logged in
            //return RedirectToAction("Index", "Home");
            return View();
        }

        //
        // GET: /Account/Details/{id}
        public ActionResult Details(int id)
        {
            JObject json = JObject.Parse(mService.GetInstitution(id));

            //Construct new Model from json based on API model for a Institution

            Models.InstitutionProfileModel ipm = new Models.InstitutionProfileModel();
            jsonToModel(ipm, json);

            return View("InstitutionPageDetails",ipm);
        }

        public ActionResult EditDetails(int id)
        {
            //TEST
            Session["inst_id"] = 1;
            //END
            if (Session["inst_id"] == null || Convert.ToInt32(Session["inst_id"])!=id)
            {
                //Cannot edit details if not logged in,
                //or not own details
                return RedirectToAction("Index", "Home");
            }

            JObject json = JObject.Parse(mService.GetInstitution(id));
            Models.InstitutionProfileModel ipm = new Models.InstitutionProfileModel();
            jsonToModel(ipm, json);

            return View("InstitutionPageEdit", ipm);
        }

        public ActionResult SubmitDetails(Models.InstitutionProfileModel model)
        {
            if (Session["inst_id"] == null || Convert.ToInt32(Session["inst_id"]) != model.id)
            {
                //Cannot edit details if not logged in,
                //or not own details
                return RedirectToAction("Index", "Home");
            }

            JObject send_data = Models.InstitutionProfileModel.modelToJSON(model);

            string response = mService.EditInstitutionDetails(send_data.ToString(),model.id);

            return RedirectToAction("Details", new { id = model.id});
        }

        public ActionResult ManagerLogin(string username, string password)
        {
            string result_str = mService.ManagerLogin(username, password);

            if (Session["inst_id"] != null)
            {
                result_str = "already logged";
            }
            else if (result_str == "invalid user" || result_str == "invalid password")
            {
                //result_str = "error";
            }
            else
            {
                //Session["userId"] = Convert.ToInt32(result_str);
                Session["inst_id"] = result_str;
            }

            ViewData["login"] = result_str;

            return View("Index");
        }

        [HttpGet]
        public ActionResult ManagerLogout()
        {
            string response = "";

            if (Session["inst_id"] == null)
            {
                response = "not logged yet";
            }
            else
            {
                Session.Remove("inst_id");
                response = "logout done";
            }

            ViewData["login"] = response;

            return View("Index");
        }

        private static void jsonToModel(Models.InstitutionProfileModel model,JObject json){
            model.id = Convert.ToInt32(json["id"].ToString());
            model.name = json["name"].ToString();
            model.address = json["address"].ToString();
            model.city = json["city"].ToString();
            model.latitude = Convert.ToDecimal(json["latitude"].ToString());
            model.longitude = Convert.ToDecimal(json["longitude"].ToString());
            model.email = json["email"].ToString();
            model.website = json["website"].ToString();
            model.phone_number = json["phone_number"].ToString();
            //model.group_id = json["group_id"];
        }


	}
}