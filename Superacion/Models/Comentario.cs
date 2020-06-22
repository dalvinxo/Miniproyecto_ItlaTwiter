using System;
using System.Collections.Generic;

namespace Superacion.Models
{
    public partial class Comentario
    {
        public int IdComentario { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdPublicacion { get; set; }
        public string Comentario1 { get; set; }
        public DateTime? Fecha { get; set; }

        public Publicacion IdPublicacionNavigation { get; set; }
        public Usuario IdUsuarioNavigation { get; set; }
    }
}
