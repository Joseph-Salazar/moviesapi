using Application.Dto.Postulant;
using AutoMapper;
using Domain.MainModule.Entity;

namespace Application.MainModule.AutoMapper.Profiles;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreDto>().ReverseMap();
        CreateMap<Genre, AddGenreDto>().ReverseMap();
        CreateMap<Genre, UpdateGenreDto>().ReverseMap();
    }
}