using Superacion.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Superacion.ViewModels
{
    public class LoginUsuarioViewModels
    {

        [Required(ErrorMessage = "Debes colocar un usuario!")]
        [Display(Name = "Usuario: ")]
        [StringLength(30)]
        public string Usuario1 { get; set; }

        [Required(ErrorMessage ="Debes colocar una contraseña!")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [StringLength(25)]
        public string Clave { get; set; }

    }

}
