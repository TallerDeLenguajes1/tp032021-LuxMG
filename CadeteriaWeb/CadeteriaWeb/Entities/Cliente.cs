namespace CadeteriaWeb.Entities
{
    public class Cliente
    {
        private static int contador = 0;

        private int id;
        private string nombre;
        private string direccion;
        private string telefono;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }

        public Cliente()
        {
            Id = 0;
        }

        public Cliente(string nombre, string direccion, string telefono)
        {
            Id = ++contador;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
        }
    }
}