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
            builder.EntitySet<Account>("Accounts");
            builder.EntitySet<Client>("Clients");

            // Adicionar a action para retornar um id de um cliente que esta associado a um id do facebook
            ActionConfiguration getClientIdFacebook = builder.Entity<Client>().Action("GetClientIDFacebook");
            getClientIdFacebook.Parameter<string>("client_id_by_session");
            getClientIdFacebook.Returns<string>();

            builder.EntitySet<Ad>("Ads");
            builder.EntitySet<Subscription>("Subscriptions");

            // Adicionar a action para retornar todas as subscricoes feitas pelo utilizador
            ActionConfiguration institutionsSubscribe = builder.Entity<Subscription>().Action("InstitutionsSubscribe");
            institutionsSubscribe.Parameter<string>("client_id");
            institutionsSubscribe.Returns<string>();

            // Adicionar a action para eliminar uma subscricao anteriormente feita
            ActionConfiguration deleteSubscription = builder.Entity<Subscription>().Action("DeleteSubscription");
            deleteSubscription.Parameter<string>("client_id");
            deleteSubscription.Parameter<string>("institution_id");
            deleteSubscription.Returns<string>();

            builder.EntitySet<Institution>("Institutions");

            // Adicionar a action para procurar instituicoes
            ActionConfiguration searchInstitution = builder.Entity<Institution>().Action("SearchInstitution");
            searchInstitution.Parameter<string>("textSearch");
            searchInstitution.Returns<string>();

            builder.EntitySet<Manager>("Managers");

            // Adicionar a action para procurar instituicoes
            ActionConfiguration managerLogin = builder.Entity<Manager>().Action("ManagerLogin");
            managerLogin.Parameter<string>("username");
            managerLogin.Parameter<string>("password");
            managerLogin.Returns<string>();

            config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
