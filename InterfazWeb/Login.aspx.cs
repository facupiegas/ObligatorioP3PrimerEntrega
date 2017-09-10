﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace InterfazWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void controlLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string unNombreUsuario = controlLogin.UserName.ToString();
            string unPass = controlLogin.Password.ToString();

            Usuario Usuario = Usuario.ValidarUsuario(unNombreUsuario, unPass);

            if (Usuario != null)
            {
                Session["UsuarioLogueado"] = Usuario.Nombre;
                Session["TipoDeUsuario"] = Usuario.Rol.ToString();
                e.Authenticated = true;
            }
        }
    }
}