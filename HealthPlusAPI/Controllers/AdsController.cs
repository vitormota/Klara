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
using System.Device.Location;

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
            int Distance = int.MaxValue/3;

            //close-1500-!-<search string>
            if (key.StartsWith("close-"))
            {
                string[] separator = new string[]{"-!-"};
                string[] separator1 = new string[] { "-" };
                string[] str0 = key.Split(separator,2,StringSplitOptions.None);
                string str1 = str0[0];
                key = str0[1];
                //str1 = str1.Substring(7, str1.Length - 7);
                str0 = str1.Split(separator1, 2, StringSplitOptions.None);
                Distance = int.Parse(str0[1]);
                
            }

            IQueryable<Ad> searchedAds01 = db.Ad.Where(Ad => Ad.name.Contains(key)).Concat( 
                                           db.Ad.Where(Ad => Ad.service.Contains(key))).Concat(
                                           db.Ad.Where(Ad => Ad.specialty.Contains(key))).Concat(
                                           db.Ad.Where(Ad => Ad.description.Contains(key))).Distinct();
            List<Ad> searchedAds01L = searchedAds01.ToList<Ad>();
            IQueryable<Ad> searchedAds = null;
            double[] myLatLong = getMyLat_Long();
            foreach(Ad ad in searchedAds01L){
                Institution ins = db.Institution.Where(Institution => Institution.id.Equals( ad.institution_id)).ToList<Institution>()[0];
                
                double dist = CalculateDistanceGPSCoordinates(Convert.ToDouble(ins.latitude), Convert.ToDouble(ins.longitude), myLatLong[0], myLatLong[1]);
                if (Distance >= dist)
                {
                    if (searchedAds == null)
                    {
                        searchedAds =db.Ad.Where(Ad => Ad.id.Equals( ad.id));
                    }else{
                        searchedAds.Concat(db.Ad.Where(Ad => Ad.id.Equals( ad.id)));
                    }
                }
            }
            if (searchedAds == null)
                return db.Ad.Where(Ad => Ad.id.Equals(-1));
            return searchedAds;

            /*
            return db.Ad.Where(Ad => Ad.name.Contains(key)).Concat( 
                   db.Ad.Where(Ad => Ad.service.Contains(key))).Concat(
                   db.Ad.Where(Ad => Ad.specialty.Contains(key))).Concat(
                   db.Ad.Where(Ad => Ad.description.Contains(key))).Distinct();
             * */
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
        [HttpPost]
        public IHttpActionResult Post(Ad ad)
        {
            string str = ModelState.ToString();

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

        private double CalculateDistanceGPSCoordinates(double lat1, double long1, double lat2, double long2)
        {
            if (lat1 < -90 || lat1 > 90 || long1 < -180 || long1 > 180 || lat2 < -90 || lat2 > 90 || long2 < -180 || long2 > 180)
                return 0.0;
            var first_point = new GeoCoordinate(lat1, long1);
            var second_point = new GeoCoordinate(lat2, long2);

            return (first_point.GetDistanceTo(second_point) / 1000);
        }

        private double[] getMyLat_Long()
        {
            //TODO usar a localização real, ex: session
            return new double[]{10.0,10.0};
        }
    }
}
