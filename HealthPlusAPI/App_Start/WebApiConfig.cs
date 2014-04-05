using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using HealthPlusAPI.Models;

namespace HealthPlusAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Acccount>("Accounts");
            config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
