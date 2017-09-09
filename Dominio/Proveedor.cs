using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        #region Constructor
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

        #endregion
        #region Otros Metodos
        #endregion
    }
}
