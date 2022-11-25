using Domain.Core.Entity;

namespace Domain.Core.Auditory;

public class Audit : Entity<int>
{
    public string TableName { get; set; }
    public DateTime DateTime { get; set; }
    public string KeyValues { get; set; }
    public string OldValues { get; set; }
    public string NewValues { get; set; }
    public string User { get; set; }
}