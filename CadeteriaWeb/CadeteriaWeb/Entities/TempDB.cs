using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CadeteriaWeb.Entities
{
    public static class TempDB
    {
        private static string pathCadetes = @"Cadetes.json";
        private static string pathPedidos = @"Pedidos.json";

        public static void GuardarCadetes(List<Cadete> cadetes)
        {
            string cadetesJson = JsonSerializer.Serialize(cadetes);

            using (StreamWriter strWriter = new StreamWriter(pathCadetes, false))
            {
                strWriter.WriteLine(cadetesJson);
                strWriter.Close();
                strWriter.Dispose();
            }
        }

        public static void GuardarPedidos(List<Pedido> pedidos)
        {
            string pedidosJson = JsonSerializer.Serialize(pedidos);
            using (StreamWriter strWriter = new StreamWriter(pathPedidos, false))
            {
                strWriter.WriteLine(pedidosJson);
                strWriter.Close();
                strWriter.Dispose();
            }
        }

        public static List<Cadete> ObtenerCadetes()
        {
            List<Cadete> cadetes = null;

            if (File.Exists(pathCadetes))
            {
                using (StreamReader strReader = new StreamReader(pathCadetes))
                {
                    string datos = strReader.ReadToEnd();
                    cadetes = JsonSerializer.Deserialize<List<Cadete>>(datos);
                    strReader.Close();
                    strReader.Dispose();
                }
            }
            return cadetes;
        }


        public static List<Pedido> ObtenerPedidos()
        {
            List<Pedido> pedidos = null;

            if (File.Exists(pathPedidos))
            {
                using (StreamReader strReader = new StreamReader(pathPedidos))
                {
                    string datos = strReader.ReadToEnd();
                    pedidos = JsonSerializer.Deserialize<List<Pedido>>(datos);
                    strReader.Close();
                    strReader.Dispose();
                }
            }
            return pedidos;
        }

    }
}
