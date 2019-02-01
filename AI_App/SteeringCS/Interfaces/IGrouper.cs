using System.Collections.Generic;

namespace SteeringCS.Interfaces
{
    public interface IGrouper<T> : IMover where T: IEntity
    {
        /// <summary>
        /// Neighbors should return all the entities the IGrouper considers his Neighbors.
        /// So no More checking should be required by the actual SteeringBehaviour
        /// </summary>
        IEnumerable<T> Neighbors { get; }
        
        /// <summary>
        /// The range for which the IGrouper checks for Neighbors.
        /// Some algorithms might require this, but don't actually use it to recheck if the
        /// entities returned by Neighbors are in fact neighbors.
        /// </summary>
        double    NeighborsRange { get; }
    }
}
