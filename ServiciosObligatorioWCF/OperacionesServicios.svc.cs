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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OperacionesServicios" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OperacionesServicios.svc or OperacionesServicios.svc.cs at the Solution Explorer and start debugging.
    public class OperacionesServicios : IOperacionesServicios
    {
        DTOServicio[] IOperacionesServicios.RetornarServicios()
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

        DTOServicio[] IOperacionesServicios.RetornarServiciosProveedor(string unRut)
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
                aux.Add(auxDTO); // agrego el DTO servicio para devolverselo a la capa Interfaz
            }
            DTOServicio[] retorno = aux.ToArray();
            return retorno;
        }
    }
}
