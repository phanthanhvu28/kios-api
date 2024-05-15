using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace VELA.WebCoreBase.Core.Persistence;

public interface IDbFacadeContext
{
    public DatabaseFacade Database { get; }
    public ChangeTracker ChangeTracker { get; }

    public Stack<object> SavePoints { get; set; }
}