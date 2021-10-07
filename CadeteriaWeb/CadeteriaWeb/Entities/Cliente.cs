namespace CadeteriaWeb.Entities
{
    public class Cliente
    {
        private string nombre;
        private string direccion;
        private string telefono;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }

        public Cliente()
        {
        }

        public Cliente(string nombre, string direccion, string telefono)
        {
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
        }
    }
}