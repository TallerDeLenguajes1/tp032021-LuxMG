using AutoMapper;
using CadeteriaWeb.Entities;
using CadeteriaWeb.Models;
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
        private readonly IMapper mapper; 

        public CadeteController(Cadeteria cadeteria, Logger nlog, IMapper mapper)
        {
            this.cadeteria = cadeteria;
            this.nlog = nlog;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            List<Cadete> ListadoCadetes = cadeteria.ListadoCadetes;
            var ListCadetesViewModel = mapper.Map<List<CadeteViewModel>>(ListadoCadetes);
            return View(cadeteria.ListadoCadetes);
        }

        // GET: Cadete/Create
        public ActionResult Create()
        {
            return View(new CadeteViewModel());
        }

        // POST: Cadete/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CadeteViewModel cadetevm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // codigo codigo codigo
                }
            }
            catch (Exception e)
            {

            }
        }

        //public IActionResult CreateCadete(string nombre, string direccion, string telefono)
        //{
        //    if (nombre == null || direccion == null || telefono == null)
        //    {
        //        return View();
        //    }

        //    Cadete C = new Cadete(nombre, direccion, telefono);
        //    cadeteria.AgregarCadete(C);
        //    cadeteria.GuardarCambiosCadetes();

        //    return View("Index", cadeteria.ListadoCadetes);
        //}

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
