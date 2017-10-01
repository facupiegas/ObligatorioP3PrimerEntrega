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
            if ((string)Session["TipoDeUsuario"] == "Administrador") //Si el usuario se logueo como administrador, no muestro el menu de navegacion para un usuario no regustrado
            {
                lnkMenuPrincipal.Visible = false;
            }
            else {
                lnkMenuPrincipal.Visible = true;
            }
            if (!IsPostBack)
                CargarTipoServicios(); //cargo el drop down list con los servicios disponibles para que el proveedor/administrador realice el alta
        }

        protected void CargarTipoServicios() {
            ddlTipoServicios.DataSource = Fachada.DevolverTipoServicios(); //se obtienen los servicios dosponible desde la fachada y se carga la lista
            ddlTipoServicios.DataValueField = "Nombre";
            ddlTipoServicios.DataBind();
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            WCF_Proveedor.OperacionesProveedoresClient proxyVendor = new WCF_Proveedor.OperacionesProveedoresClient();
            //se convierten los datos obtenidos de las cajas de texto validados
            string unNombreUsuario = Convert.ToString(txtNomUsuario.Text);
            string unaContrasena = Convert.ToString(txtPass.Text);
            string unRut = Convert.ToString(txtRut.Text);
            string unNomFantasiaProov = Convert.ToString(txtNomFantasia.Text);
            string unMailProov = Convert.ToString(txtEmail.Text);
            string unTelProov = txtTel.Text;
            bool esVip = false;
            if (radVipN.Checked || radVipS.Checked) //se define si el usuario sera VIP o no
            {
                if (radVipS.Checked)
                {
                    esVip = true;
                }
                
                Usuario tmpUser = Fachada.AltaUsuario(unNombreUsuario, unaContrasena, Usuario.EnumRol.Proveedor); //se crea un usuario temporal el cual será atribuído al proveedor
                if (stringEsSoloNumeros(unRut) && stringEsSoloNumeros(unTelProov)) //se valida que los campos RUT y Telefono sean numericos
                {

                    Proveedor tmpProv = Fachada.AltaProveedor(unRut, unNomFantasiaProov, unMailProov, unTelProov, DateTime.Now.Date, esVip, tmpUser); //se crea el objeto proveedor el cual será dado de alta en la base 
                    if (tmpUser!=null && tmpProv!=null) //se valida qye tanto el usuario como el proveedor no sean nulos (hayan sido creados existosamente)
                    {
                        lblMensaje.ForeColor = System.Drawing.Color.Green;
                        lblMensaje.Visible = true;
                        lblMensaje.Text = "Proveedor Creado con Éxito";
                        AltaProvRutProveedor = unRut; //se guarda temporalmente el rut del proveedor en la variable global
                        AltaUsu = tmpUser; //se guarda temporalmente el objeto usuario en la variable global
                        AltaProv = tmpProv; //se guarda temporalmente el objeto proveedor en la variable global
                        pnlServicio.Visible = true;
                        pnlProveedor.Visible = false;
                    }
                    else if (tmpUser==null) //si el usuario no pudo ser creado, se da aviso la usuario
                    {
                        lblMensaje.Text = "(*) El nombre de usuario ingresado ya ha sido utilizado";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                        lblMensaje.Visible = true;
                    }
                    else if (tmpProv==null) //si el proveedor no pudo ser creado, se da aviso la usuario
                    {
                        lblMensaje.Text = "(*) El proveedor ya existe en el sistema (RUT)";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                        lblMensaje.Visible = true;
                    }
                }
                else //si tanto el RUT o el telefono ingesado noes numerico, se da aviso al usuario
                {
                    lblMensaje.Text = "(*) Rut y/o telefono inválido";
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Visible = true;
                }
            }
            else //si no se especifico el tipo de proveedor se da aviso al usuario 
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

        protected void btnServicio_Click(object sender, EventArgs e) //metodo que valida la imagen ingresada para el servicio
        {
            if (imgImagenServicio.ImageUrl != "") //se valida que se haya ingresado una imagen ya que la unica forma de que contenga un string de url es si fue validado previamemnte 
            {//obtengo los atributos del objeto a crear
                string nombreSer = txtNombreServicio.Text;
                string descripcionSer = txtDescripcionServicio.Text;
                string imagenSer = imgImagenServicio.ImageUrl;
                string stringTipo = ddlTipoServicios.SelectedItem.Value;
                string rutProveedor = AltaProv.Rut;
                List<TipoServicio> listTipoServicio = Fachada.DevolverTipoServicios(); //obtengo la lista de los tipos de servicios para luego crear uno idenatico al que el usuario selecciono de la lista
                TipoServicio aux = null; //creo un objeto temporal para luego realizar la alta.
                foreach (TipoServicio tmpTipo in listTipoServicio)
                {
                    if (tmpTipo.Nombre == stringTipo) //se compara el nombre de los objetos comprendidos en ls lista con el que selecciono el usuario de la lista del web form y si son iguales, se sobreescribe el objeto aux con el de la lista
                    {
                        aux = tmpTipo; //una vez que se han validado los campos, se crea el objeto temporal
                    }
                }
                if (aux != null) //se controla que el objeto temporal tipoServicio no sea nulo para continuar con el alta
                {
                    Servicio tmpServicio = Fachada.AltaServicio(rutProveedor, nombreSer, imagenSer, descripcionSer, aux); //se crea el objeto temporal tipo servicio
                    WCF_Servicio.OperacionesServiciosClient proxy = new WCF_Servicio.OperacionesServiciosClient(); //se crea un proxy para solicitar la alta del objeto en la BD
                    if (tmpServicio != null) //se valida que el objeto tipo servicio haya podido ser creado.
                    {
                        if(proxy.AltaProveedor(AltaProv, AltaUsu, tmpServicio)) //se le envian los objetos al proxy para que realice el alta
                        { 
                            lblMsjServicio.ForeColor = System.Drawing.Color.Green;
                            lblMsjServicio.Text = "El proveedor,usuario y servicio fueron creados con exito!"; //si el alta se pudo realizar con exito se da aviso al usuario
                            imgImagenServicio.ImageUrl = null;
                            limpiarCampos(Page.Controls);
                            pnlServicio.Visible = false;
                            pnlProveedor.Visible = true;
                        }
                        else //si el alta no es posible se le solicita al usuario revisar los datos
                        { 
                            lblMsjServicio.ForeColor = System.Drawing.Color.Red;
                            lblMsjServicio.Text = "Algunos de los datos fueron erroreos, el proveedor,usuario y servicio no fueron dados de alta";
                        }
                    }
                    else //si el tipo servicio no pudo ser cerado se le da aviso al usuario
                    {
                        lblMsjServicio.ForeColor = System.Drawing.Color.Red;
                        lblMsjServicio.Text = "El servicio no pudo ser creado";
                    }
                }
                else //si el objeto tip servicio no se dio de alta correctemnte se da aviso y se interrumpe el alta
                {
                    lblMsjServicio.ForeColor = System.Drawing.Color.Red;
                    lblMsjServicio.Text = "El servicio no pudo ser creado";
                }
            }
            else //si el usuario no ingreso ninguna imagen se le da aviso y se interrumpe el alta
            {
                lblErrorFoto.Visible = true;
                lblErrorFoto.Text = "(*) Debe ingresar una imagen para el servicio";
            }
            
        }

        private bool extensionArchivoOK(string nombreArchivo) //metodo que valida que la imagen ingreasa posea una extension admitida
        {
            bool ok = false;
            string fileExtension =
                    System.IO.Path.GetExtension(nombreArchivo).ToLower();//obtengo la extension del archivo subido y lo guardo en una variable para su posterior validacion
            string[] allowedExtensions =
                {".gif", ".png", ".jpeg", ".jpg"}; //string que contiene las extensiones admitidas
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (fileExtension == allowedExtensions[i]) //comparo la extension del archivo subido con las amitidas que se encuentras dentro del string
                {
                    ok = true; //en casoq ue el archivo posea una extension admitida retorno true, caso contrario false como fue definido al comienzo del metodo
                }
            }
            return ok;
        }//verifica extension de la imagen

        protected void btnUpload_Click(object sender, ImageClickEventArgs e) 
        {
            if (extensionArchivoOK(fupImagenServicio.FileName)) //si la extension del archico ingresado es admitida, se procede a guardar el mismo
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

        protected void limpiarCampos(ControlCollection ctrls) //metodo que se encarga de limpiar los campos de la pagina para relaizar una nueva alta de proveedor
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