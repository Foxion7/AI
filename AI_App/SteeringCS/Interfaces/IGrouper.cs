using System.Collections.Generic;

namespace SteeringCS.Interfaces
{
    public interface IGrouper<T> : IMover where T: IEntity
    {
        IEnumerable<T> Neighbors { get; }
        double    NeighborsRange { get; }
    }
}
