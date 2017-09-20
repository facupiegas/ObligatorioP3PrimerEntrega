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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRetornarServicios" in both code and config file together.
    [ServiceContract]
    public interface IRetornarServicios
    {
        [OperationContract]
        DTOServicio[] RetornarServicios();
       
    }

   
}
