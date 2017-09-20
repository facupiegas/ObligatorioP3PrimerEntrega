using System;
using System.Collections.Generic;
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
        public string Rut { get; set; }
        public string NomFantasia { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime Fecha { get; set; }
        public bool Activo { get; set; }
        public bool Vip { get; set; }
        public double PorcentajePorVip { get; set; }
        public static double Arancel { get; set; }
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
                this.PorcentajePorVip = 50;
            }
            else {
                this.PorcentajePorVip = 0;
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
            string cmdText = "Proveedores_Insert";//Sentencia a ejecutar 
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

            //System.Configuration.ConfigurationManager.ConnectionStrings["Nico_Connection"].ConnectionString;
            //string conString = @"Server =SQLEXPRESS; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = Admin1234!"; //chequee nombre de servidor, Base de datos y usuario de Sqlserver 
            //string conString = @"Server =.\; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = Admin1234!"; //chequee nombre de servidor, Base de datos y usuario de Sqlserver 

           
        }

       

        public List<Servicio> DevolverServicios() {
            List<Servicio> retorno = new List<Servicio>();
            SqlConnection connection = this.ObtenerConexion();

            //string connectionString = @"Server =.\SQLEXPRESS; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = Admin1234!"; //chequee nombre de servidor, Base de datos y usuario de Sqlserver 
            //string connectionString = @"Server =.\; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = Admin1234!"; //chequee nombre de servidor, Base de datos y usuario de Sqlserver
            //connection.ConnectionString = connectionString;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Servicios_SelectServiciosByRut";
            cmd.Parameters.Add(new SqlParameter("@rut", this.Rut));
            SqlDataReader drResults;
            connection.Open();
            drResults = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (drResults.Read()) {
                //string aux = drResults["nombre"].ToString() + " | " + drResults["descripcion"].ToString()+" | "+drResults["tipoServicio"].ToString();
                Servicio auxSer = new Servicio(){Nombre = drResults["nombre"].ToString(), Descripcion= drResults["descripcion"].ToString(),TipoServicio = new TipoServicio() {Nombre= drResults["tipoServicio"].ToString() } };
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
                    this.PorcentajePorVip = Convert.ToDouble(reader["porcentajePorVip"].ToString());
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
            cmd.CommandType = CommandType.StoredProcedure; //indico que voy a ejecutar un procedimiento almacenado en la bd 
            cmd.CommandText = "Proveedores_SelectAll"; //indico el nombre del procedimiento almacenado a ejecutar, en este caso LISTAR
            //string sConnectionString = @"Server =.\SQLEXPRESS; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = Admin1234!"; //chequee nombre de servidor, Base de datos y usuario de Sqlserver 
            //string sConnectionString = @"Server =.\; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = Admin1234!"; //chequee nombre de servidor, Base de datos y usuario de Sqlserver 
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
                provTmp.PorcentajePorVip = Convert.ToDouble(drResults["porcentajePorVip"].ToString());
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
        #endregion
    }
}
