using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using CapaFachada;
using ServiciosObligatorioWCF;

namespace ConsoleAppObligatorioP3
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("aca estamo en la consola");
            Usuario us = new Usuario("prov2","123",Usuario.EnumRol.Proveedor);
            DateTime fecha = new DateTime(2017,11,11);
            Proveedor prov = new Proveedor("210001432188","Ancap","ancap@ancap.com","24090001",fecha,true,us);
            Console.WriteLine(prov.Guardar());
            */
            Console.WriteLine("\n------------prueba------------------\n");
            Servicio coso = new Servicio();
            List<Servicio> cosocoso = coso.TraerTodo();
            foreach (Servicio s in cosocoso) {
                Console.WriteLine(s);
            }

            //CONSUMIR WCF QUE RETORNA TODOS LOS SERVICIOS

            //Console.WriteLine("\n------------Listado Servicios servicio wcf------------------\n");
            //RetornarServicios.RetornarServiciosClient proxySer = new RetornarServicios.RetornarServiciosClient();
            //proxySer.Open();
            //DTOServicio[] listaDTOSerWCF = proxySer.RetornarServicios();
            //foreach (DTOServicio tmpDTOSer in listaDTOSerWCF)
            //{
            //    Console.WriteLine("- Rut Proveedor: " + tmpDTOSer.RutProveedor + "\n" +
            //                      "Nombre: " + tmpDTOSer.Nombre + "\n" + "Descripcion: " + tmpDTOSer.Descripcion + "\n" +
            //                      "Tipo Servicio: " + tmpDTOSer.TipoServicio.Nombre);
                
            //    Console.WriteLine("\n");
            //}


            Console.WriteLine("\n------------Listado Proveedores servicio wcf------------------\n");
            RetornarProveedores.RetornarProveedoresClient proxy = new RetornarProveedores.RetornarProveedoresClient();
            proxy.Open();
            DTOProveedor[] listaDTOProvWCF = proxy.RetornarProveedores();
            foreach (DTOProveedor tmpDTOProv in listaDTOProvWCF) {
                Console.WriteLine("- Nombre Fantasia: " + tmpDTOProv.NomFantasia + "\n" +
                                  "RUT: " + tmpDTOProv.Rut + "\n" + "Usuario: " + tmpDTOProv.Usuario.Nombre + "\n" +
                                  "Vip: " + tmpDTOProv.Vip);
                if (tmpDTOProv.Vip)
                {
                    Console.WriteLine("Porcentaje por vip: " + tmpDTOProv.PorcentajePorVip);
                }
                Console.WriteLine("\n");
            }


            Proveedor tmpProv = new Proveedor();
            List<Proveedor> listaProveedores = tmpProv.TraerTodo();
            Console.WriteLine("\n------------Listado Proveedores------------------\n");
            foreach (Proveedor pTmp in listaProveedores)
            {
                Console.WriteLine("- Nombre Fantasia: " + pTmp.NomFantasia + "\n" + 
                                  "RUT: " + pTmp.Rut + "\n" + "Usuario: "+ pTmp.Usuario.Nombre +"\n"+
                                  "Vip: " + pTmp.Vip);
                if (pTmp.Vip) {
                    Console.WriteLine("Porcentaje por vip: " +pTmp.PorcentajePorVip);
                }
                List<Servicio> listaSer = pTmp.DevolverServicios();
                Console.WriteLine("Servicios ofrecidos: ");
                foreach (Servicio tmp in listaSer) {
                    Console.WriteLine("* - "+tmp);
                }
                Console.WriteLine("\n");
            }
            Usuario tmpUser = new Usuario();
            List<Usuario> listaUsuarios = tmpUser.TraerTodo();
            Console.WriteLine("\n------------Listado Usuarios------------------\n");
            foreach (Usuario tmpUsuario in listaUsuarios) {
                Console.WriteLine("- nombre: "+tmpUsuario.Nombre+" Password: "+tmpUsuario.Pass+" | Rol: "+tmpUsuario.Rol);
            }
            Console.WriteLine("\n------------Validar Usuario------------------\n");
            Console.WriteLine("Ingrese nombre: ");
            string unNombre = Console.ReadLine();
            Console.WriteLine("Ingrese pass: ");
            string unPass = Console.ReadLine();
            Usuario aux = new Usuario();
            aux.Nombre = unNombre; 
            aux.Pass = unPass;

            if (Fachada.ValidarUsuario(aux.Nombre,aux.Pass)!=null)
            {
                Console.WriteLine("usuario validado");
            }
            else {
                Console.WriteLine("usuario no validado ");
            }

            Console.ReadLine();            
        }
    }
}
