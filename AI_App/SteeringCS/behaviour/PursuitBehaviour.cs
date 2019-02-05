using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.Interfaces;
using static SteeringCS.behaviour.StaticBehaviours;

namespace SteeringCS.behaviour
{
    public class Pursuit : ISteeringBehaviour
    {
        private IMover _me;
        public  IMover Evader { get; set; }
        public double SlowingRadius { get; set; }
        
        public Pursuit(IMover me, IMover evader)
        {
            _me = me;
            Evader = evader;
        }

        public Vector2D Calculate()
        {
            if (Evader == null)
                return new Vector2D();

            //if the evader is ahead and facing the agent then we can just seek
            //for the evader's current position.
            var toEvader = Evader.Pos - _me.Pos;
            double relativeHeading = _me.Heading.Dot(Evader.Heading);
            if ((toEvader.Dot(_me.Heading) > 0) &&
                (relativeHeading < -0.95)) //acos(0.95)=18 degs
            {
                return Arrive(Evader.Pos, _me, SlowingRadius);
            }
            
            //the quotiënt between the distance and the maximum speeds will serve to the determine
            //how far into the future we look
            double lookAheadTime = toEvader.Length() /
                                   (_me.MaxSpeed + Evader.Velocity.Length());

            return Arrive(Evader.Pos + Evader.Velocity * lookAheadTime, _me, SlowingRadius);
        }

    }
}
