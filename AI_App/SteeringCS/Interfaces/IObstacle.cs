namespace SteeringCS.Interfaces
{
    public interface IObstacle : IEntity
    {
        double Radius { get; set; }
        Vector2D Center { get; set; }
    }
}
