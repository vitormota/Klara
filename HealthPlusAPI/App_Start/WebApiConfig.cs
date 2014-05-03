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
            builder.EntitySet<Photo>("Photos");

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

            // Inserir fotografia para anúncio
            ActionConfiguration insertAdPhoto = builder.Entity<Photo>().Action("InsertAdPhoto");
            insertAdPhoto.Parameter<string>("ad_id");
            insertAdPhoto.Parameter<string>("photo_guid");
            insertAdPhoto.Parameter<string>("data_stream");
            insertAdPhoto.Returns<string>();

            // Obter fotografias de anúncios
            ActionConfiguration getAdPhotos = builder.Entity<Photo>().Collection.Action("GetAdPhotos");
            getAdPhotos.Parameter<string>("ad_ids");
            getAdPhotos.Returns<string>();

            // Obter anúncios ativos
            ActionConfiguration activeAds = builder.Entity<Ad>().Collection.Action("GetActiveAds");
            activeAds.Parameter<string>("institution_id");
            activeAds.Returns<string>();


            ActionConfiguration nearestInstitutions = builder.Entity<Institution>().Action("NearestInstitutions");
            nearestInstitutions.Parameter<string>("latitude");
            nearestInstitutions.Parameter<string>("longitude");
            nearestInstitutions.Parameter<string>("distance");
            nearestInstitutions.Returns<string>();

            config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
