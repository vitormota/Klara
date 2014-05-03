using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using HealthPlusAPI.Models;
using System.IO;
using System.Drawing;
using System.Web;

namespace HealthPlusAPI.Controllers
{
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using HealthPlusAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Photo>("Photo");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PhotosController : ODataController
    {
        private healthplusEntities db = new healthplusEntities();

        // GET odata/Photo
        [Queryable]
        public IQueryable<Photo> GetPhoto()
        {
            return db.Photo;
        }

        // GET odata/Photo
        [HttpPost]
        public string InsertAdPhoto([FromODataUri] int key, ODataActionParameters parameters)
        {
            string result = null;
            int ad_id = Convert.ToInt32((string)parameters["ad_id"]);
            string photo_guid = (string)parameters["photo_guid"];
            string data_stream = (string)parameters["data_stream"];

            // Add new photo entry
            Photo photo = new Photo { guid = photo_guid };
            db.Photo.Add(photo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PhotoExists(photo.guid))
                {
                    return "already exists";
                }
                else
                {
                    throw;
                }
            }

            // Add mapping to Ad
            Ad_Photo_maps apm = new Ad_Photo_maps { ad_id = ad_id, photo_id = photo_guid };
            db.Ad_Photo_maps.Add(apm);
            db.SaveChanges();

            // Save photo in local folder
            byte[] imageBytes = Convert.FromBase64String(data_stream);

            using (var ms = new MemoryStream(imageBytes, 0,imageBytes.Length))
            {
                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);

                var path = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data/Ad_Photos"), photo_guid);

                image.Save(path, System.Drawing.Imaging.ImageFormat.Png);
            }

            return "added";
        }

        // GET odata/Photo(5)
        [Queryable]
        public SingleResult<Photo> GetPhoto([FromODataUri] string key)
        {
            return SingleResult.Create(db.Photo.Where(photo => photo.guid == key));
        }

        // PUT odata/Photo(5)
        public IHttpActionResult Put([FromODataUri] string key, Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != photo.guid)
            {
                return BadRequest();
            }

            db.Entry(photo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(photo);
        }

        // POST odata/Photo
        public IHttpActionResult Post(Photo photo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Photo.Add(photo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PhotoExists(photo.guid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(photo);
        }

        // PATCH odata/Photo(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] string key, Delta<Photo> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Photo photo = db.Photo.Find(key);
            if (photo == null)
            {
                return NotFound();
            }

            patch.Patch(photo);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(photo);
        }

        // DELETE odata/Photo(5)
        public IHttpActionResult Delete([FromODataUri] string key)
        {
            Photo photo = db.Photo.Find(key);
            if (photo == null)
            {
                return NotFound();
            }

            db.Photo.Remove(photo);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PhotoExists(string key)
        {
            return db.Photo.Count(e => e.guid == key) > 0;
        }
    }
}
