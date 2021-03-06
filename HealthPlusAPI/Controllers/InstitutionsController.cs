﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Device.Location;
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
using Newtonsoft.Json.Linq;

namespace HealthPlusAPI.Controllers {
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using HealthPlusAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Institution>("Institutions");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    public class InstitutionsController : ODataController {
        private healthplusEntities db = new healthplusEntities();
        private Models.AuxiliarClass.AuxiliarFunctions auxFunctions = new Models.AuxiliarClass.AuxiliarFunctions();

        // GET odata/Institutions
        [Queryable]
        public string GetInstitutions() {
            return JsonConvert.SerializeObject(db.Institution.ToList());
        }

        // GET odata/Institutions(5)
        [Queryable]
        public SingleResult<Institution> GetInstitution([FromODataUri] int key) {
            return SingleResult.Create(db.Institution.Where(institution => institution.id == key));
        }

        [HttpPost]
        public string SearchInstitution(ODataActionParameters parameters) {   
            int last_id = Convert.ToInt32(parameters["last_id"]);
            int rowN = 4;

            if (last_id == -1)
            {
                rowN = 10;
            }
            string result = null;

            if (!ModelState.IsValid) {
                result = "error";
            } else {
                // Substituir muitos espacos introduzidos por apenas um espaco
                string textSearch = (string)parameters["textSearch"];
                textSearch.Replace("(,|?|!|&|=|\\(|\\))*\\s+(,|?|!|&|=|\\(|\\))*", " ");

                // Partir a string em palavras para a futura pesquisa
                string[] arrayStr = textSearch.Split(' ');
                List<string> listStr = new List<string>();

                // Selecionar as palavras com mais de duas letras
                for (int i = 0; i < arrayStr.Count(); i++) {
                    if (arrayStr[i].Length > 1) {
                        listStr.Add(arrayStr[i]);
                    } else if (arrayStr[i].Length.Equals(1) && arrayStr.Count().Equals(1)) {
                        listStr.Add(arrayStr[i]);
                    }
                }

                // Pedaco de codigo que vai dar a ordem da pesquisa
                Dictionary<Institution, int> searchResult = new Dictionary<Institution, int>(); // Local onde ficam guardados os registos da pesquisa
                List<List<Institution>> listInstitution = new List<List<Institution>>();

                while (searchResult.Count < rowN) {
                    if (db.Institution.ToList().Last().id <= last_id)
                        break;
                    for (int i = 0; i < listStr.Count; i++) {
                        List<Institution> auxList = new List<Institution>(); // Lista para auxiliar nos foreach
                        string searchTextFor = auxFunctions.RemoverAcentos(listStr[i]).ToLower(); // Esta variavel auxiliar teve de ser criada para que possa funcionar na query que esta abaixo
                        //listInstitution.Add(db.Institution.Where(Institution => auxFunctions.RemoverAcentos(Institution.city).Contains(searchTextFor)).Union(db.Institution.Where(Institution => auxFunctions.RemoverAcentos(Institution.name).Contains(searchTextFor))).ToList());

                        IQueryable<Institution> insts = db.Institution.Where(inst => inst.id > last_id);

                        foreach (Institution institution in insts) {
                            last_id = institution.id;

                            string[] nameSplit = auxFunctions.RemoverAcentos(institution.name).ToLower().Split(' ');
                            string[] citySplit = auxFunctions.RemoverAcentos(institution.city).ToLower().Split(' ');

                            foreach (string s in nameSplit) {
                                if (s.StartsWith(searchTextFor)) {
                                    if (!auxList.Contains(institution)) {
                                        auxList.Add(institution);
                                        break;
                                    }
                                }
                            }

                            foreach (string s in citySplit) {
                                if (s.StartsWith(searchTextFor)) {
                                    if (!auxList.Contains(institution)) {
                                        auxList.Add(institution);
                                        break;
                                    }
                                }
                            }

                            /*
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
                            */

                            if (auxList.Count == (rowN - searchResult.Count))
                                break;
                        }

                        listInstitution.Add(auxList);
                    }

                    for (int i = 0; i < listInstitution.Count; i++) {
                        for (int l = 0; l < listInstitution[i].Count; l++) {
                            int similiar_values = 0; // serve para ver o numero de repeticoes entre listas

                            if (!searchResult.ContainsKey(listInstitution[i][l])) // se a instituicao ja estiver la nao precisa de fazer os ciclos
                        {
                                for (int j = 0; j < listInstitution.Count; j++) {
                                    for (int k = 0; k < listInstitution[j].Count; k++) {
                                        if (listInstitution[i][l].id == listInstitution[j][k].id) {
                                            similiar_values++;
                                            break;
                                        }
                                    }
                                }

                                searchResult.Add(listInstitution[i][l], similiar_values);
                            }
                        }
                    }
                }
                // Final do ciclo for

                Dictionary<Institution, int> searchResultSort = new Dictionary<Institution, int>();
                foreach (var institution in searchResult.OrderBy(inst => inst.Value)) {
                    searchResultSort.Add(institution.Key, institution.Value);
                }

                List<Institution> listFinalSearch = new List<Institution>(); // Lista, gerada a partir de um dicionario, que vai ter as instituicoes ordenadas, foi feito com uma lista para auxiliar na conversao para JSON
                foreach (var institution in searchResultSort) {
                    listFinalSearch.Add(institution.Key);
                }

                listFinalSearch.Reverse(); // necessario inverter a ordem para primeiro mostrar aqueles que estao mais de acordo com o input

                List<JObject> listFinalWithGroups = new List<JObject>(); // Lista que vai ter o nome do grupo no qual a instituição esta associada
                for (int i = 0; i < listFinalSearch.Count; i++) {
                    string name_group = null;

                    if (listFinalSearch[i].group_id != null) {
                        Inst_Group group = db.Inst_Group.Where(gr => gr.id == listFinalSearch[i].group_id).First();
                        name_group = group.name;
                    }

                    JObject instWithGroup = new JObject(
                        new JProperty("id", listFinalSearch[i].id),
                        new JProperty("group_id", listFinalSearch[i].group_id),
                        new JProperty("name_group", name_group),
                        new JProperty("name", listFinalSearch[i].name),
                        new JProperty("website", listFinalSearch[i].website),
                        new JProperty("phone_number", listFinalSearch[i].phone_number),
                        new JProperty("address", listFinalSearch[i].address),
                        new JProperty("city", listFinalSearch[i].city),
                        new JProperty("email", listFinalSearch[i].email),
                        new JProperty("fax", listFinalSearch[i].fax),
                        new JProperty("latitude", listFinalSearch[i].latitude),
                        new JProperty("longitude", listFinalSearch[i].longitude),
                        new JProperty("img_link", listFinalSearch[i].img_url));

                    listFinalWithGroups.Add(instWithGroup);

                }
                result = JsonConvert.SerializeObject(listFinalWithGroups);
            }

            return result;
        }

        [HttpPost]
        public string NearestInstitutions(ODataActionParameters parameters) {
            string result = null;

            if (!ModelState.IsValid) {
                result = "error";
            } else {
                double latitude = Convert.ToDouble((string)parameters["latitude"]);
                double longitude = Convert.ToDouble((string)parameters["longitude"]);
                double distance = Convert.ToDouble((string)parameters["distance"]);

                List<Institution> listInstitutions = db.Institution.ToList();
                Dictionary<Institution, double> dictInstitutions = new Dictionary<Institution, double>();

                for (int i = 0; i < listInstitutions.Count; i++) {
                    double calculateDistance = CalculateDistanceGPSCoordinates(Convert.ToDouble(listInstitutions[i].latitude), Convert.ToDouble(listInstitutions[i].longitude), latitude, longitude);

                    if (calculateDistance <= distance) {
                        dictInstitutions.Add(listInstitutions[i], calculateDistance);
                    }
                }

                Dictionary<Institution, double> dictInstitutionsSort = new Dictionary<Institution, double>();
                foreach (var institution in dictInstitutions.OrderBy(inst => inst.Value)) {
                    dictInstitutionsSort.Add(institution.Key, institution.Value);
                }

                List<Institution> listFinalSearch = new List<Institution>(); // Lista, gerada a partir de um dicionario, que vai ter as instituicoes ordenadas, foi feito com uma lista para auxiliar na conversao para JSON
                foreach (var institution in dictInstitutionsSort) {
                    listFinalSearch.Add(institution.Key);
                }

                List<JObject> listFinalWithGroups = new List<JObject>(); // Lista que vai ter o nome do grupo no qual a instituição esta associada
                for (int i = 0; i < listFinalSearch.Count; i++) {
                    string name_group = null;

                    if (listFinalSearch[i].group_id != null) {
                        Inst_Group group = db.Inst_Group.Where(gr => gr.id == listFinalSearch[i].group_id).First();
                        name_group = group.name;
                    }

                    JObject instWithGroup = new JObject(
                        new JProperty("id", listFinalSearch[i].id),
                        new JProperty("group_id", listFinalSearch[i].group_id),
                        new JProperty("name_group", name_group),
                        new JProperty("name", listFinalSearch[i].name),
                        new JProperty("website", listFinalSearch[i].website),
                        new JProperty("phone_number", listFinalSearch[i].phone_number),
                        new JProperty("address", listFinalSearch[i].address),
                        new JProperty("city", listFinalSearch[i].city),
                        new JProperty("email", listFinalSearch[i].email),
                        new JProperty("fax", listFinalSearch[i].fax),
                        new JProperty("latitude", listFinalSearch[i].latitude),
                        new JProperty("longitude", listFinalSearch[i].longitude));

                    listFinalWithGroups.Add(instWithGroup);

                }

                result = JsonConvert.SerializeObject(listFinalWithGroups);
            }

            return result;
        }

        [HttpPost]
        public string Advertise(ODataActionParameters parameters) {
            int institutionId = Convert.ToInt32((string)parameters["institution_id"]);

            Institution ins = db.Institution.Find(institutionId);

            if (ins == null) {
                return "not found";
            } else {
                ins.advertise = true;
                db.Entry(ins).State = EntityState.Modified;
                db.SaveChanges();
            }
            return "success";
        }

        [HttpPost]
        public string FetchInstitutions([FromODataUri] int key, ODataActionParameters parameters) {
            string result = null;
            int managerId = Convert.ToInt32((string)parameters["manager_id"]);

            if (!ModelState.IsValid) {
                result = "error";
            } else {
                try {

                    var institutions =
                        from ins in db.Institution
                        join map in db.Manager_Institution_maps on ins.id equals map.institution_id
                        where map.manager_id == managerId
                        select ins;

                    List<Institution> list_institutions = institutions.ToList(); // Passar o resultado das instituicoes para uma lista para nao haver problemas no ciclo for

                    List<JObject> listFinalWithGroups = new List<JObject>(); // Lista que vai ter o nome do grupo no qual a instituição esta associada
                    for (int i = 0; i < list_institutions.Count; i++) {
                        string name_group = null;

                        if (list_institutions[i].group_id != null) {
                            Inst_Group group = db.Inst_Group.Where(gr => gr.id == list_institutions[i].group_id).First();
                            name_group = group.name;
                        }

                        JObject instWithGroup = new JObject(
                            new JProperty("id", list_institutions[i].id),
                            new JProperty("group_id", list_institutions[i].group_id),
                            new JProperty("name_group", name_group),
                            new JProperty("name", list_institutions[i].name),
                            new JProperty("website", list_institutions[i].website),
                            new JProperty("phone_number", list_institutions[i].phone_number),
                            new JProperty("address", list_institutions[i].address),
                            new JProperty("city", list_institutions[i].city),
                            new JProperty("email", list_institutions[i].email),
                            new JProperty("fax", list_institutions[i].fax),
                            new JProperty("latitude", list_institutions[i].latitude),
                            new JProperty("longitude", list_institutions[i].longitude),
                            new JProperty("advertise", list_institutions[i].advertise));

                        listFinalWithGroups.Add(instWithGroup);

                    }

                    result = JsonConvert.SerializeObject(listFinalWithGroups);

                    if (institutions == null) {
                        result = "invalid user";
                    } else {

                    }
                } catch (WebException e) {
                    result = e.Message;
                }
            }

            return result;
        }

        // PUT odata/Institutions(5)
        public IHttpActionResult Put([FromODataUri] int key, Institution institution) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (key != institution.id) {
                return BadRequest();
            }

            db.Entry(institution).State = EntityState.Modified;

            try {
                db.SaveChanges();
            } catch (DbUpdateConcurrencyException) {
                if (!InstitutionExists(key)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return Updated(institution);
        }

        // POST odata/Institutions
        public IHttpActionResult Post(Institution institution) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            db.Institution.Add(institution);

            try {
                db.SaveChanges();
            } catch (DbUpdateException) {
                if (InstitutionExists(institution.id)) {
                    return Conflict();
                } else {
                    throw;
                }
            }

            return Created(institution);
        }

        // PATCH odata/Institutions(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Institution> patch) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            Institution institution = db.Institution.Find(key);
            if (institution == null) {
                return NotFound();
            }

            patch.Patch(institution);

            try {
                db.SaveChanges();
            } catch (DbUpdateConcurrencyException) {
                if (!InstitutionExists(key)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return Updated(institution);
        }

        // DELETE odata/Institutions(5)
        public IHttpActionResult Delete([FromODataUri] int key) {
            Institution institution = db.Institution.Find(key);
            if (institution == null) {
                return NotFound();
            }

            db.Institution.Remove(institution);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InstitutionExists(int key) {
            return db.Institution.Count(e => e.id == key) > 0;
        }

        private double CalculateDistanceGPSCoordinates(double lat1, double long1, double lat2, double long2) {
            var first_point = new GeoCoordinate(lat1, long1);
            var second_point = new GeoCoordinate(lat2, long2);

            return (first_point.GetDistanceTo(second_point) / 1000);
        }
    }
}
