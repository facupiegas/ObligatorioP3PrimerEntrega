using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Organizador
    {
        #region Atributos y Properties
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public Usuario Usuario { get; set; }
        #endregion
        #region Metodos

        public Organizador(string unNombre,string unEmail,string unTelefono,Usuario unUsuario) {
            this.Nombre = unNombre;
            this.Email = unEmail;
            this.Telefono = unTelefono;
            this.Usuario = unUsuario;
        }
        #endregion
    }
}
