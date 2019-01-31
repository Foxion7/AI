namespace SteeringCS.behaviour
{
    public interface IWanderer
    {
        double WanderJitter { get; }
        double WanderRadius { get; }
        double WanderDistance { get; }
    }
}