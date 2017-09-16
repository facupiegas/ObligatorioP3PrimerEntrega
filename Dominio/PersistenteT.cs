using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Dominio
{
    public abstract class Persistente<T>:Persistente
    {
        public abstract bool Leer();
        public abstract bool Guardar();
        public abstract List<T> TraerTodo();
        public abstract bool Modificar();
        public abstract bool Eliminar();
        public SqlConnection ObtenerConexion() {
            return (string.IsNullOrEmpty(ConnString) ? null : new SqlConnection(ConnString));
        }

        

        public int EjecutarNoQuery(SqlConnection conn, string cmdText, CommandType cmdType, List<SqlParameter> parametros, SqlTransaction trans)
        {
            int filasAfectadas = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                if (trans != null)
                {
                    cmd.Transaction = trans;
                }
                else
                {
                    if (conn != null && conn.State != ConnectionState.Open)
                        conn.Open();
                }
                cmd.CommandType = cmdType;
                if (parametros != null)
                    cmd.Parameters.AddRange(parametros.ToArray());
                filasAfectadas = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return filasAfectadas;
        }
        public int EjecutarNoQuery(SqlConnection conn, string cmdText, CommandType cmdType, List<SqlParameter> parametros)
        {
            return this.EjecutarNoQuery(conn, cmdText, cmdType, parametros, null);
        }
        public SqlDataReader EjecutarReader(SqlConnection conn, string cmdText, CommandType cmdType, List<SqlParameter> parametros) {
            SqlDataReader reader = null;
            try
            {
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = cmdType;
                if(parametros != null)
                    cmd.Parameters.AddRange(parametros.ToArray());
                if (conn != null && conn.State != ConnectionState.Open)
                    conn.Open();
                reader = cmd.ExecuteReader();
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message.ToString());
            }

            return reader;     
        }
    }
}
