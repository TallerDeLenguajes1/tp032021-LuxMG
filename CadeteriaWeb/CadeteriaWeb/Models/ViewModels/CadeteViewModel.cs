using CadeteriaWeb.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Models
{
    public class CadeteCreateViewModel
    {
        [Required(ErrorMessage = "Debe ingresar un nombre")] // forza que se escriba algo ahi
        [StringLength(100)] // longitud maxima / validacion
        [Display(Name = "Nombre completo")] // como se ve en el form
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar una direccion")] // forza que se escriba algo ahi
        [StringLength(100)] // longitud maxima / validacion
        [Display(Name = "Dirección")] // como se ve en el form
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número de teléfono")] // forza que se escriba algo ahi
        [StringLength(100)] // longitud maxima / validacion
        [Display(Name = "Nro de telefono")] // como se ve en el form
        public string Telefono { get; set; }

        [Display(Name = "Error")]
        public string Message { get; set; }
    }


    public class CadeteUpdateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")] // forza que se escriba algo ahi
        [StringLength(100)] // longitud maxima / validacion
        [Display(Name = "Nombre completo")] // como se ve en el form
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar una direccion")] // forza que se escriba algo ahi
        [StringLength(100)] // longitud maxima / validacion
        [Display(Name = "Dirección")] // como se ve en el form
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe ingresar un número de teléfono")] // forza que se escriba algo ahi
        [StringLength(100)] // longitud maxima / validacion
        [Display(Name = "Nro de telefono")] // como se ve en el form
        public string Telefono { get; set; }

        [Display(Name = "Error")]
        public string Message { get; set; }
    }

}
