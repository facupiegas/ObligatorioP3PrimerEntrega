﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InterfazWeb.WCF_Proveedor {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WCF_Proveedor.IOperacionesProveedores")]
    public interface IOperacionesProveedores {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOperacionesProveedores/RetornarProveedores", ReplyAction="http://tempuri.org/IOperacionesProveedores/RetornarProveedoresResponse")]
        ServiciosObligatorioWCF.DTOProveedor[] RetornarProveedores();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOperacionesProveedores/RetornarProveedores", ReplyAction="http://tempuri.org/IOperacionesProveedores/RetornarProveedoresResponse")]
        System.Threading.Tasks.Task<ServiciosObligatorioWCF.DTOProveedor[]> RetornarProveedoresAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOperacionesProveedores/RetornarProveedorPorRut", ReplyAction="http://tempuri.org/IOperacionesProveedores/RetornarProveedorPorRutResponse")]
        ServiciosObligatorioWCF.DTOProveedor RetornarProveedorPorRut(string unRut);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOperacionesProveedores/RetornarProveedorPorRut", ReplyAction="http://tempuri.org/IOperacionesProveedores/RetornarProveedorPorRutResponse")]
        System.Threading.Tasks.Task<ServiciosObligatorioWCF.DTOProveedor> RetornarProveedorPorRutAsync(string unRut);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOperacionesProveedores/ModificarArancelesProveedor", ReplyAction="http://tempuri.org/IOperacionesProveedores/ModificarArancelesProveedorResponse")]
        bool ModificarArancelesProveedor(double unArancel, double unPorcentajeVip);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOperacionesProveedores/ModificarArancelesProveedor", ReplyAction="http://tempuri.org/IOperacionesProveedores/ModificarArancelesProveedorResponse")]
        System.Threading.Tasks.Task<bool> ModificarArancelesProveedorAsync(double unArancel, double unPorcentajeVip);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOperacionesProveedores/DesactivarProveedor", ReplyAction="http://tempuri.org/IOperacionesProveedores/DesactivarProveedorResponse")]
        bool DesactivarProveedor(string unRut);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOperacionesProveedores/DesactivarProveedor", ReplyAction="http://tempuri.org/IOperacionesProveedores/DesactivarProveedorResponse")]
        System.Threading.Tasks.Task<bool> DesactivarProveedorAsync(string unRut);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOperacionesProveedores/RetornarProveedoresActivos", ReplyAction="http://tempuri.org/IOperacionesProveedores/RetornarProveedoresActivosResponse")]
        ServiciosObligatorioWCF.DTOProveedor[] RetornarProveedoresActivos();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOperacionesProveedores/RetornarProveedoresActivos", ReplyAction="http://tempuri.org/IOperacionesProveedores/RetornarProveedoresActivosResponse")]
        System.Threading.Tasks.Task<ServiciosObligatorioWCF.DTOProveedor[]> RetornarProveedoresActivosAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOperacionesProveedoresChannel : InterfazWeb.WCF_Proveedor.IOperacionesProveedores, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OperacionesProveedoresClient : System.ServiceModel.ClientBase<InterfazWeb.WCF_Proveedor.IOperacionesProveedores>, InterfazWeb.WCF_Proveedor.IOperacionesProveedores {
        
        public OperacionesProveedoresClient() {
        }
        
        public OperacionesProveedoresClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OperacionesProveedoresClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OperacionesProveedoresClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OperacionesProveedoresClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ServiciosObligatorioWCF.DTOProveedor[] RetornarProveedores() {
            return base.Channel.RetornarProveedores();
        }
        
        public System.Threading.Tasks.Task<ServiciosObligatorioWCF.DTOProveedor[]> RetornarProveedoresAsync() {
            return base.Channel.RetornarProveedoresAsync();
        }
        
        public ServiciosObligatorioWCF.DTOProveedor RetornarProveedorPorRut(string unRut) {
            return base.Channel.RetornarProveedorPorRut(unRut);
        }
        
        public System.Threading.Tasks.Task<ServiciosObligatorioWCF.DTOProveedor> RetornarProveedorPorRutAsync(string unRut) {
            return base.Channel.RetornarProveedorPorRutAsync(unRut);
        }
        
        public bool ModificarArancelesProveedor(double unArancel, double unPorcentajeVip) {
            return base.Channel.ModificarArancelesProveedor(unArancel, unPorcentajeVip);
        }
        
        public System.Threading.Tasks.Task<bool> ModificarArancelesProveedorAsync(double unArancel, double unPorcentajeVip) {
            return base.Channel.ModificarArancelesProveedorAsync(unArancel, unPorcentajeVip);
        }
        
        public bool DesactivarProveedor(string unRut) {
            return base.Channel.DesactivarProveedor(unRut);
        }
        
        public System.Threading.Tasks.Task<bool> DesactivarProveedorAsync(string unRut) {
            return base.Channel.DesactivarProveedorAsync(unRut);
        }
        
        public ServiciosObligatorioWCF.DTOProveedor[] RetornarProveedoresActivos() {
            return base.Channel.RetornarProveedoresActivos();
        }
        
        public System.Threading.Tasks.Task<ServiciosObligatorioWCF.DTOProveedor[]> RetornarProveedoresActivosAsync() {
            return base.Channel.RetornarProveedoresActivosAsync();
        }
    }
}
