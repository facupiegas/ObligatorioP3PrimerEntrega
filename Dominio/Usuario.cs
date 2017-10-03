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
        public string Sal { get; set; }
        #endregion

        #region Constructores
        public Usuario(string unNombre, string unaPass,EnumRol unRol)
        {
            this.Sal = GenerarSal();
            string hash = GenerarSHA256Hash(unaPass, this.Sal);
            this.Nombre = unNombre;
            this.Pass = hash;
            this.Rol = unRol;
        }
        public Usuario() { }
        #endregion

        #region Otros metodos
        public static String GenerarSal()
        {
            var random = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var bytes = new byte[6];
            random.GetBytes(bytes);
            return Convert.ToBase64String(bytes);

        }
        public static String GenerarSHA256Hash(String pass, String sal)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(pass + sal);
            System.Security.Cryptography.SHA256Managed sha256hashstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public EnumRol ObtenerTipo()
        {
            return this.Rol;
        }     

        public override bool Guardar()
        {
            
            SqlConnection connection = this.ObtenerConexion();
            string cmdText = "Usuarios_Insert";
            CommandType cmdType = CommandType.StoredProcedure;
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nombre", this.Nombre));
            parametros.Add(new SqlParameter("@pass", this.Pass));
            parametros.Add(new SqlParameter("@rol", this.Rol.ToString()));
            parametros.Add(new SqlParameter("@sal", this.Sal));
            return this.EjecutarNoQuery(connection, cmdText, cmdType,parametros)!=0;
        }

        public bool GuardarTrans(SqlConnection unaConn, SqlTransaction unaTransaccion) {
            SqlConnection connection = unaConn;
            string cmdText = "Usuarios_Insert";
            CommandType cmdType = CommandType.StoredProcedure;
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@nombre", this.Nombre));
            parametros.Add(new SqlParameter("@pass", this.Pass));
            parametros.Add(new SqlParameter("@rol", this.Rol.ToString()));
            return this.EjecutarNoQuery(connection, cmdText, cmdType, parametros, unaTransaccion) != 0;
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
                    this.Sal = reader["sal"].ToString();
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
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.CommandText = "Usuarios_SelectAll";
            SqlConnection connection = this.ObtenerConexion();
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
                tmpUsuario.Sal = drResults["sal"].ToString();
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
