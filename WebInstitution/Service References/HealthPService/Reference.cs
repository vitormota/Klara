﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebInstitution.HealthPService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="HealthPService.IHPService")]
    public interface IHPService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetData", ReplyAction="http://tempuri.org/IHPService/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetData", ReplyAction="http://tempuri.org/IHPService/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IHPService/GetDataUsingDataContractResponse")]
        HpREST_Bridge.CompositeType GetDataUsingDataContract(HpREST_Bridge.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IHPService/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<HpREST_Bridge.CompositeType> GetDataUsingDataContractAsync(HpREST_Bridge.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetClientDetails", ReplyAction="http://tempuri.org/IHPService/GetClientDetailsResponse")]
        string GetClientDetails(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetClientDetails", ReplyAction="http://tempuri.org/IHPService/GetClientDetailsResponse")]
        System.Threading.Tasks.Task<string> GetClientDetailsAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetClientPurchases", ReplyAction="http://tempuri.org/IHPService/GetClientPurchasesResponse")]
        string GetClientPurchases(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetClientPurchases", ReplyAction="http://tempuri.org/IHPService/GetClientPurchasesResponse")]
        System.Threading.Tasks.Task<string> GetClientPurchasesAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/UpdateClientDetails", ReplyAction="http://tempuri.org/IHPService/UpdateClientDetailsResponse")]
        string UpdateClientDetails(int id, string client_jobj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/UpdateClientDetails", ReplyAction="http://tempuri.org/IHPService/UpdateClientDetailsResponse")]
        System.Threading.Tasks.Task<string> UpdateClientDetailsAsync(int id, string client_jobj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/RegisterUser", ReplyAction="http://tempuri.org/IHPService/RegisterUserResponse")]
        string RegisterUser(string access_token, int provider);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/RegisterUser", ReplyAction="http://tempuri.org/IHPService/RegisterUserResponse")]
        System.Threading.Tasks.Task<string> RegisterUserAsync(string access_token, int provider);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/UserLogin", ReplyAction="http://tempuri.org/IHPService/UserLoginResponse")]
        string UserLogin(string access_token, int provider);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/UserLogin", ReplyAction="http://tempuri.org/IHPService/UserLoginResponse")]
        System.Threading.Tasks.Task<string> UserLoginAsync(string access_token, int provider);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SearchInstitution", ReplyAction="http://tempuri.org/IHPService/SearchInstitutionResponse")]
        string SearchInstitution(string textSearch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SearchInstitution", ReplyAction="http://tempuri.org/IHPService/SearchInstitutionResponse")]
        System.Threading.Tasks.Task<string> SearchInstitutionAsync(string textSearch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/FetchInstitutions", ReplyAction="http://tempuri.org/IHPService/FetchInstitutionsResponse")]
        string FetchInstitutions(string manager_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/FetchInstitutions", ReplyAction="http://tempuri.org/IHPService/FetchInstitutionsResponse")]
        System.Threading.Tasks.Task<string> FetchInstitutionsAsync(string manager_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SearchAd", ReplyAction="http://tempuri.org/IHPService/SearchAdResponse")]
        string SearchAd(string textSearch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SearchAd", ReplyAction="http://tempuri.org/IHPService/SearchAdResponse")]
        System.Threading.Tasks.Task<string> SearchAdAsync(string textSearch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/InstitutionsSubscribe", ReplyAction="http://tempuri.org/IHPService/InstitutionsSubscribeResponse")]
        string InstitutionsSubscribe(int client_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/InstitutionsSubscribe", ReplyAction="http://tempuri.org/IHPService/InstitutionsSubscribeResponse")]
        System.Threading.Tasks.Task<string> InstitutionsSubscribeAsync(int client_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SubscribeInstitution", ReplyAction="http://tempuri.org/IHPService/SubscribeInstitutionResponse")]
        string SubscribeInstitution(int institution_id, int client_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SubscribeInstitution", ReplyAction="http://tempuri.org/IHPService/SubscribeInstitutionResponse")]
        System.Threading.Tasks.Task<string> SubscribeInstitutionAsync(int institution_id, int client_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/UnsubscribeInstitution", ReplyAction="http://tempuri.org/IHPService/UnsubscribeInstitutionResponse")]
        string UnsubscribeInstitution(int institution_id, int client_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/UnsubscribeInstitution", ReplyAction="http://tempuri.org/IHPService/UnsubscribeInstitutionResponse")]
        System.Threading.Tasks.Task<string> UnsubscribeInstitutionAsync(int institution_id, int client_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/ManagerLogin", ReplyAction="http://tempuri.org/IHPService/ManagerLoginResponse")]
        string ManagerLogin(string unsername, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/ManagerLogin", ReplyAction="http://tempuri.org/IHPService/ManagerLoginResponse")]
        System.Threading.Tasks.Task<string> ManagerLoginAsync(string unsername, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/NearestInstitutions", ReplyAction="http://tempuri.org/IHPService/NearestInstitutionsResponse")]
        string NearestInstitutions(double latitude, double longitude, double distance);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/NearestInstitutions", ReplyAction="http://tempuri.org/IHPService/NearestInstitutionsResponse")]
        System.Threading.Tasks.Task<string> NearestInstitutionsAsync(double latitude, double longitude, double distance);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SeeCuponsActive", ReplyAction="http://tempuri.org/IHPService/SeeCuponsActiveResponse")]
        string SeeCuponsActive(int client_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SeeCuponsActive", ReplyAction="http://tempuri.org/IHPService/SeeCuponsActiveResponse")]
        System.Threading.Tasks.Task<string> SeeCuponsActiveAsync(int client_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/EditInstitutionDetails", ReplyAction="http://tempuri.org/IHPService/EditInstitutionDetailsResponse")]
        string EditInstitutionDetails(string model_data, int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/EditInstitutionDetails", ReplyAction="http://tempuri.org/IHPService/EditInstitutionDetailsResponse")]
        System.Threading.Tasks.Task<string> EditInstitutionDetailsAsync(string model_data, int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/CreateAd", ReplyAction="http://tempuri.org/IHPService/CreateAdResponse")]
        string CreateAd(string model_data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/CreateAd", ReplyAction="http://tempuri.org/IHPService/CreateAdResponse")]
        System.Threading.Tasks.Task<string> CreateAdAsync(string model_data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetInstitution", ReplyAction="http://tempuri.org/IHPService/GetInstitutionResponse")]
        string GetInstitution(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetInstitution", ReplyAction="http://tempuri.org/IHPService/GetInstitutionResponse")]
        System.Threading.Tasks.Task<string> GetInstitutionAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/InsertAdPhoto", ReplyAction="http://tempuri.org/IHPService/InsertAdPhotoResponse")]
        string InsertAdPhoto(int ad_id, string photo_guid, string data_stream);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/InsertAdPhoto", ReplyAction="http://tempuri.org/IHPService/InsertAdPhotoResponse")]
        System.Threading.Tasks.Task<string> InsertAdPhotoAsync(int ad_id, string photo_guid, string data_stream);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetActiveAds", ReplyAction="http://tempuri.org/IHPService/GetActiveAdsResponse")]
        string GetActiveAds(int institution_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetActiveAds", ReplyAction="http://tempuri.org/IHPService/GetActiveAdsResponse")]
        System.Threading.Tasks.Task<string> GetActiveAdsAsync(int institution_id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IHPServiceChannel : WebInstitution.HealthPService.IHPService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HPServiceClient : System.ServiceModel.ClientBase<WebInstitution.HealthPService.IHPService>, WebInstitution.HealthPService.IHPService {
        
        public HPServiceClient() {
        }
        
        public HPServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public HPServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HPServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HPServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public HpREST_Bridge.CompositeType GetDataUsingDataContract(HpREST_Bridge.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<HpREST_Bridge.CompositeType> GetDataUsingDataContractAsync(HpREST_Bridge.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public string GetClientDetails(int id) {
            return base.Channel.GetClientDetails(id);
        }
        
        public System.Threading.Tasks.Task<string> GetClientDetailsAsync(int id) {
            return base.Channel.GetClientDetailsAsync(id);
        }
        
        public string GetClientPurchases(int id) {
            return base.Channel.GetClientPurchases(id);
        }
        
        public System.Threading.Tasks.Task<string> GetClientPurchasesAsync(int id) {
            return base.Channel.GetClientPurchasesAsync(id);
        }
        
        public string UpdateClientDetails(int id, string client_jobj) {
            return base.Channel.UpdateClientDetails(id, client_jobj);
        }
        
        public System.Threading.Tasks.Task<string> UpdateClientDetailsAsync(int id, string client_jobj) {
            return base.Channel.UpdateClientDetailsAsync(id, client_jobj);
        }
        
        public string RegisterUser(string access_token, int provider) {
            return base.Channel.RegisterUser(access_token, provider);
        }
        
        public System.Threading.Tasks.Task<string> RegisterUserAsync(string access_token, int provider) {
            return base.Channel.RegisterUserAsync(access_token, provider);
        }
        
        public string UserLogin(string access_token, int provider) {
            return base.Channel.UserLogin(access_token, provider);
        }
        
        public System.Threading.Tasks.Task<string> UserLoginAsync(string access_token, int provider) {
            return base.Channel.UserLoginAsync(access_token, provider);
        }
        
        public string SearchInstitution(string textSearch) {
            return base.Channel.SearchInstitution(textSearch);
        }
        
        public System.Threading.Tasks.Task<string> SearchInstitutionAsync(string textSearch) {
            return base.Channel.SearchInstitutionAsync(textSearch);
        }
        
        public string FetchInstitutions(string manager_id) {
            return base.Channel.FetchInstitutions(manager_id);
        }
        
        public System.Threading.Tasks.Task<string> FetchInstitutionsAsync(string manager_id) {
            return base.Channel.FetchInstitutionsAsync(manager_id);
        }
        
        public string SearchAd(string textSearch) {
            return base.Channel.SearchAd(textSearch);
        }
        
        public System.Threading.Tasks.Task<string> SearchAdAsync(string textSearch) {
            return base.Channel.SearchAdAsync(textSearch);
        }
        
        public string InstitutionsSubscribe(int client_id) {
            return base.Channel.InstitutionsSubscribe(client_id);
        }
        
        public System.Threading.Tasks.Task<string> InstitutionsSubscribeAsync(int client_id) {
            return base.Channel.InstitutionsSubscribeAsync(client_id);
        }
        
        public string SubscribeInstitution(int institution_id, int client_id) {
            return base.Channel.SubscribeInstitution(institution_id, client_id);
        }
        
        public System.Threading.Tasks.Task<string> SubscribeInstitutionAsync(int institution_id, int client_id) {
            return base.Channel.SubscribeInstitutionAsync(institution_id, client_id);
        }
        
        public string UnsubscribeInstitution(int institution_id, int client_id) {
            return base.Channel.UnsubscribeInstitution(institution_id, client_id);
        }
        
        public System.Threading.Tasks.Task<string> UnsubscribeInstitutionAsync(int institution_id, int client_id) {
            return base.Channel.UnsubscribeInstitutionAsync(institution_id, client_id);
        }
        
        public string ManagerLogin(string unsername, string password) {
            return base.Channel.ManagerLogin(unsername, password);
        }
        
        public System.Threading.Tasks.Task<string> ManagerLoginAsync(string unsername, string password) {
            return base.Channel.ManagerLoginAsync(unsername, password);
        }
        
        public string NearestInstitutions(double latitude, double longitude, double distance) {
            return base.Channel.NearestInstitutions(latitude, longitude, distance);
        }
        
        public System.Threading.Tasks.Task<string> NearestInstitutionsAsync(double latitude, double longitude, double distance) {
            return base.Channel.NearestInstitutionsAsync(latitude, longitude, distance);
        }
        
        public string SeeCuponsActive(int client_id) {
            return base.Channel.SeeCuponsActive(client_id);
        }
        
        public System.Threading.Tasks.Task<string> SeeCuponsActiveAsync(int client_id) {
            return base.Channel.SeeCuponsActiveAsync(client_id);
        }
        
        public string EditInstitutionDetails(string model_data, int id) {
            return base.Channel.EditInstitutionDetails(model_data, id);
        }
        
        public System.Threading.Tasks.Task<string> EditInstitutionDetailsAsync(string model_data, int id) {
            return base.Channel.EditInstitutionDetailsAsync(model_data, id);
        }
        
        public string CreateAd(string model_data) {
            return base.Channel.CreateAd(model_data);
        }
        
        public System.Threading.Tasks.Task<string> CreateAdAsync(string model_data) {
            return base.Channel.CreateAdAsync(model_data);
        }
        
        public string GetInstitution(int id) {
            return base.Channel.GetInstitution(id);
        }
        
        public System.Threading.Tasks.Task<string> GetInstitutionAsync(int id) {
            return base.Channel.GetInstitutionAsync(id);
        }
        
        public string InsertAdPhoto(int ad_id, string photo_guid, string data_stream) {
            return base.Channel.InsertAdPhoto(ad_id, photo_guid, data_stream);
        }
        
        public System.Threading.Tasks.Task<string> InsertAdPhotoAsync(int ad_id, string photo_guid, string data_stream) {
            return base.Channel.InsertAdPhotoAsync(ad_id, photo_guid, data_stream);
        }
        
        public string GetActiveAds(int institution_id) {
            return base.Channel.GetActiveAds(institution_id);
        }
        
        public System.Threading.Tasks.Task<string> GetActiveAdsAsync(int institution_id) {
            return base.Channel.GetActiveAdsAsync(institution_id);
        }
    }
}
