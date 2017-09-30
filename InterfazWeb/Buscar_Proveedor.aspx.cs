using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiciosObligatorioWCF;

namespace InterfazWeb
{
    public partial class Buscar_Proveedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["TipoDeUsuario"] != "Administrador") //Valido que el usuario se haya logueado y no se saltee la autentificación
            {
                Response.Redirect("Login.aspx"); //si no se logueó, lo redirijo a Login
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string unRut = txtRut.Text;
            WCF_Proveedor.OperacionesProveedoresClient proxyProv = new WCF_Proveedor.OperacionesProveedoresClient();
            List<DTOProveedor> listProv = new List<DTOProveedor>();
            listProv.Add(proxyProv.RetornarProveedorPorRut(unRut));
            grdProveedor.DataSource = listProv;
            WCF_Servicio.OperacionesServiciosClient proxyServ = new WCF_Servicio.OperacionesServiciosClient();
            grdServicios.DataSource = proxyServ.RetornarServiciosProveedor(unRut);
            grdProveedor.DataBind();
            grdServicios.DataBind();

        }

       
    }
}