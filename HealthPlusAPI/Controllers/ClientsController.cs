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
using Newtonsoft.Json;
using System.Net.Mail;

namespace HealthPlusAPI.Controllers
{
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using HealthPlusAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Client>("Clients");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ClientsController : ODataController
    {
        private healthplusEntities db = new healthplusEntities();

        // GET odata/Clients
        [Queryable]
        public IQueryable<Client> GetClients()
        {
            return db.Client;
        }

        // GET odata/Clients(5)
        [Queryable]
        public SingleResult<Client> GetClient([FromODataUri] int key)
        {
            return SingleResult.Create(db.Client.Where(Client => Client.id == key));
        }

        [HttpPost]
        public string GetClientIDFacebook([FromODataUri] int key, ODataActionParameters parameters)
        {
            string return_str = null;
            long facebook_id = Convert.ToInt64((string)parameters["client_id_by_session"]);

            if (!ModelState.IsValid)
            {
                return_str = "error";
            }
            else
            {
                // Obter o id na tabela account a partir do id do facebook
                List<Account> account_list = db.Account.Where(Account => Account.fb_id == facebook_id).ToList();
                int client_id = account_list[0].id;

                // Extrair o id do cliente
                List<Client> client_list = db.Client.Where(Client => Client.id == client_id).ToList();
                return_str = (client_list[0].id).ToString();
            }

            return return_str;
        }

        // PUT odata/Clients(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != client.id)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(client);
        }

        // POST odata/Clients
        public async Task<IHttpActionResult> Post(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Client.Add(client);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClientExists(client.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(client);
        }

        // PATCH odata/Clients(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Client> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Client client = await db.Client.FindAsync(key);
            if (client == null)
            {
                return NotFound();
            }

            patch.Patch(client);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(client);
        }

        // DELETE odata/Clients(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Client client = await db.Client.FindAsync(key);
            if (client == null)
            {
                return NotFound();
            }

            db.Client.Remove(client);
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

        private bool ClientExists(int key)
        {
            return db.Client.Count(e => e.id == key) > 0;
        }

        public bool ConcactSupport(string email, string msg, Client client)
        {
            SmtpClient mySmtpClient = new SmtpClient("my.smtp.exampleserver.net");

            // set smtp-client with basicAuthentication
            mySmtpClient.UseDefaultCredentials = false;
            System.Net.NetworkCredential basicAuthenticationInfo = new
               System.Net.NetworkCredential("username", "password");
            mySmtpClient.Credentials = basicAuthenticationInfo;

            // add from,to mailaddresses
            MailAddress from = new MailAddress(client.email, client.name);
            MailAddress to = new MailAddress(email, "HealthPlusSupport");
            MailMessage myMail = new System.Net.Mail.MailMessage(from, to);


            // set subject and encoding
            myMail.Subject = "Client Support";
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;

            // set body-message and encoding
            myMail.Body = msg;
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            // text or html
            myMail.IsBodyHtml = true;

            mySmtpClient.Send(myMail);

            return true;
        }
    }
}
