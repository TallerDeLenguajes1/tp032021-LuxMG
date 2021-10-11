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
            cadeteria.GuardarCambiosPedidos();

            return View("Index", cadeteria.ListadoPedidos);
        }

        public IActionResult UpdatePedido(int id, string observacion, string nombreCliente, string direccionCliente, string telefonoCliente)
        {
            if (observacion == null || nombreCliente == null || direccionCliente == null || telefonoCliente == null)
            {
                Pedido P = cadeteria.BuscarPedidoPorID(id);

                if (P != null)
                    return View(P);
                else
                    return View("Index", cadeteria.ListadoPedidos);
            }

            Pedido PNew = new Pedido(id, observacion, nombreCliente, direccionCliente, telefonoCliente);
            cadeteria.EliminarPedido(id);
            cadeteria.AgregarPedido(PNew);
            cadeteria.OrdenarPedidos();
            cadeteria.GuardarCambiosPedidos();

            return View("Index", cadeteria);
        }

        public IActionResult DeletePedido(int id, string confirm)
        {
            Pedido P = cadeteria.BuscarPedidoPorID(id);
            if (P == null)
                return View("Index", cadeteria.ListadoPedidos);

            if (confirm != "true")
                return View(P);

            cadeteria.EliminarPedido(id);
            cadeteria.GuardarCambiosPedidos();

            return View("Index", cadeteria.ListadoPedidos);
        }

        public IActionResult AsignarCadete(int id, int idCadete)
        {
            Pedido P = cadeteria.BuscarPedidoPorID(id);
            if (P == null)
                return View("Index", cadeteria.ListadoPedidos);

            if (idCadete == 0)
                return View(cadeteria);

            Cadete C = cadeteria.BuscarCadetePorID(idCadete);
            if (C == null)
                return View("Index", cadeteria.ListadoPedidos);

            P.AsignarCadete(C);
            cadeteria.GuardarCambiosPedidos();

            return View("Index", cadeteria.ListadoPedidos);
        }

        public IActionResult EntregarPedido(int id)
        {
            Pedido P = cadeteria.BuscarPedidoPorID(id);
            if (P != null)
                P.Entregar();

            cadeteria.GuardarCambiosPedidos();
            return View("Index", cadeteria.ListadoPedidos);
        }

        public IActionResult CancelarPedido(int id)
        {
            Pedido P = cadeteria.BuscarPedidoPorID(id);
            if (P != null)
                P.Cancelar();

            cadeteria.GuardarCambiosPedidos();
            return View("Index", cadeteria.ListadoPedidos);
        }

    }
}
