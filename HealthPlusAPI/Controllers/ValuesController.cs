using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthPlusAPI.Models.ReferenceBank;

namespace HealthPlusAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        // ficheiro em fase de teste, ver se consegue colocar uma referencia
        public List<Int32> Get()
        {
            return HealthPlusAPI.Models.ReferenceBank.Reference.NewReference();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}