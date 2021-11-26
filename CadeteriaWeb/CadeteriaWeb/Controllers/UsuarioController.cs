using AutoMapper;
using CadeteriaWeb.Entities;
using CadeteriaWeb.Models;
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
        private readonly DataBase DB;
        private readonly Logger nlog;
        private readonly IMapper mapper;

        public UsuarioController(DataBase DB, Logger nlog, IMapper mapper)
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

            return View("Index", "Home");
        }

        // -------------------------LOGUEO USUARIOS-------------------------
        // GET: Usuario/Login
        public IActionResult Login(string message = "")
        {
            if (!IsSesionIniciada())
                return View(new UsuarioLoginViewModel(message));

            return View("Index", "Home");
        }

        // POST: Usuario/Login
        [HttpPost]
        public IActionResult Login(UsuarioLoginViewModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario U = DB.RepoUsuario.Validate(usuario.Username, usuario.Password);
                    
                    if (U != null)
                    {
                        SetSesion(U);
                        nlog.Info($"LOGUEO DE USUARIO - ID:{U.Id}, USERNAME:{U.Username}, ROL:{U.Rol}");

                        return View("Index", "Home");
                    }
                    else
                    {
                        string message = "Usuario o contraseña incorrectos";
                        View("Login", message);
                    }
                }

                return View("Login");
            }
            catch (Exception e)
            {
                nlog.Error($"ERROR EN LOGUEO DE USUARIO - EXCEPTION:{e.Message}");
                return View("Login");
            }        
        }
        // -------------------------REGISTRO USUARIOS-------------------------
        // GET: Usuario/Register

        // -------------------------CARGA USUARIOS-------------------------
        // GET: Usuario/CreateUsuario
        public IActionResult CreateUsuario(string message = "")
        {
            return View(new UsuarioCreateViewModel(message));
        }

        // POST: Usuario/CreateUsuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUsuario(UsuarioCreateViewModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (DB.RepoUsuario.GetItemByName(usuario.Username) == null)
                    {
                        string message = "Ya existe un usuario con ese nombre";
                        return View("CreateUsuario", message);
                    }
                        
                    Usuario U = mapper.Map<Usuario>(usuario);
                    U.Rol = Rol.USER;

                    nlog.Info($"CREACION DE USUARIO - USERNAME:{U.Username}");

                    DB.RepoUsuario.Insert(U);
                    return View("Login");
                }

                return View("CreateUsuario");
            }
            catch (Exception e)
            {
                nlog.Error($"ERROR EN CREACION DE USUARIO - EXCEPTION:{e.Message}");
                return View("Index");
            }
        }

        // -------------------------DELETE USUARIOS-------------------------
        // GET: Usuario/DeleteUsuario
        public IActionResult DeleteUsuario(int id = 0)
        {
            try
            {
                if (id == 0)
                    return View("Index");

                if (GetRol() == "ADMIN" || GetIdUsuario() == id)
                {
                    Usuario U = DB.RepoUsuario.GetItemById(id);

                    if (U != null)
                    {
                        DB.RepoUsuario.Delete(id);
                        nlog.Info($"DELETE DE USUARIO - ID:{U.Id}, USERNAME:{U.Username}, ROL:{U.Rol}");
                        if (GetIdUsuario() == id) Logout();
                    }   
                    
                }
                
                return View("Login");
            }
            catch (Exception e)
            {
                nlog.Error($"ERROR EN DELETE DE USUARIO - EXCEPTION:{e.Message}");
                return View("Index");
            }
        }

        // -------------------------UPDATE USUARIOS-------------------------
        // GET: Usuario/UpdateUsuario
        public IActionResult UpdateUsuario(int id = 0)
        {
            try
            {
                if (id == 0)
                    return View("Index");

                if (GetRol() == "ADMIN" || GetIdUsuario() == id)
                {
                    Usuario U = DB.RepoUsuario.GetItemById(id);
                    if (U != null)
                        return View(mapper.Map<UsuarioUpdateViewModel>(U));
                }
                
                return View("Index");
            }
            catch (Exception e)
            {
                nlog.Error($"ERROR EN UPDATE DE USUARIO - EXCEPTION:{e.Message}");
                return View("Index");
            }
        }

        // POST: Usuario/UpdateUsuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUsuario(UsuarioUpdateViewModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (GetRol() == "ADMIN" || GetIdUsuario() == usuario.Id)
                    {
                        Usuario U = mapper.Map<Usuario>(usuario);

                        DB.RepoUsuario.Update(U);
                        nlog.Info($"UPDATE DE USUARIO - ID:{U.Id}, USERNAME:{U.Username}, ROL:{U.Rol}");
                    }
                }

                return View("UpdateUsuario", usuario.Id);
            }
            catch (Exception e)
            {
                nlog.Error($"ERROR EN UPDATE DE USUARIO - EXCEPTION:{e.Message}");
                return View("Index");
            }
        }
    }
}
