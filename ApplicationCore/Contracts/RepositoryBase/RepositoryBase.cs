using ApplicationCore.Constants;
using ApplicationCore.Contracts.Common;
using ApplicationCore.Contracts.SpecificationBase;
using ApplicationCore.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationCore.Contracts.RepositoryBase;

public abstract class RepositoryBase<TEntity> : QueryableSpecification<TEntity>, IRepository<TEntity> where TEntity : EntityBase
{
    private readonly AppDbContextBase _dbContext;

    protected RepositoryBase(AppDbContextBase dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<TEntity?> FindById(long id)
    {
        return await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual async Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec)
    {
        return GetQueryable(_dbContext.Set<TEntity>(), spec).AsNoTracking().ToList();
    }

    public virtual async Task<TEntity?> FindOneAsync(ISpecification<TEntity> spec)
    {
        return GetQueryable(_dbContext.Set<TEntity>(), spec).AsNoTracking().FirstOrDefault();
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> AddBatchAsync(IList<TEntity> entities)
    {
        await _dbContext.AddRangeAsync(entities);
        return Convert.ToBoolean(await _dbContext.SaveChangesAsync());
    }

    public virtual async Task<bool> UpsertAsync(ISpecification<TEntity> spec, TEntity newEntity)
    {
        TEntity entity = await FindOneAsync(spec) ?? newEntity;
        _dbContext.Attach(entity);

        return Convert.ToBoolean(await _dbContext.SaveChangesAsync());
    }

    public async Task<bool> UpsertBatchAsync(IList<TEntity> entities)
    {
        _dbContext.AttachRange(entities);
        return Convert.ToBoolean(await _dbContext.SaveChangesAsync());
    }


    public async Task<bool> RemoveAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        return Convert.ToBoolean(await _dbContext.SaveChangesAsync());
        //await _dbContext.SaveChangesAsync();
    }


    public async Task<bool> UpdateBatchAsync(IList<TEntity> entities)
    {
        _dbContext.UpdateRange(entities);

        return Convert.ToBoolean(await _dbContext.SaveChangesAsync());
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        _dbContext.Update(entity);
        return Convert.ToBoolean(await _dbContext.SaveChangesAsync());
    }


    public virtual async ValueTask<long> CountAsync(ISpecification<TEntity> spec)
    {
        spec.IsPagingEnabled = false;
        IQueryable<TEntity> specificationResult = GetQueryable(_dbContext.Set<TEntity>(), spec);

        return await ValueTask.FromResult(specificationResult.LongCount());
    }
}

public abstract class QueryableSpecification<TEntity> where TEntity : EntityBase
{
    public virtual IQueryable<TEntity> GetQueryable(
        IQueryable<TEntity> inputQuery,
        ISpecification<TEntity>? specification = default)
    {
        IQueryable<TEntity> query = inputQuery;
        if (specification is null)
        {
            return query;
        }

        if (specification.Criterias.Count > 0)
        {
            Expression<Func<TEntity, bool>> expr = specification.Criterias.First();
            for (int i = 1; i < specification.Criterias.Count; i++)
            {
                expr = expr.And(specification.Criterias.ElementAt(i));
            }

            query = query.Where(expr);
        }

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (specification.OrderBys is { Count: > 0 })
        {
            OrderByExpression<TEntity> firstOrderBy = specification.OrderBys.First();
            IOrderedQueryable<TEntity> queryable = firstOrderBy.Direction switch
            {
                Sort.Ascending => query.OrderBy(firstOrderBy.Expression),
                Sort.Descending => query.OrderByDescending(firstOrderBy.Expression)
            };

            for (int index = 1; index < specification.OrderBys.Count; index++)
            {
                OrderByExpression<TEntity> orderBy = specification.OrderBys[index];
                queryable = orderBy.Direction switch
                {
                    Sort.Ascending => queryable.ThenBy(orderBy.Expression),
                    Sort.Descending => queryable.ThenByDescending(orderBy.Expression)
                };
            }
            query = queryable;
        }

        if (specification.GroupBy is not null)
        {
            //TODO: got exception 'The LINQ expression could not be translated. Either rewrite the query in a form that can be translated, or switch to client evaluation' when use GroupBy
            //Temp convert to AsEnumerable to AsQueryable avoid exception
            query = query.GroupBy(specification.GroupBy)
                .SelectMany(x => x);
        }

        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        return query;
    }
}

public interface IRepository<TEntity> where TEntity : EntityBase
{
    public Task<TEntity?> FindById(long id);
    public Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec);
    public Task<TEntity?> FindOneAsync(ISpecification<TEntity> spec);
    public Task<TEntity> AddAsync(TEntity entity);
    public Task<bool> AddBatchAsync(IList<TEntity> entities);
    public Task<bool> UpsertAsync(ISpecification<TEntity> spec, TEntity newEntity);
    public Task<bool> UpsertBatchAsync(IList<TEntity> entities);
    public Task<bool> UpdateBatchAsync(IList<TEntity> entity);
    public Task<bool> UpdateAsync(TEntity entity);
    public Task<bool> RemoveAsync(TEntity entity);

    public ValueTask<long> CountAsync(ISpecification<TEntity> spec);
}