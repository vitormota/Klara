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

            // save all ad's ids in list
            List<int> adIds = new List<int>();

            foreach (AdDisplayModel adm in adList)
            {
                adIds.Add(adm.id);
            }

            // get each ad's guid(s)
            string guids_str = mService.GetAdPhotos(adIds.ToArray());

            Dictionary<int, List<string>> guids = JsonConvert.DeserializeObject<Dictionary<int, List<string>>>(guids_str);

            foreach (AdDisplayModel adm in adList)
            {
                List<string> ad_guids = guids[adm.id];

                if (ad_guids.Any())
                {
                    adm.guids = ad_guids;
                }
                else
                {
                    // add defalut ad image
                }
            }

            //List<String> photo_paths = JsonConvert.DeserializeObject<List<string>>(photos);

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
                    new JProperty("state", "pending")
                    );

                string response = mService.CreateAd(send_data.ToString());

                AdDisplayModel adm = JsonConvert.DeserializeObject<AdDisplayModel>(response);

                if (ad.photo.ContentLength > 0)
                {

                    System.Drawing.Image img = System.Drawing.Image.FromStream(ad.photo.InputStream);
                    string url = Helpers.ImgurUpload.UploadImage(img);

                    //var path = Path.Combine(Server.MapPath("~/Resources/Ad_Photos"), guid + ".png");
                    //photo.SaveAs(path);


                    // save image locally
                    //img.Save(path, System.Drawing.Imaging.ImageFormat.Png);

                    //create image ref in DB
                    mService.InsertAdPhoto(adm.id, url);

                    // upload image to API - deprecated
                    //MemoryStream stream = new MemoryStream();
                    //img.Save(stream, img.RawFormat);
                    //mService.InsertAdPhoto(adm.id, guid, Convert.ToBase64String(stream.ToArray()));
                }
                else
                {
                    return View();
                }

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
