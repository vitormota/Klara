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
    builder.EntitySet<Manager>("Manager");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ManagersController : ODataController
    {
        private healthplusEntities db = new healthplusEntities();
        private Models.AuxiliarClass.AuxiliarFunctions auxFunctions = new Models.AuxiliarClass.AuxiliarFunctions();

        // GET odata/Manager
        [Queryable]
        public IQueryable<Manager> GetManager()
        {
            return db.Manager;
        }

        // GET odata/Manager(5)
        [Queryable]
        public SingleResult<Manager> GetManager([FromODataUri] int key)
        {
            return SingleResult.Create(db.Manager.Where(manager => manager.id == key));
        }

        [HttpPost]
        public string ManagerLogin([FromODataUri] int key, ODataActionParameters parameters)
        {
            string result = null;
            string username = (string) parameters["username"];
            string password = (string) parameters["password"];

            if (!ModelState.IsValid)
            {
                result = "error";
            }
            else
            {
                try
                {
                    Manager m = (
                        from mng in db.Manager
                        where mng.username.Equals(username)
                        select mng).FirstOrDefault();

                    if (m == null)
                    {
                        result = "not a valid user";
                    }
                    else
                    {
                        if (m.password.Equals(auxFunctions.GetSHA1Hash(password)))
                        {
                            result = "logged";
                        }
                        else
                        {
                            result = "incorrect password";
                        }
                    }
                }
                catch (WebException e)
                {
                    result = e.Message;
                }
            }

            return result;
        }

        // PUT odata/Manager(5)
        public IHttpActionResult Put([FromODataUri] int key, Manager manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != manager.id)
            {
                return BadRequest();
            }

            db.Entry(manager).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(manager);
        }

        // POST odata/Manager
        public IHttpActionResult Post(Manager manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            manager.password = auxFunctions.GetSHA1Hash(manager.password);

            db.Manager.Add(manager);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ManagerExists(manager.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(manager);
        }

        // PATCH odata/Manager(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Manager> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Manager manager = db.Manager.Find(key);
            if (manager == null)
            {
                return NotFound();
            }

            patch.Patch(manager);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(manager);
        }

        // DELETE odata/Manager(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Manager manager = db.Manager.Find(key);
            if (manager == null)
            {
                return NotFound();
            }

            db.Manager.Remove(manager);
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

        private bool ManagerExists(int key)
        {
            return db.Manager.Count(e => e.id == key) > 0;
        }
    }
}
