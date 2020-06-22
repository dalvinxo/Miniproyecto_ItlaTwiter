using Superacion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Superacion.Models;

namespace Superacion.AutoMapperConfigure
{
    public class AutoMapperProfile: Profile
    {

        public AutoMapperProfile() {

            CreateMap<RegistroUsuarioViewModel, Usuario>();
            CreateMap<LoginUsuarioViewModels, Usuario>();
            CreateMap<ComentarioViewModels, Comentario>();
            CreateMap<PublicacionViewModel, Publicacion>();
            CreateMap<AgregarAmigoViewModels, Amigo>();

        }
       

    }

}
