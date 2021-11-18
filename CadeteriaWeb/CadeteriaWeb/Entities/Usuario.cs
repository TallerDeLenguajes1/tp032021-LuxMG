using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Entities
{
    public class Usuario
    {
        private int id;
        private string username;
        private string password;
        private bool alta;

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public bool Alta { get => alta; set => alta = value; }

        public Usuario()
        {
        }

        public Usuario(int id, string username, string password, bool alta)
        {
            Id = id;
            Username = username;
            Password = password;
            Alta = alta;
        }

    }
}
