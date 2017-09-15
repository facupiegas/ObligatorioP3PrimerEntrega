using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace InterfazWeb
{
    public partial class Listado__Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["tipo"] == "Administrador") //Valido que el usuario se haya logueado y no se saltee la autentificación
            {
                Response.Redirect("Login.aspx"); //si no se logueó, lo redirijo a Login
            }
            CargarListadoProveedores();
        }
        protected void CargarListadoProveedores()
        {
            List<Proveedor> listaProveedores = Proveedor.DevolverProveedores();
            grdListadoProveedores.DataSource = listaProveedores;
            grdListadoProveedores.DataBind();
        }
        protected void grdServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rut = grdListadoProveedores.SelectedRow.Cells[1].Text;
            Proveedor aux = new Proveedor() { Rut=rut};
            grdServicios.DataSource = aux.DevolverServicios();
            grdServicios.DataBind();
        }
    }
}