using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;

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

        //public void GuardarServiciosEnTxt(){
        //    TipoEvento tmpTipoEv = new TipoEvento();
        //    List<TipoEvento> tmpListTipoEv =  tmpTipoEv.TraerTodo();//recupero la lista de todos los TipoEventos desde BD
        //    StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "serviciosConEventos.txt", false);//Propiedad Append=false para sobreescribir el archivo
        //    string linea = "";
        //    List<Servicio> listServ = this.TraerTodo();//recupero la lista de todos los Servicio desde BD
        //    foreach (Servicio tmpServ in listServ) //por cada Servicio
        //    {
        //        linea += tmpServ.Nombre + "#"; //guardo el nombre del Servicio en la linea a escribir en el archivo .txt
        //        foreach (TipoEvento auxTipoEv in tmpListTipoEv)
        //        {
        //            foreach (TipoServicio tmpTipoServ in auxTipoEv.TipoServicios)//recorro la lista de TipoServicio de cada TipoEvento(es la lista de los TipoServicio adecuados para dicho TipoEvento)
        //            {
        //                if(tmpTipoServ.Nombre == tmpServ.TipoServicio.Nombre)//si el Nombre del TipoServicio actual == al Nombre del TipoServicio del Servicio seleccionado en el momento(primer foreach)
        //                    linea += auxTipoEv.Nombre + ":"; //guardo en la variable a escribir en el archivo .txt
        //            }             
                    
        //        }
        //        writer.WriteLine(linea); //escribo la variable en el archivo.txt
        //        linea = ""; //devuelvo la variable a su estado original para el proximo Servicio
        //    }
        //    writer.Close();
        //}

        public override List<Servicio> TraerTodo()
        {
            List<Servicio> lstTmp = new List<Servicio>();      
            CommandType cmdType = CommandType.StoredProcedure; 
            string cmdText = "Servicios_SelectAll"; 
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
            bool retorno = false;
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                conn = this.ObtenerConexion();
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@rutProveedor", this.RutProveedor));
                parametros.Add(new SqlParameter(@"nombre", this.Nombre));
                reader = this.EjecutarReader(conn, "Servicios_SelectByRutAndNombre", CommandType.StoredProcedure, parametros);
                if (reader.Read())
                {
                    this.Descripcion = reader["descripcion"].ToString();
                    this.Imagen = reader["imagen"].ToString();
                    TipoServicio tmpTipoSer = new TipoServicio() { Nombre = reader["tipoServicio"].ToString() };
                    tmpTipoSer.Leer();
                    this.TipoServicio = tmpTipoSer;
                    retorno = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally {
                if (conn != null && conn.State == ConnectionState.Open) conn.Close();
                if (reader != null) reader.Close();
            }
            return retorno;
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

        public bool GuardarTrans(SqlConnection unaConn, SqlTransaction unaTransaccion) { //para guardar el Servicio utilizando una transaccion
            SqlConnection conn = unaConn;
            string cmdText = "Servicios_Insert";
            CommandType cmdType = CommandType.StoredProcedure;
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@rutProveedor", this.RutProveedor));
            parametros.Add(new SqlParameter("@nombre", this.Nombre));
            parametros.Add(new SqlParameter("@descripcion", this.Descripcion));
            parametros.Add(new SqlParameter("@imagen", this.Imagen));
            parametros.Add(new SqlParameter("@tipoServicio", this.TipoServicio.Nombre));
            return this.EjecutarNoQuery(conn, cmdText, cmdType, parametros,unaTransaccion) != 0;
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
