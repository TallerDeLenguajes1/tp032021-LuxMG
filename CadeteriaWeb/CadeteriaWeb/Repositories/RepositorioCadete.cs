using CadeteriaWeb.Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Repositories
{
    public class RepositorioCadete : IRepositorio<Cadete>
    {
        private readonly string connectionString;

        public RepositorioCadete(string connection)
        {
            this.connectionString = connection;
        }

        public void Delete(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"UPDATE Cadetes" +
                    $" SET cadeteAlta = false WHERE cadeteID = {id}";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<Cadete> GetAll()
        {
            List<Cadete> ListadoCadetes = new();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Cadetes WHERE cadeteAlta";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cadete C = new Cadete()
                            {
                                Id = Convert.ToInt32(reader["cadeteID"]),
                                Nombre = reader["cadeteNombre"].ToString(),
                                Direccion = reader["cadeteDireccion"].ToString(),
                                Telefono = reader["cadeteTelefono"].ToString(),
                                Jornal = Convert.ToDouble(reader["cadeteJornal"]),
                                Alta = Convert.ToBoolean(reader["cadeteAlta"])
                            };
                            ListadoCadetes.Add(C);
                        }
                    }
                }
                connection.Close();
            }

            return ListadoCadetes;
        }

        public Cadete GetItemById(int id)
        {
            Cadete C = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Cadetes WHERE cadeteID = {id}";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        if (!reader.IsDBNull(1))
                        {
                            C = new Cadete()
                            {
                                Id = Convert.ToInt32(reader["cadeteID"]),
                                Nombre = reader["cadeteNombre"].ToString(),
                                Direccion = reader["cadeteDireccion"].ToString(),
                                Telefono = reader["cadeteTelefono"].ToString(),
                                Jornal = Convert.ToDouble(reader["cadeteJornal"]),
                                Alta = Convert.ToBoolean(reader["cadeteAlta"])
                            };
                        }
                    }
                }
                connection.Close();
            }

            return C;
        }

        public void Insert(Cadete item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"INSERT INTO Cadetes(cadeteID, cadeteNombre, cadeteDireccion, cadeteTelefono, cadeteJornal, cadeteAlta)" +
                    $" VALUES({item.Id}, {item.Nombre}, {item.Direccion}, {item.Telefono}, {item.Jornal}, {item.Alta})";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void Update(Cadete item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"UPDATE Cadetes" +
                    $" SET cadeteNombre = {item.Nombre}, cadeteDireccion = {item.Direccion}," +
                    $" cadeteTelefono = {item.Telefono}, cadeteJornal = {item.Jornal}" +
                    $" WHERE cadeteID = {item.Id}";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
