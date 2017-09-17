using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class TipoServicio
    {
        #region Atributos y properties
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<TipoEvento> TipoEventos { get; set; }
        #endregion
        #region Constructores
        public TipoServicio(string unNombre, string unaDescripcion) {
            this.Nombre = unNombre;
            this.Descripcion = unaDescripcion;
            this.TipoEventos = new List<TipoEvento>();
        }
        public TipoServicio() {
            this.TipoEventos = new List<TipoEvento>();
        }
        #endregion
        #region Otros Metodos
        public override string ToString()
        {
            return "Tipo: "+this.Nombre+" | Descripcion: "+this.Descripcion;
        }
        #endregion
    }
}
