using System.Linq.Expressions;

namespace ApplicationCore.Contracts.SpecificationBase;

public interface IRootSpecification
{
}

public interface ISpecification<T> : IRootSpecification
{
    List<Expression<Func<T, bool>>> Criterias { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    List<OrderByExpression<T>> OrderBys { get; }
    Expression<Func<T, object>>? GroupBy { get; }

    int Take { get; }
    int Skip { get; }


    /// <summary>
    ///     This only use for Pagination
    /// </summary>
    public bool IsPagingEnabled { get; set; }
}