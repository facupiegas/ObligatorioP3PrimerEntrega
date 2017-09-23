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
    public partial class Alta_Proveedor : System.Web.UI.Page
    {
        private static string AltaProvRutProveedor;//variable definida por conveniencia
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarTipoServicios();
        }

        protected void CargarTipoServicios() {
            ddlTipoServicios.DataSource = Fachada.DevolverTipoServicios();
            ddlTipoServicios.DataValueField = "Nombre";
            ddlTipoServicios.DataBind();
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            WCF_Proveedor.OperacionesProveedoresClient proxyVendor = new WCF_Proveedor.OperacionesProveedoresClient();
            //convierto los datos obtenidos de las cajas de texto validados
            string unNombreUsuario = Convert.ToString(txtNomUsuario.Text);
            string unaContrasena = Convert.ToString(txtPass.Text);
            string unRut = Convert.ToString(txtRut.Text);
            string unNomFantasiaProov = Convert.ToString(txtNomFantasia.Text);
            string unMailProov = Convert.ToString(txtEmail.Text);
            string unTelProov = txtTel.Text;
            bool esVip = false;
            if (radVipN.Checked || radVipS.Checked)
            {
                if (radVipS.Checked)
                {
                    esVip = true;
                }
                #region Metodo antiguo Directo a DOMINIO
                Usuario tmpUser = Fachada.AltaUsuario(unNombreUsuario, unaContrasena, Usuario.EnumRol.Proveedor);
                if (stringEsSoloNumeros(unRut) && stringEsSoloNumeros(unTelProov))
                {

                    Proveedor tmpProv = Fachada.AltaProveedor(unRut, unNomFantasiaProov, unMailProov, unTelProov, DateTime.Now.Date, esVip, tmpUser);
                    bool saveUser = Fachada.GuardarUsuarioEnBD(tmpUser);
                    bool saveProov = Fachada.GuardarProveedorEnBD(tmpProv);
                    if (saveUser && saveProov)
                    {
                        lblMensaje.Text = "Proveedor Creado con Éxito";
                        lblMensaje.ForeColor = System.Drawing.Color.Green;
                        lblMensaje.Visible = true;
                        AltaProvRutProveedor = unRut;
                    }
                    else if (!saveUser)
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
            #endregion
            //de aqui en mas se sigue con la logica que llama a lso web services
            pnlServicio.Visible = true;
            pnlProveedor.Visible = false;

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

        protected void btnImagenServicio_Click(object sender, EventArgs e)
        {
            bool archivoOK = false;
            string path = Server.MapPath("img");
            if (fupImagenServicio.HasFile)
            {
                string extension = System.IO.Path.GetExtension(fupImagenServicio.FileName).ToLower();
                string[] extensionesPermitidas = { ".gif", ".png", ".jpeg", ".jpg" };

                for (int i = 0; i < extensionesPermitidas.Length; i++)
                {
                    if (extension == extensionesPermitidas[i])
                    {
                        archivoOK = true;
                    }
                }
            }
            if (archivoOK)
            {

                string nombreImagen = fupImagenServicio.FileName;
                fupImagenServicio.SaveAs(path + "\\" + nombreImagen);
                imgImagenServicio.ImageUrl = "img/" + nombreImagen;


            }
            else
            {
                lblMsjImagenServicio.Text = "No se aceptan archivos de ese tipo.";
            }
        }

        protected void btnServicio_Click(object sender, EventArgs e)
        {
            string nombreSer = txtNombreServicio.Text;
            string descripcionSer = txtDescripcionServicio.Text;
            string imagenSer = imgImagenServicio.ImageAlign.ToString();

            string stringTipo = ddlTipoServicios.SelectedItem.Value;
            string rutProveedor = AltaProvRutProveedor;
            List<TipoServicio> listTipoServicio = Fachada.DevolverTipoServicios();
            TipoServicio aux=null;
            foreach (TipoServicio tmpTipo in listTipoServicio) {
                if (tmpTipo.Nombre == stringTipo) {
                    aux = tmpTipo;
                }
            }
            if (aux != null)
            {
                Servicio tmpServicio = Fachada.AltaServicio(rutProveedor, nombreSer, imagenSer, descripcionSer, aux);
                if (Fachada.GuardarServicioEnBD(tmpServicio))
                    lblMsjServicio.Text = "El servicio fue creado con exito";
                else
                    lblMsjServicio.Text = "El servicio no pudo ser creado";
            }
            else
            {
                lblMsjServicio.Text = "El servicio no pudo ser creado";
            }
        }
    }
}