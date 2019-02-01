using System;

namespace SteeringCS.behaviour
{
    public interface IArriver
    {
        BaseGameEntity Target { get; }
        double SlowingRadius  { get; }
    }
}