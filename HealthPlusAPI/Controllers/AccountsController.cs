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
    builder.EntitySet<Acccount>("Accounts");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class AccountsController : ODataController
    {
        private healthplusEntities db = new healthplusEntities();

        // GET odata/Accounts
        [Queryable]
        public IQueryable<Acccount> GetAccounts()
        {
            return db.Acccounts;
        }

        // GET odata/Accounts(5)
        [Queryable]
        public SingleResult<Acccount> GetAcccount([FromODataUri] int key)
        {
            return SingleResult.Create(db.Acccounts.Where(acccount => acccount.id == key));
        }

        // PUT odata/Accounts(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Acccount acccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != acccount.id)
            {
                return BadRequest();
            }

            db.Entry(acccount).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcccountExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(acccount);
        }

        // POST odata/Accounts
        public async Task<IHttpActionResult> Post(Acccount acccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Acccounts.Add(acccount);
            await db.SaveChangesAsync();

            return Created(acccount);
        }

        // PATCH odata/Accounts(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Acccount> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Acccount acccount = await db.Acccounts.FindAsync(key);
            if (acccount == null)
            {
                return NotFound();
            }

            patch.Patch(acccount);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcccountExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(acccount);
        }

        // DELETE odata/Accounts(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Acccount acccount = await db.Acccounts.FindAsync(key);
            if (acccount == null)
            {
                return NotFound();
            }

            db.Acccounts.Remove(acccount);
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

        private bool AcccountExists(int key)
        {
            return db.Acccounts.Count(e => e.id == key) > 0;
        }
    }
}
