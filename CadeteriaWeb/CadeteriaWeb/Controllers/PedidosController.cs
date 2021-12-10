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
                return RedirectToAction("Login", "Usuarios");
            if (id == 0)
                return RedirectToAction("IndexAdmin");

            var P = DB.Pedidos.GetPedidoById(id);
            if(P == null)
                return RedirectToAction("IndexAdmin");

            return View(P);
        }


        // -----------------------------------------------------------------
        // ---------------------------INFO CLIENTE--------------------------
        // -----------------------------------------------------------------
        // INDEX: Pedidos/VerCliente/{id}
        public IActionResult VerCliente(int id = 0)
        {
            if (!IsSesionIniciada())
                return RedirectToAction("Login", "Usuarios");
            if (id == 0)
                return RedirectToAction("IndexAdmin");

            var C = DB.Clientes.GetClienteById(id);
            if (C == null)
                return RedirectToAction("IndexAdmin");

            return View(C);
        }


        // -----------------------------------------------------------------
        // --------------------------LISTA PEDIDOS--------------------------
        // -----------------------------------------------------------------
        // GET: Pedidos/IndexAdmin
        public IActionResult IndexAdmin()
        {
            if (!IsSesionIniciada())
                return RedirectToAction("Login", "Usuarios");

            // solo los admins pueden ver todos los pedidos
            if (GetRol() == "ADMIN")
                return View(DB);

            return RedirectToAction("Index", "Home");
        }


        // -----------------------------------------------------------------
        // --------------------------CARGA PEDIDOS--------------------------
        // -----------------------------------------------------------------
        // GET: Pedidos/CreatePedido
        public IActionResult CreatePedido(int id = 0)
        {
            PedidoCreateViewModel pedidoVM = new();
            pedidoVM.IdCadete = id;

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
                    Pedido P = new()
                    {
                        Observacion = pedido.Observacion,
                        Estado = EstadoPedido.Procesando,
                        Cliente = new Cliente(pedido.NombreCliente, pedido.DireccionCliente, pedido.TelefonoCliente),
                        Cadete = DB.Cadetes.GetCadeteById(pedido.IdCadete)
                    };
                    if (P.Cadete != null) P.Estado = EstadoPedido.En_Camino;
                    DB.Clientes.InsertCliente(P.Cliente); // se crea el cliente
                    P.Cliente = DB.Clientes.GetClienteByInfo(P.Cliente.Nombre, P.Cliente.Direccion, P.Cliente.Telefono); // para traer el id
                    DB.Pedidos.InsertPedido(P);

                    _logger.LogInformation($"CREACION DE PEDIDO - OBSERVACION:{P.Observacion}, " +
                        $"NOMBRECLIENTE:{P.Cliente.Nombre}, DIRECCIONCLIENTE:{P.Cliente.Direccion}, TELEFONOCLIENTE:{P.Cliente.Telefono}");
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
                return RedirectToAction("Login", "Usuarios");
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
                        DB.Clientes.DeleteCliente(P.Cliente.Id);
                        DB.Pedidos.DeletePedido(id);
                        _logger.LogInformation($"DELETE DE PEDIDO - ID:{P.Id}, OBSERVACION:{P.Observacion}, " +
                            $"IDCLIENTE:{P.Cliente.Id}, IDCADETE:{P.Cadete?.Id}");
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
        public IActionResult UpdatePedido(int id = 0)
        {
            if (!IsSesionIniciada())
                return RedirectToAction("Login", "Usuarios");

            try
            {
                Pedido P = DB.Pedidos.GetPedidoById(id);
                if (P != null)
                {
                    PedidoUpdateViewModel pedido = new()
                    {
                        Id = P.Id,
                        Observacion = P.Observacion,
                        Estado = P.Estado.ToString(),
                        NombreCliente = P.Cliente.Nombre,
                        DireccionCliente = P.Cliente.Direccion,
                        TelefonoCliente= P.Cliente.Telefono,
                        IdCliente = P.Cliente.Id,
                        Cadetes = DB.Cadetes.GetAllCadetes()
                    };

                    if (P.Cadete == null)
                    {
                        pedido.IdCadete = 0;
                    }
                    else
                    {
                        pedido.IdCadete = P.Cadete.Id;
                    }

                    return View(pedido);
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
                    Pedido P = new()
                    {
                        Id = pedido.Id,
                        Observacion = pedido.Observacion,
                        Estado = (EstadoPedido)Enum.Parse(typeof(EstadoPedido), pedido.Estado),
                        Cliente = new Cliente(pedido.IdCliente, pedido.NombreCliente, pedido.DireccionCliente, pedido.TelefonoCliente, true),
                        Cadete = null
                    };

                    if (pedido.IdCadete != 0)
                        P.AsignarCadete(DB.Cadetes.GetCadeteById(pedido.IdCadete));

                    DB.Clientes.UpdateCliente(P.Cliente); // se modifica el cliente
                    DB.Pedidos.UpdatePedido(P);

                    _logger.LogInformation($"UPDATE DE PEDIDO - ID:{P.Id}, OBSERVACION:{P.Observacion}, ESTADO:{P.Estado}, " +
                        $"IDCLIENTE:{P.Cliente.Id}, IDCADETE:{P.Cadete?.Id}");
                    return RedirectToAction("IndexAdmin");
                }

                return RedirectToAction("UpdatePedido", pedido.Id); // si hay un error en el ModelState
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN UPDATE DE PEDIDO - EXCEPTION:{e.Message}");
                return RedirectToAction("IndexAdmin");
            }
        }

        // -----------------------------------------------------------------
        // -------------------------ACCIONES PEDIDOS------------------------
        // -----------------------------------------------------------------
        // GET: Pedidos/EntregarPedido/{id}
        public IActionResult EntregarPedido(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("IndexAdmin");

            try
            {
                Pedido P = DB.Pedidos.GetPedidoById(id);

                if (P != null)
                {
                    P.Entregar();
                    DB.Pedidos.UpdatePedido(P);
                    _logger.LogInformation($"CANCELACION DE PEDIDO - ID:{P.Id}, OBSERVACION:{P.Observacion}, ESTADO:{P.Estado}, " +
                        $"IDCLIENTE:{P.Cliente.Id}, IDCADETE:{P.Cadete.Id}");
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN CANCELACION DE PEDIDO - EXCEPTION:{e.Message}");
                return RedirectToAction("IndexAdmin");
            }
        }

        // POST: Pedidos/CancelarPedido/{id}
        public IActionResult CancelarPedido(int id = 0)
        {
            if (id == 0)
                return RedirectToAction("IndexAdmin");

            try
            {
                Pedido P = DB.Pedidos.GetPedidoById(id);

                if (P != null)
                {
                    P.Cancelar();
                    DB.Pedidos.UpdatePedido(P);
                    _logger.LogInformation($"CANCELACION DE PEDIDO - ID:{P.Id}, OBSERVACION:{P.Observacion}, ESTADO:{P.Estado}, " +
                        $"IDCLIENTE:{P.Cliente.Id}, IDCADETE:{P.Cadete?.Id}");
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN CANCELACION DE PEDIDO - EXCEPTION:{e.Message}");
                return RedirectToAction("IndexAdmin");
            }
        }
    }
}
