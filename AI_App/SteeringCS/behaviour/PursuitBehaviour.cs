using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class PursuitBehaviour : SteeringBehaviour
    {
        MovingEntity evader;

        public PursuitBehaviour(MovingEntity me) : base(me){}

        public override Vector2D Calculate()
        {
            var target = ME.Target as MovingEntity;

            Vector2D ToEvader = target.Pos.Sub(ME.Pos);

            double RelativeHeading = ME.Heading.DotProduct(target.Heading);

            if ((ToEvader.DotProduct(ME.Heading) > 0) && (RelativeHeading < -0.95))
            {
                target.SB = new SeekBehaviour(ME);
                return target.Pos;
            }
            // Target velocity should be target speed
            double LookAheadTime = ToEvader.Length() / (ME.MaxSpeed + target.Velocity.Length());

            target.SB = new SeekBehaviour(ME);

            return target.pos.Add(target.Velocity.Multiply(LookAheadTime));
            //return null;
        }
    }
}
