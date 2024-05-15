using System.Linq.Expressions;

namespace ApplicationCore.Contracts.SpecificationBase;

public class OrderByExpression<T>
{
    public Expression<Func<T, object>> Expression { get; set; }
    public string Direction { get; set; } = Constants.Sort.Descending;
}