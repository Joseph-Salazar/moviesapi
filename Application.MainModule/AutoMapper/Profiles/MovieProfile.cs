using Application.Dto.Postulant;
using AutoMapper;
using Domain.MainModule.Entity;

namespace Application.MainModule.AutoMapper.Profiles;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, MovieDto>().ReverseMap();
        CreateMap<Movie, AddMovieDto>().ReverseMap();
        CreateMap<Movie, UpdateMovieDto>().ReverseMap();
    }
}