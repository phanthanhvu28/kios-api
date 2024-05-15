using Mediator;
using System.Diagnostics;

namespace VELA.WebCoreBase.Core.Entities;

public abstract record EventBase : IDomainEvent
{

    //public EventBase()
    //{
    //    EventType = GetType().Name;
    //}

    public string EventType => GetType().FullName;
    public string CorrelationId { get; init; } = Activity.Current?.Id!;
    //public DateTime CreatedAt { get; } = DateTime.UtcNow;
    public IDictionary<string, object> MetaData { get; private set; } = new Dictionary<string, object>();

    public IDomainEvent Flatten()
    {
        //// TODO: have not use yet
        //MetaData = GetType()
        //    .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
        //    .ToDictionary(e => e.Name, e => e.GetValue(this))!;
        //return this.Adapt<EventBase>();
        return this;
    }

    protected void TryAddMetaData(string key, object value)
    {
        MetaData.TryAdd(key, value);
    }
}

public interface IDomainEvent : INotification
{
    //public string EventType { get; }
    //public string CorrelationId { get; init; }
    //DateTime CreatedAt { get; }
    IDictionary<string, object> MetaData { get; }

    public IDomainEvent Flatten();
}

public interface IDomainEventContext
{
    IList<IDomainEvent> GetDomainEvents();
}