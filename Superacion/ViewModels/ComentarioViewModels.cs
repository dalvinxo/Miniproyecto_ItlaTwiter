using Superacion.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Superacion.ViewModels
{
    public class ComentarioViewModels
    {

        public int IdComentario { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdPublicacion { get; set; }
        public string Comentario1 { get; set; }
        public DateTime? Fecha { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

    }

}
