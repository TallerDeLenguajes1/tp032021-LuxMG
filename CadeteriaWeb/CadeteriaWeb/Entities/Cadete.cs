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

        public Cadete()
        {
            Id = 0;
        }

        public Cadete(string nombre, string direccion, string telefono)
        {            
            Id = ++contador;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
        }

        public void AgregarPedido(Pedido P)
        {
            P.IdCadete = Id;
            P.Estado = EstadoPedido.En_Camino;
        }
    }
}
