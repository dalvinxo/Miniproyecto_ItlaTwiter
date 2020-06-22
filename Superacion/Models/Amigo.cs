using System;
using System.Collections.Generic;

namespace Superacion.Models
{
    public partial class Amigo
    {
        public int IdUsuarioA { get; set; }
        public int IdAmigoB { get; set; }

        public Usuario IdAmigoBNavigation { get; set; }
        public Usuario IdUsuarioANavigation { get; set; }
    }
}
