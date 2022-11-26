using Domain.MainModule.Entity;

namespace Application.Dto.Postulant;

public class GenreDto
{
    public ICollection<MovieWithoutGenre> Movies { get; set; }

}