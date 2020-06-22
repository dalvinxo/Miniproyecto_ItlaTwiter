using Superacion.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Superacion.ViewModels
{
    public class PublicacionViewModel
    {
        
        public int? IdPublicacion { get; set; }

        [Required(ErrorMessage = "Este campo no puede estar vacio si quieres publicar")]
        [Display(Name = "Titulo de la Publicacion:")]
        [StringLength(80)]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "Este campo no puede estar vacio si quieres publicar")]
        [Display(Name = "Cuerpo de la publicación:")]
        [StringLength(1500)]
        public string Cuerpo { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdUsuario { get; set; }
        
        public IEnumerable<PublicacionViewModel> Public { get; set; }
        public IEnumerable<ComentarioViewModels> Coment { get; set; }
        public IEnumerable<PublicacionAmigoViewModels> AmigosPublic { get; set; }

        public IEnumerable<AmigoViewModels> AmigosAdd { get; set; }
    }



}
