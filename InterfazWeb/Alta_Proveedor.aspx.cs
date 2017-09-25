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

        private static string AltaProvRutProveedor;//variable definida para guardar el rut del proveedor temporalmente
        private static Proveedor AltaProv;
        private static Usuario AltaUsu;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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
                
                Usuario tmpUser = Fachada.AltaUsuario(unNombreUsuario, unaContrasena, Usuario.EnumRol.Proveedor);
                if (stringEsSoloNumeros(unRut) && stringEsSoloNumeros(unTelProov))
                {

                    Proveedor tmpProv = Fachada.AltaProveedor(unRut, unNomFantasiaProov, unMailProov, unTelProov, DateTime.Now.Date, esVip, tmpUser);
                    //bool saveUser = Fachada.GuardarUsuarioEnBD(tmpUser);
                    //bool saveProov = Fachada.GuardarProveedorEnBD(tmpProv);
                    if (tmpUser!=null && tmpProv!=null)
                    {
                        
                        lblMensaje.Text = "Proveedor Creado con Éxito";
                        lblMensaje.ForeColor = System.Drawing.Color.Green;
                        lblMensaje.Visible = true;
                        AltaProvRutProveedor = unRut;
                        AltaUsu = tmpUser;
                        AltaProv = tmpProv;
                        pnlServicio.Visible = true;
                        pnlProveedor.Visible = false;
                    }
                    else if (tmpUser==null)
                    {
                        lblMensaje.Text = "(*) El nombre de usuario ingresado ya ha sido utilizado";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                        lblMensaje.Visible = true;
                    }
                    else if (tmpProv==null)
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
            string imagenSer = imgImagenServicio.ImageUrl;

            string stringTipo = ddlTipoServicios.SelectedItem.Value;
            //string rutProveedor = AltaProvRutProveedor;
            string rutProveedor = AltaProv.Rut;
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
                WCF_Servicio.OperacionesServiciosClient proxy = new WCF_Servicio.OperacionesServiciosClient();
                //if (Fachada.GuardarServicioEnBD(tmpServicio))
                if (tmpServicio != null)
                {
                    lblMsjServicio.Text = "El servicio fue creado con exito";
                    if (proxy.AltaProveedor(AltaProv,AltaUsu,tmpServicio))
                        lblMsjServicio.Text = "El proveedor,usuario y servicio fueron creados con exito!";
                    else
                        lblMsjServicio.Text = "Algunos de los datos fueron erroreos, el proveedor,usuario y servicio no fueron dados de alta";
                }
                else
                {
                    lblMsjServicio.Text = "El servicio no pudo ser creado";
                }
            }
            else
            {
                lblMsjServicio.Text = "El servicio no pudo ser creado";
            }
        }
    }
}