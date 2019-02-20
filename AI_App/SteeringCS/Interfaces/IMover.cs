namespace SteeringCS.Interfaces
{
    public interface IMover : IEntity
    {
        Vector2D Velocity { get; }
        Vector2D Heading  { get; }
        Vector2D Side     { get; }
        float Mass        { get; }
        float MaxSpeed    { get; }
        float MaxForce    { get; }
        float MaxTurnRate { get; }
        string DebugText { get; set; }
    }
}
