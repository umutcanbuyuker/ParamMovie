using AutoMapper;
using MovieStoreWebApi.Entities.Concrete;
using MovieStoreWebApi.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreWebApi.Common.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieCreateDto>();
            CreateMap<Movie, MovieDto>().ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => $"{src.MovieActor.Select(x => x.Actor.FirstName)} {src.MovieActor.Select(x => x.Actor.FirstName)}"))
                .ForMember(dest => dest.DirectorName, opt => opt.MapFrom(src => $"{src.Director.FirstName} {src.Director.LastName}"));
            CreateMap<MovieCreateDto, Movie>();

            CreateMap<DirectorCreateDto, Director>();

            CreateMap<Actor, ActorListDto>().ForMember(dest => dest.Movies, opt => opt.MapFrom(src => src.MovieActor.Select(x => x.Movie.Name)));
            CreateMap<CustomerCreateDto, Customer>();

            CreateMap<Order, OrderListDto>().ForMember(dest => dest.Customer, opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"))
                .ForMember(dest => dest.Movie, opt => opt.MapFrom(src => src.Movie.Name));
        }
    }
}
