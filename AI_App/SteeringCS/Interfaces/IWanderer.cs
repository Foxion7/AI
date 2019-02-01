namespace SteeringCS.Interfaces
{
    public interface IWanderer : IMover
    {
        double WanderJitter { get; }
        double WanderRadius { get; }
        double WanderDistance { get; }
    }
}