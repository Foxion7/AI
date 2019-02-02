using SteeringCS.entity;
using SteeringCS.Interfaces;

namespace SteeringCS.behaviour
{
    public interface IFollower : IGrouper<IMover>
    {
        MovingEntity Leader { get; }
        double SlowingRadius { get; }
        int    SeparationValue { get; }
        int    FollowValue { get; }
        int    AvoidValue { get; }
    }
}