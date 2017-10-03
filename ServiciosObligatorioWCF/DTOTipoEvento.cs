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
    [DataContract]
    public class DTOTipoEvento
    {
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public List<TipoServicio> TipoServicios { get; set; }
    }
}