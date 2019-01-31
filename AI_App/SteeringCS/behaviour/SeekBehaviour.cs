using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    public class SeekBehaviour<TS> : SteeringBehaviour<TS> where TS: MovingEntity, ISeeker
    {
        public SeekBehaviour(TS me) : base(me)
        {

        }

        public override Vector2D Calculate()
        {
            ME.DetectCollision();
            if (ME.Target != null)
                return Seek(ME.Target.Pos);
            return new Vector2D(0, 0);
        }

        protected Vector2D Seek(Vector2D targetPos)
        {
            var desiredVelocity = (targetPos - ME.Pos).Normalize() * ME.MaxSpeed;
            var neededForce = desiredVelocity - ME.Velocity;
            return neededForce.Truncate(ME.MaxForce);
        }
    }
}
