using SteeringCS.entity;

namespace SteeringCS.Interfaces
{
    public interface IPursuer : IMover
    {
        MovingEntity Evader { get; }
    }
}