﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleAppObligatorioP3.RetornarProveedores {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RetornarProveedores.IRetornarProveedores")]
    public interface IRetornarProveedores {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRetornarProveedores/RetornarProveedores", ReplyAction="http://tempuri.org/IRetornarProveedores/RetornarProveedoresResponse")]
        ServiciosObligatorioWCF.DTOProveedor[] RetornarProveedores();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRetornarProveedores/RetornarProveedores", ReplyAction="http://tempuri.org/IRetornarProveedores/RetornarProveedoresResponse")]
        System.Threading.Tasks.Task<ServiciosObligatorioWCF.DTOProveedor[]> RetornarProveedoresAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRetornarProveedoresChannel : ConsoleAppObligatorioP3.RetornarProveedores.IRetornarProveedores, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RetornarProveedoresClient : System.ServiceModel.ClientBase<ConsoleAppObligatorioP3.RetornarProveedores.IRetornarProveedores>, ConsoleAppObligatorioP3.RetornarProveedores.IRetornarProveedores {
        
        public RetornarProveedoresClient() {
        }
        
        public RetornarProveedoresClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RetornarProveedoresClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RetornarProveedoresClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RetornarProveedoresClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ServiciosObligatorioWCF.DTOProveedor[] RetornarProveedores() {
            return base.Channel.RetornarProveedores();
        }
        
        public System.Threading.Tasks.Task<ServiciosObligatorioWCF.DTOProveedor[]> RetornarProveedoresAsync() {
            return base.Channel.RetornarProveedoresAsync();
        }
    }
}
