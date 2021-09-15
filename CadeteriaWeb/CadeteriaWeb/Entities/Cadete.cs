using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Entities
{
    public class Cadete
    {
        private int id;
        private string nombre;
        private string direccion;
        private string telefono;
        //private List<Pedido> listadoPedidos = new List<Pedido>();

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        //internal List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

        public Cadete()
        {

        }

        public Cadete(int id, string nombre, string direccion, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            // ListadoPedidos = new List<Pedido>();
        }

        //public void AgregarPedido(Pedido P)
        //{
        //    if (P.Estado != EstadoPedido.Entregado)
        //    {
        //        ListadoPedidos.Add(P);
        //    }
        //}

        //public Pedido SacarPedido(int nro)
        //{
        //    foreach (Pedido P in ListadoPedidos)
        //    {
        //        if (P.Nro == nro)
        //        {
        //            //Pedido aux = P;
        //            ListadoPedidos.Remove(P);
        //            return P;
        //        }
        //    }

        //    return null;
        //}
    }
}
