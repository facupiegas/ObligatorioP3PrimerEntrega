using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        #region Atributos y Properties

        public enum EnumRol { Administrador, Proveedor, Organizador }
        public string Nombre { set; get; }
        public string Pass { set; get; }
        public EnumRol Rol { set; get; }
        #endregion

        #region Constructor
        public Usuario(EnumRol unRol, string unNombre, string unaPass)
        {
            this.Rol = unRol;
            this.Nombre = unNombre;
            this.Pass = unaPass;
        }
        #endregion
    }
}
