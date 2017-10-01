﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Dominio
{
    public class TipoServicio:Persistente<TipoServicio>
    {
        #region Atributos y properties
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        #endregion

        #region Constructores
        public TipoServicio(string unNombre, string unaDescripcion) {
            this.Nombre = unNombre;
            this.Descripcion = unaDescripcion;
            
        }
        public TipoServicio() {
            
        }
        #endregion

        #region Otros Metodos
        public override string ToString()
        {
            return "Tipo: "+this.Nombre+" | Descripcion: "+this.Descripcion;
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
                parametros.Add(new SqlParameter(@"nombre", this.Nombre));
                reader = this.EjecutarReader(conn, "TipoServicios_SelectByNombre", CommandType.StoredProcedure, parametros);
                if (reader.Read())
                {
                    this.Descripcion = reader["descripcion"].ToString();
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

        public override bool Guardar()
        {
            throw new NotImplementedException();
        }

        public override List<TipoServicio> TraerTodo()
        {
            List<TipoServicio> lstTmp = new List<TipoServicio>();
            SqlCommand cmd = new SqlCommand();
            CommandType cmdType = CommandType.StoredProcedure; 
            string cmdText = "TipoServicios_SelectAll"; 
            SqlConnection conn = this.ObtenerConexion();
            SqlDataReader drResults = this.EjecutarReader(conn, cmdText, cmdType, null);
            while (drResults.Read())
            {
                TipoServicio tipoSer = new TipoServicio() { Nombre = drResults["nombre"].ToString(), Descripcion=drResults["descripcion"].ToString()};
                lstTmp.Add(tipoSer);
            }
            drResults.Close();
            return lstTmp;
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
