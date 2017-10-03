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
    
    public class OperacionesServicios : IOperacionesServicios
    {
        DTOServicio[] IOperacionesServicios.RetornarServicios()
        {
            List<DTOServicio> aux = new List<DTOServicio>();
            List<Servicio> tmpListServ = Fachada.DevolverServicios(); //recupero la lista de Servicios de la BD
            foreach (Servicio tmpServ in tmpListServ) //por cada Servicio en la lista creo un objeto DTOServicio
            {
                DTOServicio auxDTO = new DTOServicio()
                {
                    RutProveedor = tmpServ.RutProveedor,
                    Nombre = tmpServ.Nombre,
                    Imagen = tmpServ.Imagen,
                    Descripcion = tmpServ.Descripcion,
                    TipoServicio = tmpServ.TipoServicioString
                };
                aux.Add(auxDTO); //Agrego el nuevo objeto a la lista para devolver
            }
            DTOServicio[] retorno = aux.ToArray();
            return retorno;
        }

        DTOServicio[] IOperacionesServicios.RetornarServiciosProveedor(string unRut)
        {
            List<DTOServicio> aux = new List<DTOServicio>(); 
            foreach (Servicio tmpServ in Fachada.DevolverServiciosProveedor(unRut)) //recupero la lista de Servicios del Proveedor con el rut ingresado por parametro
            { //por cada objeto Servicio creo un DTOServicio con sus datos
                DTOServicio auxDTO = new DTOServicio() 
                {
                    RutProveedor = tmpServ.RutProveedor,
                    Nombre = tmpServ.Nombre,
                    Imagen = tmpServ.Imagen,
                    Descripcion = tmpServ.Descripcion,
                    TipoServicio = tmpServ.TipoServicioString
                };
                aux.Add(auxDTO); //Agrego el nuevo objeto a la lista para devolver
            }
            DTOServicio[] retorno = aux.ToArray();
            return retorno;
        }
        bool IOperacionesServicios.AltaProveedor(Proveedor unProv, Usuario unUsu, Servicio unServ)
        {
            return Fachada.AltaProvUsuSerTransaccional(unProv, unUsu, unServ); //Guardo transaccionalmente en BD Usuario,Proveedor y un Servicio
        }
        void IOperacionesServicios.GuardarServiciosEnTxt() {
            Fachada.GuardarServiciosEnTxt();
        }

        DTOTipoServicio[] IOperacionesServicios.RetornarTipoServicios()
        {
            List<DTOTipoServicio> aux = new List<DTOTipoServicio>();
            List<TipoServicio> tmpListTipoServ = Fachada.DevolverTipoServicios(); //recupero la lista de Servicios de la BD
            foreach (TipoServicio tmpServ in tmpListTipoServ) //por cada Servicio en la lista creo un objeto DTOServicio
            {
                DTOTipoServicio auxDTO = new DTOTipoServicio()
                {
                    Nombre = tmpServ.Nombre,
                    Descripcion = tmpServ.Descripcion
                };
                aux.Add(auxDTO); //Agrego el nuevo objeto a la lista para devolver
            }
            DTOTipoServicio[] retorno = aux.ToArray();
            return retorno;
        }

        DTOTipoEvento[] IOperacionesServicios.RetornarTipoEventos()
        {
            List<DTOTipoEvento> aux = new List<DTOTipoEvento>();
            List<TipoEvento> tmpListTipoEv = Fachada.DevolverTipoEvento(); //recupero la lista de Servicios de la BD
            foreach (TipoEvento tmpServ in tmpListTipoEv) //por cada Servicio en la lista creo un objeto DTOServicio
            {
                List<TipoServicio> listTmp = new List<TipoServicio>();
                foreach (TipoServicio tmpTipoServ in tmpServ.TipoServicios) {
                    listTmp.Add(tmpTipoServ);
                }
                DTOTipoEvento auxDTO = new DTOTipoEvento()
                {
                    Nombre = tmpServ.Nombre,
                    Descripcion = tmpServ.Descripcion,
                    TipoServicios = listTmp
                };
                aux.Add(auxDTO); //Agrego el nuevo objeto a la lista para devolver
            }
            DTOTipoEvento[] retorno = aux.ToArray();
            return retorno;
        }
    }
}
