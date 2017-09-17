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
    }
    [DataContract]
    public class DTOProveedor {
        #region Atributos y Properties
        [DataMember]
        public string Rut { get; set; }
        [DataMember]
        public string NomFantasia { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public DateTime Fecha { get; set; }
        [DataMember]
        public bool Activo { get; set; }
        [DataMember]
        public bool Vip { get; set; }
        [DataMember]
        public double PorcentajePorVip { get; set; }
        [DataMember]
        public Usuario Usuario { get; set; }
        #endregion

    }
}
