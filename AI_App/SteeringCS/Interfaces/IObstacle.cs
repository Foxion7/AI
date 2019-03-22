namespace SteeringCS.Interfaces
{
    public interface IObstacle : IEntity
    {
        double Radius { get; set; }
        Vector2D Center { get; set; }
        bool CollidesWith(Vector2D point);
        bool CollidesWith(Vector2D start, Vector2D end);
    }
}
