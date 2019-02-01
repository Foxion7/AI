using SteeringCS.entity;

namespace SteeringCS.Interfaces
{
    public interface IEvader : IMover
    {
        MovingEntity Pursuer { get; }
        double PanicDistance { get; }
        //to avoid having to calculate the sqroot we use to panicDistance squared and the vector length squared
        double PanicDistanceSq();

    }
}
