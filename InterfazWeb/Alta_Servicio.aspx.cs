using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaFachada;
using ServiciosObligatorioWCF;
using Dominio;


namespace InterfazWeb
{
    public partial class Alta_Servicio : System.Web.UI.Page
    {
        private static string auxRutProveedor;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["TipoDeUsuario"] != "Administrador") //Valido que el usuario se haya logueado y no se saltee la autentificación
            {
                Response.Redirect("Login.aspx"); //si no se logueó, lo redirijo a Login
            }
            if (!IsPostBack)
                CargarTipoServicios();
        }
        protected void CargarTipoServicios()
        {
            ddlTipoServicios.DataSource = Fachada.DevolverTipoServicios();
            ddlTipoServicios.DataValueField = "Nombre";
            ddlTipoServicios.DataBind();
        }

        protected void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            auxRutProveedor = txtRut.Text;
            WCF_Proveedor.OperacionesProveedoresClient proxyProv = new WCF_Proveedor.OperacionesProveedoresClient();
            DTOProveedor dtoProv = proxyProv.RetornarProveedorPorRut(auxRutProveedor);
            if(dtoProv != null && dtoProv.Activo)
            {
                WCF_Servicio.OperacionesServiciosClient proxyServ = new WCF_Servicio.OperacionesServiciosClient();
                DTOServicio[] listSer = proxyServ.RetornarServiciosProveedor(auxRutProveedor);
                if (stringEsSoloNumeros(auxRutProveedor))
                {
                    if (listSer.Count() == 0)
                    {
                        lblMsjProveedor.ForeColor = System.Drawing.Color.Red;
                        lblMsjProveedor.Text = "El Rut ingresado no esta asociado a ningun proveedor registrado";
                    }
                    else
                    {
                        lblMsjProveedor.Text = string.Empty;
                        pnlNuevoServicio.Visible = true;
                    }
                    grdServicios.DataSource = listSer;
                    grdServicios.DataBind();
                }
                else
                {
                    lblMsjProveedor.ForeColor = System.Drawing.Color.Red;
                    lblMsjProveedor.Text = "El campo solo admite numeros";
                }
            }
            else
            {
                lblMsjProveedor.ForeColor = System.Drawing.Color.Red;
                lblMsjProveedor.Text = "El Rut del proveedor ingresado no se encuentra activo.";
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
        private bool extensionArchivoOK(string nombreArchivo)
        {
            bool ok = false;
            string fileExtension =
                    System.IO.Path.GetExtension(nombreArchivo).ToLower();
            string[] allowedExtensions =
                {".gif", ".png", ".jpeg", ".jpg"};
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i])
                {
                    ok = true;
                }
            }
            return ok;
        }//verifica extension de la imagen

        protected void btnUpload_Click(object sender, ImageClickEventArgs e)
        {
            if (extensionArchivoOK(fupImagenServicio.FileName))
            {
                string path = Server.MapPath("img");
                string archivo = path + "\\" + fupImagenServicio.FileName;
                fupImagenServicio.SaveAs(archivo);
                imgImagenServicio.ImageUrl = "img\\" + fupImagenServicio.FileName;
                lblErrorFoto.Visible = false;
            }
            else
            {
                lblErrorFoto.Visible = true;
                lblErrorFoto.Text = "Formato de imagen admitido .gif/.png/.jpeg/.jpg";
                imgImagenServicio.ImageUrl = null;
            }
        }

        protected void btnServicio_Click(object sender, EventArgs e)
        {
            if (imgImagenServicio.ImageUrl != "")
            {
                string nombreSer = txtNombreServicio.Text;
                string descripcionSer = txtDescripcionServicio.Text;
                string imagenSer = imgImagenServicio.ImageUrl;
                string stringTipo = ddlTipoServicios.SelectedItem.Value;
                string rutProveedor = auxRutProveedor;
                List<TipoServicio> listTipoServicio = Fachada.DevolverTipoServicios();
                TipoServicio aux = null;
                foreach (TipoServicio tmpTipo in listTipoServicio)
                {
                    if (tmpTipo.Nombre == stringTipo)
                    {
                        aux = tmpTipo;
                    }
                }
                if (aux != null)
                {
                    Servicio tmpServicio = Fachada.AltaServicio(rutProveedor, nombreSer, imagenSer, descripcionSer, aux);
                    WCF_Servicio.OperacionesServiciosClient proxy = new WCF_Servicio.OperacionesServiciosClient();
                    if (tmpServicio != null)
                    {
                        if (!tmpServicio.Guardar())
                        {
                            lblMsjServicio.ForeColor = System.Drawing.Color.Red;
                            lblMsjServicio.Text = "Ya existe un servicio con ese nombre para el rut ingresado";
                        }
                        else
                        {
                            lblMsjServicio.ForeColor = System.Drawing.Color.Green;
                            lblMsjServicio.Text = "El servicio fue dado de alta con exito";
                            WCF_Servicio.OperacionesServiciosClient proxyServ = new WCF_Servicio.OperacionesServiciosClient();
                            DTOServicio[] listSer = proxyServ.RetornarServiciosProveedor(auxRutProveedor);
                            grdServicios.DataSource = listSer;
                            grdServicios.DataBind();
                            limpiarCampos(Page.Controls);
                            Fachada.GuardarProvEnTxt(); //actualizo txt
                        }
                    }
                    else
                    {
                        lblMsjServicio.ForeColor = System.Drawing.Color.Red;
                        lblMsjServicio.Text = "El servicio no pudo ser creado";
                    }
                }
                else
                {
                    lblMsjServicio.ForeColor = System.Drawing.Color.Red;
                    lblMsjServicio.Text = "El servicio no pudo ser creado";
                }
            }
            else
            {
                lblErrorFoto.Visible = true;
                lblErrorFoto.Text = "(*) Debe ingresar una imagen para el servicio";
            }

        }
        protected void limpiarCampos(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox) ((TextBox)ctrl).Text = string.Empty;
                else if (ctrl is DropDownList) ((DropDownList)ctrl).ClearSelection();
                else if (ctrl is Calendar) ((Calendar)ctrl).SelectedDates.Clear();
                else if (ctrl is ListBox) ((ListBox)ctrl).ClearSelection();
                else if (ctrl is Image) ((Image)ctrl).ImageUrl = "";
                limpiarCampos(ctrl.Controls);
            }
        }
    }
   
}