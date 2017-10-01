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
            ddlTipoServicios.DataSource = Fachada.DevolverTipoServicios();//Recupero la lista de TipoServicios de la BD y la cargo en el dropdownlist
            ddlTipoServicios.DataValueField = "Nombre";
            ddlTipoServicios.DataBind();
        }

        protected void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            auxRutProveedor = txtRut.Text;
            if (stringEsSoloNumeros(auxRutProveedor))//Si se ingresa un rut con el formato correcto
            {
                WCF_Proveedor.OperacionesProveedoresClient proxyProv = new WCF_Proveedor.OperacionesProveedoresClient();
                DTOProveedor dtoProv = proxyProv.RetornarProveedorPorRut(auxRutProveedor); //Recupero los datos del Proveedor con el rut ingresado por parametro y lo guardo en un objeto DTOProveedor
                if (dtoProv != null)//si se encontro un Proveedor con el rut ingresado
                {
                    if (dtoProv.Activo)//si el Proveedor se encuentra activo
                    {
                        WCF_Servicio.OperacionesServiciosClient proxyServ = new WCF_Servicio.OperacionesServiciosClient();
                        DTOServicio[] listSer = proxyServ.RetornarServiciosProveedor(auxRutProveedor); //Recupero los Servicios del Proveedor
                        lblMsjProveedor.Text = string.Empty;
                        pnlNuevoServicio.Visible = true; //muestro el panel para agregar un nuevo Servicio
                        grdServicios.DataSource = listSer; //cargo el gridview con los servicios actuales del Proveedor
                        grdServicios.DataBind();
                    }
                    else
                    {
                        lblMsjProveedor.ForeColor = System.Drawing.Color.Red;
                        lblMsjProveedor.Text = "El Rut del proveedor ingresado no se encuentra activo.";
                    }
                }
                else
                {
                    lblMsjProveedor.ForeColor = System.Drawing.Color.Red;
                    lblMsjProveedor.Text = "No existe un Proveedor con el Rut ingresado.";
                }
               

            }
            else
            {
                lblMsjProveedor.ForeColor = System.Drawing.Color.Red;
                lblMsjProveedor.Text = "El campo solo admite numeros";
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
        private bool extensionArchivoOK(string nombreArchivo) //metodo que valida que la imagen ingreasa posea una extension admitida
        {
            bool ok = false;
            string extension =
                    System.IO.Path.GetExtension(nombreArchivo).ToLower();//obtengo la extension del archivo subido y lo guardo en una variable para su posterior validacion
            string[] extensionesPermitidas =
                {".gif", ".png", ".jpeg", ".jpg"}; //string que contiene las extensiones admitidas
            for (int i = 0; i < extensionesPermitidas.Length; i++)
            {
                if (extension == extensionesPermitidas[i]) //comparo la extension del archivo subido con las admitidas que se encuentras dentro del string
                {
                    ok = true; //en caso que el archivo posea una extension admitida retorno true, caso contrario false como fue definido al comienzo del metodo
                }
            }
            return ok;
        }

        protected void btnUpload_Click(object sender, ImageClickEventArgs e) //si la extension del archivo ingresado es admitida, se procede a guardar el mismo
        {
            if (extensionArchivoOK(fupImagenServicio.FileName))
            {
                string path = Server.MapPath("img");
                string archivo = path + "\\" + fupImagenServicio.FileName;
                fupImagenServicio.SaveAs(archivo);
                imgImagenServicio.ImageUrl = "img\\" + fupImagenServicio.FileName;
                lblErrorFoto.Visible = false;
            }
            else //caso contrario se le da aviso al usuario
            {
                lblErrorFoto.Visible = true;
                lblErrorFoto.Text = "Formato de imagen admitido .gif/.png/.jpeg/.jpg";
                imgImagenServicio.ImageUrl = null;
            }
        }

        protected void btnServicio_Click(object sender, EventArgs e)
        {
            if (imgImagenServicio.ImageUrl != "") //Si el usuario subio una imagen
            {
                //Guardo la informacion ingresada en los TextBox
                string nombreSer = txtNombreServicio.Text;
                string descripcionSer = txtDescripcionServicio.Text;
                string imagenSer = imgImagenServicio.ImageUrl;
                string stringTipo = ddlTipoServicios.SelectedItem.Value;
                string rutProveedor = auxRutProveedor;
                List<TipoServicio> listTipoServicio = Fachada.DevolverTipoServicios(); //Recupero la lista de TipoServicio de la BD
                TipoServicio aux = null;
                foreach (TipoServicio tmpTipo in listTipoServicio)
                {
                    if (tmpTipo.Nombre == stringTipo) //si el nombre seleccionado en el dropdownlist coincide con un nombre de la lista de los TipoServicio
                    {
                        aux = tmpTipo; //guardo el objeto TipoServicio
                    }
                }
                if (aux != null)
                {
                    Servicio tmpServicio = Fachada.AltaServicio(rutProveedor, nombreSer, imagenSer, descripcionSer, aux); //doy de alta el Servicio en memoria
                    WCF_Servicio.OperacionesServiciosClient proxy = new WCF_Servicio.OperacionesServiciosClient();
                    if (tmpServicio != null)
                    {
                        if (!tmpServicio.Guardar())//Guardo el Servicio en la BD, si ya existe un servicio con el nombre ingresado(para el rut ingresado) devuelvo error
                        {
                            lblMsjServicio.ForeColor = System.Drawing.Color.Red;
                            lblMsjServicio.Text = "Ya existe un servicio con ese nombre para el rut ingresado";
                        }
                        else
                        {
                            lblMsjServicio.ForeColor = System.Drawing.Color.Green;
                            lblMsjServicio.Text = "El servicio fue dado de alta con exito";
                            WCF_Servicio.OperacionesServiciosClient proxyServ = new WCF_Servicio.OperacionesServiciosClient();
                            //actualizo el gridview con los Servicios del Proveedor
                            DTOServicio[] listSer = proxyServ.RetornarServiciosProveedor(auxRutProveedor);
                            grdServicios.DataSource = listSer;
                            grdServicios.DataBind();
                            limpiarCampos(Page.Controls);//limpio los campos
                            //actualizo archivos .txt
                            Fachada.GuardarProvEnTxt(); 
                            Fachada.GuardarServiciosEnTxt();
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
        protected void limpiarCampos(ControlCollection ctrls) //metodo que se encarga de limpiar los campos de la pagina
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