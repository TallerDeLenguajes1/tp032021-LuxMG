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
        private int idCadete;

        public int Id { get => id; set => id = value; }
        public string Observacion { get => observacion; set => observacion = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public EstadoPedido Estado { get => estado; set => estado = value; }
        public int IdCadete { get => idCadete; set => idCadete = value; }
        public static int Contador { get => contador; set => contador = value; }

        public Pedido()
        {
            Id = 0;
            Estado = EstadoPedido.Vacio;
            IdCadete = 0;
            Cliente = new Cliente();
        }

        public Pedido(string observacion, string nombreCliente, string direccionCliente, string telefonoCliente)
        {
            Id = ++Contador;
            Observacion = observacion;
            Estado = EstadoPedido.Procesando;
            IdCadete = 0;
            Cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente);
        }

        public Pedido(int id, string observacion, string nombreCliente, string direccionCliente, string telefonoCliente)
        {
            Id = id;
            Observacion = observacion;
            Estado = EstadoPedido.Procesando;
            IdCadete = 0;
            Cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente);
        }
    }
}