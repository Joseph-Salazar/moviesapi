using Domain.MainModule.IUnitOfWork;
using Infrastructure.Data.MainModule.Context;

namespace Infrastructure.Data.MainModule.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly MainContext _mainContext;
    private bool _disposed;

    //MainContext para sobreescribir metodo savechangeasync
    public UnitOfWork(MainContext mainContext)
    {
        _mainContext = mainContext;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Task SaveChangesAsync()
    {
        return _mainContext.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        await _mainContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _mainContext.SaveChangesAsync();
        await _mainContext.Database.CommitTransactionAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _mainContext.Dispose();
        _disposed = true;
    }
}