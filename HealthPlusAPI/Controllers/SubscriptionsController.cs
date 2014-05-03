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
using Newtonsoft.Json.Linq;

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
                    List<JObject> listFinalWithGroups = new List<JObject>(); // Lista que vai ter o nome do grupo no qual a instituição esta associada
                    for (int i = 0; i < list_institution.Count; i++)
                    {
                        string name_group = null;

                        if (list_institution[i].group_id != null)
                        {
                            Inst_Group group = db.Inst_Group.Where(gr => gr.id == list_institution[i].group_id).First();
                            name_group = group.name;
                        }

                        JObject instWithGroup = new JObject(
                            new JProperty("id", list_institution[i].id),
                            new JProperty("group_id", list_institution[i].group_id),
                            new JProperty("name_group", name_group),
                            new JProperty("name", list_institution[i].name),
                            new JProperty("website", list_institution[i].website),
                            new JProperty("phone_number", list_institution[i].phone_number),
                            new JProperty("address", list_institution[i].address),
                            new JProperty("city", list_institution[i].city),
                            new JProperty("email", list_institution[i].email),
                            new JProperty("fax", list_institution[i].fax),
                            new JProperty("latitude", list_institution[i].latitude),
                            new JProperty("longitude", list_institution[i].longitude));

                        listFinalWithGroups.Add(instWithGroup);

                    }

                    result = JsonConvert.SerializeObject(listFinalWithGroups);
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
                    List<JObject> listFinalWithInsts = new List<JObject>(); // Lista que vai ter o nome da instituicao na qual o anuncio esta associado
                    for (int i = 0; i < list_ad.Count; i++)
                    {
                        string name_institution = "no institution";

                        Ad aux_ad = list_ad[i]; // Serve para nao dar erro na query que se segue
                        List<Institution> inst = db.Institution.Where(ins => ins.id == aux_ad.institution_id).ToList();

                        if (inst.Count != 0)
                        {
                            name_institution = inst.First().name;
                        }

                        JObject adWithInst = new JObject(
                            new JProperty("id", list_ad[i].id),
                            new JProperty("institution_id", list_ad[i].institution_id),
                            new JProperty("name_institution", name_institution),
                            new JProperty("name", list_ad[i].name),
                            new JProperty("price", list_ad[i].price),
                            new JProperty("remaining_cupons", list_ad[i].remaining_cupons),
                            new JProperty("service", list_ad[i].service),
                            new JProperty("specialty", list_ad[i].specialty),
                            new JProperty("start_time", list_ad[i].start_time),
                            new JProperty("end_time", list_ad[i].end_time),
                            new JProperty("buyed_cupons", list_ad[i].buyed_cupons),
                            new JProperty("description", list_ad[i].description),
                            new JProperty("discount", list_ad[i].discount));

                        listFinalWithInsts.Add(adWithInst);

                    }

                    result = JsonConvert.SerializeObject(listFinalWithInsts);
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
