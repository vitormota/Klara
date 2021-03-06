﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using HealthPlusAPI.Models;
using System.Threading.Tasks;


namespace HealthPlusAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            
            // Web API configuration and services
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional}
            );

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

            ActionConfiguration getClientPurchases = builder.Entity<purchased_ad>().Action("GetPurchases");
            getClientPurchases.Returns<string>();

            ActionConfiguration multipleCupons = builder.Entity<Cupon>().Collection.Action("MultipleCupons");
            multipleCupons.CollectionParameter<string>("Cupons");
            multipleCupons.Returns<Task<IHttpActionResult>>();
            

            // Ver quais os cupoes que estao ativos (que estao dentro da data de validade e que ainda nao foram usados)
            ActionConfiguration seeCuponsActive = builder.Entity<Cupon>().Action("SeeCuponsActive");
            seeCuponsActive.Returns<string>();


            // Adicionar a action para retornar um id de um cliente que esta associado a um id do facebook
            ActionConfiguration getClientIdFacebook = builder.Entity<Client>().Collection.Action("GetClientIDFacebook");
            getClientIdFacebook.Parameter<string>("client_id_by_session");
            getClientIdFacebook.Returns<string>();


            // Adicionar a action para retornar todas as subscricoes de instituicoes feitas pelo utilizador
            ActionConfiguration institutionsSubscribe = builder.Entity<Subscription>().Collection.Action("InstitutionsSubscribe");
            institutionsSubscribe.Parameter<string>("client_id");
            institutionsSubscribe.Returns<string>();


            // Adicionar a action para verificar se existe alguma conta com aquele facebook_id
            ActionConfiguration accountExistDatabase = builder.Entity<Account>().Collection.Action("AccountExistDatabase");
            accountExistDatabase.Parameter<string>("facebook_id");
            accountExistDatabase.Returns<string>();

            
            // Adicionar a action para retornar todas as subscricoes de anuncios feitas pelo utilizador
            ActionConfiguration adsSubscribe = builder.Entity<Subscription>().Collection.Action("AdsSubscribe");
            adsSubscribe.Parameter<string>("client_id");
            adsSubscribe.Returns<string>();

            // Adicionar a action para eliminar uma subscricao anteriormente feita
            ActionConfiguration deleteSubscription = builder.Entity<Subscription>().Collection.Action("DeleteSubscription");
            deleteSubscription.Parameter<string>("client_id");
            deleteSubscription.Parameter<string>("subscribable_id");
            deleteSubscription.Returns<string>();

            // Action para ver se o user tem alguma subscricao relacionada com aquele subscribable
            ActionConfiguration isSubscribeUser = builder.Entity<Subscription>().Collection.Action("IsSubscribeUser");
            isSubscribeUser.Parameter<string>("client_id");
            isSubscribeUser.Parameter<string>("subscribable_id");
            isSubscribeUser.Returns<string>();



            // Adicionar a action para procurar instituicoes
            ActionConfiguration searchInstitution = builder.Entity<Institution>().Collection.Action("SearchInstitution");
            searchInstitution.Parameter<string>("textSearch");
            searchInstitution.Parameter<string>("last_id");
            searchInstitution.Returns<string>();

            // Action que vai permitir a pesquisa de anuncios
            ActionConfiguration searchAd = builder.Entity<Ad>().Collection.Action("SearchAd");
            searchAd.Parameter<string>("textSearch");
            searchAd.Parameter<string>("last_id");
            searchAd.Returns<string>();


            // Adicionar a action para gerir login's
            ActionConfiguration managerLogin = builder.Entity<Manager>().Action("ManagerLogin");
            managerLogin.Parameter<string>("username");
            managerLogin.Parameter<string>("password");
            managerLogin.Returns<string>();

            // Instituições geríveis pelo manager
            ActionConfiguration fetchInstitutions = builder.Entity<Institution>().Action("FetchInstitutions");
            fetchInstitutions.Parameter<string>("manager_id");
            fetchInstitutions.Returns<string>();

            // Ativar publicidade para uma instituição
            ActionConfiguration advertiseInstitution = builder.Entity<Institution>().Collection.Action("Advertise");
            advertiseInstitution.Parameter<string>("institution_id");
            advertiseInstitution.Returns<string>();

            // Obter anúncios ativos
            ActionConfiguration activeAds = builder.Entity<Ad>().Collection.Action("GetActiveAds");
            activeAds.Parameter<string>("institution_id");
            activeAds.Returns<string>();

            // Obter anúncios passados mais vendidos
            ActionConfiguration inactiveBestAds = builder.Entity<Ad>().Collection.Action("GetInactiveBestAds");
            inactiveBestAds.Parameter<string>("institution_id");
            inactiveBestAds.Returns<string>();

            // Alterar estado do anúncio para apagado
            ActionConfiguration deleteAd = builder.Entity<Ad>().Collection.Action("DeleteAd");
            deleteAd.Parameter<string>("ad_id");
            deleteAd.Returns<string>();


            ActionConfiguration nearestInstitutions = builder.Entity<Institution>().Collection.Action("NearestInstitutions");
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
