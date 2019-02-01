using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.IEntity
{
    public interface IGrouper<T> where T: BaseGameEntity
    {
        IEnumerable<T> Neighbors { get; }
        double    NeighborsRange { get; }
    }
}
