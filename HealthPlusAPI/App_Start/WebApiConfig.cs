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

            config.MapHttpAttributeRoutes();

            // Web API routes
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Account>("Accounts");
            builder.EntitySet<Client>("Clients");
            builder.EntitySet<Cupon>("Cupon");
            builder.EntitySet<Ad>("Ads");
            builder.EntitySet<Subscription>("Subscriptions");
            builder.EntitySet<Institution>("Institutions");
            builder.EntitySet<Manager>("Managers");

            ActionConfiguration getClientPurchases = builder.Entity<Cupon>().Action("GetPurchases");
            getClientPurchases.Returns<string>();

            // Ver quais os cupoes que estao ativos (que estao dentro da data de validade e que ainda nao foram usados)
            ActionConfiguration seeCuponsActive = builder.Entity<Cupon>().Action("SeeCuponsActive");
            seeCuponsActive.Returns<string>();


            // Adicionar a action para retornar um id de um cliente que esta associado a um id do facebook
            ActionConfiguration getClientIdFacebook = builder.Entity<Client>().Action("GetClientIDFacebook");
            getClientIdFacebook.Parameter<string>("client_id_by_session");
            getClientIdFacebook.Returns<string>();

           

            // Adicionar a action para retornar todas as subscricoes feitas pelo utilizador
            ActionConfiguration institutionsSubscribe = builder.Entity<Subscription>().Action("InstitutionsSubscribe");
            institutionsSubscribe.Parameter<string>("client_id");
            institutionsSubscribe.Returns<string>();

            // Adicionar a action para eliminar uma subscricao anteriormente feita
            ActionConfiguration deleteSubscription = builder.Entity<Subscription>().Action("DeleteSubscription");
            deleteSubscription.Parameter<string>("client_id");
            deleteSubscription.Parameter<string>("institution_id");
            deleteSubscription.Returns<string>();

           

            // Adicionar a action para procurar instituicoes
            ActionConfiguration searchInstitution = builder.Entity<Institution>().Action("SearchInstitution");
            searchInstitution.Parameter<string>("textSearch");
            searchInstitution.Returns<string>();

            
            // Adicionar a action para gerir login's
            ActionConfiguration managerLogin = builder.Entity<Manager>().Action("ManagerLogin");
            managerLogin.Parameter<string>("username");
            managerLogin.Parameter<string>("password");
            managerLogin.Returns<string>();

            // Instituições geríveis pelo manager
            ActionConfiguration fetchInstitutions = builder.Entity<Institution>().Action("FetchInstitutions");
            fetchInstitutions.Parameter<string>("manager_id");
            fetchInstitutions.Returns<string>();

            // Inserir um novo anúncio
            //ActionConfiguration insertAd = builder.Entity<Ad>().Action("InsertAd");
            //fetchInstitutions.Parameter<Ad>("ad");
            //fetchInstitutions.Returns<string>();

            ActionConfiguration nearestInstitutions = builder.Entity<Institution>().Action("NearestInstitutions");
            nearestInstitutions.Parameter<string>("latitude");
            nearestInstitutions.Parameter<string>("longitude");
            nearestInstitutions.Parameter<string>("distance");
            nearestInstitutions.Returns<string>();


            ActionConfiguration contactSupport = builder.Entity<Client>().Action("ContactSupport");
            contactSupport.Parameter<string>("email");
            contactSupport.Parameter<string>("msg");
            contactSupport.Returns<string>();

            config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
