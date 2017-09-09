using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Evento
    {
        #region Atributos y Properties
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public TipoEvento TipoEvento{ get; set; }
        public string Direccion{ get; set; }
        #endregion

        #region Metodos
        public Evento(String unNombre, DateTime unaFecha, TipoEvento unTipoEvento, string unaDireccion)
        {
            this.Nombre = unNombre;
            this.Fecha = unaFecha;
            this.TipoEvento = unTipoEvento;
            this.Direccion = unaDireccion;
        }

        #endregion
    }
}
