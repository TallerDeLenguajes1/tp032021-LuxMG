using CadeteriaWeb.Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Repositories
{
    public class RepositorioUsuario : IRepositorio<Usuario>
    {
        private readonly string connectionString;

        public RepositorioUsuario(string connection)
        {
            this.connectionString = connection;
        }

        public void Delete(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"UPDATE Usuarios" +
                    $" SET usuarioAlta = false WHERE usuarioID = {id}";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public List<Usuario> GetAll()
        {
            List<Usuario> ListadoUsuarios = new();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Usuarios WHERE usuarioAlta";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario U = new Usuario()
                            {
                                Id = Convert.ToInt32(reader["usuarioID"]),
                                Username = reader["usuarioName"].ToString(),
                                Password = reader["usuarioPass"].ToString(),
                                Rol = (Rol)Enum.Parse(typeof(Rol), reader["usuarioRol"].ToString()),
                                Alta = Convert.ToBoolean(reader["usuarioAlta"])
                            };
                            ListadoUsuarios.Add(U);
                        }
                    }
                }
                connection.Close();
            }

            return ListadoUsuarios;
        }

        public Usuario GetItemById(int id)
        {
            Usuario U = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Usuarios WHERE usuarioID = {id}";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        if (!reader.IsDBNull(1))
                        {
                            U = new Usuario()
                            {
                                Id = Convert.ToInt32(reader["usuarioID"]),
                                Username = reader["usuarioName"].ToString(),
                                Password = reader["usuarioPass"].ToString(),
                                Rol = (Rol)Enum.Parse(typeof(Rol), reader["usuarioRol"].ToString()),
                                Alta = Convert.ToBoolean(reader["usuarioAlta"])
                            };
                        }
                    }
                }
                connection.Close();
            }

            return U;
        }

        public void Insert(Usuario item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"INSERT INTO Usuarios(usuarioID, usuarioName, usuarioPass, usuarioRol, usuarioAlta)" +
                    $" VALUES({item.Id}, {item.Username}, {item.Password}, {item.Rol}, {item.Alta})";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void Update(Usuario item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"UPDATE Usuarios" +
                    $" SET usuarioName = {item.Username}, usuarioPass = {item.Password}," +
                    $" usuarioRol = {item.Rol}" +
                    $" WHERE usuarioID = {item.Id}";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public Usuario Validate(string username, string password)
        {
            Usuario U = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Usuarios" +
                    $" WHERE usuarioName = {username} AND usuarioPass = {password}";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        if (!reader.IsDBNull(1))
                        {
                            U = new Usuario()
                            {
                                Id = Convert.ToInt32(reader["usuarioID"]),
                                Username = reader["usuarioName"].ToString(),
                                Password = reader["usuarioPass"].ToString(),
                                Rol = (Rol) Enum.Parse(typeof(Rol), reader["usuarioRol"].ToString()),
                                Alta = Convert.ToBoolean(reader["usuarioAlta"])
                            };
                        }
                    }
                }
                connection.Close();
            }

            return U;
        }
    }
}
