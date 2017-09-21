using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using CapaFachada;

namespace InterfazWeb
{
    public partial class Alta_Proveedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //convierto los datos obtenidos de las cajas de texto validados
            string unNombreUsuario = Convert.ToString(txtNomUsuario.Text);
            string unaContrasena = Convert.ToString(txtPass.Text);
            string unRut = Convert.ToString(txtRut.Text);
            string unNomFantasiaProov = Convert.ToString(txtNomFantasia.Text);
            string unMailProov = Convert.ToString(txtEmail.Text);
            string unTelProov = txtTel.Text;
            bool esVip = false;
            if(radVipN.Checked || radVipS.Checked)
            {
                if(radVipS.Checked)
                {
                    esVip = true;
                }
                Usuario tmpUser = Fachada.AltaUsuario(unNombreUsuario, unaContrasena, Usuario.EnumRol.Proveedor);
                if (stringEsSoloNumeros(unRut) && stringEsSoloNumeros(unTelProov))
                {
                    
                    Proveedor tmpProv = Fachada.AltaProveedor(unRut, unNomFantasiaProov, unMailProov, unTelProov, DateTime.Now.Date, esVip, tmpUser);
                    bool saveUser = Fachada.GuardarUsuarioEnBD(tmpUser);
                    bool saveProov = Fachada.GuardarProveedorEnBD(tmpProv);
                    if (saveUser && saveProov){
                        lblMensaje.Text = "Proveedor Creado con Éxito";
                        lblMensaje.ForeColor = System.Drawing.Color.Green;
                        lblMensaje.Visible = true;
                    }
                    else if(!saveUser)
                    {
                        lblMensaje.Text = "(*) El nombre de usuario ingresado ya ha sido utilizado";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                        lblMensaje.Visible = true;
                    }
                    else if (!saveProov)
                    {
                        lblMensaje.Text = "(*) El proveedor ya existe en el sistema (RUT)";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                        lblMensaje.Visible = true;
                    }
                }
                else
                {
                    lblMensaje.Text = "(*) Rut y/o telefono inválido";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
                }
            }
            else
            {
                lbErrorlVip.Text = "Debe seleccionar una opción";
                lbErrorlVip.Visible = true;
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