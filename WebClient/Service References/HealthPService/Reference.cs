﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetAccounts", ReplyAction="http://tempuri.org/IHPService/GetAccountsResponse")]
        string GetAccounts();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/GetAccounts", ReplyAction="http://tempuri.org/IHPService/GetAccountsResponse")]
        System.Threading.Tasks.Task<string> GetAccountsAsync();
        
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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SearchAd", ReplyAction="http://tempuri.org/IHPService/SearchAdResponse")]
        string SearchAd(string textSearch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SearchAd", ReplyAction="http://tempuri.org/IHPService/SearchAdResponse")]
        System.Threading.Tasks.Task<string> SearchAdAsync(string textSearch);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/InstitutionsSubscribe", ReplyAction="http://tempuri.org/IHPService/InstitutionsSubscribeResponse")]
        string InstitutionsSubscribe(int client_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/InstitutionsSubscribe", ReplyAction="http://tempuri.org/IHPService/InstitutionsSubscribeResponse")]
        System.Threading.Tasks.Task<string> InstitutionsSubscribeAsync(int client_id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SubscribeInstitution", ReplyAction="http://tempuri.org/IHPService/SubscribeInstitutionResponse")]
        string SubscribeInstitution(int institution_id, long client_id_by_session);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/SubscribeInstitution", ReplyAction="http://tempuri.org/IHPService/SubscribeInstitutionResponse")]
        System.Threading.Tasks.Task<string> SubscribeInstitutionAsync(int institution_id, long client_id_by_session);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/UnsubscribeInstitution", ReplyAction="http://tempuri.org/IHPService/UnsubscribeInstitutionResponse")]
        string UnsubscribeInstitution(int institution_id, long client_id_by_session);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/UnsubscribeInstitution", ReplyAction="http://tempuri.org/IHPService/UnsubscribeInstitutionResponse")]
        System.Threading.Tasks.Task<string> UnsubscribeInstitutionAsync(int institution_id, long client_id_by_session);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/NearestInstitutions", ReplyAction="http://tempuri.org/IHPService/NearestInstitutionsResponse")]
        string NearestInstitutions(double latitude, double longitude, double distance);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHPService/NearestInstitutions", ReplyAction="http://tempuri.org/IHPService/NearestInstitutionsResponse")]
        System.Threading.Tasks.Task<string> NearestInstitutionsAsync(double latitude, double longitude, double distance);
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
        
        public string GetAccounts() {
            return base.Channel.GetAccounts();
        }
        
        public System.Threading.Tasks.Task<string> GetAccountsAsync() {
            return base.Channel.GetAccountsAsync();
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
        
        public string SubscribeInstitution(int institution_id, long client_id_by_session) {
            return base.Channel.SubscribeInstitution(institution_id, client_id_by_session);
        }
        
        public System.Threading.Tasks.Task<string> SubscribeInstitutionAsync(int institution_id, long client_id_by_session) {
            return base.Channel.SubscribeInstitutionAsync(institution_id, client_id_by_session);
        }
        
        public string UnsubscribeInstitution(int institution_id, long client_id_by_session) {
            return base.Channel.UnsubscribeInstitution(institution_id, client_id_by_session);
        }
        
        public System.Threading.Tasks.Task<string> UnsubscribeInstitutionAsync(int institution_id, long client_id_by_session) {
            return base.Channel.UnsubscribeInstitutionAsync(institution_id, client_id_by_session);
        }
        
        public string NearestInstitutions(double latitude, double longitude, double distance) {
            return base.Channel.NearestInstitutions(latitude, longitude, distance);
        }
        
        public System.Threading.Tasks.Task<string> NearestInstitutionsAsync(double latitude, double longitude, double distance) {
            return base.Channel.NearestInstitutionsAsync(latitude, longitude, distance);
        }
    }
}
