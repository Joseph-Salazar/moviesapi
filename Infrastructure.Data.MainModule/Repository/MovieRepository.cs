using Domain.MainModule.Entity;
using Domain.MainModule.IRepository;
using Infrastructure.Data.MainModule.Context;

namespace Infrastructure.Data.MainModule.Repository;

public class MovieRepository : GenericRepository<Movie, int>, IMovieRepository
{
    public MovieRepository(MainContext mainContext) : base(mainContext)
    {

    }
}