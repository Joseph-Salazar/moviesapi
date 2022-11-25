using Domain.Core.Auditory;
using Domain.Core.Interface;
using Domain.MainModule.Entity;
using Infrastructure.CrossCutting.Constants;
using Infrastructure.CrossCutting.Enum;
using Infrastructure.CrossCutting.Helpers;
using Infrastructure.Data.MainModule.EntityConfig;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.MainModule.Context;

public class MainContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MainContext(DbContextOptions<MainContext> options,
        IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
        CurrentUsername = GetCurrentUsername();
    }

    public string CurrentUsername { get; set; }

    #region DbSet

    public DbSet<Audit> Audits { get; set; }
    public DbSet<Movie> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
  

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new MovieConfig());
        builder.ApplyConfiguration(new GenreConfig());
    }

    #endregion

    #region OnSaveChanges

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        UpdateAuditEntities();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    #endregion

    #region Private Methods

    private void UpdateAuditEntities()
    {
        var modifiedEntries = ChangeTracker.Entries()
            .Where(x => x.Entity is IAuditEntity && (x.State == EntityState.Added
                                                     || x.State == EntityState.Modified));

        foreach (var entry in modifiedEntries)
        {
            var entity = (IAuditEntity) entry.Entity;
            DateTime now = CoreHelper.GetDateTimePeru();

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = now;
                entity.CreatedBy = CurrentUsername;
            }
            else
            {
                base.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;

                entity.ModifiedAt = now;
                entity.ModifiedBy = CurrentUsername;
            }
        }
    }

    private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
    {
        if (auditEntries == null || auditEntries.Count == 0)
            return Task.CompletedTask;

        foreach (var auditEntry in auditEntries)
        {
            // Get the final value of the temporary properties
            foreach (var prop in auditEntry.TemporaryProperties)
            {
                if (prop.Metadata.IsPrimaryKey())
                    auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                else
                    auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
            }

            try
            {
                // Save the Audit entry
                Audits.Add(auditEntry.ToAudit());
            }
            catch (Exception)
            {
                // ignored
            }
        }

        return SaveChangesAsync();
    }

    private string GetCurrentUsername()
    {
        string currentUsername = HelperConst.SystemUser;

        if (_httpContextAccessor.HttpContext == null
            || !_httpContextAccessor.HttpContext.User.Claims.Any())
            return currentUsername;

        var claimsPrincipal = _httpContextAccessor.HttpContext.User;
        currentUsername = claimsPrincipal.FindFirst(ClaimType.UserName)!.Value;

        return currentUsername;
    }

    #endregion
}