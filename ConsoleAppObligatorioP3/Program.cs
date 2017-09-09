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
            Console.WriteLine("aca estamo en la consola");
            Usuario us = new Usuario("prov","123",Usuario.EnumRol.Proveedor);
            DateTime fecha = new DateTime(2017,11,11);
            Proveedor prov = new Proveedor("210001432188","Ancap","ancap@ancap.com","24090001",fecha,true,us);
            Console.WriteLine(prov.Guardar());
            Console.ReadKey();
        }
    }
}
