using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ServicioContratado
    {
        #region Atributos y Properties
        public Proveedor Proveedor{ get; set; }
        public List<Servicio> ListaServicios { get; set; }
        #endregion

        #region Metodos
        public ServicioContratado(Proveedor unProveedor, List<Servicio> unaListaDeServicios) {
            this.Proveedor = unProveedor;
            this.ListaServicios = unaListaDeServicios;
        }

        #endregion
    }
}
