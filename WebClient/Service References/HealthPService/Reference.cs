﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebClient_.HealthPService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/HpREST_Bridge")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="HealthPService.IHPService")]
    public interface IHPService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetData", ReplyAction="http://tempuri.org/IHPService/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetData", ReplyAction="http://tempuri.org/IHPService/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IHPService/GetDataUsingDataContractResponse")]
        WebClient_.HealthPService.CompositeType GetDataUsingDataContract(WebClient_.HealthPService.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IHPService/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<WebClient_.HealthPService.CompositeType> GetDataUsingDataContractAsync(WebClient_.HealthPService.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetUserDetails", ReplyAction="http://tempuri.org/IHPService/GetUserDetailsResponse")]
        string GetUserDetails(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetUserDetails", ReplyAction="http://tempuri.org/IHPService/GetUserDetailsResponse")]
        System.Threading.Tasks.Task<string> GetUserDetailsAsync(int id);
        
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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/BuyCupon", ReplyAction="http://tempuri.org/IHPService/BuyCuponResponse")]
        string BuyCupon(string cupon_str);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/BuyCupon", ReplyAction="http://tempuri.org/IHPService/BuyCuponResponse")]
        System.Threading.Tasks.Task<string> BuyCuponAsync(string cupon_str);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/BuyMultipleCupons", ReplyAction="http://tempuri.org/IHPService/BuyMultipleCuponsResponse")]
        string BuyMultipleCupons(string cupon_list);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/BuyMultipleCupons", ReplyAction="http://tempuri.org/IHPService/BuyMultipleCuponsResponse")]
        System.Threading.Tasks.Task<string> BuyMultipleCuponsAsync(string cupon_list);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetAdSubscriptions", ReplyAction="http://tempuri.org/IHPService/GetAdSubscriptionsResponse")]
        string GetAdSubscriptions(int client_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetAdSubscriptions", ReplyAction="http://tempuri.org/IHPService/GetAdSubscriptionsResponse")]
        System.Threading.Tasks.Task<string> GetAdSubscriptionsAsync(int client_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetInstitutionSubscriptions", ReplyAction="http://tempuri.org/IHPService/GetInstitutionSubscriptionsResponse")]
        string GetInstitutionSubscriptions(int client_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetInstitutionSubscriptions", ReplyAction="http://tempuri.org/IHPService/GetInstitutionSubscriptionsResponse")]
        System.Threading.Tasks.Task<string> GetInstitutionSubscriptionsAsync(int client_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetAdById", ReplyAction="http://tempuri.org/IHPService/GetAdByIdResponse")]
        string GetAdById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetAdById", ReplyAction="http://tempuri.org/IHPService/GetAdByIdResponse")]
        System.Threading.Tasks.Task<string> GetAdByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/RegisterUser", ReplyAction="http://tempuri.org/IHPService/RegisterUserResponse")]
        string RegisterUser(string access_token, int provider);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/RegisterUser", ReplyAction="http://tempuri.org/IHPService/RegisterUserResponse")]
        System.Threading.Tasks.Task<string> RegisterUserAsync(string access_token, int provider);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/UserLogin", ReplyAction="http://tempuri.org/IHPService/UserLoginResponse")]
        string UserLogin(string access_token, int provider);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/UserLogin", ReplyAction="http://tempuri.org/IHPService/UserLoginResponse")]
        System.Threading.Tasks.Task<string> UserLoginAsync(string access_token, int provider);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SearchInstitution", ReplyAction="http://tempuri.org/IHPService/SearchInstitutionResponse")]
        string SearchInstitution(string textSearch, int last_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SearchInstitution", ReplyAction="http://tempuri.org/IHPService/SearchInstitutionResponse")]
        System.Threading.Tasks.Task<string> SearchInstitutionAsync(string textSearch, int last_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/FetchInstitutions", ReplyAction="http://tempuri.org/IHPService/FetchInstitutionsResponse")]
        string FetchInstitutions(string manager_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/FetchInstitutions", ReplyAction="http://tempuri.org/IHPService/FetchInstitutionsResponse")]
        System.Threading.Tasks.Task<string> FetchInstitutionsAsync(string manager_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SearchAd", ReplyAction="http://tempuri.org/IHPService/SearchAdResponse")]
        string SearchAd(string textSearch, int last_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SearchAd", ReplyAction="http://tempuri.org/IHPService/SearchAdResponse")]
        System.Threading.Tasks.Task<string> SearchAdAsync(string textSearch, int last_id);
        
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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SubscribeAd", ReplyAction="http://tempuri.org/IHPService/SubscribeAdResponse")]
        string SubscribeAd(int client_id, int ad_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SubscribeAd", ReplyAction="http://tempuri.org/IHPService/SubscribeAdResponse")]
        System.Threading.Tasks.Task<string> SubscribeAdAsync(int client_id, int ad_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/UnsubscribeAd", ReplyAction="http://tempuri.org/IHPService/UnsubscribeAdResponse")]
        string UnsubscribeAd(int client_id, int ad_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/UnsubscribeAd", ReplyAction="http://tempuri.org/IHPService/UnsubscribeAdResponse")]
        System.Threading.Tasks.Task<string> UnsubscribeAdAsync(int client_id, int ad_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/AdsSubscribe", ReplyAction="http://tempuri.org/IHPService/AdsSubscribeResponse")]
        string AdsSubscribe(int client_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/AdsSubscribe", ReplyAction="http://tempuri.org/IHPService/AdsSubscribeResponse")]
        System.Threading.Tasks.Task<string> AdsSubscribeAsync(int client_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/IsSubscribeUser", ReplyAction="http://tempuri.org/IHPService/IsSubscribeUserResponse")]
        string IsSubscribeUser(int client_id, int subscribable_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/IsSubscribeUser", ReplyAction="http://tempuri.org/IHPService/IsSubscribeUserResponse")]
        System.Threading.Tasks.Task<string> IsSubscribeUserAsync(int client_id, int subscribable_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/EditInstitutionDetails", ReplyAction="http://tempuri.org/IHPService/EditInstitutionDetailsResponse")]
        string EditInstitutionDetails(string model_data, int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/EditInstitutionDetails", ReplyAction="http://tempuri.org/IHPService/EditInstitutionDetailsResponse")]
        System.Threading.Tasks.Task<string> EditInstitutionDetailsAsync(string model_data, int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/AdvertiseInstitution", ReplyAction="http://tempuri.org/IHPService/AdvertiseInstitutionResponse")]
        string AdvertiseInstitution(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/AdvertiseInstitution", ReplyAction="http://tempuri.org/IHPService/AdvertiseInstitutionResponse")]
        System.Threading.Tasks.Task<string> AdvertiseInstitutionAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/CreateAd", ReplyAction="http://tempuri.org/IHPService/CreateAdResponse")]
        string CreateAd(string model_data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/CreateAd", ReplyAction="http://tempuri.org/IHPService/CreateAdResponse")]
        System.Threading.Tasks.Task<string> CreateAdAsync(string model_data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetInstitution", ReplyAction="http://tempuri.org/IHPService/GetInstitutionResponse")]
        string GetInstitution(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetInstitution", ReplyAction="http://tempuri.org/IHPService/GetInstitutionResponse")]
        System.Threading.Tasks.Task<string> GetInstitutionAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetActiveAds", ReplyAction="http://tempuri.org/IHPService/GetActiveAdsResponse")]
        string GetActiveAds(int institution_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetActiveAds", ReplyAction="http://tempuri.org/IHPService/GetActiveAdsResponse")]
        System.Threading.Tasks.Task<string> GetActiveAdsAsync(int institution_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetInactiveBestAds", ReplyAction="http://tempuri.org/IHPService/GetInactiveBestAdsResponse")]
        string GetInactiveBestAds(int institution_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetInactiveBestAds", ReplyAction="http://tempuri.org/IHPService/GetInactiveBestAdsResponse")]
        System.Threading.Tasks.Task<string> GetInactiveBestAdsAsync(int institution_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/DeleteAd", ReplyAction="http://tempuri.org/IHPService/DeleteAdResponse")]
        string DeleteAd(int ad_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/DeleteAd", ReplyAction="http://tempuri.org/IHPService/DeleteAdResponse")]
        System.Threading.Tasks.Task<string> DeleteAdAsync(int ad_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetBankReference", ReplyAction="http://tempuri.org/IHPService/GetBankReferenceResponse")]
        string GetBankReference();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetBankReference", ReplyAction="http://tempuri.org/IHPService/GetBankReferenceResponse")]
        System.Threading.Tasks.Task<string> GetBankReferenceAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetAdsByRule", ReplyAction="http://tempuri.org/IHPService/GetAdsByRuleResponse")]
        string GetAdsByRule(int offset, int limit, string order_by);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetAdsByRule", ReplyAction="http://tempuri.org/IHPService/GetAdsByRuleResponse")]
        System.Threading.Tasks.Task<string> GetAdsByRuleAsync(int offset, int limit, string order_by);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IHPServiceChannel : WebClient_.HealthPService.IHPService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HPServiceClient : System.ServiceModel.ClientBase<WebClient_.HealthPService.IHPService>, WebClient_.HealthPService.IHPService {
        
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
        
        public WebClient_.HealthPService.CompositeType GetDataUsingDataContract(WebClient_.HealthPService.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<WebClient_.HealthPService.CompositeType> GetDataUsingDataContractAsync(WebClient_.HealthPService.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
        
        public string GetUserDetails(int id) {
            return base.Channel.GetUserDetails(id);
        }
        
        public System.Threading.Tasks.Task<string> GetUserDetailsAsync(int id) {
            return base.Channel.GetUserDetailsAsync(id);
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
        
        public string BuyCupon(string cupon_str) {
            return base.Channel.BuyCupon(cupon_str);
        }
        
        public System.Threading.Tasks.Task<string> BuyCuponAsync(string cupon_str) {
            return base.Channel.BuyCuponAsync(cupon_str);
        }
        
        public string BuyMultipleCupons(string cupon_list) {
            return base.Channel.BuyMultipleCupons(cupon_list);
        }
        
        public System.Threading.Tasks.Task<string> BuyMultipleCuponsAsync(string cupon_list) {
            return base.Channel.BuyMultipleCuponsAsync(cupon_list);
        }
        
        public string GetAdSubscriptions(int client_id) {
            return base.Channel.GetAdSubscriptions(client_id);
        }
        
        public System.Threading.Tasks.Task<string> GetAdSubscriptionsAsync(int client_id) {
            return base.Channel.GetAdSubscriptionsAsync(client_id);
        }
        
        public string GetInstitutionSubscriptions(int client_id) {
            return base.Channel.GetInstitutionSubscriptions(client_id);
        }
        
        public System.Threading.Tasks.Task<string> GetInstitutionSubscriptionsAsync(int client_id) {
            return base.Channel.GetInstitutionSubscriptionsAsync(client_id);
        }
        
        public string GetAdById(int id) {
            return base.Channel.GetAdById(id);
        }
        
        public System.Threading.Tasks.Task<string> GetAdByIdAsync(int id) {
            return base.Channel.GetAdByIdAsync(id);
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
        
        public string SearchInstitution(string textSearch, int last_id) {
            return base.Channel.SearchInstitution(textSearch, last_id);
        }
        
        public System.Threading.Tasks.Task<string> SearchInstitutionAsync(string textSearch, int last_id) {
            return base.Channel.SearchInstitutionAsync(textSearch, last_id);
        }
        
        public string FetchInstitutions(string manager_id) {
            return base.Channel.FetchInstitutions(manager_id);
        }
        
        public System.Threading.Tasks.Task<string> FetchInstitutionsAsync(string manager_id) {
            return base.Channel.FetchInstitutionsAsync(manager_id);
        }
        
        public string SearchAd(string textSearch, int last_id) {
            return base.Channel.SearchAd(textSearch, last_id);
        }
        
        public System.Threading.Tasks.Task<string> SearchAdAsync(string textSearch, int last_id) {
            return base.Channel.SearchAdAsync(textSearch, last_id);
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
        
        public string SubscribeAd(int client_id, int ad_id) {
            return base.Channel.SubscribeAd(client_id, ad_id);
        }
        
        public System.Threading.Tasks.Task<string> SubscribeAdAsync(int client_id, int ad_id) {
            return base.Channel.SubscribeAdAsync(client_id, ad_id);
        }
        
        public string UnsubscribeAd(int client_id, int ad_id) {
            return base.Channel.UnsubscribeAd(client_id, ad_id);
        }
        
        public System.Threading.Tasks.Task<string> UnsubscribeAdAsync(int client_id, int ad_id) {
            return base.Channel.UnsubscribeAdAsync(client_id, ad_id);
        }
        
        public string AdsSubscribe(int client_id) {
            return base.Channel.AdsSubscribe(client_id);
        }
        
        public System.Threading.Tasks.Task<string> AdsSubscribeAsync(int client_id) {
            return base.Channel.AdsSubscribeAsync(client_id);
        }
        
        public string IsSubscribeUser(int client_id, int subscribable_id) {
            return base.Channel.IsSubscribeUser(client_id, subscribable_id);
        }
        
        public System.Threading.Tasks.Task<string> IsSubscribeUserAsync(int client_id, int subscribable_id) {
            return base.Channel.IsSubscribeUserAsync(client_id, subscribable_id);
        }
        
        public string EditInstitutionDetails(string model_data, int id) {
            return base.Channel.EditInstitutionDetails(model_data, id);
        }
        
        public System.Threading.Tasks.Task<string> EditInstitutionDetailsAsync(string model_data, int id) {
            return base.Channel.EditInstitutionDetailsAsync(model_data, id);
        }
        
        public string AdvertiseInstitution(int id) {
            return base.Channel.AdvertiseInstitution(id);
        }
        
        public System.Threading.Tasks.Task<string> AdvertiseInstitutionAsync(int id) {
            return base.Channel.AdvertiseInstitutionAsync(id);
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
        
        public string GetActiveAds(int institution_id) {
            return base.Channel.GetActiveAds(institution_id);
        }
        
        public System.Threading.Tasks.Task<string> GetActiveAdsAsync(int institution_id) {
            return base.Channel.GetActiveAdsAsync(institution_id);
        }
        
        public string GetInactiveBestAds(int institution_id) {
            return base.Channel.GetInactiveBestAds(institution_id);
        }
        
        public System.Threading.Tasks.Task<string> GetInactiveBestAdsAsync(int institution_id) {
            return base.Channel.GetInactiveBestAdsAsync(institution_id);
        }
        
        public string DeleteAd(int ad_id) {
            return base.Channel.DeleteAd(ad_id);
        }
        
        public System.Threading.Tasks.Task<string> DeleteAdAsync(int ad_id) {
            return base.Channel.DeleteAdAsync(ad_id);
        }
        
        public string GetBankReference() {
            return base.Channel.GetBankReference();
        }
        
        public System.Threading.Tasks.Task<string> GetBankReferenceAsync() {
            return base.Channel.GetBankReferenceAsync();
        }
        
        public string GetAdsByRule(int offset, int limit, string order_by) {
            return base.Channel.GetAdsByRule(offset, limit, order_by);
        }
        
        public System.Threading.Tasks.Task<string> GetAdsByRuleAsync(int offset, int limit, string order_by) {
            return base.Channel.GetAdsByRuleAsync(offset, limit, order_by);
        }
    }
}
