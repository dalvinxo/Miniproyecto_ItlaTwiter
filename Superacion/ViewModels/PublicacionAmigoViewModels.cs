using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Superacion.ViewModels
{
    public class PublicacionAmigoViewModels
    {
        public int? IdPublicacion { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }


    }
}
