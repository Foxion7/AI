using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.behaviour
{
    public class PursuitBehaviour<TP> : SeekBehaviour<TP> where TP : MovingEntity, IPursuer
    {
        public PursuitBehaviour(TP me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            //if the evader is ahead and facing the agent then we can just seek
            //for the evader's current position.
            var evader = ME.Evader;
            var toEvader = evader.Pos - ME.Pos;
            double relativeHeading = ME.Heading.Dot(evader.Heading);
            if ((toEvader.Dot(ME.Heading) > 0) &&
                (relativeHeading < -0.95)) //acos(0.95)=18 degs
            {
                return Seek(evader.Pos);
            }

            //Not considered ahead so we predict where the evader will be.
            //the look-ahead time is proportional to the distance between the evader
            //and the pursuer; and is inversely proportional to the sum of the
            //agents' velocities
            double lookAheadTime = toEvader.Length() /
                                   (ME.MaxSpeed + evader.Velocity.Length());
            //now seek to the predicted future position of the evader
            return Seek(evader.Pos + evader.Velocity * lookAheadTime);
        }

    }
}
