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

namespace HealthPlusAPI.Controllers
{
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using HealthPlusAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Ad>("Ads");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AdsController : ODataController
    {
        private healthplusEntities db = new healthplusEntities();

        // GET odata/Ads
        [Queryable]
        public IQueryable<Ad> GetAds()
        {
            return db.Ad;
        }

        // GET odata/Ads(5)
        [Queryable]
        public IQueryable<Ad> GetAd([FromODataUri] string key)
        {
            return db.Ad.Where(Ad => Ad.name.Contains(key)).Concat( 
                   db.Ad.Where(Ad => Ad.service.Contains(key))).Concat(
                   db.Ad.Where(Ad => Ad.specialty.Contains(key))).Concat(
                   db.Ad.Where(Ad => Ad.description.Contains(key))).Distinct();
        }

        // PUT odata/Ads(5)
        public IHttpActionResult Put([FromODataUri] int key, Ad ad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != ad.id)
            {
                return BadRequest();
            }

            db.Entry(ad).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(ad);
        }

        // POST odata/Ads
        public IHttpActionResult Post(Ad ad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ad.Add(ad);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AdExists(ad.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(ad);
        }

        // PATCH odata/Ads(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Ad> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ad ad = db.Ad.Find(key);
            if (ad == null)
            {
                return NotFound();
            }

            patch.Patch(ad);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(ad);
        }

        // DELETE odata/Ads(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Ad ad = db.Ad.Find(key);
            if (ad == null)
            {
                return NotFound();
            }

            db.Ad.Remove(ad);
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

        private bool AdExists(int key)
        {
            return db.Ad.Count(e => e.id == key) > 0;
        }
    }
}
