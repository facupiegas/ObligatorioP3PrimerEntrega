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
    public partial class Modificacion_Arancel_Proveedores_Porcentaje_Vip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["tipo"] == "Administrador") //Valido que el usuario se haya logueado y no se saltee la autentificación
            {
                Response.Redirect("Login.aspx"); //si no se logueó, lo redirijo a Login
            }
            ActualizarPorcentajesActuales(); //cargo valores actuales para mostrar
        }

        protected void ActualizarPorcentajesActuales()
        {
            txtArancelActual.Text = Fachada.DevolverArancelActual().ToString();
            txtPorcentajeVipActual.Text = Fachada.DevolverPorcentajeVipActual().ToString();
        }

        protected void btnActualizarArancel_Click(object sender, EventArgs e)
        {
            lblMensajeArancel.Visible = false;
            lblErrorArancel.Visible = false;
            double tmpValor = Convert.ToDouble(txtArancelNuevo.Text);
            if(tmpValor >= 0 && tmpValor <101)
            {
                lblErrorArancel.Visible = false;
                WCF_Proveedor.OperacionesProveedoresClient proxy = new WCF_Proveedor.OperacionesProveedoresClient();
                lblMensajeArancel.Visible = true;
                proxy.ModificarArancelProveedor(tmpValor);
                lblMensajeArancel.ForeColor = System.Drawing.Color.Green;
                lblMensajeArancel.Text = "El arancel fue modificado existosamente";
                txtArancelNuevo.Text = ""; //no encontre .Clear
                ActualizarPorcentajesActuales();
            }
            else
            {
                lblErrorArancel.Visible = true;
                lblErrorArancel.ForeColor = System.Drawing.Color.Red;
                lblErrorArancel.Text = "Por favor ingrese un valor entre 0 y 100";
            }
        }

        protected void btnPorcentajeVip_Click(object sender, EventArgs e)
        {
            lblMensajeVip.Visible = false;
            lblErrorVip.Visible = false;
            double tmpValorVip = Convert.ToDouble(txtPorcentajeVipNuevo.Text);
            if (tmpValorVip >= 0 && tmpValorVip < 101)
            {
                lblErrorVip.Visible = false;
                WCF_Proveedor.OperacionesProveedoresClient proxy = new WCF_Proveedor.OperacionesProveedoresClient();
                proxy.ModificarPorcentajeVip(tmpValorVip);
                lblMensajeVip.Visible = true;
                lblMensajeVip.ForeColor = System.Drawing.Color.Green;
                lblMensajeVip.Text = "El porcentaje fue modificado existosamente";
                txtPorcentajeVipNuevo.Text = ""; //no encontre .Clear
                ActualizarPorcentajesActuales();
            }
            else
            {
                lblErrorVip.Visible = true;
                lblErrorVip.ForeColor = System.Drawing.Color.Red;
                lblErrorVip.Text = "Por favor ingrese un valor entre 0 y 100";
            }
        }
    }
}