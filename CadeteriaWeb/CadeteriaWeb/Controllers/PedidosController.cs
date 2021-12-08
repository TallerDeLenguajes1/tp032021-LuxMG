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
    public class PedidosController : SessionController
    {
        private readonly DataContext DB;
        private readonly ILogger<PedidosController> _logger;
        private readonly IMapper mapper;

        public PedidosController(DataContext DB, ILogger<PedidosController> logger, IMapper mapper)
        {
            this.DB = DB;
            this._logger = logger;
            this.mapper = mapper;
        }


        // -----------------------------------------------------------------
        // ---------------------------INFO PEDIDO---------------------------
        // -----------------------------------------------------------------
        // INDEX: Pedidos/{id}
        public IActionResult Index(int id = 0)
        {
            if (!IsSesionIniciada())
                return RedirectToAction("Login");
            if (id == 0)
                return RedirectToAction("IndexAdmin");

            return View(DB.Pedidos.GetAllPedidosByPeopleID(cadeteID: id));
        }


        // -----------------------------------------------------------------
        // --------------------------LISTA PEDIDOS--------------------------
        // -----------------------------------------------------------------
        // GET: Pedidos/IndexAdmin
        public IActionResult IndexAdmin()
        {
            if (!IsSesionIniciada())
                return RedirectToAction("Login");

            // solo los admins pueden ver todos los pedidos
            if (GetRol() == "ADMIN")
                return View(DB);

            return RedirectToAction("Index", "Home");
        }


        // -----------------------------------------------------------------
        // --------------------------CARGA PEDIDOS--------------------------
        // -----------------------------------------------------------------
        // GET: Pedidos/CreatePedido
        public IActionResult CreatePedido(PedidoCreateViewModel pedidoVM = null)
        {
            return View(pedidoVM);
        }

        // POST: Pedidos/CreatePedidoPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePedidoPost(PedidoCreateViewModel pedido)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Pedido P = mapper.Map<Pedido>(pedido);
                    P.Cliente = DB.Clientes.GetClienteById(P.Cliente.Id);

                    _logger.LogInformation($"CREACION DE PEDIDO - OBSERVACION:{P.Observacion}, IDCLIENTE:{P.Cliente.Id}, NOMBRECLIENTE:{P.Cliente.Nombre}");

                    DB.Pedidos.InsertPedido(P);
                    return RedirectToAction("IndexAdmin");
                }

                return RedirectToAction("CreatePedido");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN CREACION DE PEDIDO - EXCEPTION:{e.Message}");
                return RedirectToAction("IndexAdmin");
            }
        }


        // -----------------------------------------------------------------
        // --------------------------DELETE PEDIDOS-------------------------
        // -----------------------------------------------------------------
        // GET: Pedidos/DeletePedido/{id}
        public IActionResult DeletePedido(int id = 0)
        {
            if (!IsSesionIniciada())
                return RedirectToAction("Login");
            if (id == 0)
                return RedirectToAction("IndexAdmin");

            try
            {
                // solo pueden eliminar pedidos el admin
                if (GetRol() == "ADMIN")
                {
                    Pedido P = DB.Pedidos.GetPedidoById(id);

                    if (P != null)
                    {
                        DB.Pedidos.DeletePedido(id);
                        _logger.LogInformation($"DELETE DE PEDIDO - ID:{P.Id}, NOMBRE:{P.Nombre}, DIRECCION:{P.Direccion}, TELEFONO:{P.Telefono}");
                    }

                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN DELETE DE PEDIDO - EXCEPTION:{e.Message}");
                return RedirectToAction("IndexAdmin");
            }
        }


        // -----------------------------------------------------------------
        // --------------------------UPDATE PEDIDOS-------------------------
        // -----------------------------------------------------------------
        // GET: Pedidos/UpdatePedido/{id}
        public IActionResult UpdatePedido(int id = 0, PedidoUpdateViewModel pedidoVM = null)
        {
            if (!IsSesionIniciada())
                return RedirectToAction("Login");

            try
            {
                // solo un admin puede modificar un pedido
                if (GetRol() == "ADMIN")
                {
                    if (pedidoVM.Nombre != null)
                        return View(pedidoVM);

                    Pedido P = DB.Pedidos.GetPedidoById(id);
                    if (P != null)
                        return View(mapper.Map<PedidoUpdateViewModel>(P));
                }

                return RedirectToAction("IndexAdmin");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN UPDATE DE PEDIDO - EXCEPTION:{e.Message}");
                return RedirectToAction("IndexAdmin");
            }
        }

        // POST: Pedido/UpdatePedidoPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePedidoPost(PedidoUpdateViewModel pedido)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Pedido P = mapper.Map<Pedido>(pedido);

                    DB.Pedidos.UpdatePedido(P);
                    _logger.LogInformation($"UPDATE DE PEDIDO - ID:{P.Id}, NOMBRE:{P.Nombre}, DIRECCION:{P.Direccion}, TELEFONO:{P.Telefono}");
                    return RedirectToAction("IndexAdmin");
                }

                return RedirectToAction("UpdatePedido", pedido); // si hay un error en el ModelState
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN UPDATE DE PEDIDO - EXCEPTION:{e.Message}");
                return RedirectToAction("IndexAdmin");
            }
        }
    }
}
