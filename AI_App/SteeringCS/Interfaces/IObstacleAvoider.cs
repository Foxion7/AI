using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Interfaces
{
    interface IObstacleAvoider : IMover
    {
        double DetectionBoxLengthFactor { get; set; }
        List<IObstacle> Obstacles { get; }
    }
}
