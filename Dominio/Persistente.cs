using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Persistente
    {
        protected static string connString = @"Server =.\; DataBase = ObligatorioP3PrimerEntrega; User Id = sa; Password = Admin1234!";
        public static string ConnString
        {
            get { return connString; }
            set { connString = value; }
        }
    }
}
