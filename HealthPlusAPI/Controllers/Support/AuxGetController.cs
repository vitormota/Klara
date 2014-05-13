using HealthPlusAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
                                         select new
                                         {
                                             ad_id = ad.id,
                                             name = ad.name,
                                             inst = ad.institution_id,
                                             price = ad.price,
                                             desc = ad.description,
                                             end = ad.end_time,
                                             service = ad.service,
                                             speciality = ad.specialty,
                                             remaining = ad.remaining_cupons
                                         }
                         ).AsQueryable();
            return query;
        }

        /// <summary>
        /// Get ad details by its id
        /// NOTE: Moved here because someone rewritten default action on ads controller
        /// </summary>
        /// <param name="ad_id">The ad id</param>
        /// <returns></returns>
        [Route("odata/Ads({ad_id})")]
        public dynamic getAdDetails(int ad_id)
        {
            return (db.Ad.Where(Ad => Ad.id == ad_id).Single());
        }
    }



}
