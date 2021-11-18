using CadeteriaWeb.Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Repositories
{
    public class RepositorioCliente : IRepositorio<Cliente>
    {
        private readonly string connectionString;

        public RepositorioCliente(string connection)
        {
            this.connectionString = connection;
        }

        public void Delete(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"UPDATE Clientes" +
                    $" SET clienteAlta = false WHERE clienteID = {id}";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<Cliente> GetAll()
        {
            List<Cliente> ListadoClientes = new();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Clientes WHERE clienteAlta";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente C = new Cliente()
                            {
                                Id = Convert.ToInt32(reader["clienteID"]),
                                Nombre = reader["clienteNombre"].ToString(),
                                Direccion = reader["clienteDireccion"].ToString(),
                                Telefono = reader["clienteTelefono"].ToString(),
                                Alta = Convert.ToBoolean(reader["clienteAlta"])
                            };
                            ListadoClientes.Add(C);
                        }
                    }
                }
                connection.Close();
            }

            return ListadoClientes;
        }

        public Cliente GetItemById(int id)
        {
            Cliente C = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Clientes WHERE clienteID = {id}";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        if (!reader.IsDBNull(1))
                        {
                            C = new Cliente()
                            {
                                Id = Convert.ToInt32(reader["clienteID"]),
                                Nombre = reader["clienteNombre"].ToString(),
                                Direccion = reader["clienteDireccion"].ToString(),
                                Telefono = reader["clienteTelefono"].ToString(),
                                Alta = Convert.ToBoolean(reader["clienteAlta"])
                            };
                        }
                    }
                }
                connection.Close();
            }

            return C;
        }

        public void Insert(Cliente item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"INSERT INTO Clientes(clienteID, clienteNombre, clienteDireccion, clienteTelefono, clienteAlta)" +
                    $" VALUES({item.Id}, {item.Nombre}, {item.Direccion}, {item.Telefono}, {item.Alta})";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void Update(Cliente item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"UPDATE Clientes" +
                    $" SET clienteNombre = {item.Nombre}, clienteDireccion = {item.Direccion}," +
                    $" clienteTelefono = {item.Telefono}" +
                    $" WHERE clienteID = {item.Id}";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
