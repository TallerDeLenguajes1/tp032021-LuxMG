using CadeteriaWeb.Entities;
using CadeteriaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly Cadeteria cadeteria;
        private readonly Logger nlog;

        public HomeController(Logger nlog, Cadeteria cadeteria)
        {
            this.nlog = nlog;
            this.cadeteria = cadeteria;
        }

        public IActionResult Index()
        {
            return View(cadeteria);
        }

        //public IActionResult CreatePedido(string observacion, string nombreCliente, string direccionCliente, string telefonoCliente)
        //{
        //    if(observacion == null || nombreCliente == null || direccionCliente == null || telefonoCliente == null)
        //    {
        //        return View();
        //    }

        //    Pedido P = new Pedido(observacion, nombreCliente, direccionCliente, telefonoCliente);
        //    db.ListadoPedidos.Add(P);
            
        //    return View("ListadoPedidos", db.ListadoPedidos);
        //}

        //public IActionResult ListadoPedidos()
        //{
        //    return View(db.ListadoPedidos);
        //}

        //public IActionResult CreateCadete(string nombre, string direccion, string telefono)
        //{
        //    if (nombre == null || direccion == null || telefono == null)
        //    {
        //        return View();
        //    }

        //    Cadete C = new Cadete(nombre, direccion, telefono);
        //    db.ListadoCadetes.Add(C);

        //    return View("ListadoCadetes", db.ListadoCadetes);
        //}

        //public IActionResult ListadoCadetes()
        //{
        //    return View(db.ListadoCadetes);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
