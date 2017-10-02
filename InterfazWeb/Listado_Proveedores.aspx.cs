using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using CapaFachada;
using ServiciosObligatorioWCF;


namespace InterfazWeb
{
    public partial class Listado__Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["TipoDeUsuario"] != "Administrador") //Valido que el usuario se haya logueado y no se saltee la autentificación
            {
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();
                Response.Redirect("Login.aspx"); //si no se logueó o quiso ingresar con otras credenciales, lo redirijo a Login y deslogueo
            }
            CargarListadoProveedores(); //se invoca al metodo para cargar el grid view
        }

        protected void CargarListadoProveedores() //metodo que obtiene la lista de proveedores para mostrar en el grid view proveedores
        {
            WCF_Proveedor.OperacionesProveedoresClient proxyOpProv = new WCF_Proveedor.OperacionesProveedoresClient();
            DTOProveedor[] listaDTOProveedoresWCF = proxyOpProv.RetornarProveedores();
            grdListadoProveedores.DataSource = listaDTOProveedoresWCF;
            grdListadoProveedores.DataBind();
        }

        protected void grdServicios_SelectedIndexChanged(object sender, EventArgs e) //metodo que muestra en otro grid view los servicios que el proveedor ofrece
        {
            string rut = grdListadoProveedores.SelectedRow.Cells[1].Text;
            WCF_Servicio.OperacionesServiciosClient proxySer = new WCF_Servicio.OperacionesServiciosClient();
            DTOServicio[] listaDTOSerWCF = proxySer.RetornarServiciosProveedor(rut);
            grdServicios.DataSource = listaDTOSerWCF;
            grdServicios.DataBind();
            
        }
    }
}