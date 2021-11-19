using CadeteriaWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Entities
{
    public class DataBase
    {
        public Cadeteria Cadeteria;
        public RepositorioCadete RepoCadete;
        public RepositorioCliente RepoCliente;
        public RepositorioPedido RepoPedido;
        public RepositorioUsuario RepoUsuario;

        public DataBase(string connectionString)
        {
            Cadeteria = new Cadeteria();
            RepoCadete = new RepositorioCadete(connectionString);
            RepoCliente = new RepositorioCliente(connectionString);
            RepoPedido = new RepositorioPedido(connectionString);
            RepoUsuario = new RepositorioUsuario(connectionString);
        }
    }
}
