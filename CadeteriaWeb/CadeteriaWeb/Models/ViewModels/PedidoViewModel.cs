using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CadeteriaWeb.Entities;

namespace CadeteriaWeb.Models
{
    public class PedidoCreateViewModel
    {
        [Required(ErrorMessage = "Debe ingresar una observacion")] // forza que se escriba algo ahi
        [StringLength(255)] // longitud maxima / validacion
        [Display(Name = "Observación")] // como se ve en el form
        public string Observacion { get; set; }

        [Required(ErrorMessage = "Debe asignar un cliente")]
        public int IdCliente { get; set; }

        [Display(Name = "Error")]
        public string Message { get; set; }
    }


    public class PedidoUpdateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar una observacion")] // forza que se escriba algo ahi
        [StringLength(255)] // longitud maxima / validacion
        [Display(Name = "Observación")] // como se ve en el form
        public string Observacion { get; set; }

        [Required(ErrorMessage = "Debe asignar un cliente")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Debe asignar un cadete")]
        public int IdCadete { get; set; }

        [Display(Name = "Error")]
        public string Message { get; set; }
    }
}
