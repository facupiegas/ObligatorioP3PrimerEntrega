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
    public interface IOperacionesProveedores
    {
        [OperationContract]
        DTOProveedor[] RetornarProveedores();

        [OperationContract]
        DTOProveedor RetornarProveedorPorRut(string unRut);

        [OperationContract]
        bool ModificarArancelesProveedor(double unArancel, double unPorcentajeVip);

        [OperationContract]
        bool DesactivarProveedor(string unRut);

        [OperationContract]
        DTOProveedor[] RetornarProveedoresActivos();

        [OperationContract]
        bool AltaProveedor(string unNombreUsuario, string unaContrasena, string unRut, string unNomFantasia, string unEmail, string unTelefono, bool esVip);

    }

}
