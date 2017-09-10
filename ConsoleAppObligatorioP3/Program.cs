using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace ConsoleAppObligatorioP3
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine("aca estamo en la consola");
            Usuario us = new Usuario("prov","123",Usuario.EnumRol.Proveedor);
            DateTime fecha = new DateTime(2017,11,11);
            Proveedor prov = new Proveedor("210001432188","Ancap","ancap@ancap.com","24090001",fecha,true,us);
            Console.WriteLine(prov.Guardar());*/

            List<Proveedor> listaProveedores = Proveedor.DevolverProveedores();
            Console.WriteLine("\n------------Listado Proveedores------------------\n");
            foreach (Proveedor pTmp in listaProveedores)
            {
                Console.WriteLine("- Nombre Fantasia: " + pTmp.NomFantasia + "\n" + 
                                  "RUT: " + pTmp.Rut + "\n" +
                                  "Vip: " + pTmp.Vip);
            }
            List<Usuario> listaUsuarios = Usuario.DevolverUsuarios();
            Console.WriteLine("\n------------Listado Usuarios------------------\n");
            foreach (Usuario tmpUsuario in listaUsuarios) {
                Console.WriteLine("- nombre: "+tmpUsuario.Nombre+" Password: "+tmpUsuario.Pass+" | Rol: "+tmpUsuario.Rol);
            }
            Console.ReadLine();            
        }
    }
}
