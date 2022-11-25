using Application.Dto.Postulant;
using Domain.MainModule.Entity;

namespace Application.MainModule.Interface;

public interface IMovieAppService
{
    Task<MovieDto> GetById(int movieId);
    Task<string> Update(UpdateMovieDto updateMovieDto);
    Task<string> Add(AddMovieDto addMovieDto);
    Task<string> AddGenreToMovie(int movieId, int genreId);
    Task<string> Delete(int movieId);
    List<MovieDto> ListAll();
}