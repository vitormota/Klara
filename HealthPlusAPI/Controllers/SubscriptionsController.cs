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
        [HttpGet]
        public string GetSubscription([FromODataUri] int key)
        {
            string result = null;

            if(!ModelState.IsValid)
            {
                result = "error";
            }
            else
            {
                // Dada a chave permite ir a tabela de subscricoes saber quais as subcricoes relacionadas com o id do cliente
                List<Subscription> list_subscription = db.Subscription.Where(Subscription => Subscription.client_id == key).ToList();
                List<Institution> list_institution = new List<Institution>();

                for(int i = 0; i < list_subscription.Count; i++)
                {
                    int subscrible_id = list_subscription[i].subscribable_id;
                    // Ira servir para a obtencao da query e assim extrair os resultados
                    List<Institution> list_institution_query = db.Institution.Where(Institution => Institution.id == subscrible_id).ToList();
                    
                    if(!list_institution_query.Count.Equals(0))
                    {
                        list_institution.Add(list_institution_query[0]);
                    }
                }

                if(list_institution.Count.Equals(0))
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
        public IHttpActionResult Post(Subscription subscription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Subscription.Add(subscription);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SubscriptionExists(subscription.subscribable_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(subscription);
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
