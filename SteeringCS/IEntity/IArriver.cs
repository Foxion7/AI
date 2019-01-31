namespace SteeringCS.behaviour
{
    public interface IArriver
    {
        BaseGameEntity Target      { get; }

        Deceleration Deceleration   { get; }
        
        //because Deceleration is enumerated as an int, this value is required
        //to provide fine tweaking of the deceleration.
        double DecelerationTweaker { get; }
    }
    public enum Deceleration { slow = 3, normal = 2, fast = 1 };
}