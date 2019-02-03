namespace SteeringCS.Interfaces
{
    public interface IArriver : IMover
    {
        BaseGameEntity Target { get; }
        double SlowingRadius  { get; }
    }
}