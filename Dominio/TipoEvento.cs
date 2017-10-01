using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Dominio
{
    public class TipoEvento:Persistente<TipoEvento>
    {
        #region Atributos y Properties
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public List<TipoServicio> TipoServicios { get; set; }
        #endregion

        #region Constructores
        public TipoEvento(string unNombre, string unaDescripcion)
        {
            this.Nombre = unNombre;
            this.Descripcion = unaDescripcion;
            this.TipoServicios = new List<TipoServicio>();
        }

        public TipoEvento()
        {
            this.TipoServicios = new List<TipoServicio>();
        }

        #endregion

        #region Metodos

        public override bool Leer()
        {
            throw new NotImplementedException();
        }

        public override bool Guardar()
        {
            throw new NotImplementedException();
        }

        public override List<TipoEvento> TraerTodo()
        {
            List<TipoEvento> lstTmp = new List<TipoEvento>();
            CommandType cmdType = CommandType.StoredProcedure;
            string cmdText = "TiposEventos_SelectAll";
            SqlConnection conn = this.ObtenerConexion();
            SqlDataReader drResults = this.EjecutarReader(conn,cmdText,cmdType,null);
            while (drResults.Read())
            {
                TipoEvento tmpTipoEv = new TipoEvento()
                { Nombre = drResults["nombre"].ToString(),
                  Descripcion = drResults["descripcion"].ToString()
                };

                List<TipoServicio> auxListTipoServ = new List<TipoServicio>();
                SqlCommand auxCmd = new SqlCommand();
                string auxCmdText = "ServiciosTipoEventos_SelectByEvento";
                List<SqlParameter> parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@nombreTipoEvento", tmpTipoEv.Nombre));
                SqlConnection conn2 = this.ObtenerConexion();
                SqlDataReader servReader = this.EjecutarReader(conn2,auxCmdText,cmdType,parametros);

                while (servReader.Read())
                {
                    TipoServicio auxTipoServ = new TipoServicio()
                    {
                        Nombre = servReader["nombreTipoServicio"].ToString()
                    };
                    auxListTipoServ.Add(auxTipoServ);
                }
                servReader.Close();
                tmpTipoEv.TipoServicios = auxListTipoServ;
                lstTmp.Add(tmpTipoEv);
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
            throw new NotImplementedException();
        }
        #endregion
    }
}
    