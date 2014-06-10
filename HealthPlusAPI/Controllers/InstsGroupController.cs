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
    builder.EntitySet<Inst_Group>("InstsGroup");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class InstsGroupController : ODataController
    {
        private healthplusEntities db = new healthplusEntities();

        // GET odata/InstsGroup
        [Queryable]
        public IQueryable<Inst_Group> GetInstsGroup()
        {
            return db.Inst_Group;
        }

        // GET odata/InstsGroup(5)
        [Queryable]
        public SingleResult<Inst_Group> GetInst_Group([FromODataUri] int key)
        {
            return SingleResult.Create(db.Inst_Group.Where(inst_group => inst_group.id == key));
        }

        // PUT odata/InstsGroup(5)
        public IHttpActionResult Put([FromODataUri] int key, Inst_Group inst_group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != inst_group.id)
            {
                return BadRequest();
            }

            db.Entry(inst_group).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Inst_GroupExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(inst_group);
        }

        // POST odata/InstsGroup
        public IHttpActionResult Post(Inst_Group inst_group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Inst_Group.Add(inst_group);
            db.SaveChanges();

            return Created(inst_group);
        }

        // PATCH odata/InstsGroup(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Inst_Group> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Inst_Group inst_group = db.Inst_Group.Find(key);
            if (inst_group == null)
            {
                return NotFound();
            }

            patch.Patch(inst_group);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Inst_GroupExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(inst_group);
        }

        // DELETE odata/InstsGroup(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Inst_Group inst_group = db.Inst_Group.Find(key);
            if (inst_group == null)
            {
                return NotFound();
            }

            db.Inst_Group.Remove(inst_group);
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

        private bool Inst_GroupExists(int key)
        {
            return db.Inst_Group.Count(e => e.id == key) > 0;
        }
    }
}
