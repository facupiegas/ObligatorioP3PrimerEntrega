using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;



namespace CapaFachada
{
    public class Fachada
    {


        public static Usuario BuscarUsuario(string nombre)
        {
            Usuario retorno = new Usuario() { Nombre = nombre };
            if (!retorno.Leer())
                retorno = null;
            return retorno;
        }
        public static Usuario ValidarUsuario(string nombre, string pass)
        {
            Usuario tmpUsuario = new Usuario { Nombre = nombre };
            Usuario retorno = null;
            if (tmpUsuario.Leer() && tmpUsuario.Pass == pass)
                retorno = tmpUsuario;
            return retorno;
        }
        public static Usuario AltaUsuario(string unNombre, string unPass, Usuario.EnumRol unRol)
        {
            Usuario retorno = null;
            if (BuscarUsuario(unNombre) == null)
            {
                retorno = new Usuario(unNombre, unPass, unRol);
            }
            return retorno;
        }
        public static Proveedor BuscarProveedor(string unRut)
        {
            Proveedor retorno = new Proveedor() { Rut = unRut };
            if (!retorno.Leer())
                retorno = null;
            return retorno;
        }
        public static Proveedor AltaProveedor(string unRut, string unNomFantasia, string unEmail, string unTelefono, DateTime unaFecha, bool esVip, Usuario unUsuario)
        {
            Proveedor retorno = null;
            if (BuscarProveedor(unRut) == null)
            {
                retorno = new Proveedor(unRut, unNomFantasia, unEmail, unTelefono, unaFecha, esVip, unUsuario);
            }
            return retorno;
        }
        public static List<Proveedor> DevolverProveedores()
        {
            Proveedor auxProv = new Proveedor();
            return auxProv.TraerTodo();
        }
        public static List<Servicio> DevolverServicios() {
            Servicio auxServ = new Servicio();
            return auxServ.TraerTodo();
        }
        public static bool GuardarProveedorEnBD(Proveedor unProv)
        {
            return unProv.Guardar();
        }
        public static bool GuardarUsuarioEnBD(Usuario unUser)
        {
            return unUser.Guardar();
        }

    }
}
