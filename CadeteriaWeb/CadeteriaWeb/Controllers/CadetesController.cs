using AutoMapper;
using CadeteriaWeb.Entities;
using CadeteriaWeb.Models;
using CadeteriaWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Controllers
{
    public class CadetesController : SessionController
    {
        private readonly DataContext DB;
        private readonly ILogger<CadetesController> _logger;
        private readonly IMapper mapper;

        public CadetesController(DataContext DB, ILogger<CadetesController> logger, IMapper mapper)
        {
            this.DB = DB;
            this._logger = logger;
            this.mapper = mapper;
        }


        // -----------------------------------------------------------------
        // ---------------------------INFO CADETE---------------------------
        // -----------------------------------------------------------------
        // INDEX: Cadetes/{id}
        public IActionResult Index(int id = 0)
        {
            if (!IsSesionIniciada())
                return RedirectToAction("Login", "Usuarios");
            if (id == 0)
                return RedirectToAction("IndexAdmin");

            Cadete C = DB.Cadetes.GetCadeteById(id);
            if (C == null) 
                return RedirectToAction("IndexAdmin");

            CadeteListadoViewModel cadeteVM = new()
            {
                Id = C.Id,
                Nombre = C.Nombre,
                Direccion = C.Direccion,
                Telefono = C.Telefono,
                Pedidos = DB.Pedidos.GetAllPedidos(C.Id)
            };

            return View(cadeteVM);
        }


        // -----------------------------------------------------------------
        // --------------------------LISTA CADETES--------------------------
        // -----------------------------------------------------------------
        // GET: Cadetes/IndexAdmin
        public IActionResult IndexAdmin()
        {
            if (!IsSesionIniciada())
                return RedirectToAction("Login", "Usuarios");

            // solo los admins pueden ver todos los cadetes
            if (GetRol() == "ADMIN")
                return View(DB);

            return RedirectToAction("Index", "Home");
        }


        // -----------------------------------------------------------------
        // --------------------------CARGA CADETES--------------------------
        // -----------------------------------------------------------------
        // GET: Cadetes/CreateCadete
        public IActionResult CreateCadete(CadeteCreateViewModel cadeteVM = null)
        {
            return View(cadeteVM);
        }

        // POST: Cadetes/CreateCadetePost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCadetePost(CadeteCreateViewModel cadete)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Cadete C = mapper.Map<Cadete>(cadete);

                    _logger.LogInformation($"CREACION DE CADETE - NOMBRE:{C.Nombre}, DIRECCION:{C.Direccion}, TELEFONO:{C.Telefono}");

                    DB.Cadetes.InsertCadete(C);
                    return RedirectToAction("IndexAdmin");
                }

                return RedirectToAction("CreateCadete");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN CREACION DE CADETE - EXCEPTION:{e.Message}");
                return RedirectToAction("IndexAdmin");
            }
        }


        // -----------------------------------------------------------------
        // --------------------------DELETE CADETES-------------------------
        // -----------------------------------------------------------------
        // GET: Cadetes/DeleteCadete/{id}
        public IActionResult DeleteCadete(int id = 0)
        {
            if (!IsSesionIniciada())
                return RedirectToAction("Login", "Usuarios");
            if (id == 0)
                return RedirectToAction("IndexAdmin");

            try
            {
                // solo pueden eliminar cadetes el admin
                if (GetRol() == "ADMIN")
                {
                    Cadete C = DB.Cadetes.GetCadeteById(id);

                    if (C != null)
                    {
                        DB.Cadetes.DeleteCadete(id);
                        _logger.LogInformation($"DELETE DE CADETE - ID:{C.Id}, NOMBRE:{C.Nombre}, DIRECCION:{C.Direccion}, TELEFONO:{C.Telefono}");
                    }

                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN DELETE DE CADETE - EXCEPTION:{e.Message}");
                return RedirectToAction("IndexAdmin");
            }
        }


        // -----------------------------------------------------------------
        // --------------------------UPDATE CADETES-------------------------
        // -----------------------------------------------------------------
        // GET: Cadetes/UpdateCadete/{id}
        public IActionResult UpdateCadete(int id = 0, CadeteUpdateViewModel cadeteVM = null)
        {
            if (!IsSesionIniciada())
                return RedirectToAction("Login", "Usuarios");

            try
            {
                // solo un admin puede modificar un cadete
                if (GetRol() == "ADMIN")
                {
                    if (cadeteVM.Nombre != null)
                        return View(cadeteVM);
                
                    Cadete C = DB.Cadetes.GetCadeteById(id);
                    if (C != null)
                        return View(mapper.Map<CadeteUpdateViewModel>(C));
                }

                return RedirectToAction("IndexAdmin");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN UPDATE DE CADETE - EXCEPTION:{e.Message}");
                return RedirectToAction("IndexAdmin");
            }
        }

        // POST: Cadete/UpdateCadetePost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCadetePost(CadeteUpdateViewModel cadete)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Cadete C = mapper.Map<Cadete>(cadete);
                    C.Id = cadete.Id;

                    DB.Cadetes.UpdateCadete(C);
                    _logger.LogInformation($"UPDATE DE CADETE - ID:{C.Id}, NOMBRE:{C.Nombre}, DIRECCION:{C.Direccion}, TELEFONO:{C.Telefono}");
                    return RedirectToAction("IndexAdmin");
                }

                return RedirectToAction("UpdateCadete", cadete); // si hay un error en el ModelState
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN UPDATE DE CADETE - EXCEPTION:{e.Message}");
                return RedirectToAction("IndexAdmin");
            }
        }
    }
}
