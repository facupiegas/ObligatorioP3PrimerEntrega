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
    
    public class RetornarProveedores : IRetornarProveedores
    {


        DTOProveedor[] IRetornarProveedores.RetornarProveedores()
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
    }
}
