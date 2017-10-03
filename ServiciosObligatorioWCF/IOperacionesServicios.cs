using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Dominio;


namespace ServiciosObligatorioWCF
{
    
    [ServiceContract]
    public interface IOperacionesServicios
    {
        [OperationContract]
        DTOServicio[] RetornarServicios();
        [OperationContract]
        DTOTipoServicio[] RetornarTipoServicios();
        [OperationContract]
        DTOTipoEvento[] RetornarTipoEventos();

        [OperationContract]
        DTOServicio[] RetornarServiciosProveedor(string unRut);

        [OperationContract]
        bool AltaProveedor(Proveedor unProv, Usuario unUsu, Servicio unServ);
        [OperationContract]
        void GuardarServiciosEnTxt();
    }
}
