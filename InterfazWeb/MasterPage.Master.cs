using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace InterfazWeb
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string tipo = (string)Session["TipoDeUsuario"];
            lblBienvenidaUser.Text = "Bienvenido,  " + (string)Session["UsuarioLogueado"];
            lblBienvenidaUser.Visible = true;
            lnkSalir.Visible = true;
            if (tipo != null)
            {
                if (tipo == "Administrador")
                {
                    menuAdmin.Visible = true;
                    menuProveedor.Visible = false;
                    menuOrg.Visible = false;
                }
                if (tipo == "Proveedor")
                {
                    menuAdmin.Visible = false;
                    menuProveedor.Visible = true;
                    menuOrg.Visible = false;
                }
                if (tipo == "Organizador")
                {
                    menuAdmin.Visible = false;
                    menuProveedor.Visible = false;
                    menuOrg.Visible = true;
                }
            }
            else //si el login no valido lso datos ingresados no muestro ninguno de las barras de navegacion
            {
                menuAdmin.Visible = false;
                menuProveedor.Visible = false;
                menuOrg.Visible = false;
                lnkSalir.Visible = false;
                lblBienvenidaUser.Visible = false;
            }

        }

        protected void lnkSalir_Click(object sender, EventArgs e)//Boton que desloguea al usuario
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }
    }
}