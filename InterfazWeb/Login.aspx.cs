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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Fachada.GuardarServiciosEnTxt(); //guardo el catalogo de servicios en un txt
        }

        protected void controlLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string unNombreUsuario = controlLogin.UserName.ToString();
            string unPass = controlLogin.Password.ToString();
            Usuario Usuario = Fachada.ValidarUsuario(unNombreUsuario, unPass);

            if (Usuario != null)
            {
                Session["UsuarioLogueado"] = Usuario.Nombre;
                Session["TipoDeUsuario"] = Usuario.Rol.ToString();
                e.Authenticated = true;
            }
        }
    }
}