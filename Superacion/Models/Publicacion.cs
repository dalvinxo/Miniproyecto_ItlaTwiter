using System;
using System.Collections.Generic;

namespace Superacion.Models
{
    public partial class Publicacion
    {
        public Publicacion()
        {
            Comentario = new HashSet<Comentario>();
        }

        public int IdPublicacion { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdUsuario { get; set; }

        public Usuario IdUsuarioNavigation { get; set; }
        public ICollection<Comentario> Comentario { get; set; }

    }
}
