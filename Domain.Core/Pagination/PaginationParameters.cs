using System.Linq.Expressions;
using Infrastructure.CrossCutting.Enum;
using Microsoft.EntityFrameworkCore.Query;

namespace Domain.Core.Pagination;

public class PaginationParameters<T> where T : class
{
    public LambdaExpression? SortField { get; set; }
    public SortTypeEnum SortType { get; set; }
    public int Start { get; set; }
    public int AmountRows { get; set; }
    public bool IgnorePagination { get; set; }
    public Expression<Func<T, bool>> WhereFilter { get; set; }
    public Func<IQueryable<T>, IIncludableQueryable<T, object>>? Includes { get; set; }
}