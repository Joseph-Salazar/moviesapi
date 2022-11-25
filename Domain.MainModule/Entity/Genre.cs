using Domain.Core.Entity;

namespace Domain.MainModule.Entity;

public class Genre : Entity<int>
{
    public Genre()
    {
        this.Movies = new HashSet<Movie>();
    }

    public string GenreName { get; set; }
    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}