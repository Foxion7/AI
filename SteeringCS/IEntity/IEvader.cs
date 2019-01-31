using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.entity;

namespace SteeringCS.IEntity
{
    public interface IEvader : IFleer
    {
        MovingEntity Pursuer { get; }
    }
}
