﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using static SteeringCS.behaviour.StaticBehaviours;

namespace SteeringCS.behaviour
{
    class PursuitAndArriveBehaviour<TA> : SteeringBehaviour<TA> where TA : MovingEntity, IPursuer, IArriver
    {
        public PursuitAndArriveBehaviour(TA me) : base(me)
        {
        }
        public override Vector2D Calculate()
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
                return Arrive(ME.Target.Pos, ME, ME.SlowingRadius);
            }

            //the quotiënt between the distance and the maximum speeds will serve to the determine
            //how far into the future we look
            double lookAheadTime = toEvader.Length() /
                                   (ME.MaxSpeed + evader.Velocity.Length());

            return Arrive(evader.Pos + evader.Velocity * lookAheadTime, ME, ME.SlowingRadius);
        }
    }
}
