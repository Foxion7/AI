using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Interfaces
{
    interface IObstacleAvoider : IMover
    {
        List<IObstacle> Obstacles { get; }
    }
}
