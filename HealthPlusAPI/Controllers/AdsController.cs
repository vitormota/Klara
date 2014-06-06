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
using System.Device.Location;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace HealthPlusAPI.Controllers
{
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using HealthPlusAPI.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Ad>("Ads");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */



    public class AdsController : ODataController
    {
        private healthplusEntities db = new healthplusEntities();
        private Models.AuxiliarClass.AuxiliarFunctions auxFunctions = new Models.AuxiliarClass.AuxiliarFunctions();

        // GET odata/Ads
        [Queryable]
        public IQueryable<Ad> GetAds()
        {
            return db.Ad;
        }

        // GET odata/Ads(5)
        [Queryable]
        public IQueryable<Ad> GetAd([FromODataUri] string key)
        {
            int Distance = int.MaxValue/3;

            //close-1500-!-<search string>
            if (key.StartsWith("close-"))
            {
                string[] separator = new string[]{"-!-"};
                string[] separator1 = new string[] { "-" };
                string[] str0 = key.Split(separator,2,StringSplitOptions.None);
                string str1 = str0[0];
                key = str0[1];
                //str1 = str1.Substring(7, str1.Length - 7);
                
                str0 = str1.Split(separator1, 2, StringSplitOptions.None);
                Distance = int.Parse(str0[1]);
                
            }

            IQueryable<Ad> searchedAds01 = db.Ad.Where(Ad => Ad.name.Contains(key)).Concat( 
                                           db.Ad.Where(Ad => Ad.service.Contains(key))).Concat(
                                           db.Ad.Where(Ad => Ad.specialty.Contains(key))).Concat(
                                           db.Ad.Where(Ad => Ad.description.Contains(key))).Distinct();
        
            List<Ad> searchedAds01L = searchedAds01.ToList<Ad>();
            IQueryable<Ad> searchedAds = null;
            double[] myLatLong = getMyLat_Long();
            
            foreach(Ad ad in searchedAds01L)
            {
                Institution ins = db.Institution.Where(Institution => Institution.id.Equals( ad.institution_id)).ToList<Institution>()[0];
                
                double dist = CalculateDistanceGPSCoordinates(Convert.ToDouble(ins.latitude), Convert.ToDouble(ins.longitude), myLatLong[0], myLatLong[1]);
                if (Distance >= dist)
                {
                    if (searchedAds == null)
                    {
                        searchedAds =db.Ad.Where(Ad => Ad.id.Equals( ad.id));
                    }
                    else
                    {
                        searchedAds.Concat(db.Ad.Where(Ad => Ad.id.Equals( ad.id)));
                    }
                }
            }
            
            if (searchedAds == null)
                return db.Ad.Where(Ad => Ad.id.Equals(-1));
            
            return searchedAds;

            /*
            return db.Ad.Where(Ad => Ad.name.Contains(key)).Concat( 
                   db.Ad.Where(Ad => Ad.service.Contains(key))).Concat(
                   db.Ad.Where(Ad => Ad.specialty.Contains(key))).Concat(
                   db.Ad.Where(Ad => Ad.description.Contains(key))).Distinct();
             * */
        }

        [HttpPost]
        public string SearchAd(ODataActionParameters parameters)
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
                for (int i = 0; i < arrayStr.Count(); i++)
                {
                    if (arrayStr[i].Length > 1)
                    {
                        listStr.Add(arrayStr[i]);
                    }
                    else if (arrayStr[i].Length.Equals(1) && arrayStr.Count().Equals(1))
                    {
                        listStr.Add(arrayStr[i]);
                    }
                }

                List<List<Ad>> listAd = new List<List<Ad>>();
                for (int i = 0; i < listStr.Count; i++)
                {
                    List<Ad> auxList = new List<Ad>(); // Lista para auxiliar nos foreach
                    string searchTextFor = auxFunctions.RemoverAcentos(listStr[i]).ToLower(); // Esta variavel auxiliar teve de ser criada para que possa funcionar na query que esta abaixo
                    //listInstitution.Add(db.Institution.Where(Institution => auxFunctions.RemoverAcentos(Institution.city).Contains(searchTextFor)).Union(db.Institution.Where(Institution => auxFunctions.RemoverAcentos(Institution.name).Contains(searchTextFor))).ToList());

                    foreach (Ad ad in db.Ad)
                    {
                        if (auxFunctions.RemoverAcentos(ad.name).ToLower().Contains(searchTextFor))
                        {
                            if (!auxList.Contains(ad))
                            {
                                auxList.Add(ad);
                            }
                        }
                        else if (auxFunctions.RemoverAcentos(ad.service).ToLower().Contains(searchTextFor))
                        {
                            if (!auxList.Contains(ad))
                            {
                                auxList.Add(ad);
                            }
                        }
                        else if (auxFunctions.RemoverAcentos(ad.specialty).ToLower().Contains(searchTextFor))
                        {
                            if (!auxList.Contains(ad))
                            {
                                auxList.Add(ad);
                            }
                        }
                    }

                    listAd.Add(auxList);
                }

                // Pedaco de codigo que vai dar a ordem da pesquisa
                Dictionary<Ad, int> searchResult = new Dictionary<Ad, int>(); // Local onde ficam guardados os registos da pesquisa

                for (int i = 0; i < listAd.Count; i++)
                {
                    for (int l = 0; l < listAd[i].Count; l++)
                    {
                        int similiar_values = 0; // serve para ver o numero de repeticoes entre listas

                        if (!searchResult.ContainsKey(listAd[i][l])) // se a instituicao ja estiver la nao precisa de fazer os ciclos
                        {
                            for (int j = 0; j < listAd.Count; j++)
                            {
                                for (int k = 0; k < listAd[j].Count; k++)
                                {
                                    if (listAd[i][l].id == listAd[j][k].id)
                                    {
                                        similiar_values++;
                                        break;
                                    }
                                }
                            }

                            searchResult.Add(listAd[i][l], similiar_values);
                        }
                    }
                }
                // Final do ciclo for

                Dictionary<Ad, int> searchResultSort = new Dictionary<Ad, int>();
                foreach (var ad in searchResult.OrderBy(ad_aux => ad_aux.Value))
                {
                    searchResultSort.Add(ad.Key, ad.Value);
                }

                List<Ad> listFinalSearch = new List<Ad>(); // Lista, gerada a partir de um dicionario, que vai ter as instituicoes ordenadas, foi feito com uma lista para auxiliar na conversao para JSON
                foreach (var ad in searchResultSort)
                {
                    listFinalSearch.Add(ad.Key);
                }

                listFinalSearch.Reverse(); // necessario inverter a ordem para primeiro mostrar aqueles que estao mais de acordo com o input

                List<JObject> listFinalWithInsts = new List<JObject>(); // Lista que vai ter o nome do grupo no qual a instituição esta associada
                for (int i = 0; i < listFinalSearch.Count; i++)
                {
                    string name_institution = "no institution";
                    float latitude_institution = 0.0f;
                    float longitude_institution = 0.0f;

                    Ad aux_ad = listFinalSearch[i]; // Serve para nao dar erro na query que se segue
                    List<Institution> inst = db.Institution.Where(ins => ins.id == aux_ad.institution_id).ToList();
                    
                    if(inst.Count != 0)
                    {
                        name_institution = inst.First().name;
                        latitude_institution = (float)inst.First().latitude;
                        longitude_institution = (float)inst.First().longitude;
                    }

                    JObject adWithInst = new JObject(
                        new JProperty("id", listFinalSearch[i].id),
                        new JProperty("institution_id", listFinalSearch[i].institution_id),
                        new JProperty("name_institution", name_institution),
                        new JProperty("name", listFinalSearch[i].name),
                        new JProperty("price", listFinalSearch[i].price),
                        new JProperty("remaining_cupons", listFinalSearch[i].remaining_cupons),
                        new JProperty("service", listFinalSearch[i].service),
                        new JProperty("specialty", listFinalSearch[i].specialty),
                        new JProperty("start_time", listFinalSearch[i].start_time),
                        new JProperty("end_time", listFinalSearch[i].end_time),
                        new JProperty("buyed_cupons", listFinalSearch[i].buyed_cupons),
                        new JProperty("description", listFinalSearch[i].description),
                        new JProperty("discount", listFinalSearch[i].discount),
                        new JProperty("latitude_institution",latitude_institution),
                        new JProperty("longitude_institution",longitude_institution));

                    listFinalWithInsts.Add(adWithInst);

                }

                result = JsonConvert.SerializeObject(listFinalWithInsts);
            }

            return result;
        }
        
        [HttpPost]
        public string GetActiveAds(ODataActionParameters parameters)
        {
            int institution_id = Convert.ToInt32( (string)parameters["institution_id"] );

            if (institution_id == 0)
            {
                var ads = (from ad in db.Ad
                           from institution in db.Institution
                           where ad.state != "deleted" &&
                           ad.institution_id.Equals(institution.id) &&
                           ad.start_time.CompareTo(DateTime.Now) < 0 &&
                           ad.end_time.CompareTo(DateTime.Now) > 0
                           orderby ad.end_time descending
                           select new {ad, institution});

                var l = ads.ToList();

                return JsonConvert.SerializeObject(ads);
            }
            else if (institution_id != 0)
            {
                var ads = (from ad in db.Ad
                           where ad.institution_id == institution_id &&
                           ad.state != "deleted" &&
                           ad.start_time.CompareTo(DateTime.Now) < 0 &&
                           ad.end_time.CompareTo(DateTime.Now) > 0
                           orderby ad.end_time descending
                           select ad);

                var l = ads.ToList();

                return JsonConvert.SerializeObject(ads);
            }

            return null;
        }

        [HttpPost]
        public string GetInactiveBestAds(ODataActionParameters parameters)
        {
            int institution_id = Convert.ToInt32((string)parameters["institution_id"]);

            var ads = (from ad in db.Ad
                       where ad.institution_id == institution_id &&
                       ad.state == "deleted" ||
                       ad.end_time.CompareTo(DateTime.Now) < 0
                       orderby ad.buyed_cupons descending
                       select ad).Take(5);

            var l = ads.ToList();

            return JsonConvert.SerializeObject(ads); ;
        }

        [HttpPost]
        public string DeleteAd(ODataActionParameters parameters)
        {
            int ad_id = Convert.ToInt32((string)parameters["ad_id"]);

            Ad ad = db.Ad.Find(ad_id);
            if (ad == null)
            {
                return "ad not found";
            }
            else
            {
                ad.state = "deleted";
                db.Entry(ad).State = EntityState.Modified;
                db.SaveChanges();
            }

            return "deleted";
        }

        // PUT odata/Ads(5)
        public IHttpActionResult Put([FromODataUri] int key, Ad ad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != ad.id)
            {
                return BadRequest();
            }

            db.Entry(ad).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(ad);
        }

        // POST odata/Ads
        [HttpPost]
        public IHttpActionResult Post(Ad ad)
        {
            string str = ModelState.ToString();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Subscribable s = new Subscribable();

            /// Insert in Subscribable to create id
            db.Subscribable.Add(s);
            db.SaveChanges();
            ad.id = s.id;

            db.Ad.Add(ad);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AdExists(ad.id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(ad);
        }

      
        // PATCH odata/Ads(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Ad> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ad ad = db.Ad.Find(key);
            if (ad == null)
            {
                return NotFound();
            }

            patch.Patch(ad);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(ad);
        }

        // DELETE odata/Ads(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Ad ad = db.Ad.Find(key);
            if (ad == null)
            {
                return NotFound();
            }
            ad.state = "deleted";
            db.Entry(ad).State = EntityState.Modified;
            db.SaveChanges();

            return Updated(ad);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdExists(int key)
        {
            return db.Ad.Count(e => e.id == key) > 0;
        }

        private double CalculateDistanceGPSCoordinates(double lat1, double long1, double lat2, double long2)
        {
            if (lat1 < -90 || lat1 > 90 || long1 < -180 || long1 > 180 || lat2 < -90 || lat2 > 90 || long2 < -180 || long2 > 180)
                return 0.0;
            var first_point = new GeoCoordinate(lat1, long1);
            var second_point = new GeoCoordinate(lat2, long2);

            return (first_point.GetDistanceTo(second_point) / 1000);
        }

        private double[] getMyLat_Long()
        {
            //TODO usar a localização real, ex: session
            return new double[]{10.0,10.0};
        }
    }
}
