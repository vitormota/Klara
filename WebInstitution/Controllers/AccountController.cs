﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebInstitution.HealthPService;
using WebInstitution.Models;
using Resources;

namespace WebInstitution.Controllers
{
    public class AccountController : BaseController
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

            Models.InstitutionModel ipm = new Models.InstitutionModel();
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
            Models.InstitutionModel ipm = new Models.InstitutionModel();
            jsonToModel(ipm, json);

            return View("InstitutionPageEdit", ipm);
        }

        public ActionResult SubmitDetails(Models.InstitutionModel model)
        {
            SessionModel session = (SessionModel)Session["manager"];

            if (session == null)
            {
                //Cannot edit details if not logged in,
                //or not own details
                return RedirectToAction("Index", "Home");
            }

            // set institution ID to fix javascript call
           // model.id = session.currentInstitution.id;

            JObject send_data = Models.InstitutionModel.modelToJSON(model);

            string response = mService.EditInstitutionDetails(send_data.ToString(),session.currentInstitution.id);

            // get updated institutions
            string result_str = mService.FetchInstitutions(session.manager_id.ToString());
            var institutions = JsonConvert.DeserializeObject<List<InstitutionModel>>(result_str);

            int curInstitution = session.currentInstitution.id;

            foreach (InstitutionModel i in institutions)
            {
                if (i.id == curInstitution)
                {
                    session.currentInstitution = i;
                    break;
                }
            }

            session.institutions = institutions;

            return RedirectToAction("Settings", "Dashboard");
        }

        public ActionResult ManagerLogin(LoginFormModel form)
        {

            string result_str = mService.ManagerLogin(form.username, form.password);

            if (Session["manager"] != null)
            {
                TempData["msg"] = Resources.Resources.ManagerLoginErrorAlreadyLogged;
            }
            else if (result_str == "invalid user" || result_str == "invalid password")
            {
                TempData["msg"] = Resources.Resources.ManagerLoginErrorInvalid;
            }
            else
            {
                int manager_id = Convert.ToInt32(result_str);

                /// Fetch all institutions under this manager's control
                result_str = mService.FetchInstitutions( manager_id.ToString() );
                var institutions = JsonConvert.DeserializeObject<List<InstitutionModel>>(result_str);

                Session["manager"] = new SessionModel { manager_id = manager_id, institutions = institutions, currentInstitution =  institutions[0] };

                ViewData["login"] = result_str;

                return RedirectToAction("Index", "Dashboard");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ManagerLogout()
        {
            string response = "";

            if (Session["manager"] == null)
            {
                response = Resources.Resources.ManagerLogoutError;
            }
            else
            {
                Session.Remove("manager");
                response = Resources.Resources.ManagerLogoutMsg;
            }

            TempData["msg"] = response;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult SwitchInstitution(int institution)
        {
            SessionModel session = (SessionModel)Session["manager"];

            foreach (InstitutionModel i in session.institutions)
            {
                if (i.id == institution)
                {
                    session.currentInstitution = i;
                    break;
                }
            }

            return View("Index");
        }

        private static void jsonToModel(Models.InstitutionModel model, JObject json)
        {
            model.id = Convert.ToInt32(json["id"].ToString());
            model.name = json["name"].ToString();
            model.address = json["address"].ToString();
            model.city = json["city"].ToString();
           // model.latitude = Convert.ToDecimal(json["latitude"].ToString());
            model.latitude = Convert.ToDecimal(json["latitude"]);
            model.longitude = Convert.ToDecimal(json["longitude"]);
            model.email = json["email"].ToString();
            model.website = json["website"].ToString();
            model.phone_number = json["phone_number"].ToString();
            //model.group_id = json["group_id"];
        }


	}
}