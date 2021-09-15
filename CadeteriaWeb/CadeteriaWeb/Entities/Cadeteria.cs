using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Entities
{
    public class Cadeteria
    {
        private string nombre;
        private List<Cadete> listadoCadetes = new();
        private List<Pedido> listadoPedidos = new();

        public string Nombre { get => nombre; set => nombre = value; }
        internal List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
        public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

        public Cadeteria()
        {
            Nombre = "La Cadeteria";
        }

        public Cadeteria(string nombre)
        {
            Nombre = nombre;
        }
    }
}
