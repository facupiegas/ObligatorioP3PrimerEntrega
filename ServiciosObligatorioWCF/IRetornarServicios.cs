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
    public interface IRetornarServicios
    {
        [OperationContract]
        DTOServicio[] RetornarServicios();
    }
    
    [DataContract]
    public class DTOServicio
    {
        [DataMember]
        public string RutProveedor { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public TipoServicio TipoServicio { get; set; }

    }
}
