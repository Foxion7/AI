using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.Interfaces;

namespace SteeringCS.entity
{
    public abstract class MovingEntity : BaseGameEntity, IMover
    {
        public Vector2D Velocity { get; set; }
        public Vector2D Heading { get; set; }
        public Vector2D Side { get; set; }
        public float Mass        { get; set; }
        public float MaxSpeed    { get; set; }
        public float MaxForce    { get; set; }
        public float MaxTurnRate { get; set; }
        public string name { get; set; }
        double MAX_SEE_AHEAD = 50;
        double MAX_AVOID_FORCE = 30;
        private Vector2D ahead;

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
            ahead = Pos + Velocity.Normalize() * MAX_SEE_AHEAD;

            foreach (Obstacle obstacle in MyWorld.obstacles)
            {
                if (lineIntersectsCircleAhead(obstacle))
                {
                    Vector2D avoidanceForce = ahead - obstacle.Pos;
                    avoidanceForce = avoidanceForce.Normalize() * MAX_AVOID_FORCE;

                    Velocity += avoidanceForce;
                }
            }

        }

        // Allows re-entry on other side of form if entity leaves.
        protected void WrapAround()
        {
            if (this.Pos.X > MyWorld.Width)
            {
                this.Pos = new Vector2D(1, Pos.Y);
            }
            else if (this.Pos.X < 0)
            {
                this.Pos = new Vector2D(MyWorld.Width - 1, Pos.Y);
            }

            if (this.Pos.Y > MyWorld.Height)
            {
                this.Pos = new Vector2D(Pos.X, 1);
            }
            else if (this.Pos.Y < 0)
            {
                this.Pos = new Vector2D(Pos.X, MyWorld.Height - 1);
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
