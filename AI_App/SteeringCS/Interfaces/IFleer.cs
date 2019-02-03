namespace SteeringCS.Interfaces
{
    public interface IFleer : IMover
    {
        BaseGameEntity Target { get; }

        double PanicDistance { get; }

        //to avoid having to calculate the sqroot we use to panicDistance squared and the vector length squared
        double PanicDistanceSq();
    }
}