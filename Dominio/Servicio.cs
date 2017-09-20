using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Dominio
{
    public class Servicio:Persistente<Servicio>
    {
        #region Atributos y Proeperties
        public string RutProveedor { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public TipoServicio TipoServicio { get; set; }
        public string TipoServicioString { get { return TipoServicio.Nombre; } }
        #endregion

        #region Constructores
        public Servicio(string unNombre,string unaDescripcion,TipoServicio unTipoServicio) {
            this.Nombre = unNombre;
            this.Descripcion = unaDescripcion;
            this.Imagen = null;
            this.TipoServicio = unTipoServicio;
        }
        public Servicio() { }
        #endregion
        #region Otros Metodos
        public override string ToString()
        {
            return "-Nombre Servicio: "+Nombre +" | Tipo Servicio: "+TipoServicio.Nombre;
        }
        //public void GuardarServiciosEnTxt() { }
        //public void CargarServiciosDesdeTxt() { }
        public override List<Servicio> TraerTodo()
        {
            List<Servicio> lstTmp = new List<Servicio>();
            SqlCommand cmd = new SqlCommand();
            
            CommandType cmdType = CommandType.StoredProcedure; //indico que voy a ejecutar un procedimiento almacenado en la bd 
            string cmdText = "Servicios_SelectAll"; //indico el nombre del procedimiento almacenado a ejecutar, en este caso LISTAR
            SqlConnection conn = this.ObtenerConexion();
            SqlDataReader drResults = this.EjecutarReader(conn,cmdText,cmdType,null); 
            
            while (drResults.Read())
            {
                Servicio tmpServ = new Servicio();
                tmpServ.RutProveedor = drResults["rutProveedor"].ToString();
                tmpServ.Nombre = drResults["nombre"].ToString();
                tmpServ.Descripcion = drResults["descripcion"].ToString();
                tmpServ.Imagen = drResults["imagen"].ToString();
                TipoServicio tipoSer = new TipoServicio() { Nombre = drResults["tipoServicio"].ToString() };
                tmpServ.TipoServicio = tipoSer;
                lstTmp.Add(tmpServ);
            }
            drResults.Close();
            return lstTmp;
        }

        public override bool Leer()
        {
            throw new NotImplementedException();
        }

        public override bool Guardar()
        {
            SqlConnection conn = this.ObtenerConexion();
            string cmdText = "Servicios_Insert";
            CommandType cmdType = CommandType.StoredProcedure;
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@rutProveedor", this.RutProveedor));
            parametros.Add(new SqlParameter("@nombre", this.Nombre));
            parametros.Add(new SqlParameter("@descripcion", this.Descripcion));
            parametros.Add(new SqlParameter("@imagen", this.Imagen));
            parametros.Add(new SqlParameter("@tipoServicio", this.TipoServicio.Nombre));
            return this.EjecutarNoQuery(conn, cmdText, cmdType, parametros) != 0;
        }

        public override bool Modificar()
        {
            throw new NotImplementedException();
        }

        public override bool Eliminar()
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
