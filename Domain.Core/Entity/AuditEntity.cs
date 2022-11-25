using Domain.Core.Interface;

namespace Domain.Core.Entity;

public class AuditEntity<T> : Entity<T>, IAuditEntity
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
}