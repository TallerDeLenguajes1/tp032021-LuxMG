using CadeteriaWeb.Entities;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Controllers
{
    public class CadeteController : Controller
    {
        private readonly Cadeteria cadeteria;
        private readonly Logger nlog;

        public CadeteController(Cadeteria cadeteria, Logger nlog)
        {
            this.cadeteria = cadeteria;
            this.nlog = nlog;
        }

        public IActionResult Index()
        {
            return View(cadeteria.ListadoCadetes);
        }

        public IActionResult CreateCadete(string nombre, string direccion, string telefono)
        {
            if (nombre == null || direccion == null || telefono == null)
            {
                return View();
            }

            Cadete C = new Cadete(nombre, direccion, telefono);
            cadeteria.AgregarCadete(C);
            cadeteria.GuardarCambiosCadetes();

            return View("Index", cadeteria.ListadoCadetes);
        }

        public IActionResult UpdateCadete(int id, string nombre, string direccion, string telefono)
        {
            if (nombre == null || direccion == null || telefono == null)
            {
                Cadete C = cadeteria.BuscarCadetePorID(id);

                if (C != null)
                    return View(C);
                else
                    return View("Index", cadeteria.ListadoCadetes);
            }

            Cadete CNew = new Cadete(id, nombre, direccion, telefono);
            cadeteria.EliminarCadete(id);
            cadeteria.AgregarCadete(CNew);
            cadeteria.OrdenarCadetes();
            cadeteria.GuardarCambiosCadetes();

            return View("Index", cadeteria.ListadoCadetes);
        }

        public IActionResult DeleteCadete(int id, string confirm)
        {
            Cadete C = cadeteria.BuscarCadetePorID(id);
            if (C == null)
                return View("Index", cadeteria.ListadoCadetes);

            if (confirm != "true")
                return View(C);

            cadeteria.EliminarCadete(id);
            cadeteria.GuardarCambiosCadetes();

            return View("Index", cadeteria.ListadoCadetes);
        }

    }
}
