using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Dominio
{
    public class Proveedor:Persistente<Proveedor>
    {
        #region Atributos y Properties
        public static double porcentajePorVipActual;

        public static double arancel;

        public string Rut { get; set; }

        public string NomFantasia { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public DateTime Fecha { get; set; }

        public bool Activo { get; set; }

        public bool Vip { get; set; }

        public static double PorcentajePorVipActual {
            get { return Proveedor.DevolverPorcentajeVipActual(); }
            set { Proveedor.porcentajePorVipActual = Proveedor.ModificarPorcentajeVip(value); }
        }

        private double porcentajePorVip;

        public double PorcentajePorVip { get { return porcentajePorVip; } }

        public static double Arancel
        {
            get { return Proveedor.DevolverArancelActual(); }
            set { Proveedor.arancel = Proveedor.ModificarArancel(value); }
        }

        public List<Servicio> ServiciosOfrecidos { get; set; }

        public Usuario Usuario { get; set; }
        
        #endregion

        #region Constructores
        public Proveedor(string unRut,string unNomFantasia,string unEmail,string unTelefono,DateTime unaFecha,bool esVip,Usuario unUsuario) {
            this.Rut = unRut;
            this.NomFantasia = unNomFantasia;
            this.Email = unEmail;
            this.Telefono = unTelefono;
            this.Fecha = unaFecha;
            this.Vip = esVip;
            this.Usuario = unUsuario;
            this.Activo = true;
            this.ServiciosOfrecidos = new List<Servicio>();
            if (this.Vip)
            {
                this.porcentajePorVip = Proveedor.PorcentajePorVipActual;
            }
            else {
                this.porcentajePorVip = 0;
            }
           
        }

        public Proveedor() //Creo un constructor sin parametros para crear un objeto temporal
        {
        }

        #endregion

        #region Otros Metodos
        public override string ToString()
        {
            return ("RUT: " + this.Rut);
        }

        public override bool Guardar()
        {
            SqlConnection conn = this.ObtenerConexion();
            string cmdText = "Proveedores_Insert";
            CommandType cmdType = CommandType.StoredProcedure;
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Rut", this.Rut));
            parametros.Add(new SqlParameter("@nomFantasia", this.NomFantasia));
            parametros.Add(new SqlParameter("@email", this.Email));
            parametros.Add(new SqlParameter("@telefono", this.Telefono));
            parametros.Add(new SqlParameter("@fecha", this.Fecha));
            parametros.Add(new SqlParameter("@vip", this.Vip));
            parametros.Add(new SqlParameter("@porcentajePorVip", this.PorcentajePorVip));
            parametros.Add(new SqlParameter("@usuario", this.Usuario.Nombre));
            return this.EjecutarNoQuery(conn, cmdText, cmdType, parametros)!=0;
           
        }

        public bool GuardarTrans(SqlConnection unaConn, SqlTransaction unaTransaccion)
        { //para guardar el Proveedor utilizando una transaccion

            SqlConnection conn = unaConn;
            string cmdText = "Proveedores_Insert";
            CommandType cmdType = CommandType.StoredProcedure;
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Rut", this.Rut));
            parametros.Add(new SqlParameter("@nomFantasia", this.NomFantasia));
            parametros.Add(new SqlParameter("@email", this.Email));
            parametros.Add(new SqlParameter("@telefono", this.Telefono));
            parametros.Add(new SqlParameter("@fecha", this.Fecha));
            parametros.Add(new SqlParameter("@vip", this.Vip));
            parametros.Add(new SqlParameter("@porcentajePorVip", this.PorcentajePorVip));
            parametros.Add(new SqlParameter("@usuario", this.Usuario.Nombre));
            return this.EjecutarNoQuery(conn, cmdText, cmdType, parametros, unaTransaccion) != 0;
        }

        public List<Servicio> DevolverServicios() {
            List<Servicio> retorno = new List<Servicio>();
            SqlConnection connection = this.ObtenerConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Servicios_SelectServiciosByRut";
            cmd.Parameters.Add(new SqlParameter("@rut", this.Rut));
            SqlDataReader drResults;
            connection.Open();
            drResults = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (drResults.Read()) {
                
                Servicio auxSer = new Servicio(){
                    Nombre = drResults["nombre"].ToString(),
                    Descripcion = drResults["descripcion"].ToString(),
                    TipoServicio = new TipoServicio() {
                        Nombre = drResults["tipoServicio"].ToString() } ,
                    Imagen = drResults["imagen"].ToString()
                };
                retorno.Add(auxSer);
            }
            drResults.Close();
            connection.Close();
            return retorno;
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
                parametros.Add(new SqlParameter("@rut", this.Rut));
                reader = this.EjecutarReader(conn, "Proveedores_SelectByRut", CommandType.StoredProcedure, parametros);

                if (reader.Read())
                {
                    this.NomFantasia = reader["nomFantasia"].ToString();
                    this.Email = reader["email"].ToString();
                    this.Telefono = reader["telefono"].ToString();
                    this.Vip = (bool)reader["vip"];
                    this.Fecha = (DateTime)reader["fecha"];
                    this.porcentajePorVip = Convert.ToDouble(reader["porcentajePorVip"].ToString());
                    this.Activo = (bool)reader["activo"];
                    Usuario aux = new Usuario() { Nombre = reader["nomUsuario"].ToString() };
                    aux.Leer();
                    this.Usuario = aux;
                    retorno = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open) conn.Close();
                if (reader != null) reader.Close();
            }

            return retorno;
        }

        public override List<Proveedor> TraerTodo()
        {
            List<Proveedor> lstTmp = new List<Proveedor>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.CommandText = "Proveedores_SelectAll";  
            SqlConnection conn = this.ObtenerConexion();
            SqlDataReader drResults;
            cmd.Connection = conn;
            conn.Open();
            drResults = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (drResults.Read())
            {
                Proveedor provTmp = new Proveedor();
                provTmp.Rut = drResults["rut"].ToString();
                provTmp.NomFantasia = drResults["nomFantasia"].ToString();
                provTmp.Email = drResults["email"].ToString();
                provTmp.Telefono = drResults["telefono"].ToString();
                provTmp.Fecha = (DateTime)drResults["fecha"];
                provTmp.Vip = (bool)drResults["vip"];
                provTmp.porcentajePorVip = Convert.ToDouble(drResults["porcentajePorVip"].ToString());
                provTmp.Activo = (bool)drResults["activo"];
                Usuario aux = new Usuario() { Nombre = drResults["nomUsuario"].ToString() };
                provTmp.Usuario = aux;
                lstTmp.Add(provTmp);
            }
            drResults.Close();
            conn.Close();
            return lstTmp;
        }

        public List<Proveedor> TraerActivos()
        {
            List<Proveedor> lstTmp = new List<Proveedor>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.CommandText = "Proveedores_SelectActiveOnly"; 
            SqlConnection conn = this.ObtenerConexion();
            SqlDataReader drResults;
            cmd.Connection = conn;
            conn.Open();
            drResults = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (drResults.Read())
            {
                Proveedor provTmp = new Proveedor();
                provTmp.Rut = drResults["rut"].ToString();
                provTmp.NomFantasia = drResults["nomFantasia"].ToString();
                provTmp.Email = drResults["email"].ToString();
                provTmp.Telefono = drResults["telefono"].ToString();
                provTmp.Fecha = (DateTime)drResults["fecha"];
                provTmp.Vip = (bool)drResults["vip"];
                provTmp.porcentajePorVip = Convert.ToDouble(drResults["porcentajePorVip"].ToString());
                provTmp.Activo = (bool)drResults["activo"];
                Usuario aux = new Usuario() { Nombre = drResults["nomUsuario"].ToString() };
                provTmp.Usuario = aux;
                lstTmp.Add(provTmp);
            }
            drResults.Close();
            conn.Close();
            return lstTmp;
        } 

        public override bool Modificar()
        {
            throw new NotImplementedException();
        }

        public override bool Eliminar()
        {
            bool retorno = false;
            SqlConnection conn = this.ObtenerConexion();
            string cmdText = "Proveedores_BajaByRut";
            CommandType cmdType = CommandType.StoredProcedure;
            List<SqlParameter> parametro = new List<SqlParameter>();
            parametro.Add(new SqlParameter("@rut", this.Rut));
            if (this.EjecutarNoQuery(conn, cmdText, cmdType, parametro) != 0) {
                this.Activo = false;
                retorno = true;
            }
            return retorno;
        }

        public static double DevolverArancelActual()
        {
            double retorno = -1;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Auxiliar_Devolver_Arancel"; 
            SqlConnection conn = new SqlConnection() { ConnectionString = Persistente.ConnString };
            SqlDataReader drResults;
            cmd.Connection = conn;
            conn.Open();
            drResults = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (drResults.Read())
            {
                retorno = Convert.ToDouble(drResults["porcentaje_Arancel"].ToString());
            }
            drResults.Close();
            conn.Close();
            return retorno;
        }

        public static double DevolverPorcentajeVipActual()
        {
            double retorno = -1;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;  
            cmd.CommandText = "Auxiliar_Devolver_PorcentajeVip"; 
            SqlConnection conn = new SqlConnection() { ConnectionString = Persistente.ConnString };
            SqlDataReader drResults;
            cmd.Connection = conn;
            conn.Open();
            drResults = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (drResults.Read())
            {
                retorno = Convert.ToDouble(drResults["porcentaje_Vip"].ToString());
            }
            drResults.Close();
            conn.Close();
            return retorno;
        }

        public static double ModificarArancel(double unArancel)
        {
            double retorno = -1;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection() { ConnectionString = Persistente.ConnString };
            cmd.CommandText = "Auxiliar_Arancel_Update"; 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.Add(new SqlParameter("@arancel", unArancel));
            conn.Open();
            if (cmd.ExecuteNonQuery() != 0 ){
                retorno = unArancel;
            }
            else
            {
                retorno = Proveedor.DevolverArancelActual();
            }
            conn.Close(); 
            return retorno;
        }

        public static double ModificarPorcentajeVip(double unPorcentaje)
        {
            double retorno = -1;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection() { ConnectionString = Persistente.ConnString };
            cmd.CommandText = "Auxiliar_PorcentajeVip_Update";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
            cmd.Parameters.Add(new SqlParameter("@porcentajeVip", unPorcentaje));
            conn.Open();
            if (cmd.ExecuteNonQuery() != 0)
            {
                retorno = unPorcentaje;
            }
            else
            {
                retorno = Proveedor.DevolverPorcentajeVipActual();
            }
            conn.Close();
            return retorno;
        }

        public void GuardarProveedoresEnTxt()
        {
            List<Proveedor> proveedores = TraerTodo();
            using (StreamWriter file =
            new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Proveedores.txt", false))//Propiedad Append=false para sobreescribir el archivo
            foreach(Proveedor p in proveedores)
                {
                    file.Write(p.Rut);
                    file.Write("#" + p.NomFantasia);
                    file.Write("#" + p.Email);
                    file.Write("#" + p.Telefono);
                    p.ServiciosOfrecidos = p.DevolverServicios(); //cargo la lista del proveedor con los servicios que el mismo oferce.
                    if (p.ServiciosOfrecidos.Count != 0) { file.Write(" | "); }
                    for (int i = 0; i < p.ServiciosOfrecidos.Count; i++)
                    {
                        if (i != p.ServiciosOfrecidos.Count-1)
                        {
                            file.Write(p.ServiciosOfrecidos[i].Nombre + ":");
                            file.Write(p.ServiciosOfrecidos[i].Descripcion + ":");
                            file.Write(p.ServiciosOfrecidos[i].Imagen + ":");
                        }
                        else // no agrego los : al final del ultimo servicio y agrego un salto de linea para comenzar con el siguinete proveedor
                        {
                            file.Write(p.ServiciosOfrecidos[i].Nombre + ":");
                            file.Write(p.ServiciosOfrecidos[i].Descripcion + ":");
                            file.Write(p.ServiciosOfrecidos[i].Imagen);
                            file.WriteLine("");
                        }                        
                    }
                }
        }
        #endregion
    }
}
