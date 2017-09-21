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

            //Console.WriteLine("aca estamo en la consola");
            //Usuario us = new Usuario("prov2","123",Usuario.EnumRol.Proveedor);
            //DateTime fecha = new DateTime(2017,11,11);
            //Proveedor prov = new Proveedor("210001432188","Ancap","ancap@ancap.com","24090001",fecha,true,us);
            //Console.WriteLine(prov.Guardar());
            Console.WriteLine("\n-------------Modificar Arancel y Porcentaje Vip (Proveedor) servicio wcf------------------\n");
            OperacionesProveedoreRef.OperacionesProveedoresClient proxyOpProv = new OperacionesProveedoreRef.OperacionesProveedoresClient();
            
            Console.WriteLine("Por favor ingrese un nuevo valor para Arancel(Proveedor): ");
            double tmpArancel = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Por favor ingrese un nuevo valor para Porcentaje Vip (Proveedor): ");
            double tmpPorcentajeVip = Convert.ToDouble(Console.ReadLine());
            if (proxyOpProv.ModificarArancelesProveedor(tmpArancel, tmpPorcentajeVip)) Console.WriteLine("Cambio realizado con exito!");
            else Console.WriteLine("El cambio no pudo ser efectuado, por favor ingrese valores mayores o igual a 0 (cero)");
            
            Console.WriteLine("\n-------------Obtener Proveedor Por Rut servicio wcf------------------\n");
            Console.WriteLine("Ingrese un Rut: ");
            string rut = Console.ReadLine();
            
            
            DTOProveedor tmpDTOProv1 = proxyOpProv.RetornarProveedorPorRut(rut);
            if(tmpDTOProv1 != null) { 
            Console.WriteLine("- Nombre Fantasia: " + tmpDTOProv1.NomFantasia + "\n" +
                                  "RUT: " + tmpDTOProv1.Rut + "\n" + "Usuario: " + tmpDTOProv1.Usuario.Nombre + "\n" +
                                  "Vip: " + tmpDTOProv1.Vip);
                if (tmpDTOProv1.Vip)
                {
                    Console.WriteLine("Porcentaje por vip: " + tmpDTOProv1.PorcentajePorVip);
                }
                Console.WriteLine("\n");
            }
            else
            {
                Console.WriteLine("Proveedor no encontrado");
            }
            

            Console.WriteLine("\n------------Listado Servicios servicio wcf------------------\n");
            RetornarServiciosRef.RetornarServiciosClient proxySer = new RetornarServiciosRef.RetornarServiciosClient();
            
            DTOServicio[] listaDTOSerWCF = proxySer.RetornarServicios();
            foreach (DTOServicio tmpDTOSer in listaDTOSerWCF)
            {
                Console.WriteLine("- Rut Proveedor: " + tmpDTOSer.RutProveedor + "\n" +
                                  "Nombre: " + tmpDTOSer.Nombre + "\n" + "Descripcion: " + tmpDTOSer.Descripcion + "\n" +
                                  "Tipo Servicio: " + tmpDTOSer.TipoServicio
                                  );

                Console.WriteLine("\n");
            }


            Console.WriteLine("\n------------Listado Proveedores servicio wcf------------------\n");
            
            DTOProveedor[] listaDTOProvWCF = proxyOpProv.RetornarProveedores();
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
            

            Console.WriteLine("\n------------FIN SERVICIOS WCF------------------\n");

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
