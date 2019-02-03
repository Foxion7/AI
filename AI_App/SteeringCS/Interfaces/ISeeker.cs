namespace SteeringCS.Interfaces
{
    public interface ISeeker : IMover
    {
        BaseGameEntity Target { get; }
    }
}