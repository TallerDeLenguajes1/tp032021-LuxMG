using CadeteriaWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Repositories
{
    public interface IRepositorioCliente
    {
        void DeleteCliente(int id);
        List<Cliente> GetAllClientes();
        Cliente GetClienteById(int id);
        void InsertCliente(Cliente item);
        void UpdateCliente(Cliente item);
    }
}
