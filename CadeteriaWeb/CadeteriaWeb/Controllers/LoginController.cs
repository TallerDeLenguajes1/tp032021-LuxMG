using CadeteriaWeb.Entities;
using CadeteriaWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Controllers
{
    public class LoginController : SessionController
    {
        private readonly DataBase DB;

        public LoginController(DataBase _DB)
        {
            this.DB = _DB;
        }

        // -------------------------LOGUEO USUARIOS-------------------------
        // GET: Usuario/Login
        public IActionResult Login()
        {
            return View(new UsuarioLoginViewModel());
        }

        // POST: Usuario/Login
        [HttpPost]
        public IActionResult Login(UsuarioLoginViewModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario Usuario = DB.RepoUsuario.Validate(usuario.Username, usuario.Password);
                    SetSesion(Usuario);

                    if (Usuario != null)
                    {
                        switch (Usuario.Rol)
                        {
                            case Rol.ADMIN:
                                return RedirectToAction("Index", "Home");

                            case Rol.USER:
                                return RedirectToAction("UserInfo", "User", DB);
                        }
                    }
                }                    
            }
            catch (Exception e)
            {
                return View("Login");
            }

            return View("Login");
        }

        // -------------------------CARGA USUARIOS-------------------------
        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View(new UsuarioCreateViewModel());            
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsuarioCreateViewModel Usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario U = new Usuario
                    {
                        Username = Usuario.Username,
                        Password = Usuario.Password,
                        Rol = Rol.USER
                    };

                    DB.RepoUsuario.Insert(U);
                    return View("Login");
                }
            }
            catch (Exception e)
            {
                return View("Index");
            }

            return View("Create");
        }

        public IActionResult BajaUsuario()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                string error = e.Message;
                return View(nameof(Login));
            }
        }

        public IActionResult BajaUsuario(Usuario Usuario)
        {
            try
            {
                DB.RepositorioUsuarios.DesactivarUsuario(Usuario.ID);
                return View(nameof(Login));
            }
            catch (Exception e)
            {
                string error = e.Message;
                return View(nameof(Login));
            }
        }
    }
}
