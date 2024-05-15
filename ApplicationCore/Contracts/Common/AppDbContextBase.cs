using ApplicationCore.Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics;
using VELA.WebCoreBase.Core.Entities;
using VELA.WebCoreBase.Core.Persistence;

namespace ApplicationCore.Contracts.Common;

public abstract class AppDbContextBase : DbContext, IDbFacadeContext, IDomainEventContext, IModifyContext, ITraceContext
{
    protected AppDbContextBase(DbContextOptions options) : base(options)
    {
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public Stack<object> SavePoints { get; set; } = new();

    public IList<IDomainEvent> GetDomainEvents()
    {
        List<EntityEntry<EntityBase>> domainEntities = ChangeTracker
            .Entries<EntityBase>()
            .Where(x => x.Entity.DomainEvents is { Count: > 0 })
            .ToList();

        List<IDomainEvent> domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ForEach(entity => entity.Entity.DomainEvents.Clear());

        return domainEvents;
    }

    public virtual Task AutoModifyUpdateAt()
    {
        ChangeTracker.Entries<EntityBase>()
            .Where(x => x.State is EntityState.Modified)
            .ToList()
            .ForEach(item =>
            {
                item.Entity.UpdateDate = DateTime.UtcNow;
            });

        return Task.CompletedTask;
    }

    public virtual Task SetTraceRequest()
    {
        ChangeTracker.Entries<ITraceRequest>()
            .Where(x => x.State is EntityState.Modified or EntityState.Added)
            .ToList()
            .ForEach(item =>
            {
                item.Entity.TraceId = Activity.Current?.TraceId.ToString();
            });
        return Task.CompletedTask;
    }

    public virtual Task GenerateCode()
    {
        ChangeTracker.Entries<IContractProcess>()
            .Where(x => x.State is EntityState.Added)
            .ToList()
            .ForEach(item =>
            {
                item.Entity.GenerateCode();
            });
        return Task.CompletedTask;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        Task.WaitAll(AutoModifyUpdateAt(), SetTraceRequest());
        return base.SaveChangesAsync(cancellationToken);
    }
}

public interface ITraceContext
{
    public Task SetTraceRequest();
}

public interface IModifyContext
{
    public Task AutoModifyUpdateAt();
}
