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
            if ((string)Session["TipoDeUsuario"] != "Administrador") //Valido que el usuario se haya logueado y no se saltee la autentificación
            {
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();
                Response.Redirect("Login.aspx"); //si no se logueó o quiso ingresar con otras credenciales, lo redirijo a Login y deslogueo
            }
            ActualizarPorcentajesActuales(); //cargo valores actuales para mostrar
            lblErrorVip.Visible = false;
            lblMensajeArancel.Visible = false;
            lblMensajeVip.Visible = false;
        }

        protected void ActualizarPorcentajesActuales() //metodo que obtiene los valores actuales de los porcentajes para mostrarlos en los text box
        {
            WCF_Proveedor.OperacionesProveedoresClient proxy = new WCF_Proveedor.OperacionesProveedoresClient();
            txtArancelActual.Text = proxy.DevolverArancelActual().ToString();
            txtPorcentajeVipActual.Text = proxy.DevolverPorcentajeVipActual().ToString();
        }

        protected void btnActualizarArancel_Click(object sender, EventArgs e) //metodo que modifica el arancel de los proveedores
        {
            string arancel = txtArancelNuevo.Text;
            if (stringEsSoloNumeros(arancel) && arancel != "") //valido que el usuario haya ingresado un valor numerico
            {
                lblMensajeArancel.Visible = false;
                lblErrorArancel.Visible = false;
                double tmpValor = Convert.ToDouble(arancel);
                if (tmpValor >= 0 && tmpValor < 101) //valido que el valor ingresado este dentro del rango admitido
                {
                    lblErrorArancel.Visible = false;
                    WCF_Proveedor.OperacionesProveedoresClient proxy = new WCF_Proveedor.OperacionesProveedoresClient();
                    lblMensajeArancel.Visible = true;
                    proxy.ModificarArancelProveedor(tmpValor); //si es un valor valido, mediante el proxy modifico el valor
                    lblMensajeArancel.ForeColor = System.Drawing.Color.Green;
                    lblMensajeArancel.Text = "El arancel fue modificado existosamente";
                    txtArancelNuevo.Text = ""; //no encontre .Clear
                    ActualizarPorcentajesActuales();
                }
                else //doy aviso que no es un porcentaje valido
                {
                    lblErrorArancel.Visible = true;
                    lblErrorArancel.ForeColor = System.Drawing.Color.Red;
                    lblErrorArancel.Text = "Por favor ingrese un valor entero entre 0 y 100";
                }
            }
            else //doy aviso que no es un porcentaje valido
            {
                lblErrorArancel.Visible = true;
                lblErrorArancel.ForeColor = System.Drawing.Color.Red;
                lblErrorArancel.Text = "Por favor ingrese un valor entero entre 0 y 100";
            }
        }

        protected void btnPorcentajeVip_Click(object sender, EventArgs e) //metodo que modifica el porcentaje de los proveedores vip
        {
            string porcVip = txtPorcentajeVipNuevo.Text;
            if (stringEsSoloNumeros(porcVip) && porcVip != "") //valido que el usuario haya ingresado un valor numerico
            {
                lblMensajeVip.Visible = false;
                lblErrorVip.Visible = false;
                double tmpValorVip = Convert.ToDouble(porcVip);
                if (tmpValorVip >= 0 && tmpValorVip < 101) //valido que el valor ingresado este dentro del rango admitido
                {
                    lblErrorVip.Visible = false;
                    WCF_Proveedor.OperacionesProveedoresClient proxy = new WCF_Proveedor.OperacionesProveedoresClient();
                    proxy.ModificarPorcentajeVip(tmpValorVip); //si es un valor valido, mediante el proxy modifico el valor
                    lblMensajeVip.Visible = true;
                    lblMensajeVip.ForeColor = System.Drawing.Color.Green;
                    lblMensajeVip.Text = "El porcentaje fue modificado existosamente";
                    txtPorcentajeVipNuevo.Text = ""; //no encontre .Clear
                    ActualizarPorcentajesActuales();
                }
                else //doy aviso que no es un porcentaje valido
                {
                    lblErrorVip.Visible = true;
                    lblErrorVip.ForeColor = System.Drawing.Color.Red;
                    lblErrorVip.Text = "Por favor ingrese un valor entero entre 0 y 100";
                }
            }
            else //doy aviso que no es un porcentaje valido
            {
                lblErrorVip.Visible = true;
                lblErrorVip.ForeColor = System.Drawing.Color.Red;
                lblErrorVip.Text = "Por favor ingrese un valor entero entre 0 y 100";
            }
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