using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiciosObligatorioWCF;
using Dominio;


namespace InterfazWeb
{
    public partial class Catalogo_Servicios : System.Web.UI.Page
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
            if (!IsPostBack) {
                CargarTipoServicios();
            }
        }
        protected void CargarTipoServicios() //metodo que obtiene la lista de servicios para mostrar en el grid view
        {
            WCF_Servicio.OperacionesServiciosClient proxy = new WCF_Servicio.OperacionesServiciosClient();
            grdTipoServicios.DataSource = proxy.RetornarTipoServicios();
            grdTipoServicios.DataBind();
        }

        protected void grdTipoServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            WCF_Servicio.OperacionesServiciosClient proxy = new WCF_Servicio.OperacionesServiciosClient();
            DTOTipoEvento[] listTipoEventos = proxy.RetornarTipoEventos();
            List<DTOTipoEvento> aux = new List<DTOTipoEvento>();
            foreach (DTOTipoEvento tmpTipoEv in listTipoEventos) {
                foreach (TipoServicio tmpTipoSer in tmpTipoEv.TipoServicios) {
                    if (grdTipoServicios.SelectedRow.Cells[1].Text == tmpTipoSer.Nombre) {
                        aux.Add(tmpTipoEv);
                    }
                }
            }
            grdTipoEvento.DataSource = aux;
            grdTipoEvento.DataBind();
        }
    }
}