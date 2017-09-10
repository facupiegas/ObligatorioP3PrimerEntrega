using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Dominio
{
    public class Usuario
    {
        #region Atributos y Properties

        public enum EnumRol { Administrador, Proveedor, Organizador }
        public string Nombre { set; get; }
        public string Pass { set; get; }
        public EnumRol Rol { set; get; }
        #endregion

        #region Constructores
        public Usuario(string unNombre, string unaPass,EnumRol unRol)
        {
            
            this.Nombre = unNombre;
            this.Pass = unaPass;
            this.Rol = unRol;
        }
        public Usuario() { }
        #endregion
        #region Otros metodos
        public EnumRol obtenerTipo()
        {
            return this.Rol;
        }
        
        public static List<Usuario> DevolverUsuarios()
        {
            List<Usuario> ListaAux = new List<Usuario>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure; //indico que voy a ejecutar un procedimiento almacenado en la bd 
            cmd.CommandText = "Usuarios_SelectAll"; //indico el nombre del procedimiento almacenado a ejecutar
            //string sConnectionString = @"Server =.\; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = Admin1234!"; //chequee nombre de servidor, Base de datos y usuario de Sqlserver 
            string sConnectionString = @"Server =.\; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = facundo23";
            SqlConnection connection = new SqlConnection(sConnectionString);
            SqlDataReader drResults;
            cmd.Connection = connection;
            connection.Open();
            drResults = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (drResults.Read())
            {
                EnumRol rol;
                if (drResults["rol"].ToString() == "Administrador")
                    rol = EnumRol.Administrador;
                else if (drResults["rol"].ToString() == "Proveedor")
                    rol = EnumRol.Proveedor;
                else 
                    rol = EnumRol.Organizador;

                Usuario tmpUsuario = new Usuario(drResults["nombre"].ToString(), drResults["pass"].ToString(), rol);
                ListaAux.Add(tmpUsuario);
            }
            drResults.Close();
            connection.Close();
            return ListaAux;
        }
        public static Usuario ValidarUsuario(string unNombre, string unPass) {
            Usuario retorno = null;
            List<Usuario> listaUsuarios = Usuario.DevolverUsuarios();
            int aux = 0;
            bool encontre = false;
            while (aux < listaUsuarios.Count && !encontre) {
                if (listaUsuarios[aux].Nombre == unNombre && listaUsuarios[aux].Pass == unPass) {
                    encontre = true;
                    retorno = listaUsuarios[aux];
                }
                aux++;
            }
            return retorno;

        }
        public bool ValidarUsuarioInstancia() {
            bool retorno = false;

            //string connectionString = @"Server =.\; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = Admin1234!"; //chequee nombre de servidor, Base de datos y usuario de Sqlserver 
            string connectionString = @"Server =.\; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = facundo23";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure; //indico que voy a ejecutar un procedimiento almacenado en la bd 
            cmd.CommandText = "Usuarios_SelectByNombre"; //indico el nombre del procedimiento almacenado a ejecutar
            cmd.Connection = connection;
            cmd.Parameters.Add(new SqlParameter("@nombre", this.Nombre));
            connection.Open();
            SqlDataReader drResults;
            drResults = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (drResults.Read())
            {
                if (this.Nombre == drResults["nombre"].ToString() && this.Pass == drResults["pass"].ToString())
                {
                    retorno = true;
                } 
            }
            drResults.Close();
            connection.Close();

            return retorno;
        }
       

        #endregion
    }
}
