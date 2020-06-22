using System;
using System.Collections.Generic;

namespace Superacion.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            AmigoIdAmigoBNavigation = new HashSet<Amigo>();
            AmigoIdUsuarioANavigation = new HashSet<Amigo>();
            Comentario = new HashSet<Comentario>();
            Publicacion = new HashSet<Publicacion>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Usuario1 { get; set; }
        public string Clave { get; set; }
        public string Estado{ get; set; }

        public ICollection<Amigo> AmigoIdAmigoBNavigation { get; set; }
        public ICollection<Amigo> AmigoIdUsuarioANavigation { get; set; }
        public ICollection<Comentario> Comentario { get; set; }
        public ICollection<Publicacion> Publicacion { get; set; }
    }
}
