using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HealthPlusAPI.Controllers
{
    public class ReferenceBankController : ApiController
    {
        public IHttpActionResult getReference()
        {
            return Ok(Models.ReferenceBank.Reference.NewReference());
        }
    }
}
