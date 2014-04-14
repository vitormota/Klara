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
using System.Web.Http.OData.Builder;
using HealthPlusAPI.Models;
using Newtonsoft.Json;

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
        private Models.AuxiliarClass.AuxiliarFunctions auxFunctions = new Models.AuxiliarClass.AuxiliarFunctions();

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

        [HttpPost]
        public string SearchInstitution([FromODataUri] int key, ODataActionParameters parameters)
        {
            string result = null;

            if (!ModelState.IsValid)
            {
                result = "error";
            }
            else
            {
                // Substituir muitos espacos introduzidos por apenas um espaco
                string textSearch = (string)parameters["textSearch"];
                textSearch.Replace("(,|?|!|&|=|\\(|\\))*\\s+(,|?|!|&|=|\\(|\\))*", " ");

                // Partir a string em palavras para a futura pesquisa
                string[] arrayStr = textSearch.Split(' ');
                List<string> listStr = new List<string>();

                // Selecionar as palavras com mais de duas letras
                for(int i = 0; i < arrayStr.Count(); i++)
                {
                    if(arrayStr[i].Length > 2)
                    {
                        listStr.Add(arrayStr[i]);
                    }
                }

                List<List<Institution>> listInstitution = new List<List<Institution>>();
                for (int i = 0; i < listStr.Count; i++)
                {
                    List<Institution> auxList = new List<Institution>(); // Lista para auxiliar nos foreach
                    string searchTextFor = auxFunctions.RemoverAcentos(listStr[i]).ToLower(); // Esta variavel auxiliar teve de ser criada para que possa funcionar na query que esta abaixo
                    //listInstitution.Add(db.Institution.Where(Institution => auxFunctions.RemoverAcentos(Institution.city).Contains(searchTextFor)).Union(db.Institution.Where(Institution => auxFunctions.RemoverAcentos(Institution.name).Contains(searchTextFor))).ToList());
                    
                    foreach(Institution institution in db.Institution)
                    {
                        if(auxFunctions.RemoverAcentos(institution.name).ToLower().Contains(searchTextFor))
                        {
                            if(!auxList.Contains(institution))
                            {
                                auxList.Add(institution);
                            }
                        }
                        else if(auxFunctions.RemoverAcentos(institution.city).ToLower().Contains(searchTextFor))
                        {
                            if (!auxList.Contains(institution))
                            {
                                auxList.Add(institution);
                            }
                        }
                    }

                    listInstitution.Add(auxList);
                }

                // Pedaco de codigo que vai dar a ordem da pesquisa
                Dictionary<Institution, int> searchResult = new Dictionary<Institution, int>(); // Local onde ficam guardados os registos da pesquisa
            
                for (int i = 0; i < listInstitution.Count; i++)
                {
                    for (int l = 0; l < listInstitution[i].Count; l++)
                    {
                        int similiar_values = 0; // serve para ver o numero de repeticoes entre listas

                        if (!searchResult.ContainsKey(listInstitution[i][l])) // se a instituicao ja estiver la nao precisa de fazer os ciclos
                        {
                            for (int j = 0; j < listInstitution.Count; j++)
                            {
                                for (int k = 0; k < listInstitution[j].Count; k++)
                                {
                                    if (listInstitution[i][l].id == listInstitution[j][k].id)
                                    {
                                        similiar_values++;
                                        break;
                                    }
                                }
                            }

                            searchResult.Add(listInstitution[i][l], similiar_values);
                        }
                    }
                }
                // Final do ciclo for

                Dictionary<Institution, int> searchResultSort = new Dictionary<Institution, int>();
                foreach(var institution in searchResult.OrderBy(inst => inst.Value))
                {
                    searchResultSort.Add(institution.Key, institution.Value);
                }

                List<Institution> listFinalSearch = new List<Institution>(); // Lista, gerada a partir de um dicionario, que vai ter as instituicoes ordenadas, foi feito com uma lista para auxiliar na conversao para JSON
                foreach(var institution in searchResultSort)
                {
                    listFinalSearch.Add(institution.Key);
                }

                listFinalSearch.Reverse(); // necessario inverter a ordem para primeiro mostrar aqueles que estao mais de acordo com o input
                result = JsonConvert.SerializeObject(listFinalSearch);
            }

            return result;
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
