using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Models
{
    public class CadeteViewModel
    {
        public int IdCadete { get; set; }

        [Required(ErrorMessage = "")] // forza que se escriba algo ahi
        [StringLength(100)] // longitud maxima / validacion
        [Display(Name = "Nomvre")] // como se ve en el form
        public string Nombre { get; set; }


    }
}
