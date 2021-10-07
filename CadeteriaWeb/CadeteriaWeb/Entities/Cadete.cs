using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Entities
{
    public class Cadete
    {
        private static int contador = 0;

        private int id;
        private string nombre;
        private string direccion;
        private string telefono;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public static int Contador { get => contador; set => contador = value; }

        public Cadete()
        {
            Id = 0;
        }

        public Cadete(string nombre, string direccion, string telefono)
        {            
            Id = ++Contador;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
        }

        public Cadete(int id, string nombre, string direccion, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
        }
    }
}
