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
        public TipoServicio TipoServicio { get; set; }
        #endregion

        #region Constructor
        public Servicio(string unNombre,string unaDescripcion,TipoServicio unTipoServicio) {
            this.Nombre = unNombre;
            this.Descripcion = unaDescripcion;
            this.Imagen = null;
            this.TipoServicio = unTipoServicio;
        }

        //public void GuardarServiciosEnTxt() { }
        //public void CargarServiciosDesdeTxt() { }
        #endregion

    }
}
