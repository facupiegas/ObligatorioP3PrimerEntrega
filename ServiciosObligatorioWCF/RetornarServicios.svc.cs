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
    }
}
