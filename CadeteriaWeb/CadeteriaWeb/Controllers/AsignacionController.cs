using CadeteriaWeb.Entities;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Controllers
{
    public class AsignacionController : Controller
    {
        private readonly Cadeteria cadeteria;
        private readonly Logger nlog;

        public AsignacionController(Cadeteria cadeteria, Logger nlog)
        {
            this.cadeteria = cadeteria;
            this.nlog = nlog;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AsignarCadete(int id, int idCadete)
        {
            if (idCadete == 0 || id == 0)
                return View("Index", cadeteria);

            Pedido P = cadeteria.BuscarPedidoPorID(id);
            Cadete C = cadeteria.BuscarCadetePorID(idCadete);

            if (C != new Cadete() && P != new Pedido())
            {
                P.AsignarCadete(C);
                cadeteria.GuardarCambiosPedidos();
            }

            return View("Index", cadeteria);
        }
    }
}
