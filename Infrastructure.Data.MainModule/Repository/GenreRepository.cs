using Domain.MainModule.Entity;
using Domain.MainModule.IRepository;
using Infrastructure.Data.MainModule.Context;

namespace Infrastructure.Data.MainModule.Repository;

public class GenreRepository : GenericRepository<Genre, int>, IGenreRepository
{
    public GenreRepository(MainContext mainContext) : base(mainContext)
    {

    }
}