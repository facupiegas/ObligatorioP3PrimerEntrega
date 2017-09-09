using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class TipoEvento
    {
        #region Atributos y Properties
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        #endregion
        #region Metodos
        public TipoEvento(string unNombre,string unaDescripcion) {
            this.Nombre = unNombre;
            this.Descripcion = unaDescripcion;
        }
        #endregion
    }
}
