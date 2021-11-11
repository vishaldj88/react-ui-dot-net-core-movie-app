using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaApi.Entities;

namespace CinemaApi.AutoMapper
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<CinemaModel, CinemaDto>()
               .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
               .ForMember(dest => dest.Director, act => act.MapFrom(src => src.Director))
               .ForMember(dest => dest.Producer, act => act.MapFrom(src => src.Producer))
               .ForMember(dest => dest.Release, act => act.MapFrom(src => src.Release))
               .ForMember(dest => dest.Hit, act => act.MapFrom(src => src.Hit));
            CreateMap<CinemaDto, CinemaModel>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
               .ForMember(dest => dest.Director, act => act.MapFrom(src => src.Director))
               .ForMember(dest => dest.Producer, act => act.MapFrom(src => src.Producer))
               .ForMember(dest => dest.Release, act => act.MapFrom(src => src.Release))
               .ForMember(dest => dest.Hit, act => act.MapFrom(src => src.Hit));
        }
    }
}
