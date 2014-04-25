using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using HealthPlusAPI.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HealthPlusAPI.Controllers
{
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using HealthPlusAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Cupon>("Cupon");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CuponController : ODataController
    {
        private healthplusEntities db = new healthplusEntities();

        // GET odata/Cupon
        [Queryable]
        public IQueryable<Cupon> GetCupon()
        {
            return db.Cupon;
        }

        // GET odata/Cupon(5)
        [Queryable]
        public SingleResult<Cupon> GetCupon([FromODataUri] int key)
        {
            return SingleResult.Create(db.Cupon.Where(cupon => cupon.id == key));
        }

        ///// <summary>
        ///// Get purchased cupons from client id
        ///// </summary>
        ///// <param name="key">User's internal id</param>
        ///// <returns></returns>
        //[HttpPost]
        //public string GetPurchases([FromODataUri] int key)
        //{
        //    List<Cupon> list = db.Cupon.Where(cupon => cupon.client_id == key).ToList();
        //    return JsonConvert.SerializeObject(list);
        //}



        // PUT odata/Cupon(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Cupon cupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != cupon.id)
            {
                return BadRequest();
            }

            db.Entry(cupon).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuponExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(cupon);
        }

        // POST odata/Cupon
        public async Task<IHttpActionResult> Post(Cupon cupon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cupon.Add(cupon);
            await db.SaveChangesAsync();

            return Created(cupon);
        }

        // PATCH odata/Cupon(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Cupon> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cupon cupon = await db.Cupon.FindAsync(key);
            if (cupon == null)
            {
                return NotFound();
            }

            patch.Patch(cupon);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuponExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(cupon);
        }

        // DELETE odata/Cupon(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Cupon cupon = await db.Cupon.FindAsync(key);
            if (cupon == null)
            {
                return NotFound();
            }

            db.Cupon.Remove(cupon);
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

        private bool CuponExists(int key)
        {
            return db.Cupon.Count(e => e.id == key) > 0;
        }
    }
}
