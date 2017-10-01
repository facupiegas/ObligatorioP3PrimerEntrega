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
    public partial class Desactivar_Proveedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["TipoDeUsuario"] != "Administrador") //Valido que el usuario se haya logueado y no se saltee la autentificación
            {
                Response.Redirect("Login.aspx"); //si no se logueó, lo redirijo a Login
            }
            CargarListadoProveedoresActivos();
        }

        protected void CargarListadoProveedoresActivos() //metodo que obtiene una lista de proveedores activos (a traves del proxy) para mostrarlos en el grid view
        {
            WCF_Proveedor.OperacionesProveedoresClient proxyOpProv = new WCF_Proveedor.OperacionesProveedoresClient();
            DTOProveedor[] listaDTOProveedoresActivosWCF = proxyOpProv.RetornarProveedoresActivos();
            if(listaDTOProveedoresActivosWCF.Count() != 0) { //se carga el grid view unicamente si se encuentran proveedores activos en el sistema
                grdProveedoresActivos.Visible = true;
                grdProveedoresActivos.DataSource = listaDTOProveedoresActivosWCF;
                grdProveedoresActivos.DataBind();
                btnDesactivar.Visible = true;
                lblHeader.Visible = true;
            }
            else
            {
                lblMensaje.Visible = true;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "(*) No se encontraron Proveedores Activos en el sistema.";
                btnDesactivar.Visible = false;
                lblHeader.Visible = false;
                grdProveedoresActivos.Visible = false;
            }
            
        }

        protected void btnDesactivar_Click(object sender, EventArgs e) //metodo que desactiva el proveedor seleccionado del grid view
        {
            lblMensaje.Visible = false;
            WCF_Proveedor.OperacionesProveedoresClient proxyOpProv = new WCF_Proveedor.OperacionesProveedoresClient(); //creo el proxy
            if (grdProveedoresActivos.SelectedRow != null) //debo manejar la excepcion que surge si el usuario aprieta el boton desactivar sin haber seleccionado una fila
            { 
                string rut = grdProveedoresActivos.SelectedRow.Cells[1].Text;//no valido !=null porque si selecciono una fila quiere decir que el proveedor esta activo y por ende tiene un rut, le programa no se caerá
                if (proxyOpProv.DesactivarProveedor(rut))
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "El proveedor fue desactivado exitosamente";
                    CargarListadoProveedoresActivos(); //vuelvo a mostrar los proveedores activos que ahora no contienen al que acabo de desactivar
                }
            }
            else //doy aviso al usuario de que no ha seleccionado ningun proveedor
            {
                lblMensaje.Visible = true;
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "(*) Debe seleccionar un Proveedor";
            }
        }
    }
}