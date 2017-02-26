﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChatClient.Services.ParticipantsService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Participant", Namespace="http://schemas.datacontract.org/2004/07/ChatServer.Common.Models")]
    [System.SerializableAttribute()]
    public partial class Participant : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
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
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ParticipantsService.IParticipantsManager")]
    public interface IParticipantsManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IParticipantsManager/GetAllOnline", ReplyAction="http://tempuri.org/IParticipantsManager/GetAllOnlineResponse")]
        ChatClient.Services.ParticipantsService.Participant[] GetAllOnline(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IParticipantsManager/GetAllOnline", ReplyAction="http://tempuri.org/IParticipantsManager/GetAllOnlineResponse")]
        System.Threading.Tasks.Task<ChatClient.Services.ParticipantsService.Participant[]> GetAllOnlineAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IParticipantsManager/Login", ReplyAction="http://tempuri.org/IParticipantsManager/LoginResponse")]
        ChatClient.Services.ParticipantsService.Participant Login(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IParticipantsManager/Login", ReplyAction="http://tempuri.org/IParticipantsManager/LoginResponse")]
        System.Threading.Tasks.Task<ChatClient.Services.ParticipantsService.Participant> LoginAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IParticipantsManager/Logout", ReplyAction="http://tempuri.org/IParticipantsManager/LogoutResponse")]
        void Logout(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IParticipantsManager/Logout", ReplyAction="http://tempuri.org/IParticipantsManager/LogoutResponse")]
        System.Threading.Tasks.Task LogoutAsync(string userName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IParticipantsManagerChannel : ChatClient.Services.ParticipantsService.IParticipantsManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ParticipantsManagerClient : System.ServiceModel.ClientBase<ChatClient.Services.ParticipantsService.IParticipantsManager>, ChatClient.Services.ParticipantsService.IParticipantsManager {
        
        public ParticipantsManagerClient() {
        }
        
        public ParticipantsManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ParticipantsManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ParticipantsManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ParticipantsManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ChatClient.Services.ParticipantsService.Participant[] GetAllOnline(string userName) {
            return base.Channel.GetAllOnline(userName);
        }
        
        public System.Threading.Tasks.Task<ChatClient.Services.ParticipantsService.Participant[]> GetAllOnlineAsync(string userName) {
            return base.Channel.GetAllOnlineAsync(userName);
        }
        
        public ChatClient.Services.ParticipantsService.Participant Login(string userName) {
            return base.Channel.Login(userName);
        }
        
        public System.Threading.Tasks.Task<ChatClient.Services.ParticipantsService.Participant> LoginAsync(string userName) {
            return base.Channel.LoginAsync(userName);
        }
        
        public void Logout(string userName) {
            base.Channel.Logout(userName);
        }
        
        public System.Threading.Tasks.Task LogoutAsync(string userName) {
            return base.Channel.LogoutAsync(userName);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ParticipantsService.IParticipantsManagerREST")]
    public interface IParticipantsManagerREST {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IParticipantsManagerREST/GetAllOnlineREST", ReplyAction="http://tempuri.org/IParticipantsManagerREST/GetAllOnlineRESTResponse")]
        ChatClient.Services.ParticipantsService.Participant[] GetAllOnlineREST(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IParticipantsManagerREST/GetAllOnlineREST", ReplyAction="http://tempuri.org/IParticipantsManagerREST/GetAllOnlineRESTResponse")]
        System.Threading.Tasks.Task<ChatClient.Services.ParticipantsService.Participant[]> GetAllOnlineRESTAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IParticipantsManagerREST/LoginREST", ReplyAction="http://tempuri.org/IParticipantsManagerREST/LoginRESTResponse")]
        ChatClient.Services.ParticipantsService.Participant LoginREST(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IParticipantsManagerREST/LoginREST", ReplyAction="http://tempuri.org/IParticipantsManagerREST/LoginRESTResponse")]
        System.Threading.Tasks.Task<ChatClient.Services.ParticipantsService.Participant> LoginRESTAsync(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IParticipantsManagerREST/LogoutREST", ReplyAction="http://tempuri.org/IParticipantsManagerREST/LogoutRESTResponse")]
        void LogoutREST(string userName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IParticipantsManagerREST/LogoutREST", ReplyAction="http://tempuri.org/IParticipantsManagerREST/LogoutRESTResponse")]
        System.Threading.Tasks.Task LogoutRESTAsync(string userName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IParticipantsManagerRESTChannel : ChatClient.Services.ParticipantsService.IParticipantsManagerREST, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ParticipantsManagerRESTClient : System.ServiceModel.ClientBase<ChatClient.Services.ParticipantsService.IParticipantsManagerREST>, ChatClient.Services.ParticipantsService.IParticipantsManagerREST {
        
        public ParticipantsManagerRESTClient() {
        }
        
        public ParticipantsManagerRESTClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ParticipantsManagerRESTClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ParticipantsManagerRESTClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ParticipantsManagerRESTClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ChatClient.Services.ParticipantsService.Participant[] GetAllOnlineREST(string userName) {
            return base.Channel.GetAllOnlineREST(userName);
        }
        
        public System.Threading.Tasks.Task<ChatClient.Services.ParticipantsService.Participant[]> GetAllOnlineRESTAsync(string userName) {
            return base.Channel.GetAllOnlineRESTAsync(userName);
        }
        
        public ChatClient.Services.ParticipantsService.Participant LoginREST(string userName) {
            return base.Channel.LoginREST(userName);
        }
        
        public System.Threading.Tasks.Task<ChatClient.Services.ParticipantsService.Participant> LoginRESTAsync(string userName) {
            return base.Channel.LoginRESTAsync(userName);
        }
        
        public void LogoutREST(string userName) {
            base.Channel.LogoutREST(userName);
        }
        
        public System.Threading.Tasks.Task LogoutRESTAsync(string userName) {
            return base.Channel.LogoutRESTAsync(userName);
        }
    }
}
