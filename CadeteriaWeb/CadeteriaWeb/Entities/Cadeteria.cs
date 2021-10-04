using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Entities
{
    public class Cadeteria
    {
        private string nombre;
        private List<Cadete> listadoCadetes;
        private List<Pedido> listadoPedidos;

        public string Nombre { get => nombre; set => nombre = value; }
        internal List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
        public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

        public Cadeteria()
        {
            Nombre = "La Cadeteria";
            listadoCadetes = TempDB.ObtenerCadetes();
            listadoPedidos = TempDB.ObtenerPedidos();
        }

        // ---------------------------- CADETES ----------------------------
        public void AgregarCadete(Cadete C)
        {
            ListadoCadetes.Add(C);
            TempDB.GuardarCadetes(ListadoCadetes);
        }

        public void EliminarCadete(int id)
        {
            ListadoCadetes.Remove(BuscarCadetePorID(id));
            TempDB.GuardarCadetes(ListadoCadetes);
        }

        public Cadete BuscarCadetePorID(int id)
        {
            return ListadoCadetes.Find(x =>  x.Id == id);
        }

        // ---------------------------- PEDIDOS ----------------------------
        public void AgregarPedido(Pedido P)
        {
            ListadoPedidos.Add(P);
            TempDB.GuardarPedidos(ListadoPedidos);
        }

        public void EliminarPedido(int id)
        {
            ListadoPedidos.Remove(BuscarPedidoPorID(id));
            TempDB.GuardarPedidos(ListadoPedidos);
        }

        public Pedido BuscarPedidoPorID(int id)
        {
            return ListadoPedidos.Find(x => x.Id == id);
        }
        
    }
}
