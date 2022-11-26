using Application.Dto.Postulant;
using Domain.MainModule.Entity;

namespace Application.MainModule.Interface;

public interface IGenreAppService
{
    Task<GenreDto> GetById(string genreId);
    Task<string> Update(UpdateGenreDto updateGenreDto);
    Task<string> Add(AddGenreDto addGenreDto);
    Task<string> Delete(int genreId);
    List<GenreDto> ListAll();
}