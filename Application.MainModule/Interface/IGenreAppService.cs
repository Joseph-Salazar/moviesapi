using Application.Dto.Postulant;
using Domain.MainModule.Entity;

namespace Application.MainModule.Interface;

public interface IGenreAppService
{
    Task<GenreDto> GetById(int genreId);
    Task<string> Update(UpdateGenreDto updateGenreDto);
    Task<string> Add(AddGenreDto addGenreDto);
    List<GenreDto> ListAll();
}