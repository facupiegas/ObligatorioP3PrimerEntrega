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
                lblError.Visible = false;
                lblMensaje.Visible = false;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string unRut = txtRut.Text;
            WCF_Proveedor.OperacionesProveedoresClient proxyProv = new WCF_Proveedor.OperacionesProveedoresClient();
            List<DTOProveedor> listProv = new List<DTOProveedor>();
            DTOProveedor provDTO = proxyProv.RetornarProveedorPorRut(unRut); //Guardo el Proveedor con el rut ingresado en un objeto DTOProveedor
            if (provDTO != null)
            {
                listProv.Add(provDTO); //lo agrego a una lista auxiliar para mostrarlo en un gridview
            }
            grdProveedor.DataSource = listProv;
            WCF_Servicio.OperacionesServiciosClient proxyServ = new WCF_Servicio.OperacionesServiciosClient();
            if (stringEsSoloNumeros(unRut))//si ingresa un rut con el formato correcto
            {
                lblError.Visible = false;
                DTOServicio [] arrayServ = proxyServ.RetornarServiciosProveedor(unRut);//se cargan los Servicios del proveedor con el rut ingresado
                grdServicios.DataSource = arrayServ;
                if (listProv.Count == 0)//si la lista auxiliar esta vacia quiere decir que no se encontro un Proveedor con el rut ingresado
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
                    lblMensaje.Text = "Proveedor no encontrado";
                }
            }
            else
            {
                lblError.Visible = true;
            }
            grdProveedor.DataBind();
            grdServicios.DataBind();
        }

        public bool stringEsSoloNumeros(string unString) //metodo que valida que un string ingresado solo contenga numeros
        {
            foreach (char c in unString)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }
            return true;
        }
    }
}