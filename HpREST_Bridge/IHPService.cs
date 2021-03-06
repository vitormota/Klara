﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HpREST_Bridge
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHPService" in both code and config file together.
    [ServiceContract]
    public interface IHPService
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here

        [OperationContract]
        string GetUserDetails(int id);

        [OperationContract]
        string GetClientDetails(int id);

        [OperationContract]
        string GetClientPurchases(int id);

        [OperationContract]
        string UpdateClientDetails(int id, string client_jobj);

        [OperationContract]
        string BuyCupon(string cupon_str);

        [OperationContract]
        string BuyMultipleCupons(string cupon_list);

        [OperationContract]
        string GetAdSubscriptions(int client_id);

        [OperationContract]
        string GetInstitutionSubscriptions(int client_id);

        [OperationContract]
        string GetAdById(int id);

        [OperationContract]
        string RegisterUser(string access_token, int provider);

        [OperationContract]
        string UserLogin(string access_token, int provider);

        [OperationContract]
        string SearchInstitution(string textSearch, int last_id);

        [OperationContract]
        string FetchInstitutions(string manager_id);

        [OperationContract]
        string SearchAd(string textSearch, int last_id);

        [OperationContract]
        string InstitutionsSubscribe(int client_id);

        [OperationContract]
        string SubscribeInstitution(int institution_id, int client_id);

        [OperationContract]
        string UnsubscribeInstitution(int institution_id, int client_id);

        [OperationContract]
        string ManagerLogin(string unsername, string password);

        [OperationContract]
        string NearestInstitutions(double latitude, double longitude, double distance);

        [OperationContract]
        string SeeCuponsActive(int client_id);

        [OperationContract]
        string SubscribeAd(int client_id, int ad_id);

        [OperationContract]
        string UnsubscribeAd(int client_id, int ad_id);

        [OperationContract]
        string AdsSubscribe(int client_id);

        [OperationContract]
        string IsSubscribeUser(int client_id, int subscribable_id);

        /// <summary>
        /// Change institution details
        /// </summary>
        /// <param name="model_data">data to update</param>
        /// <returns>API response</returns>
        [OperationContract]
        string EditInstitutionDetails(string model_data, int id);

        [OperationContract]
        string AdvertiseInstitution(int id);

        [OperationContract]
        string CreateAd(string model_data);

        [OperationContract]
        string GetInstitution(int id);

        [OperationContract]
        string GetActiveAds(int institution_id);

        [OperationContract]
        string GetInactiveBestAds(int institution_id);

        [OperationContract]
        string DeleteAd(int ad_id);

        [OperationContract]
        string GetBankReference();

        [OperationContract]
        string GetAdsByRule(int offset, int limit, string order_by);

    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "HpREST_Bridge.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
