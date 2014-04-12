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

namespace HealthPlusAPI.Controllers
{
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using HealthPlusAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Institution>("Institutions");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class InstitutionsController : ODataController
    {
        private healthplusEntities db = new healthplusEntities();

        // GET odata/Institutions
        [Queryable]
        public IQueryable<Institution> GetInstitutions()
        {
            return db.Institution;
        }

        // GET odata/Institutions(5)
        [Queryable]
        public SingleResult<Institution> GetInstitution([FromODataUri] int key)
        {
            return SingleResult.Create(db.Institution.Where(institution => institution.id == key));
        }

        public string SearchInstitution(ODataActionParameters parameters)
        {
            return (string)parameters["textSearch"];
        }

        // PUT odata/Institutions(5)
        public IHttpActionResult Put([FromODataUri] int key, Institution institution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != institution.id)
            {
                return BadRequest();
            }

            db.Entry(institution).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstitutionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(institution);
        }

        // POST odata/Institutions
        public IHttpActionResult Post(Institution institution)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Institution.Add(institution);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (InstitutionExists(institution.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(institution);
        }

        // PATCH odata/Institutions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Institution> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Institution institution = db.Institution.Find(key);
            if (institution == null)
            {
                return NotFound();
            }

            patch.Patch(institution);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstitutionExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(institution);
        }

        // DELETE odata/Institutions(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Institution institution = db.Institution.Find(key);
            if (institution == null)
            {
                return NotFound();
            }

            db.Institution.Remove(institution);
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

        private bool InstitutionExists(int key)
        {
            return db.Institution.Count(e => e.id == key) > 0;
        }
    }
}
