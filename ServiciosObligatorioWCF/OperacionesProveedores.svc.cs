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
        public bool ModificarArancelProveedor(double unArancel)
        {
            return Fachada.ModificarArancelProveedor(unArancel);
        }

        public bool ModificarPorcentajeVip(double unPorcentajeVip)
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

        public bool DesactivarProveedor(string unRut)
        {
            return Fachada.DesactivarProveedor(unRut);//le paso el rut a la fachada para que le pido al proveedor que se elimine
        }

        DTOProveedor[] IOperacionesProveedores.RetornarProveedores()
        {
            List<DTOProveedor> aux = new List<DTOProveedor>();
            List<Proveedor> tmpListProv = Fachada.DevolverProveedores();
            foreach (Proveedor tmpProv in tmpListProv)
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
                aux.Add(auxDTO);
            }
            DTOProveedor[] retorno = aux.ToArray();
            return retorno;
        }

        DTOProveedor IOperacionesProveedores.RetornarProveedorPorRut(string unRut)
        {
            DTOProveedor aux = null;
            Proveedor tmpProv = Fachada.BuscarProveedor(unRut);
            if (tmpProv != null)
            {
                aux = new DTOProveedor()
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
            return aux; //null si no existe o con sus datos si lo encontre
        }

        DTOProveedor[] IOperacionesProveedores.RetornarProveedoresActivos()
        {
            List<DTOProveedor> aux = new List<DTOProveedor>();
            List<Proveedor> tmpListProv = Fachada.DevolverProveedoresActivos();
            foreach (Proveedor tmpProv in tmpListProv)
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
                aux.Add(auxDTO);
            }
            DTOProveedor[] retorno = aux.ToArray();
            return retorno;
        }

        public bool AltaProveedor(string unNombreUsuario, string unaContrasena, string unRut, string unNomFantasia, string unEmail, string unTelefono, bool esVip)
        {
            Usuario tmpUser = Fachada.AltaUsuario(unNombreUsuario, unaContrasena, Usuario.EnumRol.Proveedor);
            Proveedor tmpVendor = Fachada.AltaProveedor(unRut, unNomFantasia, unEmail, unTelefono, DateTime.Now.Date, esVip, tmpUser);
            bool saveUser = Fachada.GuardarUsuarioEnBD(tmpUser);
            bool saveVendor = Fachada.GuardarProveedorEnBD(tmpVendor);
            return saveUser && saveVendor;
        }
    }
}
