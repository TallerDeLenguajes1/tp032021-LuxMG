using CadeteriaWeb.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Models
{
    public class UsuarioCreateViewModel
    {
        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")] // forza que se escriba algo ahi
        [StringLength(100)] // longitud maxima / validacion
        [Display(Name = "Usuario")] // como se ve en el form
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [Compare("Password", ErrorMessage = "Ambas contraseñas deben ser iguales")]
        [StringLength(50, MinimumLength = 6)]
        [Display(Name = "Reingrese la contraseña")]
        public string PasswordRetry { get; set; }

        [Display(Name = "Error")]
        public string Message { get; set; }

        public UsuarioCreateViewModel() { }
        public UsuarioCreateViewModel(string message) { Message = message; }
    }

    public class UsuarioLoginViewModel
    {
        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")]
        [StringLength(100)]
        [Display(Name = "Usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [StringLength(50, MinimumLength = 6)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Error")]
        public string Message { get; set; }

        public UsuarioLoginViewModel() { }
        public UsuarioLoginViewModel(string message) { Message = message; }
    }

    public class UsuarioUpdateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")] 
        [StringLength(100)] 
        [Display(Name = "Usuario")] 
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [Compare("Password", ErrorMessage = "Ambas contraseñas deben ser iguales")]
        [StringLength(50, MinimumLength = 6)]
        [Display(Name = "Reingrese la contraseña")]
        public string PasswordRetry { get; set; }

        public Rol Rol { get; set; }
    }

}
