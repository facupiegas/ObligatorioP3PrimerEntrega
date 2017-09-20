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
    public interface IRetornarProveedores
    {
        [OperationContract]
        DTOProveedor[] RetornarProveedores();

        [OperationContract]
        DTOProveedor RetornarProveedorPorRut(string unRut);
    }
   
}
