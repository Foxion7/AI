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
            Velocity = new Vector2D();
            Heading = Velocity.Normalize();
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
                    Console.WriteLine("Collision detected!");
                    Console.WriteLine(name + " collides with " + obstacle.name);
                }
            }
        }

        private bool lineIntersectsCircleAhead(Obstacle obstacle)
        {
            // Optionally add ahead2 check.
            return DistanceBetweenPositions(obstacle.Pos, ahead) <= obstacle.Radius;
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
