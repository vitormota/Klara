﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HealthPlusAPI.Models.ReferenceBank;

namespace HealthPlusAPI.Controllers
{
    public class ReferenceBankController : ApiController
    {
        public IEnumerable<Int32> getReference()
        {
            return Models.ReferenceBank.Reference.NewReference();
        }
    }
}