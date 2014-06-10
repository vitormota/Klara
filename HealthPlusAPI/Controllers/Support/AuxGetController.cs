using HealthPlusAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HealthPlusAPI.Controllers.Support
{
    public class AuxGetController : ApiController
    {
        /// <summary>
        /// DB access
        /// </summary>
        private healthplusEntities db = new healthplusEntities();

        /// <summary>
        /// Get client purchases
        /// </summary>
        /// <param name="client_id"></param>
        /// <returns></returns>
        [Route("odata/Cupon({client_id:int})/Purchases")]
        public IEnumerable<dynamic> getClientPurchases(int client_id)
        {
            IQueryable<dynamic> query = (from cupon in db.Cupon
                                         join ad in db.Ad
                                         on cupon.ad_id equals ad.id
                                         where cupon.client_id == client_id
                                         select new
                                         {
                                             service = ad.service,
                                             name = ad.name,
                                             speciality = ad.specialty,
                                             state = cupon.state,
                                             end_time = cupon.end_time,
                                             purchase_time = cupon.purchase_time

                                         }).AsQueryable();

            return query;
        }

        /// <summary>
        /// Retrieve this client's account options
        /// </summary>
        /// <param name="client_id"></param>
        /// <returns>Single result containning the client id and options</returns>
        [Route("odata/Accounts({client_id:int})/OptionsFlags")]
        public dynamic getClientAccountInfo(int client_id)
        {
            IQueryable<dynamic> query = db.Account
                .Select(account => new
                {
                    id = account.id,
                    notifications = account.receive_notification,
                    show_ads = account.show_ads
                })
                .Where(account => account.id == client_id);

            return query.Single();

            //return SingleResult.Create(db.Account
            //    .Select(account => new Account 
            //    { id = account.id, 
            //        show_ads = account.show_ads, 
            //        receive_notification = account.receive_notification 
            //    })
            //    .Where(account => account.id == client_id));
        }

        /// <summary>
        /// Retrieve client's cupon subscriptions
        /// </summary>
        /// <param name="client_id">The client id</param>
        /// <returns></returns>
        [Route("odata/Subscriptions({client_id})/Cupons")]
        public dynamic getClientCuponSubs(int client_id)
        {
            IQueryable<dynamic> query = (from client in db.Client
                                         join sub in db.Subscription
                                         on new { id = client.id } equals new { id = sub.client_id }
                                         join s in db.Subscribable
                                         on new { subid = sub.subscribable_id } equals new { subid = s.id }
                                         join ad in db.Ad
                                         on new { adid = sub.subscribable_id } equals new { adid = ad.id }
                                         join inst in db.Institution
                                         on new { instid = ad.institution_id } equals new { instid = inst.id }
                                         select new
                                         {
                                             id = ad.id,
                                             name = ad.name,
                                             institution_id = ad.institution_id,
                                             price = ad.price,
                                             description = ad.description,
                                             end_time = ad.end_time,
                                             service = ad.service,
                                             specialty = ad.specialty,
                                             remaining_cupons = ad.remaining_cupons,
                                             local = inst.city,
                                             institution_name = inst.name    
                                         }
                         ).AsQueryable();
            return query;
        }

        /// <summary>
        /// Get ads details by its id
        /// NOTE: Moved here because someone rewritten default action on ads controller
        /// </summary>
        /// <param name="ad_id">The ads id</param>
        /// <returns></returns>
        [Route("odata/Ads({ad_id})")]
        public KeyValuePair<Ad, Institution> getAdDetails(int ad_id)
        {
            Ad ad = db.Ad.Where(Ad => Ad.id == ad_id).Single();
            Institution ins = db.Institution.Where(Institution => Institution.id.Equals( ad.institution_id)).Single();

            return new KeyValuePair<Ad,Institution>(ad, ins);
        }

        /// <summary>
        /// Dynamic get for adds, retrieved portions of ads table, specifing what to retrieve
        /// by limit, offset and order by column
        /// </summary>
        /// <param name="lower_bound">Meaning Sql offset from the result set</param>
        /// <param name="upper_bound">Meaning Sql limit of the result set</param>
        /// <param name="on">Column name in which data should be ordered specifying also type of order (DESC/ASC)
        /// using the following model "name-otype" e.g. "id-desc", defaults to desc</param>
        /// <returns></returns>
        [Route("odata/Ads/{lower_bound:int}/{upper_bound:int}/{on}")]
        public dynamic getAdsByRule(int lower_bound, int upper_bound,string on)
        {
            //DbSet<Ad> ads = db.Ad;
            //DbSet<Institution> institutions = db.Institution;
            string[] criteria = on.Split('-');
            string order_column = criteria[0];
            string order_criteria = "DESC";
            if (criteria.Length > 1)
            {
                order_criteria = criteria[1];
            }
            string command = "SELECT * FROM robinfoo_lgp.searchable_ad" + 
                " ORDER BY " + order_column + " " + order_criteria +
                " LIMIT " + lower_bound + "," + upper_bound;
            var res = db.Database.SqlQuery<searchable_ad>(command);

            return res;
        }
    }



}
