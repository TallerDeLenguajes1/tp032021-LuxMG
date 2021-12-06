using AutoMapper;
using CadeteriaWeb.Entities;
using CadeteriaWeb.Models;
using CadeteriaWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Controllers
{
    public class UsuarioController : SessionController
    {
        private readonly DataContext DB;
        private readonly Logger nlog;
        private readonly IMapper mapper;

        public UsuarioController(DataContext DB, Logger nlog, IMapper mapper)
        {
            this.DB = DB;
            this.nlog = nlog;
            this.mapper = mapper;
        }

        // -------------------------LISTA USUARIOS-------------------------
        // INDEX: Usuario/
        public IActionResult Index()
        {
            if (GetRol() == "ADMIN")
                return View(DB);

            return RedirectToAction("Index", "Home");
        }

        // -------------------------LOGUEO USUARIOS-------------------------
        // GET: Usuario/Login
        public IActionResult Login(string message = "")
        {
            if (!IsSesionIniciada())
                return View(new UsuarioLoginViewModel(message));

            return RedirectToAction("Index", "Home");
        }

        // POST: Usuario/LoginPost
        [HttpPost]
        public IActionResult LoginPost(UsuarioLoginViewModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario U = DB.Usuarios.Validate(usuario.Username, usuario.Password);
                    
                    if (U != null)
                    {
                        SetSesion(U);
                        nlog.Info($"LOGUEO DE USUARIO - ID:{U.Id}, USERNAME:{U.Username}, ROL:{U.Rol}");

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        string message = "No existe un usuario con ese nombre";

                        if (DB.Usuarios.GetUsuarioByName(usuario.Username) != null)
                            message = "Contraseña incorrecta";
                        
                        return RedirectToAction("Login", message);
                    }
                }

                return RedirectToAction("Login");
            }
            catch (Exception e)
            {
                nlog.Error($"ERROR EN LOGUEO DE USUARIO - EXCEPTION:{e.Message}");
                return RedirectToAction("Login");
            }        
        }
        // -------------------------REGISTRO USUARIOS-------------------------
        // GET: Usuario/Register
        public IActionResult Register(string message = "")
        {
            if (!IsSesionIniciada())
                return View(new UsuarioCreateViewModel(message)); // el registro utiliza como metodo post el CreateUsuarioPost

            return RedirectToAction("Index", "Home");
        }

        // -------------------------CARGA USUARIOS-------------------------
        // GET: Usuario/CreateUsuario
        public IActionResult CreateUsuario(string message = "")
        {
            return View(new UsuarioCreateViewModel(message));
        }

        // POST: Usuario/CreateUsuarioPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUsuarioPost(UsuarioCreateViewModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (DB.Usuarios.GetUsuarioByName(usuario.Username) != null)
                    {
                        string message = "Ya existe un usuario con ese nombre";
                        return RedirectToAction("CreateUsuario", message);
                    }
                        
                    Usuario U = mapper.Map<Usuario>(usuario);
                    U.Rol = Rol.USER;

                    nlog.Info($"CREACION DE USUARIO - USERNAME:{U.Username}");

                    DB.Usuarios.InsertUsuario(U);
                    return RedirectToAction("Login");
                }

                return RedirectToAction("CreateUsuario");
            }
            catch (Exception e)
            {
                nlog.Error($"ERROR EN CREACION DE USUARIO - EXCEPTION:{e.Message}");
                return RedirectToAction("Index");
            }
        }

        // -------------------------DELETE USUARIOS-------------------------
        // GET: Usuario/DeleteUsuario
        public IActionResult DeleteUsuario(int id = 0)
        {
            try
            {
                if (id == 0)
                    return RedirectToAction("Index");

                if (GetRol() == "ADMIN" || GetIdUsuario() == id)
                {
                    Usuario U = DB.Usuarios.GetUsuarioById(id);

                    if (U != null)
                    {
                        DB.Usuarios.DeleteUsuario(id);
                        nlog.Info($"DELETE DE USUARIO - ID:{U.Id}, USERNAME:{U.Username}, ROL:{U.Rol}");
                        if (GetIdUsuario() == id) Logout();
                    }   
                    
                }
                
                return RedirectToAction("Login");
            }
            catch (Exception e)
            {
                nlog.Error($"ERROR EN DELETE DE USUARIO - EXCEPTION:{e.Message}");
                return RedirectToAction("Index");
            }
        }

        // -------------------------UPDATE USUARIOS-------------------------
        // GET: Usuario/UpdateUsuario
        public IActionResult UpdateUsuario(int id = 0)
        {
            try
            {
                if (id == 0)
                    return RedirectToAction("Index");

                if (GetRol() == "ADMIN" || GetIdUsuario() == id)
                {
                    Usuario U = DB.Usuarios.GetUsuarioById(id);
                    if (U != null)
                        return View(mapper.Map<UsuarioUpdateViewModel>(U));
                }
                
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                nlog.Error($"ERROR EN UPDATE DE USUARIO - EXCEPTION:{e.Message}");
                return RedirectToAction("Index");
            }
        }

        // POST: Usuario/UpdateUsuarioPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUsuarioPost(UsuarioUpdateViewModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (GetRol() == "ADMIN" || GetIdUsuario() == usuario.Id)
                    {
                        Usuario U = mapper.Map<Usuario>(usuario);

                        DB.Usuarios.UpdateUsuario(U);
                        nlog.Info($"UPDATE DE USUARIO - ID:{U.Id}, USERNAME:{U.Username}, ROL:{U.Rol}");
                    }
                }

                return RedirectToAction("UpdateUsuario", usuario.Id);
            }
            catch (Exception e)
            {
                nlog.Error($"ERROR EN UPDATE DE USUARIO - EXCEPTION:{e.Message}");
                return RedirectToAction("Index");
            }
        }
    }
}
