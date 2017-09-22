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
    public class RetornarServicios : IRetornarServicios
    {
        DTOServicio[] IRetornarServicios.RetornarServicios()
        {
            List<DTOServicio> aux = new List<DTOServicio>();
            List<Servicio> tmpListServ = Fachada.DevolverServicios();
            foreach (Servicio tmpServ in tmpListServ)
            {
                DTOServicio auxDTO = new DTOServicio()
                {
                    RutProveedor = tmpServ.RutProveedor,
                    Nombre = tmpServ.Nombre,
                    Imagen = tmpServ.Imagen,
                    Descripcion = tmpServ.Descripcion,
                    TipoServicio = tmpServ.TipoServicioString
                };
                aux.Add(auxDTO);
            }
            DTOServicio[] retorno = aux.ToArray();
            return retorno;
        }

        DTOServicio[] IRetornarServicios.RetornarServiciosProveedor(string unRut)
        {
            List<DTOServicio> aux = new List<DTOServicio>(); //creo lista a devolver
            foreach (Servicio tmpServ in Fachada.DevolverServiciosProveedor(unRut)) // le pido a la fachada me devuelva lso servicios del proveedor y la recorro creando un DTOServicio por cada uno que contenga la lista
            {
                DTOServicio auxDTO = new DTOServicio()
                {
                    RutProveedor = tmpServ.RutProveedor,
                    Nombre = tmpServ.Nombre,
                    Imagen = tmpServ.Imagen,
                    Descripcion = tmpServ.Descripcion,
                    TipoServicio = tmpServ.TipoServicioString
                };
                aux.Add(auxDTO); // agrego el DRO servicio para devolverselo a la capa Interfaz
            }
            DTOServicio[] retorno = aux.ToArray();
            return retorno;
        }
    }
}
