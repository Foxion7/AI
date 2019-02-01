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
    public class PursuitBehaviour : ISteeringBehaviour<IPursuer>
    {
        public IPursuer ME { get; set; }
        public PursuitBehaviour(IPursuer me)
        {
            ME = me;
        }


        public Vector2D Calculate()
        {
            if (ME.Evader == null)
                return new Vector2D();

            //if the evader is ahead and facing the agent then we can just seek
            //for the evader's current position.
            var evader = ME.Evader;
            var toEvader = evader.Pos - ME.Pos;
            double relativeHeading = ME.Heading.Dot(evader.Heading);
            if ((toEvader.Dot(ME.Heading) > 0) &&
                (relativeHeading < -0.95)) //acos(0.95)=18 degs
            {
                return Seek(evader.Pos, ME);
            }


            //the quotiënt between the distance and the maximum speeds will serve to the determine
            //how far into the future we look
            double lookAheadTime = toEvader.Length() /
                                   (ME.MaxSpeed + evader.Velocity.Length());

            return Seek(evader.Pos + evader.Velocity * lookAheadTime, ME);
        }

    }
}
