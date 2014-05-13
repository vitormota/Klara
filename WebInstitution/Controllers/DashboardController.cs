using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebInstitution.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Localization() {
            return PartialView("_Localization");
        }

        public ActionResult Add() {
            return PartialView("_Add");
        }

        public ActionResult Settings() {
            return PartialView("_Settings");
        }

        public ActionResult Edit() {
            return PartialView("_Edit");
        }

        public ActionResult Stats() {
            return PartialView("_Stats");
        }
	}
}