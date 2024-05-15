using ApplicationCore.Constants;
using System.Linq.Expressions;
using VELA.WebCoreBase.Core.Models;

namespace ApplicationCore.Contracts.SpecificationBase;

public abstract class SpecificationBase<T> : ISpecification<T>, IRootSpecification
{
    public virtual List<Expression<Func<T, bool>>> Criterias { get; } = new();
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public List<string> IncludeStrings { get; } = new();
    public List<OrderByExpression<T>> OrderBys { get; } = new();
    public Expression<Func<T, object>> GroupBy { get; private set; }

    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; set; }

    protected ISpecification<T> ApplyFilterList(IEnumerable<FilterModel> filters)
    {
        foreach ((string fieldName, string comparison, string fieldValue) in filters)
        {
            ApplyFilter(PredicateBuilder.Build<T>(fieldName, comparison, fieldValue));
        }

        return this;
    }

    protected ISpecification<T> ApplyFilter(Expression<Func<T, bool>> expr)
    {
        Criterias.Add(expr);

        return this;
    }

    protected void ApplyIncludeList(IEnumerable<Expression<Func<T, object>>> includes)
    {
        foreach (Expression<Func<T, object>> include in includes)
        {
            AddInclude(include);
        }
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void ApplyIncludeList(IEnumerable<string> includes)
    {
        foreach (string include in includes)
        {
            AddInclude(include);
        }
    }

    private void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    protected void ApplyPaging(int page, int take)
    {
        if (page > 1)
        {
            Skip = (page - 1) * take;
        }

        Take = take;
        IsPagingEnabled = true;
    }

    protected void ApplySortList(IEnumerable<string> sorts)
    {
        foreach (string sort in sorts)
        {
            ApplySort(sort);
        }
    }

    protected void ApplySort(string sort)
    {
        this.ApplySort(sort, nameof(ApplyOrderByDescending), nameof(ApplyOrderBy));
    }

    protected void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
    {
        GroupBy = groupByExpression;
    }

    protected void ApplyOrderBy(Expression<Func<T, object>> expression)
    {
        OrderByExpression<T> orderBy = new()
        {
            Expression = expression,
            Direction = Sort.Ascending
        };
        OrderBys.Add(orderBy);
    }

    protected void ApplyOrderByDescending(Expression<Func<T, object>> expression)
    {
        OrderByExpression<T> orderBy = new()
        {
            Expression = expression,
            Direction = Sort.Descending
        };
        OrderBys.Add(orderBy);
    }
}