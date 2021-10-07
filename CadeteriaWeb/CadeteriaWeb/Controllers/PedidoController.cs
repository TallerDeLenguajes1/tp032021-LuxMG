using CadeteriaWeb.Entities;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Controllers
{
    public class PedidoController : Controller
    {
        private readonly Cadeteria cadeteria;
        private readonly Logger nlog;

        public PedidoController(Logger nlog, Cadeteria cadeteria)
        {
            this.nlog = nlog;
            this.cadeteria = cadeteria;
        }

        public IActionResult Index()
        {
            return View(cadeteria.ListadoPedidos);
        }

        public IActionResult CreatePedido(string observacion, string nombreCliente, string direccionCliente, string telefonoCliente)
        {
            if (observacion == null || nombreCliente == null || direccionCliente == null || telefonoCliente == null)
            {
                return View();
            }

            Pedido P = new Pedido(observacion, nombreCliente, direccionCliente, telefonoCliente);
            cadeteria.AgregarPedido(P);

            return View("Index", cadeteria.ListadoPedidos);
        }



    }
}
