using CadeteriaWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Repositories
{
    public interface IRepositorioPedido
    {
        void DeletePedido(int id);
        List<Pedido> GetAllPedidos();
        Pedido GetPedidoById(int id);
        List<Pedido> GetAllPedidosByPeopleID(int cadeteID = 0, int clienteID = 0);
        void InsertPedido(Pedido item);
        void UpdatePedido(Pedido item);
    }
}
