using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
    builder.EntitySet<Account>("Accounts");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AccountsController : ODataController
    {
        private healthplusEntities db = new healthplusEntities();

        // GET odata/Accounts
        [Queryable]
        public IQueryable<Account> GetAccounts()
        {
            return db.Account;
        }

        // GET odata/Accounts(5L)
        [Queryable]
        public SingleResult<Account> GetAccount([FromODataUri] long key)
        {
            return SingleResult.Create(db.Account.Where(Account => Account.fb_id == key));
        }

        // PUT odata/Accounts(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Account Account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != Account.id)
            {
                return BadRequest();
            }

            db.Entry(Account).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(Account);
        }


        [HttpPost]
        public string AccountExistDatabase(ODataActionParameters parameters)
        {
            string return_str = null;
            long facebook_id = Convert.ToInt64((string)parameters["facebook_id"]);

            if (!ModelState.IsValid)
            {
                return_str = "error";
            }
            else
            {
                List<Account> list_accounts = db.Account.Where(account => account.fb_id == facebook_id).ToList();

                if (list_accounts.Count > 0)
                {
                    return_str = list_accounts[0].id.ToString();
                }
                else if (list_accounts.Count == 0)
                {
                    return_str = "false";
                }
            }

            return return_str;
        }

        // POST odata/Accounts
        public async Task<IHttpActionResult> Post(Account Account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Account.Add(Account);
            await db.SaveChangesAsync();

            return Created(Account);
        }

        // PATCH odata/Accounts(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Account> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Account Account = await db.Account.FindAsync(key);
            if (Account == null)
            {
                return NotFound();
            }

            patch.Patch(Account);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(Account);
        }

        // DELETE odata/Accounts(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Account Account = await db.Account.FindAsync(key);
            if (Account == null)
            {
                return NotFound();
            }

            db.Account.Remove(Account);
            await db.SaveChangesAsync();

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

        private bool AccountExists(int key)
        {
            return db.Account.Count(e => e.id == key) > 0;
        }
    }
}
