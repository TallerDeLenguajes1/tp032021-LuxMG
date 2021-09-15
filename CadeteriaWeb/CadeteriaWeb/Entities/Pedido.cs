namespace CadeteriaWeb.Entities
{
    public enum EstadoPedido { Procesando, En_Camino, Entregado };

    public class Pedido
    {
        private int nro;
        private string observacion;
        private Cliente cliente;
        private EstadoPedido estado;
        private int idCadete;

        public int Nro { get => nro; set => nro = value; }
        public string Observacion { get => observacion; set => observacion = value; }

        internal Cliente Cliente { get => cliente; set => cliente = value; }
        public EstadoPedido Estado { get => estado; set => estado = value; }
        public int IdCadete { get => idCadete; set => idCadete = value; }

        public Pedido()
        {

        }

        public Pedido(int nro, string observacion, EstadoPedido estado, int idCadete, int idCliente, string nombreCliente, string direccionCliente, string telefonoCliente)
        {
            Nro = nro;
            Observacion = observacion;
            Estado = estado;
            IdCadete = idCadete;
            Cliente = new Cliente(idCliente, nombreCliente, direccionCliente, telefonoCliente);
        }
    }
}