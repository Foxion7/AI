using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;

namespace SteeringCS.entity
{
    public abstract class MovingEntity : BaseGameEntity
    {
        public Vector2D Velocity { get; set; }
        public Vector2D Heading { get; set; }
        public Vector2D Side { get; set; }
        public float Mass        { get; set; }
        public float MaxSpeed    { get; set; }
        public float MaxForce    { get; set; }
        public float MaxTurnRate { get; set; }
        public string name { get; set; }
        // Maybe turn this private
        public Vector2D ahead { get; set; }

        protected MovingEntity(string name, Vector2D pos, World w) : base(pos, w)
        {
            Pos = pos;
            Velocity = new Vector2D();
            Heading = new Vector2D(1,1);
            Side = Heading.Perp();
            this.name = name;
        }

        public void DetectCollision()
        {
            ahead = Pos + Velocity.Normalize();

            foreach (Obstacle obstacle in MyWorld.obstacles)
            {
                if (lineIntersectsCircleAhead(obstacle))
                {
                    Vector2D centerOfObstacle = new Vector2D(obstacle.Pos.X + obstacle.Radius, obstacle.Pos.Y + obstacle.Radius);
                    double distanceX = (DistanceBetweenPositions(centerOfObstacle, Pos) - obstacle.Radius);
                    if(distanceX < 0)
                    {
                        distanceX *= -1;
                    }
                    Console.WriteLine("distance: " + distanceX);

                    Vector2D avoidanceForce = ahead - obstacle.Pos;
                    avoidanceForce = avoidanceForce.Normalize() * 50;
                    Console.WriteLine("avoidanceForce: " + avoidanceForce);

                    Velocity += avoidanceForce;
                }
            }

        }

        private bool lineIntersectsCircleAhead(Obstacle obstacle)
        {
            Vector2D centerOfObstacle = new Vector2D(obstacle.Pos.X + obstacle.Radius, obstacle.Pos.Y + obstacle.Radius);

            return DistanceBetweenPositions(centerOfObstacle, Pos) <= obstacle.Radius + Velocity.Length();
        }

        private double DistanceBetweenPositions(Vector2D pointA, Vector2D pointB)
        {
            return Math.Sqrt((pointA.X - pointB.X) * (pointA.X - pointB.X) + (pointA.Y - pointB.Y) * (pointA.Y - pointB.Y));
        }

        public override string ToString()
        {
            return $"{Velocity}";
        }
    }
}
