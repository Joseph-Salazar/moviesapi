using Domain.MainModule.Entity;

namespace Application.Dto.Postulant;

public class GenreDto
{
    public string GenreName { get; set; }
    public ICollection<MovieWithoutGenre> Movies { get; set; }

}