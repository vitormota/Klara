using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebInstitution.HealthPService;

namespace WebInstitution.Controllers
{
    public class ManagerController : Controller
    {
        private HealthPService.IHPService mService = new HPServiceClient();

        //
        // GET: /Manager/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ManagerLogin(string username, string password)
        {
            string result_str = mService.ManagerLogin(username, password);

            if (Session["userId"] != null)
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
                Session["userId"] = result_str;
            }

            ViewData["login"] = result_str;

            return View("Index");
        }

        [HttpGet]
        public ActionResult ManagerLogout()
        {
            string response = "";

            if (Session["userId"] == null)
            {
                response = "not logged yet";
            }
            else
            {
                Session.Remove("userId");
                response = "logout done";
            }

            ViewData["login"] = response;

            return View("Index");
        }

        //
        // GET: /Manager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Manager/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Manager/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Manager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Manager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Manager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Manager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
