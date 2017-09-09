using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Dominio
{
    public class Proveedor
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
        public int Guardar()
        {
            //System.Configuration.ConfigurationManager.ConnectionStrings["Nico_Connection"].ConnectionString;
            //string conString = @"Server =.\; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = Admin1234!"; //chequee nombre de servidor, Base de datos y usuario de Sqlserver 
            string conString = @"Server =.\; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = facundo23"; //chequee nombre de servidor, Base de datos y usuario de Sqlserver 
            SqlConnection connection = new SqlConnection(conString);
            int filasAfectadas = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;//asignar conexion al commando a ejecutar 
                    cmd.CommandText = "Proveedor_Insert";//Sentencia a ejecutar 
                    cmd.CommandType = CommandType.StoredProcedure;//Tipo de query // agregamos parametros 
                    cmd.Parameters.Add(new SqlParameter("@Rut", this.Rut));
                    cmd.Parameters.Add(new SqlParameter("@nomFantasia", this.NomFantasia));
                    cmd.Parameters.Add(new SqlParameter("@email", this.Email));
                    cmd.Parameters.Add(new SqlParameter("@telefono", this.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@fecha", this.Fecha));
                    cmd.Parameters.Add(new SqlParameter("@vip", this.Vip));
                    cmd.Parameters.Add(new SqlParameter("@porcentajePorVip", this.PorcentajePorVip));
                    cmd.Parameters.Add(new SqlParameter("@usuario", this.Usuario.Nombre));
                    connection.Open();//abrimos la conexion 
                    filasAfectadas = cmd.ExecuteNonQuery();//ejecutamos consulta 
                    
                }//fin using
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                connection.Close(); //cerramos conexion 
            }
            return filasAfectadas;
        }

        public static List<Proveedor> ListarProveedores()
        {
            List<Proveedor> lstTmp = new List<Proveedor>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure; //indico que voy a ejecutar un procedimiento almacenado en la bd 
            cmd.CommandText = "Proveedor_SelectAll"; //indico el nombre del procedimiento almacenado a ejecutar, en este caso LISTAR
            //string conString = @"Server =.\; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = Admin1234!"; //chequee nombre de servidor, Base de datos y usuario de Sqlserver 
            string sConnectionString = @"Server =.\; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = facundo23";
            SqlConnection conn = new SqlConnection(sConnectionString);
            SqlDataReader drResults; cmd.Connection = conn; conn.Open();
            drResults = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (drResults.Read())
            {
                Proveedor provTmp = new Proveedor();
                provTmp.Rut = drResults["rut"].ToString();
                provTmp.NomFantasia = drResults["nomFantasia"].ToString();
                provTmp.Email = drResults["email"].ToString();
                provTmp.Telefono = drResults["telefono"].ToString();
                provTmp.Fecha = (DateTime) drResults["fecha"];
                provTmp.Vip = (bool)drResults["vip"];
               // provTmp.PorcentajePorVip = (double) drResults["porcentajePorVip"];
                provTmp.Activo = (bool)drResults["activo"];
                //provTmp.Usuario.Nombre = drResults["nomUsuario"].ToString();
                lstTmp.Add(provTmp);
            }
            drResults.Close();
            conn.Close();
            return lstTmp;
        }

        public override string ToString()
        {
            return ("RUT: " + this.Rut) ;
        }
        #endregion
    }
}
