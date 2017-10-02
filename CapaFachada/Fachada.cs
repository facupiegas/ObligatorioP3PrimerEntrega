using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dominio;



namespace CapaFachada
{
    public class Fachada
    {
        public static Usuario BuscarUsuario(string nombre)
        {
            Usuario retorno = new Usuario() { Nombre = nombre };
            if (!retorno.Leer())//si es false el Usuario no existe en la BD, sino devuelvo el objeto Usuario con sus datos cargados
                retorno = null;
            return retorno;
        }

        public static Usuario ValidarUsuario(string nombre, string pass)
        {
            Usuario tmpUsuario = new Usuario { Nombre = nombre };
            Usuario retorno = null;
            if (tmpUsuario.Leer() && tmpUsuario.Pass == pass)//si el Usuario existe en la BD y la contraseña es igual a la ingresada por parametro devuelvo el objeto Usuario
                retorno = tmpUsuario;
            return retorno;
        }

        public static Usuario AltaUsuario(string unNombre, string unPass, Usuario.EnumRol unRol)
        {
            Usuario retorno = null;
            if (BuscarUsuario(unNombre) == null)//Si el usuario no existe en la BD 
            {
                retorno = new Usuario(unNombre, unPass, unRol); //doy de alta el Usuario con los datos ingresados
            }
            return retorno;
        }

        public static Proveedor BuscarProveedor(string unRut)
        {
            Proveedor retorno = new Proveedor() { Rut = unRut };
            if (!retorno.Leer())//si es false el Proveedor no existe en la BD, sino devuelvo el objeto Proveedor con sus datos cargados
                retorno = null;
            return retorno;
        }

        public static Proveedor AltaProveedor(string unRut, string unNomFantasia, string unEmail, string unTelefono, DateTime unaFecha, bool esVip, Usuario unUsuario)
        {
            Proveedor retorno = null;
            if (BuscarProveedor(unRut) == null)//si el Proveedor no existe en la BD
            {
                retorno = new Proveedor(unRut, unNomFantasia, unEmail, unTelefono, unaFecha, esVip, unUsuario);//doy de alta el Proveedor con los datos ingresados por parametro
            }
            return retorno;
        }

        public static List<Proveedor> DevolverProveedores()
        {
            Proveedor auxProv = new Proveedor();
            return auxProv.TraerTodo(); //Retorno la lista completa(incluye Proveedores desactivados) de Proveedores de la BD
        }

        public static List<Servicio> DevolverServicios() {
            Servicio auxServ = new Servicio();
            return auxServ.TraerTodo();//Retorno la lista completa de Servicios de la BD
        }

        public static List<Servicio> DevolverServiciosProveedor(string unRut)
        {
            Proveedor auxProv = new Proveedor();
            auxProv.Rut = unRut;
            return auxProv.DevolverServicios();//Retorno la lista completa de Servicios del Proveedor con el rut ingresado por parametro
        }

        public static bool GuardarProveedorEnBD(Proveedor unProv)
        {
            return unProv.Guardar();//Guardo el Proveedor en la BD
        }

        public static bool GuardarUsuarioEnBD(Usuario unUser)
        {
            return unUser.Guardar();//Guardo el Usuario en la BD
        }

        public static bool ModificarArancelProveedor(double unArancel) {
            bool retorno = false;
            if (unArancel >= 0 )//si el valor ingresado es mayor o igual a 0
            {
                Proveedor.Arancel = unArancel;
                retorno = true;
            }
            return retorno;
        }

        public static bool ModificarPorcentajeVip(double unPorcentajeVip)
        {
            bool retorno = false;
            if (unPorcentajeVip >= 0)//si el valor ingresado es mayor o igual a 0
            {
                Proveedor.PorcentajePorVipActual = unPorcentajeVip;
                retorno = true;
            }
            return retorno;
        }

        public static double DevolverArancelActual()
        {
            return Proveedor.Arancel; //Devuelvo el valor actual del Arancel de los Proveedores
        } 

        public static double DevolverPorcentajeVipActual()
        {
            return Proveedor.PorcentajePorVipActual; //Devuelvo el valor actual del porcentaje vip para los nuevos Proveedores
        } 

        public static bool DesactivarProveedor(string unRut)
        {
            bool retorno = false;
            Proveedor tmpProv = new Proveedor(); // creo un objeto proveedor temporal y le agrego el rut que recibo desde la interfaz
            tmpProv.Rut = unRut;
            if (tmpProv.Eliminar())
            {
                retorno = true; //si el proveedor se pudo eliminar retorno true
            }
            return retorno;
        }

        public static List<Proveedor> DevolverProveedoresActivos()
        {
            Proveedor auxProv = new Proveedor();
            return auxProv.TraerActivos();//Devuelve el listado de Proveedores Activos de la BD
        }

        public static Servicio BuscarServicio(string unRut,string unNombre)
        {
            Servicio retorno = new Servicio() { RutProveedor = unRut, Nombre=unNombre };
            if (!retorno.Leer())//si es false el Servicio no existe en la BD, sino devuelvo el objeto Servicio con sus datos cargados
                retorno = null;
            return retorno;
        }

        public static Servicio AltaServicio(string unRutProveedor,string unNombre,string unaImagen,string unaDescripcion,TipoServicio unTipoServicio)
        {
            Servicio retorno = null;
            if (BuscarServicio(unRutProveedor,unNombre) == null)//Si el servicio no existe en la BD
            {
                retorno = new Servicio() {
                    RutProveedor = unRutProveedor,
                    Nombre = unNombre,
                    Imagen = unaImagen,
                    Descripcion = unaDescripcion,
                    TipoServicio = unTipoServicio
                }; //doy de alta el Servicio con los datos ingresados por parametro
            }
            return retorno;
        }

        public static bool GuardarServicioEnBD(Servicio unServicio)
        {
            return unServicio.Guardar(); //guardo el Servicio en la BD
        }

        public static List<TipoServicio> DevolverTipoServicios() {
            TipoServicio tmpTipoSer = new TipoServicio();
            return tmpTipoSer.TraerTodo();//devuelvo la lista de TipoServicio de la BD
        }

        public static bool AltaProvUsuSerTransaccional(Proveedor unProv,Usuario unUsu,Servicio unServ) {
            bool retorno = false;
            SqlTransaction trans = null;
            SqlConnection conn = unUsu.ObtenerConexion();
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                if (unUsu.GuardarTrans(conn, trans) && unProv.GuardarTrans(conn, trans) && unServ.GuardarTrans(conn, trans)) //si los tres objetos son guardados en la BD
                {
                    trans.Commit();
                    retorno = true;
                    //actualizo los archivos .txt
                    GuardarProvEnTxt(); 
                    GuardarServiciosEnTxt();
                }
                        
                else{ //si el guardado de algunos de los objetos falla devuelvo la BD a su estado original
                    trans.Rollback();
                }

            }
            catch (Exception ex)//si el guardado de algunos de los objetos falla devuelvo la BD a su estado original
            {
                if (trans != null) trans.Rollback();
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open) conn.Close(); //si la conexion esta abierta la cierro
            }

            return retorno;
        }

        public static void GuardarProvEnTxt()
        {
            Proveedor tmp = new Proveedor();
            tmp.GuardarProveedoresEnTxt(); //Guardo los proveedores y sus servicios en el archivo .txt
        }

        public static void GuardarServiciosEnTxt() {
            Servicio tmpServ = new Servicio();
            tmpServ.GuardarServiciosEnTxt(); //Guardo los Servicios con sus TipoEvento correspondientes
        }
    }
}
