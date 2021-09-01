using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP03
{
    class Pedido
    {
        private int nro;
        private string observacion;
        private Cliente cliente;
        private string estado;

        public int Nro { get => nro; set => nro = value; }
        public string Observacion { get => observacion; set => observacion = value; }
        public string Estado { get => estado; set => estado = value; }
        internal Cliente Cliente { get => cliente; set => cliente = value; }

        public Pedido()
        {

        }

        public Pedido(int nro, string observacion, string estado)
        {
            Nro = nro;
            Observacion = observacion;
            Estado = estado;
            Cliente = new Cliente();
        }
    }
}
