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

    [DataContract]
    public class DTOServicio
    {
        #region Atributos y Properties
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
        #endregion
    }
}
