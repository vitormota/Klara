using Newtonsoft.Json.Linq;
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
        string GetClientPurchases(int id);

        [OperationContract]
        string RegisterUser(string access_token, int provider);

        [OperationContract]
        string UserLogin(string access_token, int provider);

        [OperationContract]
        string SearchInstitution(string textSearch);

        [OperationContract]
        string SearchAd(string textSearch);

        [OperationContract]
        string InstitutionsSubscribe(int client_id);

        [OperationContract]
        string SubscribeInstitution(int institution_id, long client_id_by_session);

        [OperationContract]
        string UnsubscribeInstitution(int institution_id, long client_id_by_session);

        [OperationContract]
        string ManagerLogin(string unsername, string password);

        [OperationContract]
        string NearestInstitutions(double latitude, double longitude, double distance);

        /// <summary>
        /// Change institution details
        /// </summary>
        /// <param name="model_data">data to update</param>
        /// <returns>API response</returns>
        [OperationContract]
        string EditInstitutionDetails(string model_data, int id);

        [OperationContract]
        string GetInstitution(int id);

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
