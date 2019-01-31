namespace SteeringCS.behaviour
{
    public interface IWanderer
    {
        WanderCircle WanderCircle { get; set; }
    }

    public class WanderCircle
    {
        public double WanderJitter { get; }
        public double WanderRadius { get; }
        public double WanderDistance { get; }
        public Vector2D WanderTarget { get; set; }

        public WanderCircle(double wndJitter, double wndRadius, double wndDistance, Vector2D pos)
        {
            WanderJitter = wndJitter;
            WanderRadius = wndRadius;
            WanderDistance = wndDistance;
            WanderTarget = pos + new Vector2D(1 * wndRadius, 1 * wndRadius);

        }
    }
}