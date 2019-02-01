using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.entity;

namespace SteeringCS.IEntity
{
    public interface IEvader
    {
        MovingEntity Pursuer { get; }
        double PanicDistance { get; }
        //to avoid having to calculate the sqroot we use to panicDistance squared and the vector length squared
        double PanicDistanceSq();

    }
}
