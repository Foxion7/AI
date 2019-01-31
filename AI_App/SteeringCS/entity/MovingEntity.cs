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

        protected MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Velocity = new Vector2D();
            Heading = Velocity.Normalize();
            Side = Heading.Perp();
        }

        
        public override string ToString()
        {
            return $"{Velocity}";
        }
    }
}
