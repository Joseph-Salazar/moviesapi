using Domain.Core.Entity;

namespace Domain.MainModule.Entity;

public class Genre : Entity<int>
{
    public string GenreName { get; set; }
    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}