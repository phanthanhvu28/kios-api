using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VELA.WebCoreBase.Core.Entities;

public abstract class EntityBase : IEntityRoot, ITraceRequest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; init; }

    [Column(TypeName = "varchar(50)")]
    public string? CreateBy { get; set; }
    public DateTime CreateDate { get; init; } = DateTime.UtcNow;

    [Column(TypeName = "varchar(50)")]
    public string? UpdateBy { get; set; }
    public DateTime? UpdateDate { get; set; } = DateTime.UtcNow;

    public bool IsPublish { get; set; } = true;
    public bool IsDelete { get; set; } = false;


    [NotMapped]
    public List<IDomainEvent> DomainEvents { get; } = new();

    [Column(TypeName = "varchar(100)")]
    public string? TraceId { get; set; }

    public void AddDomainEvent(params IDomainEvent[] events)
    {
        DomainEvents.AddRange(events);
    }

    public void RemoveDomainEvent(IDomainEvent eventItem)
    {
        DomainEvents.Remove(eventItem);
    }
}

public interface IEntityRoot
{
}

/// <summary>
///     TraceId for trace request
/// </summary>
public interface ITraceRequest
{
    [MaxLength(55)]
    public string? TraceId { get; set; }
}