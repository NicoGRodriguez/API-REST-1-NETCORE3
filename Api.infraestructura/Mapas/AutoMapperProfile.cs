using Api.Core.DTOs;
using Api.Core.Entidades;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.infraestructura.Mapas
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Publicacion, PublicacionDTO>();
            CreateMap<PublicacionDTO, Publicacion>();
        }
    }
}
