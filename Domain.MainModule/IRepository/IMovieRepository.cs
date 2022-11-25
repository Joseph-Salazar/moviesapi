using Domain.MainModule.Entity;

namespace Domain.MainModule.IRepository;

public interface IMovieRepository : IRepository<Movie, int>
{
    
}