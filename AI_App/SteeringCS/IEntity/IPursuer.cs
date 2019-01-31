using SteeringCS.entity;

namespace SteeringCS.behaviour
{
    public interface IPursuer : ISeeker 
    {
        MovingEntity Evader { get; }
    }
}