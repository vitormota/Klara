using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebInstitution.HealthPService;
using WebInstitution.Models;
using System.IO;

namespace WebInstitution.Controllers
{
    public class AdController : BaseController
    {
        private HealthPService.IHPService mService = new HPServiceClient();
        
        //
        // GET: /Ad/
        public ActionResult Index()
        {
            return View(); 
        }

        //
        // GET: /Ad/Details
        public ActionResult Details()
        {
            SessionModel session = (SessionModel)Session["manager"];

            string ads = mService.GetActiveAds(session.currentInstitution.id);

            List<AdDisplayModel> adList = JsonConvert.DeserializeObject<List<AdDisplayModel>>(ads);

            ViewBag.Ads = adList;

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
                string url;

                if (ad.photo.ContentLength > 0)
                {
                    // upload selected image
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ad.photo.InputStream);
                    url = Helpers.ImgurUpload.UploadImage(img);
                }
                else if(ad.img_url != null)
                {
                    // use template image
                    url = ad.img_url;
                }
                else
                {
                    // use healthPlus standard image
                    url = "http://i.imgur.com/eNO1So9.png";
                }


                JObject send_data = new JObject(
                    new JProperty("institution_id", session.currentInstitution.id),
                    new JProperty("service", ad.service),
                    new JProperty("specialty", ad.specialty),
                    new JProperty("name", ad.name),
                    new JProperty("description", ad.description),
                    new JProperty("start_time", ad.start_time),
                    new JProperty("end_time", ad.end_time),
                    new JProperty("price", ad.price.ToString()), // decimals must have quotes !!
                    new JProperty("discount", Math.Round(100 * ((ad.previous_price - ad.price) / ad.previous_price)).ToString()),
                    new JProperty("remaining_cupons", ad.total_cupons),
                    new JProperty("buyed_cupons", 0),
                    new JProperty("state", "active"),
                    new JProperty("img_url", url)
                    );

                string response = mService.CreateAd(send_data.ToString());
                return RedirectToAction("Edit", "Dashboard");
                
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
            mService.DeleteAd(id);
            return RedirectToAction("Edit", "Dashboard");
        }

        private Boolean IsFileAnImage(string strFileName)
        {
            Boolean RetVal = true;
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(strFileName);
                img.Dispose();
            }
            catch
            {
                RetVal = false;
            }
            return RetVal;
        }
    }
}
