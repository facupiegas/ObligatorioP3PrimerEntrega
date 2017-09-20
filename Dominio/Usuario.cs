using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Dominio
{
    public class Usuario:Persistente<Usuario>
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
        public EnumRol ObtenerTipo()
        {
            return this.Rol;
        }
        
        

        

        public override bool Guardar()
        {
            
            SqlConnection connection = this.ObtenerConexion();
            string cmdText = "Usuarios_Insert";
            CommandType cmdType = CommandType.StoredProcedure;//Tipo de query // agregamos parametros 
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nombre", this.Nombre));
            parametros.Add(new SqlParameter("@pass", this.Pass));
            parametros.Add(new SqlParameter("@rol", this.Rol.ToString()));
            return this.EjecutarNoQuery(connection, cmdText, cmdType,parametros)!=0;
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
                parametros.Add(new SqlParameter("@nombre", this.Nombre));
                reader = this.EjecutarReader(conn, "Usuarios_SelectByNombre", CommandType.StoredProcedure, parametros);

                if (reader.Read())
                {
                    this.Nombre = reader["nombre"].ToString();
                    this.Pass = reader["pass"].ToString();
                    switch (reader["rol"].ToString()) {
                        case "Administrador":
                            this.Rol = EnumRol.Administrador;
                            break;
                        case "Proveedor":
                            this.Rol = EnumRol.Proveedor;
                            break;
                        case "Organizador":
                            this.Rol = EnumRol.Organizador;
                            break;
                    }
                    retorno = true;
                }
            }
            catch(Exception ex)
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

        public override List<Usuario> TraerTodo()
        {
            List<Usuario> ListaAux = new List<Usuario>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure; //indico que voy a ejecutar un procedimiento almacenado en la bd 
            cmd.CommandText = "Usuarios_SelectAll"; //indico el nombre del procedimiento almacenado a ejecutar
            //string sConnectionString = @"Server =.\SQLEXPRESS; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = Admin1234!"; //chequee nombre de servidor, Base de datos y usuario de Sqlserver 
            string sConnectionString = @"Server =.\; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = Admin1234!"; //chequee nombre de servidor, Base de datos y usuario de Sqlserver 
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
