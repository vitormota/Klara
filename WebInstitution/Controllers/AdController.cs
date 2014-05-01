using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebInstitution.HealthPService;
using WebInstitution.Models;

namespace WebInstitution.Controllers
{
    public class AdController : Controller
    {
        private HealthPService.IHPService mService = new HPServiceClient();
        
        //
        // GET: /Ad/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Ad/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Ad/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Ad/Create
        [HttpPost]
        public ActionResult Create(Models.AdFormModel ad)
        {
            
            SessionModel session = (SessionModel)Session["manager"];

            if (session == null)
            {
                return View();
            }

            try
            {
                JObject send_data = new JObject(
                    new JProperty("institution_id", session.currentInstitution.id),
                    new JProperty("service", ad.service),
                    new JProperty("specialty", ad.specialty),
                    new JProperty("name", ad.name),
                    new JProperty("description", ad.description),
                    new JProperty("start_time", ad.start_time),
                    new JProperty("end_time", ad.end_time),
                    new JProperty("price", ad.price.ToString() ), // decimals must have quotes !!
                    new JProperty("discount", Math.Round( 100*((ad.previous_price - ad.price) / ad.previous_price) ).ToString() ),
                    new JProperty("remaining_cupons", ad.total_cupons),
                    new JProperty("buyed_cupons", ad.total_cupons)
                    );

                string response = mService.CreateAd(send_data.ToString());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Ad/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Ad/Edit/5
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
        // GET: /Ad/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Ad/Delete/5
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
