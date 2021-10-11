namespace CadeteriaWeb.Entities
{
    public enum EstadoPedido { Vacio, Procesando, En_Camino, Entregado, Cancelado };

    public class Pedido
    {
        private static int contador = 0;

        private int id;
        private string observacion;
        private Cliente cliente;
        private EstadoPedido estado;
        private Cadete cadete;

        public int Id { get => id; set => id = value; }
        public string Observacion { get => observacion; set => observacion = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public EstadoPedido Estado { get => estado; set => estado = value; }
        public static int Contador { get => contador; set => contador = value; }
        public Cadete Cadete { get => cadete; set => cadete = value; }

        public Pedido()
        {
            Id = 0;
            Estado = EstadoPedido.Vacio;
            Cadete = null;
            Cliente = new Cliente();
        }

        public Pedido(string observacion, string nombreCliente, string direccionCliente, string telefonoCliente)
        {
            Id = ++Contador;
            Observacion = observacion;
            Estado = EstadoPedido.Procesando;
            Cadete = null;
            Cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente);
        }

        public Pedido(int id, string observacion, string nombreCliente, string direccionCliente, string telefonoCliente)
        {
            Id = id;
            Observacion = observacion;
            Estado = EstadoPedido.Procesando;
            Cadete = null;
            Cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente);
        }

        public void AsignarCadete(Cadete C)
        {
            Cadete = C;
            Estado = EstadoPedido.En_Camino;
        }
    }
}