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
        public string ManagerLogin(string username, string password)
        {
            string return_str = null;
            string result_str = mService.ManagerLogin(username, password);

            //if (result.Equals("error"))
            //{
            //    return_str = "error";
            //}
            //else
            //{
            //    List<Dictionary<string, string>> resultList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(result); // permite passar as instituicoes que recebeu para uma lista, com um dicionario la dentro
            //    return_str = result; // Para testar se esta a funcionar bem
            //}

            return return_str;
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
