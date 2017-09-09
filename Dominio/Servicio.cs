using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Servicio
    {
        #region Atributos y Proeperties
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public List<TipoEvento> Eventos { get; set; }

        #endregion

        #region Metdos
        public Servicio(string unNombre,string unaDescripcion) {
            this.Nombre = unNombre;
            this.Descripcion = unaDescripcion;
            this.Imagen = null;
            this.Eventos = new List<TipoEvento>();
        }

        //public void GuardarServiciosEnTxt() { }
        //public void CargarServiciosDesdeTxt() { }
        #endregion

    }
}
