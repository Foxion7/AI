using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.behaviour
{
    class FleeBehaviour<TF> : SteeringBehaviour<TF> where TF: MovingEntity, IFleer
    {
        public FleeBehaviour(TF me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            if (ME.Target == null)
                return new Vector2D(0, 0);
            return Flee(ME.Target.Pos);
        }

        protected Vector2D Flee(Vector2D targetPos)
        {

            var distance = (ME.Pos - targetPos);
            if (ME.PanicDistanceSq() < distance.LengthSquared())
            {
                return new Vector2D(0, 0);
            };
            var desiredVelocity = distance.Normalize() * ME.MaxSpeed;
            var neededForce = desiredVelocity - ME.Velocity;
            return neededForce.Truncate(ME.MaxForce);
        }
    }
}
