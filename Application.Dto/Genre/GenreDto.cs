using Domain.MainModule.Entity;

namespace Application.Dto.Postulant;

public class GenreDto
{
    public int Id { get; set; }
    public string GenreName { get; set; }
    public ICollection<MovieWithoutGenre> Movies { get; set; }

}