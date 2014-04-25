using HealthPlusAPI.Models;
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
        public IEnumerable<Cupon> getClientPurchases(int client_id)
        {
            return db.Cupon.Where(cupon => cupon.client_id == client_id);
        }
    }
}
