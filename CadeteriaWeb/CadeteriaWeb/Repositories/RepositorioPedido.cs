using CadeteriaWeb.Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Repositories
{
    public class RepositorioPedido : IRepositorio<Pedido>
    {
        private readonly string connectionString;

        public RepositorioPedido(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Delete(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"UPDATE Pedidos" +
                    $" SET pedidoAlta = false WHERE pedidoID = {id}";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<Pedido> GetAll()
        {
            List<Pedido> ListadoPedidos = new();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Pedido WHERE pedidoAlta";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cadete C = new RepositorioCadete(connectionString).GetItemById(Convert.ToInt32(reader["cadeteID"]));
                            Cliente Ci = new RepositorioCliente(connectionString).GetItemById(Convert.ToInt32(reader["clienteID"]));

                            Pedido P = new Pedido()
                            {
                                Id = Convert.ToInt32(reader["pedidoID"]),
                                Observacion = reader["pedidoObs"].ToString(),
                                Estado = (EstadoPedido)Enum.Parse(typeof(EstadoPedido), reader["pedidoEstado"].ToString()),
                                Cliente = Ci,
                                Cadete = C,
                                Alta = Convert.ToBoolean(reader["pedidoAlta"])
                            };
                            ListadoPedidos.Add(P);
                        }
                    }
                }
                connection.Close();
            }

            return ListadoPedidos;
        }

        public Pedido GetItemById(int id)
        {
            Pedido P = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Pedidos WHERE pedidoID = {id}";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

                        Cadete C = new RepositorioCadete(connectionString).GetItemById(Convert.ToInt32(reader["cadeteID"]));
                        Cliente Ci = new RepositorioCliente(connectionString).GetItemById(Convert.ToInt32(reader["clienteID"]));

                        P = new Pedido()
                        {
                            Id = Convert.ToInt32(reader["pedidoID"]),
                            Observacion = reader["pedidoObs"].ToString(),
                            Estado = (EstadoPedido)Enum.Parse(typeof(EstadoPedido), reader["pedidoEstado"].ToString()),
                            Cliente = Ci,
                            Cadete = C,
                            Alta = Convert.ToBoolean(reader["pedidoAlta"])
                        };
                    }
                }
                connection.Close();
            }

            return P;
        }

        public void Insert(Pedido item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"INSERT INTO Pedidos(pedidoID, pedidoObs, pedidoEstado, clienteID, cadeteID, pedidoAlta)" +
                    $" VALUES({item.Id}, {item.Observacion}, {item.Estado}, {item.Cliente.Id}, {item.Cadete.Id}, {item.Alta})";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void Update(Pedido item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"UPDATE Pedidos" +
                    $" SET pedidoObservacion = {item.Observacion}, pedidoEstado = {item.Estado}," +
                    $" clienteID = {item.Cliente.Id}, cadeteID = {item.Cadete.Id}" +
                    $" WHERE pedidoID = {item.Id}";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<Pedido> GetAllByPeopleID(int cadeteID = 0, int clienteID = 0)
        {
            List<Pedido> ListadoPedidos = new();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Pedido WHERE pedidoAlta";

                if (cadeteID != 0) SQLQuery += $" AND cadeteID = {cadeteID}";
                if (clienteID != 0) SQLQuery += $" AND clienteID = {clienteID}";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cadete C = new RepositorioCadete(connectionString).GetItemById(Convert.ToInt32(reader["cadeteID"]));
                            Cliente Ci = new RepositorioCliente(connectionString).GetItemById(Convert.ToInt32(reader["clienteID"]));

                            Pedido P = new Pedido()
                            {
                                Id = Convert.ToInt32(reader["pedidoID"]),
                                Observacion = reader["pedidoObs"].ToString(),
                                Estado = (EstadoPedido)Enum.Parse(typeof(EstadoPedido), reader["pedidoEstado"].ToString()),
                                Cliente = Ci,
                                Cadete = C,
                                Alta = Convert.ToBoolean(reader["pedidoAlta"])
                            };
                            ListadoPedidos.Add(P);
                        }
                    }
                }
                connection.Close();
            }

            return ListadoPedidos;
        }
    }
}
