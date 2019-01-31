using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.behaviour
{
    class FleeBehaviour : SteeringBehaviour
    {
        public FleeBehaviour(MovingEntity me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            const double PanicDistanceSq = 100.0 * 100.0;

            if (TargetDistanceSq() > PanicDistanceSq)
            {
                // Return this to brake right outside of panicdistance.
                //ME.Velocity = ME.Velocity.Multiply(0);

                return new Vector2D(0, 0);
            }

            Vector2D DesiredVelocity = ME.Pos.Sub(ME.Target.Pos).Multiply(ME.MaxSpeed).Normalize();
            return (DesiredVelocity.Sub(ME.Velocity));
        }

        private double TargetDistanceSq()
        {
            var position = ME.Pos.Sub(ME.Target.Pos);
            return(Math.Pow(position.Length(), 2));
        }
    }
}
