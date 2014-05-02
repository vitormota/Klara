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
using Newtonsoft.Json;

namespace HealthPlusAPI.Controllers
{
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using HealthPlusAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Subscription>("Subscriptions");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class SubscriptionsController : ODataController
    {
        private healthplusEntities db = new healthplusEntities();

        // GET odata/Subscriptions
        [Queryable]
        public IQueryable<Subscription> GetSubscriptions()
        {
            return db.Subscription;
        }

        // GET odata/Subscriptions(5)
        [Queryable]
        public SingleResult<Subscription> GetSubscription([FromODataUri] int key)
        {
            return null;
        }

        [HttpPost]
        public string InstitutionsSubscribe(ODataActionParameters parameters)
        {
            string result = null;
            int client_id = Convert.ToInt32((string)parameters["client_id"]);

            if (!ModelState.IsValid)
            {
                result = "error";
            }
            else
            {
                // Dada a chave permite ir a tabela de subscricoes saber quais as subcricoes relacionadas com o id do cliente
                List<Subscription> list_subscription = db.Subscription.Where(Subscription => Subscription.client_id == client_id).ToList();
                List<Institution> list_institution = new List<Institution>();

                for (int i = 0; i < list_subscription.Count; i++)
                {
                    int subscrible_id = list_subscription[i].subscribable_id;
                    // Ira servir para a obtencao da query e assim extrair um resultado de cada vez
                    Institution list_institution_query = db.Institution.Where(Institution => Institution.id == subscrible_id).ToList().First();
                    list_institution.Add(list_institution_query);
                }

                if (list_institution.Count.Equals(0))
                {
                    result = "no subscriptions";
                }
                else
                {
                    result = JsonConvert.SerializeObject(list_institution);
                }
            }

            return result;
        }

        [HttpPost]
        public string AdsSubscribe(ODataActionParameters parameters)
        {
            string result = null;
            int client_id = Convert.ToInt32((string)parameters["client_id"]);

            if (!ModelState.IsValid)
            {
                result = "error";
            }
            else
            {
                // Dada a chave permite ir a tabela de subscricoes saber quais as subcricoes relacionadas com o id do cliente
                List<Subscription> list_subscription = db.Subscription.Where(Subscription => Subscription.client_id == client_id).ToList();
                List<Ad> list_ad = new List<Ad>();

                for (int i = 0; i < list_subscription.Count; i++)
                {
                    int subscrible_id = list_subscription[i].subscribable_id;
                    // Ira servir para a obtencao da query e assim extrair um resultado de cada vez
                    Ad list_ad_query = db.Ad.Where(Ad => Ad.id == subscrible_id).ToList().First();
                    list_ad.Add(list_ad_query);
                }

                if (list_ad.Count.Equals(0))
                {
                    result = "no subscriptions";
                }
                else
                {
                    result = JsonConvert.SerializeObject(list_ad);
                }
            }

            return result;
        }

        // PUT odata/Subscriptions(5)
        public IHttpActionResult Put([FromODataUri] int key, Subscription subscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != subscription.subscribable_id)
            {
                return BadRequest();
            }

            db.Entry(subscription).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriptionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(subscription);
        }

        // POST odata/Subscriptions
        public string Post(Subscription subscription)
        {
            if (!ModelState.IsValid)
            {
                return "error";
            }
            else
            {
                if (db.Subscription.Count(subs => subs.subscribable_id == subscription.subscribable_id) == 0)
                {
                    db.Subscription.Add(subscription);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateException)
                    {
                        return "error update";
                    }
                }
                else
                {
                    return "exist subscription";
                }
            }

            return JsonConvert.SerializeObject(subscription);
        }

        // PATCH odata/Subscriptions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Subscription> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Subscription subscription = db.Subscription.Find(key);
            if (subscription == null)
            {
                return NotFound();
            }

            patch.Patch(subscription);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriptionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(subscription);
        }

        [HttpPost]
        public string DeleteSubscription(ODataActionParameters parameters)
        {
            string return_str = null;

            if(!ModelState.IsValid)
            {
                return_str = "error";
            }
            else
            {
                int subscribable_id_parameter = Convert.ToInt32((string)parameters["subscribable_id"]);
                int client_id = Convert.ToInt32((string)parameters["client_id"]);

                // Seleccao das subscricoes a partir da instituicao
                List<Subscription> listSubscription = db.Subscription.Where(Subscription => Subscription.client_id == client_id && Subscription.subscribable_id == subscribable_id_parameter).ToList();
                Subscription selectedSubscription = listSubscription[0];

                db.Subscription.Remove(selectedSubscription);
                db.SaveChanges();

                return_str = "ok";
            }

            return return_str;
        }

        // DELETE odata/Subscriptions(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Subscription subscription = db.Subscription.Find(key);
            if (subscription == null)
            {
                return NotFound();
            }

            db.Subscription.Remove(subscription);
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

        private bool SubscriptionExists(int key)
        {
            return db.Subscription.Count(e => e.subscribable_id == key) > 0;
        }
    }
}
