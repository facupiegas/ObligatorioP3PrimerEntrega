using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Dominio;
using CapaFachada;

namespace ServiciosObligatorioWCF
{

    public class OperacionesProveedores : IOperacionesProveedores
    {
        bool IOperacionesProveedores.ModificarArancelProveedor(double unArancel)
        {
            return Fachada.ModificarArancelProveedor(unArancel);
        }

        bool IOperacionesProveedores.ModificarPorcentajeVip(double unPorcentajeVip)
        {
            return Fachada.ModificarPorcentajeVip(unPorcentajeVip);
        }

        double IOperacionesProveedores.DevolverArancelActual()
        {
            return Fachada.DevolverArancelActual();
        }

        double IOperacionesProveedores.DevolverPorcentajeVipActual()
        {
            return Fachada.DevolverPorcentajeVipActual();
        }

        bool IOperacionesProveedores.DesactivarProveedor(string unRut)
        {
            return Fachada.DesactivarProveedor(unRut);
        }

        DTOProveedor[] IOperacionesProveedores.RetornarProveedores()
        {
            List<DTOProveedor> aux = new List<DTOProveedor>();
            List<Proveedor> tmpListProv = Fachada.DevolverProveedores(); //recupero la lista completa de proveedores de la BD
            foreach (Proveedor tmpProv in tmpListProv) //por cada Proveedor creo un DTOProveedor con sus datos
            {
                DTOProveedor auxDTO = new DTOProveedor()
                {
                    Rut = tmpProv.Rut,
                    NomFantasia = tmpProv.NomFantasia,
                    Email = tmpProv.Email,
                    Telefono = tmpProv.Telefono,
                    Fecha = tmpProv.Fecha,
                    Activo = tmpProv.Activo,
                    Vip = tmpProv.Vip,
                    PorcentajePorVip = tmpProv.PorcentajePorVip,
                    Usuario = tmpProv.Usuario
                };
                aux.Add(auxDTO); //agrego el DTOProveedor a la lista para devolver
            }
            DTOProveedor[] retorno = aux.ToArray();
            return retorno;
        }

        DTOProveedor IOperacionesProveedores.RetornarProveedorPorRut(string unRut)
        {
            DTOProveedor aux = null;
            Proveedor tmpProv = Fachada.BuscarProveedor(unRut); //busco un Proveedor con el rut ingresado por parametro
            if (tmpProv != null) //si lo encontre
            {
                aux = new DTOProveedor() //creo un objeto DTOProveedor con los datos del Proveedor encontrado
                {
                    Rut = tmpProv.Rut,
                    NomFantasia = tmpProv.NomFantasia,
                    Email = tmpProv.Email,
                    Telefono = tmpProv.Telefono,
                    Fecha = tmpProv.Fecha,
                    Activo = tmpProv.Activo,
                    Vip = tmpProv.Vip,
                    PorcentajePorVip = tmpProv.PorcentajePorVip,
                    Usuario = tmpProv.Usuario
                };
            }
            return aux; 
        }

        DTOProveedor[] IOperacionesProveedores.RetornarProveedoresActivos()
        {
            List<DTOProveedor> aux = new List<DTOProveedor>();
            List<Proveedor> tmpListProv = Fachada.DevolverProveedoresActivos(); //Devuelvo la lista de Proveedores Activos de la BD
            foreach (Proveedor tmpProv in tmpListProv) //Por cada Proveedor en la lista creo un DTOProveedor
            {
                DTOProveedor auxDTO = new DTOProveedor()
                {
                    Rut = tmpProv.Rut,
                    NomFantasia = tmpProv.NomFantasia,
                    Email = tmpProv.Email,
                    Telefono = tmpProv.Telefono,
                    Fecha = tmpProv.Fecha,
                    Activo = tmpProv.Activo,
                    Vip = tmpProv.Vip,
                    PorcentajePorVip = tmpProv.PorcentajePorVip,
                    Usuario = tmpProv.Usuario
                };
                aux.Add(auxDTO); //agrego el DTOProveedor a la lista para devolver
            }
            DTOProveedor[] retorno = aux.ToArray();
            return retorno;
        }

        bool IOperacionesProveedores.AltaProveedor(string unNombreUsuario, string unaContrasena, string unRut, string unNomFantasia, string unEmail, string unTelefono, bool esVip)
        {
            Usuario tmpUser = Fachada.AltaUsuario(unNombreUsuario, unaContrasena, Usuario.EnumRol.Proveedor); //doy de alta el Usuario en memoria
            Proveedor tmpVendor = Fachada.AltaProveedor(unRut, unNomFantasia, unEmail, unTelefono, DateTime.Now.Date, esVip, tmpUser); //doy de alta el Proveedor en memoria
            bool saveUser = Fachada.GuardarUsuarioEnBD(tmpUser); //Guardo el Usuario en la BD
            bool saveVendor = Fachada.GuardarProveedorEnBD(tmpVendor); //Guardo el Proveedor en la BD
            return saveUser && saveVendor;
        }

        void IOperacionesProveedores.GuardarProvEnTxt()
        {
            Fachada.GuardarProvEnTxt();
        }
    }
}
